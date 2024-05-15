using System;
using System.Diagnostics;
using System.Collections.Generic; //ジェネリック型
using System.Collections.ObjectModel; //ReadOnlyCollection
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


using Rhino;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Input.Custom;
using Rhino.Display;
using Rhino.Input;



namespace ProfileCut7
{
    public class CurveProcess
    {
        //工具補正
        public static string G40 = "G40 ( 曲線上 )";
        public static string G41 = "G41 ( 左 / Downcut )";
        public static string G42 = "G42 ( 右 / Upcut )";

        //加工メソッド
        public enum ProfileMethod { DirectCurve, OnCurve, OffsetDir, OffsetDirWithApproach };

    }

    public class ProfilingArcLines
    {

        public ProfileCut7PlugIn PlugIn
        {
            get
            {
                return ProfileCut7PlugIn.Instance;
            }
        }

        public class GetProfilingDirection : GetPoint
        {
            private Curve m_curve;
            private Circle m_circle;
            private Point3d m_start_point, m_closest_point;
            private Point3d m_center_point;
            private Vector3d m_start_tangnt, m_tangent, m_normal, m_offset;
            private Plane m_plane;
            private double m_radius;
            private bool m_reverse_tangent;
            private bool m_oncurve = false;
            private string m_message = CurveProcess.G41;

            public GetProfilingDirection()
            {
                m_circle = new Circle();
                m_closest_point = new Point3d();
                m_start_point = new Point3d();
                m_center_point = new Point3d();
                m_start_tangnt = new Vector3d();
                m_tangent = new Vector3d();
                m_normal = new Vector3d(0, 0, 1);
                m_offset = new Vector3d(0, 0, 0);
                m_plane = new Plane();
            }

            public void SetParameters(Curve crv, bool OnCurve, double radius)
            {
                m_curve = crv;
                m_radius = radius;
                m_oncurve = OnCurve;
            }

            public void SetOnCurveToggle(bool OnCurve)
            {
                m_oncurve = OnCurve;
            }

            public bool IsCurveReversed()
            {
                return m_reverse_tangent;
            }

            public bool IsClimb()
            {
                if (m_message == CurveProcess.G41)
                    return true;
                return false;
            }

            public Point3d OffsetPoint()
            {
                return m_center_point;
            }

            public bool CalculateClosestPoint(RhinoViewport vp, Point3d pt)
            {
                m_reverse_tangent = false;
                double t = m_curve.Domain.Min;
                double tmid = m_curve.Domain.Mid;
                m_curve.ClosestPoint(pt, out t);
                m_closest_point = m_curve.PointAt(t);
                m_start_point = m_curve.PointAtStart;
                m_start_tangnt = m_curve.TangentAt(m_curve.Domain.Min);
                m_tangent = m_curve.TangentAt(t);
                if (t > tmid)
                {
                    m_tangent.Reverse();
                    m_reverse_tangent = true;
                    m_start_point = m_curve.PointAtEnd;
                    m_start_tangnt = m_curve.TangentAt(m_curve.Domain.Max);
                    m_start_tangnt.Reverse();
                }

                Vector3d vtest = new Vector3d(pt.X - m_closest_point.X, pt.Y - m_closest_point.Y, pt.Z - m_closest_point.Z);
                float[] pr = { (float)m_closest_point.X, (float)m_closest_point.Y, (float)m_closest_point.Z };

                m_normal = Rhino.RhinoDoc.ActiveDoc.Views.ActiveView.ActiveViewport.ConstructionPlane().ZAxis;

                m_offset = Vector3d.CrossProduct(m_tangent, m_normal);
                if (Vector3d.Multiply(m_offset, vtest) < 0)
                {
                    m_offset.Reverse();
                }
                m_center_point.X = m_closest_point.X;
                m_center_point.Y = m_closest_point.Y;
                m_center_point.Z = m_closest_point.Z;

                if (!m_oncurve)
                {
                    m_center_point.X = m_center_point.X + m_radius * m_offset.X;
                    m_center_point.Y = m_center_point.Y + m_radius * m_offset.Y;
                    m_center_point.Z = m_center_point.Z + m_radius * m_offset.Z;
                }

                m_plane = new Plane(m_center_point, m_normal);
                m_circle = new Circle(m_plane, m_radius);

                if (m_oncurve) m_message = CurveProcess.G40;
                else
                {
                    m_message = CurveProcess.G41;
                    vtest = Vector3d.CrossProduct(m_tangent, m_offset);
                    if (Vector3d.Multiply(m_normal, vtest) < 0)
                        m_message = CurveProcess.G42;
                }

                return m_circle.IsValid;
            }

            /*
            protected override void OnPostDrawObjects(DrawEventArgs e)
            {
                base.OnPostDrawObjects(e);
                Mc.Cutter.CommonMesh.Draw(e, m_plane);
            }
             */

            protected override void OnDynamicDraw(GetPointDrawEventArgs e)
            {
                base.OnDynamicDraw(e);
                if (CalculateClosestPoint(e.Viewport, e.CurrentPoint))
                {
                    System.Drawing.Color clr = Rhino.ApplicationSettings.AppearanceSettings.CrosshairColor;
                    e.Display.DrawCircle(m_circle, clr);
                    e.Display.DrawDirectionArrow(m_center_point, m_tangent, clr);
                    e.Display.DrawDirectionArrow(m_center_point, m_normal, clr);
                    e.Display.DrawPoint(m_start_point, clr);
                    e.Display.DrawDirectionArrow(m_start_point, m_start_tangnt, clr);
                    e.Display.DrawLine(new Line(m_closest_point, e.CurrentPoint), clr);
                    e.Display.Draw2dText(m_message, clr, e.CurrentPoint, false,20, "ＭＳ ゴシック");

                    //Mc.Cutter.CommonMesh.Draw(e, m_plane);
                }
            }
        }

        public class GetProfilingLeadInOutMulti: GetPoint
        {
            public List<GetProfilingLeadInOut> MultiList;
            public List<PolyCurve> PolyCurves;
            public bool LinearToggle;

            public GetProfilingLeadInOutMulti(List<PolyCurve> polyCurves)
            {
                PolyCurves = polyCurves;
                MultiList = new List<GetProfilingLeadInOut>();
                foreach(var pc in polyCurves)
                {
                    var lio = new GetProfilingLeadInOut();
                    MultiList.Add(lio);
                    lio.ConstrainToTargetPlane();
                    pc.Reverse();
                    lio.SetParameters(pc);
                    pc.Reverse();
                }
            }

            public void SetLinearToggle(bool Linear)
            {
                LinearToggle = Linear;
                foreach(var lio in MultiList)
                {
                    lio.SetLinearToggle(Linear);
                }
            }

            public void SetParameters()
            {
                for(var i =0; i< MultiList.Count; i++)
                {
                    var lio = MultiList[i];
                    var pc = PolyCurves[i];
                    lio.SetParameters(pc);
                }
            }

            public List<Guid> AddCurveIDs(RhinoDoc doc, string LayerName)
            {
                var ret = new List<Guid>();
                foreach(var lio in MultiList)
                {
                    Guid guid;
                    if (LinearToggle)
                    {
                        guid = doc.Objects.AddLine( lio.GetLine() );
                    }
                    else
                    {
                        guid = doc.Objects.AddArc(lio.GetArc());
                    }
                    if (LayerName != "")
                    {
                        var obj = doc.Objects.Find(guid);
                        ProfileCut7PlugIn.Instance.SetObjectLayer(obj, LayerName);
                    }
                    ret.Add(guid);
                }
                return ret;
            }

            protected override void OnDynamicDraw(GetPointDrawEventArgs e)
            {
                foreach (var lio in MultiList)
                {
                    lio.Draw(e);
                }
            }
        }

        public class GetProfilingLeadInOut : GetPoint
        {
            private Point3d m_start_point;
            private Point3d m_end_point;
            private Vector3d m_start_tangnt;
            private Arc m_arc;
            private Plane m_plane;
            private Vector3d m_normal;
            private PolyCurve m_polycurve;
            //private double m_radius;
            private bool m_linear;

            public GetProfilingLeadInOut()
            {
                m_start_point = new Point3d();
                m_end_point = new Point3d();
                m_start_tangnt = new Vector3d();
                m_arc = new Arc();
                m_normal = new Vector3d(0, 0, 1);
                m_plane = new Plane();
                m_polycurve = new PolyCurve();
            }

            public void SetParameters(PolyCurve pcrv)
            {
                m_polycurve = pcrv;
                m_start_point = pcrv.PointAtEnd;
                m_start_tangnt = pcrv.TangentAtEnd;
            }

            public void SetLinearToggle(bool Linear)
            {
                m_linear = Linear;
            }

            public Arc GetArc()
            {
                return m_arc;
            }

            public Line GetLine()
            {
                return new Line(m_start_point, m_end_point);
            }

            public bool CalculateLead(RhinoViewport vp, Point3d pt)
            {

                m_normal = Rhino.RhinoDoc.ActiveDoc.Views.ActiveView.ActiveViewport.ConstructionPlane().ZAxis;

                double t = Vector3d.Multiply(new Vector3d(m_start_point - pt), m_normal);
                pt = pt + Vector3d.Multiply(t, m_normal);

                m_end_point = pt;
                
                m_arc = new Arc(m_start_point, m_start_tangnt, pt);
                if (m_arc.IsValid)
                {
                    m_plane = new Plane(m_end_point, m_normal);
                    return true;
                }

                return false;
            }

            public void Draw(GetPointDrawEventArgs e)
            {
                OnDynamicDraw(e);
            }

            protected override void OnDynamicDraw(GetPointDrawEventArgs e)
            {
                base.OnDynamicDraw(e);
                if (CalculateLead(e.Viewport, e.CurrentPoint))
                {
                    System.Drawing.Color clr = Rhino.ApplicationSettings.AppearanceSettings.CrosshairColor;

                    var nv = m_polycurve.ToNurbsCurve();
                    e.Display.DrawCurve(nv, clr);
                    var tan = nv.TangentAt(nv.Domain.Max);
                    var pos = nv.PointAt(nv.Domain.Max);
                    e.Display.DrawDirectionArrow(pos, tan, clr);

                    tan = nv.TangentAt(nv.Domain.Min);
                    pos = nv.PointAt(nv.Domain.Min);
                    e.Display.DrawDirectionArrow(pos, tan, clr);

                    if (m_linear)
                        e.Display.DrawLine(m_start_point, m_end_point, clr);
                    else
                    {
                        e.Display.DrawArc(m_arc, clr);
                        //nv = m_arc.ToNurbsCurve();
                        //tan = nv.TangentAt(nv.Domain.Max);
                        //pos = nv.PointAt(nv.Domain.Max);
                        //e.Display.DrawDirectionArrow(pos, tan, clr);
                    }

                    //Mc.Cutter.CommonMesh.Draw(e, m_plane);
                }
            }


        }

        public static bool CurveSegments(Rhino.Collections.CurveList L, Rhino.Geometry.Curve crv)
        {
            if (crv == null) { return false; }

            Rhino.Geometry.PolyCurve polycurve = crv as Rhino.Geometry.PolyCurve;
            if (polycurve != null)
            {
                polycurve.RemoveNesting();

                Rhino.Geometry.Curve[] segments = polycurve.Explode();

                if (segments == null) { return false; }
                if (segments.Length == 0) { return false; }

                foreach (Rhino.Geometry.Curve S in segments)
                {
                    CurveSegments(L, S);
                }

                return true;
            }

            Rhino.Geometry.PolylineCurve polyline = crv as Rhino.Geometry.PolylineCurve;
            if (polyline != null)
            {
                for (int i = 0; i < (polyline.PointCount - 1); i++)
                {
                    L.Add(new Rhino.Geometry.LineCurve(polyline.Point(i), polyline.Point(i + 1)));
                }

                return true;
            }

            Rhino.Geometry.Polyline p;
            if (crv.TryGetPolyline(out p))
            {
                for (int i = 0; i < (p.Count - 1); i++)
                {
                    L.Add(new Rhino.Geometry.LineCurve(p[i], p[i + 1]));
                }

                return true;
            }

            //Maybe it's a LineCurve?
            Rhino.Geometry.LineCurve line = crv as Rhino.Geometry.LineCurve;
            if (line != null)
            {
                L.Add(line.DuplicateCurve());
                return true;
            }

            //It might still be an ArcCurve...
            Rhino.Geometry.ArcCurve arc = crv as Rhino.Geometry.ArcCurve;
            if (arc != null)
            {
                L.Add(arc.DuplicateCurve());
                return true;
            }

            //Nothing else worked, lets assume it's a nurbs curve and go from there...
            Rhino.Geometry.NurbsCurve nurbs = crv.ToNurbsCurve();
            if (nurbs == null) { return false; }

            double t0 = nurbs.Domain.Min;
            double t1 = nurbs.Domain.Max;
            double t;

            int LN = L.Count;

            do
            {
                if (!nurbs.GetNextDiscontinuity(Rhino.Geometry.Continuity.C1_locus_continuous, t0, t1, out t)) { break; }

                Rhino.Geometry.Interval trim = new Rhino.Geometry.Interval(t0, t);
                if (trim.Length < 1e-10)
                {
                    t0 = t;
                    continue;
                }

                Rhino.Geometry.Curve M = nurbs.DuplicateCurve();
                M = M.Trim(trim);
                if (M.IsValid) { L.Add(M); }

                t0 = t;
            } while (true);

            if (L.Count == LN) { L.Add(nurbs); }

            return true;
        }


        private static void OffsetCrv(Curve aCurve, Point3d aOffsetPoint, double aOffsetLength, ref PolyCurve RefPolycurve)
        {
            Curve[] offsetCurves;

            Rhino.Collections.CurveList crvList = new Rhino.Collections.CurveList();

            if (aOffsetLength > 0)
            {
                //オフセット平面の法線ベクトル = ActiveViewPort-CPlane-ZAxis
                Vector3d CPlaneZAxis = Rhino.RhinoDoc.ActiveDoc.Views.ActiveView.ActiveViewport.ConstructionPlane().ZAxis;
                double tol = Rhino.RhinoDoc.ActiveDoc.PageAbsoluteTolerance;

                //オフセット関数
                offsetCurves = aCurve.Offset(aOffsetPoint, CPlaneZAxis, aOffsetLength, tol, CurveOffsetCornerStyle.Round);

                foreach (Curve crv in offsetCurves)
                    if (CurveSegments(crvList, crv))
                        foreach (Curve S in crvList)
                            RefPolycurve.Append(S);
            }
            else
                // aOffsetLength == 0 
                if (CurveSegments(crvList, aCurve))
                foreach (Curve S in crvList)
                    RefPolycurve.Append(S);

        }


        private static void ObjectsToLayer(RhinoDoc doc, List<Guid> ObjectIDs, string LayerName)
        {
            foreach(Guid id in ObjectIDs)
            {
                var obj = doc.Objects.Find(id);
                ProfileCut7PlugIn.Instance.SetObjectLayer(obj, LayerName);
            }

        }



        /// <summary>
        /// 曲線を選択するUI　
        /// 必要に応じてLeadIn / LeadOutをR方向の複数のカーブに付加する（端点は一致する）
        /// <param name="polyCurves"> ポリカーブ 周回の場合は外側から内側へ　</param>
        /// <param name="LeadInIDs"> LeadInのIDリスト(出力) </param>
        /// <param name="LeadOutIDs">LeadInのOutリスト(出力)</param>
        /// <param name="MethodIndex">選択されたカーブ方法のインデックス　(出力)
        ///　0="DirectCurve", 1="OnCurveDir", 2="OffsetDir", 3="OffsetDirWithApproach"
        ///　</param>

        /// </summary>
        public static bool SelCrvs_AddR_AddInOut(RhinoDoc doc, out CurveProcess.ProfileMethod pm, List<Guid> ToolPathCurveIDs, List<Guid> LeadInIDs, List<Guid> LeadOutIDs, out Guid SrcID)
        {
            SrcID = Guid.Empty;

            const ObjectType filter = ObjectType.Curve;
            ObjRef objref;

            var gp = new Rhino.Input.Custom.GetObject();
            gp.SetCommandPrompt("曲線を選択してください。");
            gp.GeometryFilter = filter;
            gp.EnablePreSelect(true, true);


            // Option の書き方サンプル 　http://developer.rhino3d.com/api/RhinoCommon/html/M_Rhino_Input_Custom_GetBaseClass_AddOptionToggle.htm

            int opList;
            {
                int mi = 0;
                pm = (CurveProcess.ProfileMethod)mi;
                opList = gp.AddOptionList("Method", typeof(CurveProcess.ProfileMethod).GetEnumNames(), mi);
            }

            while (true)
            {

                GetResult get_rc;
                if (pm == CurveProcess.ProfileMethod.DirectCurve)
                {
                    get_rc = gp.GetMultiple(1,0);
                }
                else
                {
                    get_rc = gp.Get();
                }

                if (gp.CommandResult() != Rhino.Commands.Result.Success)
                    return false;

                if (get_rc == Rhino.Input.GetResult.Option)
                {
                    {
                        if (gp.OptionIndex() == opList) 
                            pm = (CurveProcess.ProfileMethod) gp.Option().CurrentListOptionIndex;
                    }
                    continue;
                }
                else if (get_rc == Rhino.Input.GetResult.Object)
                {
                    break;
                }

            }

            if (pm == CurveProcess.ProfileMethod.DirectCurve)
            {
                var p = new Point3d(0, 0, 0);
                foreach(var obj_ref in gp.Objects())
                {
                    PolyCurve ofsPolyCurve = new PolyCurve();
                    var crv = obj_ref.Curve();
                    if (crv == null || crv.IsShort(RhinoMath.ZeroTolerance))
                        continue;
                    OffsetCrv(crv, p, 0, ref ofsPolyCurve);
                    if (ofsPolyCurve != null)
                    {
                        ToolPathCurveIDs.Add( doc.Objects.Add(ofsPolyCurve));

                    }
                }
                ObjectsToLayer(doc, ToolPathCurveIDs, ProfileCut7PlugIn.TOOLPATH_CALC_LAYER);
                return (ToolPathCurveIDs.Count > 0);
                // DirectCurve の場合はここで終了
            }


            // そのほかは、１個として
            objref = gp.Object(0);
            SrcID = objref.ObjectId;
            var curve = objref.Curve();
            if (curve == null || curve.IsShort(RhinoMath.ZeroTolerance))
                return false;
            // ツールとパスの取得
            var gc = new GetProfilingDirection();
            var Tool = ProfileCut7PlugIn.Instance.CurrentTool();
            var Path = ProfileCut7PlugIn.Instance.CurrentPath();
            double roffs = 0;
            if ( pm == CurveProcess.ProfileMethod.OnCurve)
            {
                gc.SetOnCurveToggle(true);
            }
            else if ( pm==CurveProcess.ProfileMethod.OffsetDir || pm == CurveProcess.ProfileMethod.OffsetDirWithApproach )
            {

                //Need Offset
                roffs = (0.5 * Tool.Dia + Path.OffsetStart) * (0.5 * Tool.Dia + Path.OffsetEnd);
                if (roffs <= 0)
                {
                    Rhino.RhinoApp.WriteLine("オフセットできません(tool.Dia + path.OffsetStart/End が0以下)");
                    return false;
                }
                gc.ConstrainToTargetPlane();
                gc.SetCommandPrompt("工具の側と方向を選択してください。");
                gc.SetParameters(curve, false, roffs);
            }


            // 入力オプションの設定
            var mainPolyCurves = new List<PolyCurve>();
            // 側、方向の入力
            while (true)
            {
                Rhino.Input.GetResult get_rc = gc.Get();
                if (gc.CommandResult() != Rhino.Commands.Result.Success)
                    return false;

                if (get_rc == Rhino.Input.GetResult.Point)
                {
                    if (gc.IsCurveReversed())
                        curve.Reverse();

                    // Offsetする ( Rを考慮したループ )
                    PolyCurve ofsPolyCurve = null;
                    var offs = Path.OffsetStart;
                    while (offs >= Path.OffsetEnd)
                    {
                        roffs = Tool.Dia / 2 + offs;

                        ofsPolyCurve = new PolyCurve();
                        OffsetCrv(curve, gc.OffsetPoint(), roffs, ref ofsPolyCurve);

                        if (ofsPolyCurve != null)
                        {
                            mainPolyCurves.Add(ofsPolyCurve);
                            ToolPathCurveIDs.Add(doc.Objects.Add(ofsPolyCurve));
                        }

                        if (offs == Path.OffsetEnd)
                            break;
                        if (Path.OffsetStep <= 0)
                            break;
                        offs = offs - Path.OffsetStep;

                        if (offs - Path.OffsetEnd < 0.001)
                            offs = Path.OffsetEnd;

                    }

                    ObjectsToLayer(doc, ToolPathCurveIDs, ProfileCut7PlugIn.TOOLPATH_CALC_LAYER);

                    if (mainPolyCurves.Count > 0)
                    {
                        if ( pm == CurveProcess.ProfileMethod.OffsetDirWithApproach) 
                        {
                            //Leadの有無でReadIn/Outを付加する。
                            ProfilingArcLines.GetLeadInOutInteractiveUI2(doc, mainPolyCurves, LeadInIDs, LeadOutIDs);
                            SKUtil.DebugPrint("MainCurve Count = " + mainPolyCurves.Count.ToString());
                            SKUtil.DebugPrint("LeadInIDs Count = " + LeadInIDs.Count.ToString());
                            SKUtil.DebugPrint("LeadOutIDs Count = " + LeadOutIDs.Count.ToString());
                        }
                        return true;
                    }

                }
                break;
            }

            return false;
        }


        /// <summary>
        /// LeadIn / LeadOutを複数のカーブに付加する（端点は一致する）
        /// <param name="polyCurves"> ポリカーブ 周回の場合は外側から内側へ　</param>
        /// <param name="LeadInIDs"> LeadInのIDリスト(出力) </param>
        /// <param name="LeadOutIDs">LeadInのOutリスト(出力)</param>
        /// </summary>
        public static bool GetLeadInOutInteractiveUI2(RhinoDoc doc, List<PolyCurve> polyCurves, List<Guid> LeadInIDs, List<Guid> LeadOutIDs)
        {
            GetProfilingLeadInOutMulti LeadInOuts = new GetProfilingLeadInOutMulti(polyCurves);
            OptionToggle IsLeadLinear = new OptionToggle(false, "No", "Yes");
            Rhino.UI.LocalizeStringPair sp1 = new Rhino.UI.LocalizeStringPair("Linear", "直線");
            LeadInOuts.AddOptionToggle(sp1, ref IsLeadLinear);

            //　接近曲線の入力
            int nInputSeq = 0;
            LeadInOuts.SetCommandPrompt("接近曲線");
            while (true)
            {
                GetResult UserInputResult = LeadInOuts.Get();
                if (LeadInOuts.CommandResult() != Rhino.Commands.Result.Success)
                    return false;

                if (UserInputResult == GetResult.Option)
                {
                    //オプションが指定された（リニアをトグル）
                    LeadInOuts.SetLinearToggle(IsLeadLinear.CurrentValue);
                    continue;
                }
                else if (UserInputResult == GetResult.Point)
                {
                    //点が指定された
                    nInputSeq = nInputSeq + 1;

                    //開始点？終了点？
                    if (nInputSeq == 1)
                    {
                        //接近曲線の決定

                        LeadInIDs.AddRange(LeadInOuts.AddCurveIDs(doc,"TOOLPATH_CALC"));
                        //離脱曲線側を設定
                        LeadInOuts.SetCommandPrompt("離脱曲線");
                        LeadInOuts.SetParameters();
                        continue;
                    }
                    else
                    {
                        LeadOutIDs.AddRange(LeadInOuts.AddCurveIDs(doc, "TOOLPATH_CALC"));
                        return true;
                    }
                }
                break;
            }
            return false;
        }

        public static bool GetLeadInOutInteractiveUI(RhinoDoc doc, ref PolyCurve polyCurve, out Guid LeadInID, out Guid LeadOutID)
        {
            LeadInID = Guid.Empty;
            LeadOutID = Guid.Empty;

            var getInOut = new GetProfilingLeadInOut();
            getInOut.ConstrainToTargetPlane();

            OptionToggle IsLeadLinear = new OptionToggle(false, "No", "Yes");
            Rhino.UI.LocalizeStringPair sp1 = new Rhino.UI.LocalizeStringPair("Linear", "直線");
            getInOut.AddOptionToggle( sp1, ref IsLeadLinear);

            //　接近曲線の入力
            int nInputSeq = 0;
            getInOut.SetCommandPrompt("接近曲線");
            polyCurve.Reverse();
            getInOut.SetParameters(polyCurve);
            polyCurve.Reverse();

            while (true)
            {
                GetResult UserInputResult = getInOut.Get();
                if (getInOut.CommandResult() != Rhino.Commands.Result.Success)
                    return false;

                if (UserInputResult == GetResult.Option)
                {
                    //オプションが指定された（リニアをトグル）
                    getInOut.SetLinearToggle(IsLeadLinear.CurrentValue);
                    continue;
                }
                else if (UserInputResult == GetResult.Point)
                {
                    //点が指定された
                    nInputSeq = nInputSeq + 1;

                    //開始点？終了点？
                    if (nInputSeq == 1)
                    {
                        //接近曲線の決定
                        if (IsLeadLinear.CurrentValue)
                            LeadInID = doc.Objects.AddLine(getInOut.GetLine());
                        else
                            LeadInID = doc.Objects.AddArc(getInOut.GetArc());
                        //離脱曲線側を設定
                        getInOut.SetCommandPrompt("離脱曲線");
                        getInOut.SetParameters(polyCurve);

                        continue;
                    }
                    else
                    {
                        if (IsLeadLinear.CurrentValue)
                            LeadOutID = doc.Objects.AddLine(getInOut.GetLine());
                        else
                            LeadOutID = doc.Objects.AddArc(getInOut.GetArc());
                        return true;
                    }
                }
                break;
            }
            return false;
        }
    }

    

    [System.Runtime.InteropServices.Guid("689E4AB9-FCD7-419D-A282-8C33A34B4590"),
        Rhino.Commands.CommandStyle(Rhino.Commands.Style.ScriptRunner)]
    public class ProfileCutCommand : Rhino.Commands.Command
    {
        static ProfileCutCommand _instance;

        public ProfileCutCommand()
        {
            _instance = this;
        }

        public static string Internal_Name_Prefix = "_Tp";
        public static string Final_Name_Prefix = "Tp";

        ///<summary>The only instance of the ProfilingCommand2 command.</summary>
        public static ProfileCutCommand Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "ProfileCut"; }
        }

        private RhinoDoc Doc;

        /// <summary>
        /// CreateToolPathCrvs
        /// コマンドから、ツールパスの曲線生成を行う関数
        /// 設定により、導入部、R方向、Z方向のツールパス曲線生成が行われる。
        /// 曲線には名前が付けられポスト時に区別される。
        /// </summary>
        /// <returns>bool true:ツールパスを生成した　/生成しなかった　 </returns>
        public bool CreateToolPathCrvs()
        {
            // Prefixは固定とした
            string Prefix = Internal_Name_Prefix;

            // ProfileCommand用の名前をクリアする
            SKUtil.ClearObjectsName(Doc, Prefix + "_C");
            SKUtil.ClearObjectsName(Doc, Prefix + "_IN");
            SKUtil.ClearObjectsName(Doc, Prefix + "_OUT");

            //加工カーブと、入力と出力の円弧を別々に返す。
            Guid idSrc;
            List<Guid> idCurveList = new List<Guid>();
            List<Guid> idOutList = new List<Guid>();
            List<Guid> idInList = new List<Guid>();

            // idCurveList が複数あるときは、ダイレクトカーブの時のみで、idIn, idOutは無い(null)
            CurveProcess.ProfileMethod pm;
            if (!ProfilingArcLines.SelCrvs_AddR_AddInOut(Doc, 
                out pm, idCurveList, idInList, idOutList, out idSrc))
            {
                SKUtil.DebugPrint("コマンドはキャンセルされました。");
                return false;
            }

            foreach (var id in idCurveList)
            {
                SKUtil.SetObjectName(Doc, id, Prefix + "_C");
            }
            foreach (var id in idInList)
            {
                SKUtil.SetObjectName(Doc, id, Prefix + "_IN");
            }
            foreach (var id in idOutList)
            {
                SKUtil.SetObjectName(Doc, id, Prefix + "_OUT");
            }

            return ToolPathCrvs_AddZ_IntoGroup(pm, idCurveList, idInList, idOutList);

        }

        public ProfileCut7PlugIn MyPlugIn
        {
            get
            {
                return ProfileCut7PlugIn.Instance;
            }
        }

        public string Prefix = ProfileCutCommand.Internal_Name_Prefix;

        /// <summary>
        /// 曲線を接続する情報を付加し、Z方向を追加し、グループ化する
        /// <param name="pm"> 加工方法(CurveProcess.ProfileMethod)　</param>
        /// <param name="idCurveList"> CurveのIDリスト(出力) </param>
        /// <param name="idInList">加工導入曲線リスト(出力)</param>
        /// <param name="idOutList">加工終了曲線リスト(出力)</param>
        /// </summary>
        public bool ToolPathCrvs_AddZ_IntoGroup(CurveProcess.ProfileMethod pm, List<Guid> idCurveList, List<Guid> idInList, List<Guid> idOutList)
        {
            //? idCurveList, idInList, idOutListの数は同じである必要がある。
            // Debug.Assert((idInList.Count == idCurveList.Count), "AddZ_ToolPathCrvs(): idInListの要素数が異常");
            // Debug.Assert((idOutList.Count == idCurveList.Count), "AddZ_ToolPathCrvs(): idOutListの要素数が異常");


            //グループ化の準備
            List<Guid> group_ids = new List<Guid>();

            //メインカーブは最後のオブジェクトになっている。
            RhinoObject MainCurveObj = Doc.Objects.Find ( idCurveList.Last() );
            //パス加工の順序(R_ORDER)を MainCurveObjにリストの順に設定　（外側から加工）
            SetIDListToUserString(MainCurveObj, idCurveList, "R_ORDER");
            MainCurveObj.CommitChanges();

            for ( int i=0; i<idCurveList.Count; i++)
            {
                RhinoObject obj = Doc.Objects.Find(idCurveList[i]);
                if (i == idCurveList.Count - 1)
                {
                    // Last= Main
                    obj.Attributes.Name = Prefix + "_MAIN";
                    // CurveObjに加工情報を設定
                    SetUserString(obj, "TOOL", Tool.Name);
                    SetUserString(obj, "PATH", Path.Name);
                }
                else
                {
                    obj.Attributes.Name = Prefix + "_R_PATH";
                }
                //TPInfo
                MyPlugIn.SetObjectLayerAndColor(obj, ProfileCut7PlugIn.TOOLPATH_CALC_LAYER, Color.Black);
                SetUserString(obj, "TPInfo", string.Format("{0},{1:0}", "C", Tool.Feed));
                obj.CommitChanges();

                Guid idIn, idOut;
                
                //空のGUIDは導入部、退出部が無いことを示す。

                if (idInList.Count == idCurveList.Count)
                    idIn = idInList[i];
                else
                    idIn = new Guid();

                if (idOutList.Count == idCurveList.Count)
                    idOut = idOutList[i];
                else
                    idOut = new Guid();

                List<Guid> curve_order_ids;                
                curve_order_ids = CreateCrvOrderIDList_AddAproachSpeed(idCurveList[i], idIn, idOut);

                List<Guid> created_ids = new List<Guid>();
                if (pm==CurveProcess.ProfileMethod.DirectCurve)
                    PathZOrderLoop(obj, curve_order_ids, created_ids, true);
                else
                    PathZOrderLoop( obj, curve_order_ids, created_ids, i==0 );

                //メインカーブもグループに加える
                created_ids.AddRange(curve_order_ids);

                group_ids.AddRange(created_ids);
            }
            var Ret = group_ids.Count > 0;
            //すべてグループ化
            if (Ret)
            {
                int grp = Doc.Groups.Add(group_ids);
            }
            return Ret;
        }

        /// <summary>
        /// 曲線を整列した順のIDをリストにして出力する
        /// その際、In,Outの曲線に、ユーザー文字列を加えて速度を記載する
        /// <param name="idCurve">メインカーブのID</param>
        /// <param name="idIn">導入カーブのID</param>
        /// <param name="idOut">導出カーブのID</param>
        /// </summary>
        public List<Guid> CreateCrvOrderIDList_AddAproachSpeed(Guid idCurve, Guid idIn, Guid idOut)
        {
            var curve_order_ids = new List<Guid>();

            //レイヤ(TOOLPATH)
            var CurveObj = Doc.Objects.Find(idCurve);

            //LeadIn/Outの存在チェック
            var LeadInObj = Doc.Objects.Find(idIn);
            var LeadOutObj = Doc.Objects.Find(idOut);
            var IsExistLead = (LeadInObj != null && LeadOutObj != null);

            if (IsExistLead)
            {
                //色とレイヤの設定
                MyPlugIn.SetObjectLayerAndColor(LeadInObj, ProfileCut7PlugIn.TOOLPATH_CALC_LAYER, Color.Black);
                MyPlugIn.SetObjectLayerAndColor(LeadOutObj, ProfileCut7PlugIn.TOOLPATH_CALC_LAYER, Color.Black);

                //カーブの順序を整列
                List<RhinoObject> objs = new List<RhinoObject>();
                objs.Add(LeadInObj);
                objs.Add(CurveObj);
                objs.Add(LeadOutObj);
                SKCurveOrder.CurveObjectDirByOrder(objs);

                // アプローチの速度を設定
                SetUserString(LeadInObj, "TPInfo", string.Format("{0},{1:0}", "AP", Tool.AprXYFeed));
                SetUserString(LeadOutObj, "TPInfo", string.Format("{0},{1:0}", "RT", Tool.AprXYFeed));

                //var LeadIn = LeadInObj.Geometry as Rhino.Geometry.Curve;
                //var LeadOut = LeadOutObj.Geometry as Rhino.Geometry.Curve;

                curve_order_ids.Add(LeadInObj.Id);
                curve_order_ids.Add(CurveObj.Id);
                curve_order_ids.Add(LeadOutObj.Id);
            }
            else
            {
                curve_order_ids.Add(CurveObj.Id);
            }


            //線加工の順序(CURVE_ORDER)をリストの順に設定
            SetIDListToUserString(CurveObj, curve_order_ids, "CURVE_ORDER");
            return curve_order_ids;
        }

        public int SetIDListToUserString(RhinoObject aObj, List<Guid> aIDList, string aUserStringKey)
        {
            var value = "";
            var nRet = 0;
            foreach (var id in aIDList)
            {
                if (value != "")
                    value = value + ",";
                value = value + id.ToString();
                nRet++;
            }
            SKUtil.SetUserString(Doc, aObj, aUserStringKey, value);
            return nRet;
        }

        /// <summary>
        /// 整列後の直線をZ方向の情報を付加し、必要なら複製をする
        /// <param name="aPathCurve">メインカーブ</param>
        /// <param name="aOrderIDList">メインカーブの加工順のIDリスト</param>
        /// <param name="aCreatedIDList">生成したIDのリスト(出力)</param>
        /// 
        /// </summary>
        private void PathZOrderLoop(RhinoObject aPathCurve, List<Guid> aOrderIDList,  List<Guid> aCreatedIDList, bool bZLoop)
        {
            var z_order_ids = new List<Guid>();
            if (Path.ZStep > 0 && bZLoop )
            {
                // TODO: とてもたくさんになる場合はエラー( form側で ）
                double zoffset = Path.ZStart;
                while(zoffset >= Path.ZEnd)
                {
                    PathZOrderAtZ(aPathCurve, aOrderIDList, aCreatedIDList, z_order_ids, zoffset);
                    if (zoffset == Path.ZEnd)
                        break;
                    // zoffsetの再設定
                    zoffset = zoffset - Path.ZStep;
                    if (zoffset - Path.ZEnd < 0.001)
                        zoffset = Path.ZEnd;
                }
            }
            else
            {
                //1回のみ
                PathZOrderAtZ(aPathCurve, aOrderIDList, aCreatedIDList, z_order_ids, Path.ZEnd);
            }
            SetIDListToUserString(aPathCurve, z_order_ids, "Z_ORDER");
        }

        private Guid AddTextDot(Curve crv, string txt)
        {
            var pt1 = new Point3d(crv.PointAtStart);
            var pt2 = new Point3d(crv.PointAtEnd);
            var pt = (pt1 + pt2) / 2;
            var id= Doc.Objects.AddTextDot(txt, pt);
            var obj = Doc.Objects.Find(id);
            if(obj!= null)
            {
                MyPlugIn.SetObjectLayerAndColor(obj, ProfileCut7PlugIn.TOOLPATH_INFO_LAYER, Color.Lime);
            }
            return id;
        }

        private RhinoObject AddLineObj(Point3d start_pt, Point3d end_pt, string name, double feed, Color col)
        {
            Line ln = new Line(start_pt, end_pt);
            var line_id = Doc.Objects.AddLine(ln);
            var obj = Doc.Objects.Find(line_id);
            if (obj != null)
            {
                obj.Attributes.Name = name;
                SetUserString(obj, "TPInfo", string.Format("{0},{1:0}", name, feed));
                MyPlugIn.SetObjectToToolpathLayer(obj, Color.Lime);
                obj.CommitChanges();
            }
            return obj;
        }

        /// <summary>
        /// Z方向の繰り返しで、元のカーブを複製する
        /// <param name="aMainCurve">メインカーブ</param>
        /// <param name="aOrderIDList">加工順のIDリスト</param>
        /// <param name="aCreatedIDList">生成したIDのリスト(出力)</param>
        /// <param name="aZOrderIDList">Zオーダーのリスト</param>
        /// <param name="aZoffset">Zオフセット量</param>
        /// </summary>
        private void PathZOrderAtZ(RhinoObject aMainCurve, List<Guid> aOrderIDList, 
            List<Guid> aCreatedIDList, List<Guid> aZOrderIDList, double aZoffset)
        {
            //複製後のオブジェクトの管理用
            var dup_order_list = new List<Guid>();
            int dup_order_count = 0;
            RhinoObject dup_maincurve_obj = null;

            for(int i=0; i<aOrderIDList.Count; i++)
            {
                var src_id = aOrderIDList[i];
                var src_obj = Doc.Objects.Find(src_id);
                if (src_obj != null)
                {
                    GeometryBase dup_geom;
                    Curve dup_crv = null;
                    Guid new_id;
                    RhinoObject new_obj = null;

                    dup_geom = src_obj.DuplicateGeometry();
                    dup_crv = dup_geom as Curve;
                    dup_crv.Translate(new Vector3d(0, 0, aZoffset));
                    new_id = Doc.Objects.AddCurve(dup_crv);
                    new_obj = Doc.Objects.Find(new_id);
                    new_obj.Attributes.Name = src_obj.Attributes.Name;
                    new_obj.Attributes = src_obj.Attributes.Duplicate();
                    /*
                    // ツールパスなどのAttributes のCopy (Duplicateではなく）
                    foreach (var key in src_obj.Attributes.GetUserStrings().AllKeys)
                    {
                        var value = src_obj.Attributes.GetUserString(key);
                        new_obj.Attributes.SetUserString(key, value);
                    }
                    */

                    //Z方向アプローチを生成
                    if (i == 0)
                    {
                        //低速アプローチ
                        var pt = new Point3d(dup_crv.PointAtStart);
                        pt.Z = pt.Z + Path.ZApr;

                        if (pt.Z >= Path.ZRapid)
                            pt.Z = Path.ZRapid;

                        var ZAprObj = AddLineObj(pt, dup_crv.PointAtStart,"APZ", Tool.AprZFeed, Color.Lime );

                        //必要なら高速アプローチを追加
                        if (pt.Z < Path.ZRapid)
                        {
                            var pt0 = new Point3d(pt);
                            pt0.Z = Path.ZRapid;
                            var ZRapidObj = AddLineObj(pt0, pt, "RAPZ", Path.RapidFeed, Color.Lime );

                            dup_order_count++;
                            dup_order_list.Add(ZRapidObj.Id);

                            aCreatedIDList.Add(ZRapidObj.Id);
                            aCreatedIDList.Add(
                                AddTextDot(ZRapidObj.Geometry as Curve, string.Format("Z={0}, ({1}) {2}", aZoffset, dup_order_count, ZRapidObj.Name))
                            );
                        }

                        dup_order_count++;
                        dup_order_list.Add(ZAprObj.Id);

                        aCreatedIDList.Add(ZAprObj.Id);
                        aCreatedIDList.Add(
                            AddTextDot(ZAprObj.Geometry as Curve, string.Format("Z={0}, ({1}) {2}", aZoffset, dup_order_count, ZAprObj.Name))
                            );
                    }

                    if (src_id == aMainCurve.Id)
                    {
                        // MainCurveの投影の場合
                        dup_maincurve_obj = new_obj;
                        // aMainCurveの名前 --> _Z_PATH
                        new_obj.Attributes.Name = Prefix + "_Z_PATH";
                        aZOrderIDList.Add(new_id);
                        MyPlugIn.SetObjectToToolpathLayer(new_obj, Color.Cyan);
                    }
                    else
                    {
                        // In/Outの投影の場合
                        if (i == 0)
                        {
                            MyPlugIn.SetObjectToToolpathLayer(new_obj, Color.Magenta);
                        }
                        else
                        {
                            MyPlugIn.SetObjectToToolpathLayer(new_obj, Color.LimeGreen);
                        }
                    }
                    new_obj.CommitChanges();
                    // src_obj.CommitChanges();

                    // 順番リスト
                    dup_order_count++;
                    dup_order_list.Add(new_id);
                    //全体グループへ登録
                    aCreatedIDList.Add(new_id);
                    aCreatedIDList.Add(
                        AddTextDot(dup_crv, string.Format("Z={0}, ({1}) {2}", aZoffset, dup_order_count, new_obj.Name))
                    );

                    SKUtil.DebugPrint("src_obj name=" + src_obj.Attributes.Name);
                    SKUtil.DebugPrint("new_obj name=" + new_obj.Attributes.Name);

                    //Z方向退避を生成
                    if (i == aOrderIDList.Count-1)
                    {
                        var pt = new Point3d(dup_crv.PointAtEnd);
                        pt.Z = Path.ZRapid;

                        var ZRetObj = AddLineObj(dup_crv.PointAtEnd,pt, "RTZ", Path.RapidFeed, Color.Lime);

                        dup_order_count++;
                        dup_order_list.Add(ZRetObj.Id);

                        aCreatedIDList.Add(ZRetObj.Id);
                        aCreatedIDList.Add(
                            AddTextDot(ZRetObj.Geometry as Curve, string.Format("Z={0}, ({1}) {2}", aZoffset, dup_order_count, ZRetObj.Name))
                            );
                    }
                }
            }

            // ORDERの設定
            SetIDListToUserString(dup_maincurve_obj, dup_order_list, "CURVE_ORDER");
        }


        protected ToolData Tool;
        protected PathData Path;

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            Doc = doc;
            // CAMオブジェクト
            Tool = MyPlugIn.CurrentTool();
            Path = MyPlugIn.CurrentPath();
            if (CreateToolPathCrvs() == true)
            {
                doc.Views.Redraw();
                // Z方向は基本カーブを変換してから複製したほうがよいかも？
                MyPlugIn.ConvertPrecisionAll();
                return Result.Success;
            }
            return Result.Cancel;
        }

        private void SetUserString(RhinoObject obj, string key, string value)
        {
            SKUtil.SetUserString(Doc, obj, key, value);
        }
    }
}
