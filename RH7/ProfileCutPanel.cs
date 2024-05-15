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

namespace ProfileCut7
{


    /// GUID 2016/10/10 sae
    //[System.Runtime.InteropServices.Guid("84B50283-5AFB-4BC0-B9E5-1D719CA916FA")]

    // 2021/01/20
    [System.Runtime.InteropServices.Guid("5fe5cbbf-09b9-47ee-8fbd-1712dadd4aeb")]
    public partial class ProfileCutPanel : UserControl
    {
        public Tool tool;

        public RhinoDoc Doc
        {
            get
            {
                return RhinoDoc.ActiveDoc;
            }
        }

        public ProfileCutPanel()
        {
            InitializeComponent();
            PlugIn.MainUIPanel = this;
            this.VisibleChanged += new EventHandler(ProfileCutPanel_VisibleChanged);
            this.Disposed += new EventHandler(ProfileCutPanel_Disposed);
            var tool = PlugIn.CurrentTool();
            if (tool != null)
                LblToolName.Text = tool.Name;
            else
                LblToolName.Text = "";
            var pathgen = PlugIn.CurrentPath();
            if (pathgen != null)
                LblPathName.Text = pathgen.Name;
            else
                LblPathName.Text = "";
            var post = PlugIn.CurrentPost();
            if (post != null)
                LblPostName.Text = post.Name;
            else
                LblPostName.Text = "";

        }

        public ProfileCut7PlugIn PlugIn
        {
            get
            {
                return ProfileCut7PlugIn.Instance;
            }
        }

        void ProfileCutPanel_VisibleChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Occurs when the component is disposed by a call to the
        /// System.ComponentModel.Component.Dispose() method.
        /// </summary>
        void ProfileCutPanel_Disposed(object sender, EventArgs e)
        {
            // Clear the user control property on our plug-in
            PlugIn.MainUIPanel = null;
        }

        /// <summary>
        /// Returns the ID of this panel.
        /// </summary>
        public static System.Guid PanelId
        {
            get
            {
                return typeof(ProfileCutPanel).GUID;
            }
        }

        private void BtnInitialSet_Click(object sender, EventArgs e)
        {
        }

        private void BtnTool_Click(object sender, EventArgs e)
        {
            var f = new EditTool();
            if (PlugIn.CurrentTool() != null)
            {
                f.BrowseStartName = PlugIn.CurrentTool().Name;
            }
            if (f.ShowDialog() == DialogResult.OK)
            {
                //選択された名前を取得
                string name = f.GetSelectedName();
                LblToolName.Text = name;
                PlugIn.SetCurrentTool(name);
            }
        }

        private void BtnPathGen_Click(object sender, EventArgs e)
        {
            var f = new EditPath();
            if (PlugIn.CurrentPath() != null)
            {
                f.BrowseStartName = PlugIn.CurrentPath().Name;
            }
            if (f.ShowDialog() == DialogResult.OK)
            {
                //選択された名前を取得
                string name = f.GetSelectedName();
                LblPathName.Text = name;
                PlugIn.SetCurrentPath(name);
            }
        }

        private void BtnPost_Click(object sender, EventArgs e)
        {
            //機械コードを出力する
            var f = new EditPost();
            if (PlugIn.CurrentPost() != null)
            {
                f.BrowseStartName = PlugIn.CurrentPost().Name;
            }
            if (f.ShowDialog() == DialogResult.OK)
            {
                //選択された名前を取得
                string name = f.GetSelectedName();
                LblPostName.Text = name;
                PlugIn.SetCurrentPost(name);
            }
        }


        private void BtnProfileCut_Click(object sender, EventArgs e)
        {
            //加工＋導入パスを作る
            var pathgen = PlugIn.CurrentPath();

            string cmd = @"ProfileCut ";
            string param = @"";
            if (pathgen.Method == ToolPathMethod.DirectCurve)
                param = @"M=DirectCurve";
            else if (pathgen.Method == ToolPathMethod.Offset)
                param = @"M=OffsetDir";
            else if (pathgen.Method == ToolPathMethod.OffsetAddCurve)
                param = @"M=OffsetDirWithApproach";

            RhinoApp.RunScript(cmd + param, false);
        }

        private void SetUserString(RhinoObject obj, string key, string value)
        {
            SKUtil.SetUserString(Doc, obj, key, value);
        }

        private void BtnDebugUserString_Click(object sender, EventArgs e)
        {
            //選択されたオブジェクトが無い場合は終了
            var sels = Doc.Objects.GetSelectedObjects(false, false);
            if (sels.Count() == 0)
                return;

            //UserStringFormを生成
            var USForm = new UserStringForm();
            //選択されたオブジェクトのUserStringを全て表示
            foreach (var obj in Doc.Objects.GetSelectedObjects(false, false))
            {
                USForm.AddLine(string.Format("Name={0} ID={1}",obj.Name, obj.Id.ToString()));
                var ustrs = obj.Attributes.GetUserStrings();
                foreach( string key in ustrs)
                {
                    string[] values = ustrs.GetValues(key);
                    foreach (var val in values)
                    {
                        USForm.AddLine(string.Format("[{0}] = {1}", key,val));
                    }
                }
                USForm.AddLine("");
            }
            USForm.ShowDialog();
        }


        public ObjRef CreateBaseSrf(BoundingBox bbox, string name, bool bVisible, Color col)
        {
            // bboxの領域の最小値が1以上になるように設定
            if (bbox.Max.X - bbox.Min.X < 1.0 )
            {
                var p = new Point3d(bbox.Min);
                p.X = p.X - 0.5;
                bbox.Min = p;
                p = new Point3d(bbox.Max);
                p.X = p.X + 0.5;
                bbox.Max = p;
            }

            if (bbox.Max.Y - bbox.Min.Y < 1.0)
            {
                var p = new Point3d(bbox.Min);
                p.Y = p.Y - 0.5;
                bbox.Min = p;
                p = new Point3d(bbox.Max);
                p.Y = p.Y + 0.5;
                bbox.Max = p;
            }

            var pt1 = new Point3d(bbox.Min);
            var z = bbox.Min.Z;
            var pt2 = new Point3d(bbox.Min.X, bbox.Max.Y, z);
            var pt3 = new Point3d(bbox.Max.X, bbox.Max.Y, z);
            var pt4 = new Point3d(bbox.Max.X, bbox.Min.Y, z);

            //すでにあれば削除する
            SKUtil.DeleteObjectsByName(Doc, name);

            //加工範囲に平面をつくる
            Surface surface = NurbsSurface.CreateFromCorners(
                pt1, pt2, pt3, pt4);
            var SrfID = Doc.Objects.AddSurface(surface);

            //オブジェクトを設定する
            // 唯一名のオブジェクト
            ObjRef SrfRef = new ObjRef(SrfID);
            SKUtil.ClearSetObjectName(Doc, SrfRef, name);

            PlugIn.SetObjectToToolpathLayer(SrfRef.Object(), Color.Blue);
            return SrfRef;
        }


        //aObjRefが所属しているグループのオブジェクトを選択状態にして、それをリストに入れて返す
        public List<ObjRef> GetGroupObjectAndSelect(ObjRef aObjRef, bool aSelectOn)
        {
            //SAMPLE https://github.com/mcneel/rhinocommon/blob/master/examples/wiki/selectgroupobject.txt

            var aObj = aObjRef.Object();
            if (aObj == null)
                return null;

            var ret = new List<ObjRef>();
            var rhino_object_groups = aObj.Attributes.GetGroupList().DefaultIfEmpty(-1);

            var selectable_objects = from obj in Doc.Objects.GetObjectList(ObjectType.AnyObject)
                                     where obj.IsSelectable(true, false, false, false)
                                     select obj;

            foreach (var selectable_object in selectable_objects)
            {
                var group_list = selectable_object.Attributes.GetGroupList();
                if (group_list == null)
                    continue;
                foreach (var group in selectable_object.Attributes.GetGroupList())
                {
                    if (rhino_object_groups.Contains(group))
                    {
                        selectable_object.Select(aSelectOn);
                        ret.Add( new ObjRef(selectable_object) );
//                        Doc.Objects.Select(selectable_object.Id);
                        continue;
                    }
                }
            }
            Doc.Views.Redraw();
            return ret;
        }

        // SKUtil.PickOneより(意味がなかった）
        public static ObjRef PickOne(RhinoDoc aDoc, string aMsg, ObjectType GeometryFilter = 0, bool bAfterUnselect = true)
        {
            ObjRef ret = null;
            var go = new GetObject();
            go.SetCommandPrompt(aMsg);
            go.GeometryFilter = GeometryFilter;
            go.AlreadySelectedObjectSelect = true;
            go.DeselectAllBeforePostSelect = false;
            go.EnableUnselectObjectsOnExit(false);
            //go.EnableClearObjectsOnEntry(false);
            go.EnablePreSelect(true,true);

            go.Get();

            if (go.CommandResult() != Result.Success)
            {
                if (go.CommandResult() == Result.Cancel)
                    SKUtil.DebugPrint("中止しました。");
                else
                    SKUtil.DebugPrint("選択に失敗しました。");
                return ret;
            }
            ret = go.Object(0);
            SKUtil.DebugPrint("選択=>" + ret.ObjectId.ToString());

            if (bAfterUnselect)
            {
                aDoc.Objects.UnselectAll();
                aDoc.Views.Redraw();
            }
            return ret;
        }


        public static ObjRef[] MultipleGroupSelect(RhinoDoc aDoc, string aMsg, ObjectType GeometryFilter = 0, bool bAfterUnselect = true)
        {
            ObjRef[] ret = null;
            var go = new GetObject();
            go.SetCommandPrompt(aMsg);
            go.GeometryFilter = GeometryFilter;
            go.SubObjectSelect = false;
            go.GroupSelect = true;
            go.GetMultiple(1, 0);

            if (go.CommandResult() != Result.Success)
            {
                if (go.CommandResult() == Result.Cancel)
                    SKUtil.DebugPrint("中止しました。");
                else
                    SKUtil.DebugPrint("選択に失敗しました。");
                return ret;
            }
            ret = go.Objects();
            return ret;
        }

        private void BtnSetOrder_Click(object sender, EventArgs e)
        {
            //加工順指定UI
            string MainPathCurveName = ProfileCutCommand.Final_Name_Prefix + "_MAIN";

            List<ObjRef> main_path_list = new List<ObjRef>();
            List<ObjRef> order_path_list = new List<ObjRef>();

            ObjRef[] selection = MultipleGroupSelect(Doc, "加工順にツールパスをピック", ObjectType.Curve);
            if (selection == null)
            {
                SKUtil.DebugPrint("選択がありません。");
                return;
            }

            foreach (var oref in selection)
            {
                order_path_list.Add(oref);
                //名前をチェックして、main_path_listに追加する
                if (oref.Object().Attributes.Name == MainPathCurveName)
                    main_path_list.Add(oref);
            }
            if (main_path_list.Count == 0)
            {
                SKUtil.DebugPrint("主加工線 [" + MainPathCurveName + "]がありません。");
                return;
            }

            #region 実装のメモ（グループの選択について）
            /*  PickOneだとグループを検索せねばならず、またPickOne(GetObject)のオプションを使っても選択状態を維持するのが不可能のように見える
            while (true)
            {
                ObjRef obj_ref = PickOne(Doc, "加工順にツールパスをピック", ObjectType.Curve, false);
                if (obj_ref == null)
                    break;
                var ref_list = GetGroupObjectAndSelect(obj_ref, true);

                foreach(var oref in ref_list)
                {
                    all_path_list.Add(oref);
                    //名前をチェックして、main_path_listに追加する
                    if (oref.Object().Attributes.Name == MainPathCurveName)
                        main_path_list.Add(obj_ref);
                }
            }
            */
            #endregion


            //bounding boxの計算

            //API http://developer.rhino3d.com/api/RhinoCommonWin/html/Methods_T_Rhino_Geometry_BoundingBox.htm
            //CURVE http://developer.rhino3d.com/samples/rhinocommon/curve_bounding_box/
            BoundingBox bbox= new BoundingBox();
            bool emptyBox = true;
            for (int i=0; i<order_path_list.Count; i++)
            {
                Curve crv = order_path_list[i].Curve();
                if (crv!= null)
                {
                    if (emptyBox)
                    {
                        bbox = crv.GetBoundingBox(true);
                        emptyBox = false;
                    }
                    else
                    {
                        BoundingBox bbox2 = crv.GetBoundingBox(true);
                        bbox = BoundingBox.Union(bbox, bbox2);
                    }
                }
            }

            //曲線を包含するバウンディングボックスから、
            //加工範囲をあらわすWORK_PLANE平面をつくる
            ObjRef WorkPlane = CreateBaseSrf(bbox, "PROFILE_CUT_WORK_PLANE", false, Color.Blue);
            ObjRef ABSPlane = CreateBaseSrf(bbox, "PROFILE_CUT_ABS_PLANE", false, Color.Blue);

            /* ツールパスオブジェクト 
                *[WORK_PLANE] = 加工平面のGUIDを設定
            */
            foreach (var crvref in main_path_list)
            {
                SetUserString(crvref.Object(), "WORK_PLANE", WorkPlane.ObjectId.ToString());
            }

            /* 加工平面 (G54) 
                [Name] = "PROFILE_CUT_WORK_PLANE"  (複数あり)
                *[WORK_SYS]=(ex.)"G54P03" , 
                *[ORG_ABS]="x,y,z" (ABS平面上の原点）
                *[Z_MAX]=50.0, (作業平面上のパスの最大座標）
                *[Z_MIN]=0.0   (作業平面上のパスの最小座標）
            */
            SetUserString(WorkPlane.Object(), "WORK_SYS", "RML1");
            SetUserString(WorkPlane.Object(), "ORG_ABS", "0,0,0");
            SetUserString(WorkPlane.Object(), "Z_MAX", bbox.Max.Z.ToString());
            SetUserString(WorkPlane.Object(), "Z_MIN", bbox.Min.Z.ToString());

            /* 機械座標平面+加工情報 (G28 / ABS=WORLD)
                [Name] = "PROFILE_CUT_ABS_PLANE" (唯一)
                *[Z_MOVE]=100.0 (ABS座標での移動面)（ポストプロセス時）
                *[Z_MAX]=50.0   (ABS座標での最大座標)
                *[Z_MIN]=0.0    (ABS座標での最小座標)
                *[MAIN_ORDER] = ToolPath.GUIDのカンマ区切りリスト
                *[POST] = ポストプロセス名（ポストプロセス時）
            */
            SetUserString(ABSPlane.Object(), "Z_MAX", bbox.Max.Z.ToString());
            SetUserString(ABSPlane.Object(), "Z_MIN", bbox.Min.Z.ToString());
            SetUserString(ABSPlane.Object(), "Z_MOVE", "");
            SetUserString(ABSPlane.Object(), "POST", PlugIn.CurrentPost().Name);

            string order = "";
            foreach (var crvref in main_path_list)
            {
                if (order != "")
                    order = order + ",";
                order = order + crvref.ObjectId.ToString();
            }
            SetUserString(ABSPlane.Object(), "MAIN_ORDER", order);
            Doc.Views.Redraw();
        }

        private void BtnOutput_Click(object sender, EventArgs e)
        {
            RhinoObject ABSPlane = SKUtil.GetObjectByName(Doc, "PROFILE_CUT_ABS_PLANE");
            if (ABSPlane == null)
            {
                SKUtil.DebugPrint("ツールパス用のABS_PLANEがありません。");
                return;
            }
            var post_name = ABSPlane.Attributes.GetUserString("POST");
            var Post = PostData.LoadByName(post_name);
            if (Post == null)
            {
                SKUtil.DebugPrint("ABS_PLANEにポストが設定されていません。");
                return;
            }


            if ( Post.SaveFileName== null)
            {
                RhinoApp.WriteLine("保存するファイル名が設定されていません。");
                return;
            }

            string filename = Post.SaveFileName;
            if (filename == "")
            {
                //
                //SaveFileDialogクラスのインスタンスを作成
                SaveFileDialog sfd = new SaveFileDialog();

                //はじめのファイル名を指定する
                sfd.FileName = "加工データ.txt";
                // デスクトップを指定
                sfd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //[ファイルの種類]に表示される選択肢を指定する
                sfd.Filter = "txt ファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
                //[ファイルの種類]ではじめに選択されるものを指定する
                sfd.FilterIndex = 1;
                //タイトルを設定する
                sfd.Title = "保存先のファイルを選択してください";
                //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
                sfd.RestoreDirectory = true;
                //既に存在するファイル名を指定したとき警告する
                sfd.OverwritePrompt = true;
                //存在しないパスが指定されたとき警告を表示する
                sfd.CheckPathExists = true;

                //ダイアログを表示する
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //OKボタンがクリックされたとき、選択されたファイル名を表示する
                   filename = sfd.FileName;
                }
                else
                {
                    RhinoApp.WriteLine("ファイルの指定がキャンセルされました。");
                    return;
                }

            }

            //( Shift JIS )
            //書き込むファイルが既に存在している場合は、上書きする
            System.IO.StreamWriter sw;
            try
            {
                sw = new System.IO.StreamWriter(
                    filename,
                    false,
                    System.Text.Encoding.GetEncoding("shift_jis"));
            }
            catch
            {
                RhinoApp.WriteLine("保存するファイル名等に問題があります。"+ Post.SaveFileName);
                return;
            }

            SKOutputToolPath outToolPath = new SKOutputToolPath(Doc, cbxOutputInfo.Checked);
            try
            {
                if ( outToolPath.WriteMachineCode(sw) == true)
                {
                    //機械への送信アプリケーションを起動する (Post.ExecApp)
                    string app = Post.ExecFileName.Trim();
                    if (app != "")
                    {
                        System.Diagnostics.Process.Start(app, @"" + filename + @"");
                    }
                }
            }
            finally
            {
                sw.Close();
            }

        }

        private void BtnDelPath_Click(object sender, EventArgs e)
        {
            //計算パスを消去します
            PlugIn.DeleteDataObject();
        }
    }
}
