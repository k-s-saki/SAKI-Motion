using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Drawing;
using System.Linq;
//using System.Text;
using System.Windows.Forms;

using Rhino;
using Rhino.DocObjects;
using Rhino.Commands;
using Rhino.Input;
using Rhino.Input.Custom;
using Rhino.Geometry;

using static SakiMotion.MachineCodeConverter;

namespace SakiMotion
{
    class SKOutputToolPath
    {
        RhinoDoc Doc;
        bool IsNeedDebugInfo = false;

        ToolData _Tool = null;
        PathData _Path = null;
        PostData _Post = null;

        public SKOutputToolPath(RhinoDoc aDoc,bool aNeedDebugInfo)
        {
            Doc = aDoc;
            IsNeedDebugInfo = aNeedDebugInfo;
        }

        public string PointToString(Point3d pt)
        {
            return string.Format("({0:N3},{1:N3},{2:N3}", pt.X, pt.Y, pt.Z);
        }

        //RML1Modal仕様
        //private SKRML1Modal GCM = new SKRML1Modal();

        //GCode仕様
        private MachineCodeConverter CodeConverter;

        // >> Main加工移動コード出力 TOOL_PATH_OBJ 図形
        private void WriteToolPathCodeMain(System.IO.StreamWriter sw, RhinoObject aMainToolPath)
        {
            //R方向のツールパスを取得
            string r_paths = aMainToolPath.Attributes.GetUserString("R_ORDER");
            string[] r_guid_list = r_paths.Split(',');
            foreach (var r_id in r_guid_list)
            {
                var rObj = Doc.Objects.Find(new Guid(r_id));
                if (rObj != null)
                {
                    //Z方向のツールパスを取得
                    string z_paths = rObj.Attributes.GetUserString("Z_ORDER");
                    string[] z_guid_list = z_paths.Split(',');
                    foreach (var z_id in z_guid_list)
                    {
                        RhinoObject zObj = Doc.Objects.Find(new Guid(z_id));
                        if (zObj != null)
                        {
                            WriteToolPathCode(sw, zObj);
                        }
                    }
                }
            }
            //Mainの図形の処理
            //WriteToolPathCode(sw, aMainToolPath);
        }

        private void WriteToolPathCode(System.IO.StreamWriter sw, RhinoObject aToolPath)
        {
            if (_Path == null)
            {
                SKUtil.DebugPrint("Error _Path==null ( WriteToolPathCode )");
                return;
            }

            //（前後のツールパスのループ） [ORDER]
            string curves_str = aToolPath.Attributes.GetUserString("CURVE_ORDER");
            // カンマ区切りを分割
            string[] curve_guid_list = curves_str.Split(',');
            //foreach (var id in curve_guid_list)
            for (int j=0;j<curve_guid_list.Length;j++)
            {
                string id = curve_guid_list[j];
                RhinoObject obj = Doc.Objects.Find(new Guid(id));
                var Crv = obj.Geometry as Rhino.Geometry.Curve;
                var ptStart = Crv.PointAtStart;
                var ptEnd = Crv.PointAtEnd;
                var TPInfoStr = obj.Attributes.GetUserString("TPInfo");
                string[] TPInfo = TPInfoStr.Split(',');
                double Feed = 0;
                double.TryParse(TPInfo[1], out Feed);

                OutputInfo(sw, string.Format("Info Curve Degree={0}, IsPolyline={1}, IsArc={2}, IsLinear={3} ", Crv.Degree, Crv.IsPolyline(), Crv.IsArc(), Crv.IsLinear()));
                /*
                plane = rs.CurvePlane(idCrv, segment)
                print("平面=" + SKUtils.PointToString(plane))
                */

                //Start点移動 が必要なら出力する(G00で良い??)
                OutputInfo(sw, "PtStart");
                if (j == 0)
                {
                    CodeConverter.OutputCodeLinearIfNeeded(sw, CodeType.G00, ptStart, _Path.RapidFeed);
                }
                else
                {
                    CodeConverter.OutputCodeLinearIfNeeded(sw, CodeType.G01, ptStart, Feed);
                }

                if (Crv.IsPolyline())
                {
                    OutputInfo(sw, "Type = Polyline");
                    Polyline pl;
                    if (Crv.TryGetPolyline(out pl))
                    {
                        for (int i = 0; i < pl.Count; i++)
                        {
                            var pt = pl[i];
                            CodeConverter.OutputCodeLinearIfNeeded(sw, CodeType.G01, pt, Feed);
                        }
                    }
                    else
                    {
                        sw.WriteLine("エラー IsPolyline");
                    }
                }
                else if (Crv.IsArc())
                {
                    sw.WriteLine("Debug: Type = Arc");
                    if (CodeConverter.IsSupportArc())
                    {
                        Arc arc;
                        if (Crv.TryGetArc(out arc))
                        {
                            var radius = arc.Radius;
                            SKUtil.DebugPrint(string.Format("円弧：半径 {0}", radius));
                            var ptCenter = arc.Center;
                            SKUtil.DebugPrint(string.Format("円弧：中心 {0}", PointToString(ptCenter)));
                            var angle = arc.AngleDegrees;
                            SKUtil.DebugPrint(string.Format("円弧：角度 {0}", angle));

                            var v1 = ptStart - ptCenter;
                            var v2 = ptEnd - ptCenter;
                            var CenterOffset = ptCenter - ptStart;

                            if (v2.Z > 0)
                            {
                                SKUtil.DebugPrint("左周り/G03");
                                // GLine = "G03";
                                CodeConverter.OutputCodeArc(sw, CodeType.G03, ptEnd, CenterOffset, Feed);
                            }
                            else if (v2.Z < 0)
                            {
                                SKUtil.DebugPrint("右周り/G02");
                                // GLine = "G02";
                                CodeConverter.OutputCodeArc(sw, CodeType.G02, ptEnd, CenterOffset, Feed);
                            }
                            else
                            {
                                SKUtil.DebugPrint("周回エラー");
                                // GLine = "ERROR in G02/03";
                            }
                        }
                        else
                        {
                            // error
                            sw.WriteLine("エラー TryGetArc");
                            // GLine = "ERROR in Arc";
                        }
                    }
                    else
                    {
                        sw.WriteLine("エラー：この出力器はARCをサポートしていません。出力器を変更するか、単純な直線に分解する必要があります。");
                    }
                }
                else if (Crv.IsLinear())
                {
                    OutputInfo(sw, "Type = Line");
                    //必要なら出力する
                    CodeConverter.OutputCodeLinearIfNeeded(sw, CodeType.G01, ptEnd, Feed);
                }
                else
                {
                    SKUtil.DebugPrint("エラー convertなどを使って単純な円弧＋直線に分解する必要があります。");
                    OutputInfo(sw, "ERROR CURVE TYPE ");
                }

            }


        }

        private void OutputInfo(System.IO.StreamWriter sw, string s)
        {
            if (IsNeedDebugInfo)
            {
                sw.WriteLine("INFO:" + s);
            }
        }

        public bool WriteMachineCode(System.IO.StreamWriter sw)
        {

            /*　ツールパス出力のアルゴリズム

            ・機械平面座標(ABS_PLANE_OBJ)を取得
            ・>> 初期化コード出力 POST.InitCode
            ・（ツールパスのループ） ABS_PLANE_OBJ.[MAIN_ORDER] 
                ・ツールパスを取得   TOOL_PATH_OBJ  <==  ABS_PLANE.[MAIN_ORDER]
                ・>> ツール交換＆起動コード出力  Tool <== TOOL_PATH_OBJ.[TOOL] , Tool.ChangeCode, Tool.StartCode
                ・座標平面を取得 WORK_PLANE_OBJ <= TOOL_PATH_OBJ.[WORK_PLANE]
                ・>> 座標平面コード出力 WORK_PLANE_OBJ.[WORK_SYS] , Post.WorkplaneStartCode
                ・>> 加工移動コード出力 TOOL_PATH_OBJ 図形
                ・>> ツール停止コード出力 Tool.StopCode
                ・>> 座標平面退避コード出力 Post.WorkplaneEndCode 
            ・>> 終了コードを出力 Post.EndCode
            */
            // 機械平面座標(ABS_PLANE_OBJ)を取得
            RhinoObject ABSPlane = SKUtil.GetObjectByName(Doc, "PROFILE_CUT_ABS_PLANE");
            if (ABSPlane == null)
            {
                SKUtil.DebugPrint("ツールパス用のABS_PLANEがありません。");
                return false;
            }
            var post_name = ABSPlane.Attributes.GetUserString("POST");
            _Post = PostData.LoadByName(post_name);
            if (_Post == null)
            {
                SKUtil.DebugPrint("ABS_PLANEにポストが設定されていません。");
                return false;
            }

            // CodeProの生成
            if (_Post.CodeType == "GCODE")
            {
                CodeConverter = new SKGCodeModal();
            }
            else
            {
                CodeConverter = new SKRML1Modal();
            }

            // 初期化コード出力 POST.StartCode
            sw.WriteLine(_Post.StartCode);
            //（Mainツールパスのループ） ABS_PLANE_OBJ.[MAIN_ORDER]
            string main_tool_paths_str = ABSPlane.Attributes.GetUserString("MAIN_ORDER");
            // カンマ区切りを分割
            string[] main_tool_path_guid_list = main_tool_paths_str.Split(',');
            if (main_tool_path_guid_list.Count() == 0)
            {
                SKUtil.DebugPrint("ツールパスがありません。");
                return false;
            }

            foreach (var id in main_tool_path_guid_list)
            {
                RhinoObject ToolPath = Doc.Objects.Find(new Guid(id));
                if (ToolPath == null)
                {
                    SKUtil.DebugPrint("MAIN_ORDERで指定されたツールパス曲線がありません。");
                    return false;
                }
                // >> ツール交換＆起動コード出力 Tool <== TOOL_PATH_OBJ.[TOOL] , Tool.ChangeCode, Tool.StartCode
                string tool_name = ToolPath.Attributes.GetUserString("TOOL");
                _Tool = ToolData.LoadByName(tool_name);
                if (_Tool == null)
                {
                    SKUtil.DebugPrint("MainにTOOLで指定されたオブジェクトがありません。");
                    return false;
                }

                string path_name = ToolPath.Attributes.GetUserString("PATH");
                _Path = PathData.LoadByName(path_name);
                if (_Path == null)
                {
                    SKUtil.DebugPrint("MainにPATHで指定されたオブジェクトがありません。");
                    return false;
                }

                OutputInfo(sw, "ツール開始コード");
                sw.WriteLine(string.Format(_Tool.StartCode, _Tool.Spin));
                //sw.WriteLine( string.Format("{0}{1:F0};",tool.StartCode,tool.Spin));

                // 座標平面を取得 WORK_PLANE_OBJ <= TOOL_PATH_OBJ.[WORK_PLANE]
                string wp_id = ToolPath.Attributes.GetUserString("WORK_PLANE");
                RhinoObject WorkPlane = Doc.Objects.Find(new Guid(wp_id));
                if (WorkPlane == null)
                {
                    SKUtil.DebugPrint("WORK_PLANEで指定されたオブジェクトがありません。");
                    return false;
                }

                // >> 座標平面コード出力 WORK_PLANE_OBJ.[WORK_SYS] , Post.WorkplaneStartCode
                OutputInfo(sw, "平面コード出力 (部分未実装） WORKSYS= " + WorkPlane.Attributes.GetUserString("WORK_SYS"));
                CodeConverter.ClearLast();
                // >> 加工移動コード出力 TOOL_PATH_OBJ 図形
                OutputInfo(sw, "加工移動コード");
                WriteToolPathCodeMain(sw, ToolPath);
                // >> ツール停止コード出力 Tool.StopCode
                OutputInfo(sw, "ツール停止コード");
                sw.WriteLine(_Tool.StopCode);

                // >> 座標平面退避コード出力 Post.WorkplaneEndCode
                OutputInfo(sw, "平面退避コード出力 (未出力)");
                //sw.WriteLine(_Path.ZRapid); TODO
            }
            // >> 終了コードを出力 Post.EndCode
            sw.WriteLine(_Post.EndCode);
            OutputInfo(sw, "PostProcess 終了");
            return true;
        }


    }
}
