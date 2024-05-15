using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic; //ジェネリック型
using Rhino;
using Rhino.DocObjects;
using Rhino.Commands;
using Rhino.Input;
using Rhino.Input.Custom;
using Rhino.Geometry;

namespace ProfileCut7
{
    class SKUtil
    {
        /// <summary>
        /// ライノのコマンドエリアと、開発環境のデバッグエリアに文字を出力する
        /// </summary>
        /// <param name="aMsg"></param>
        public static void DebugPrint(String aMsg)
        {
            RhinoApp.WriteLine(aMsg);
            Debug.WriteLine(aMsg);
        }

        /// <summary>
        /// RunPluginPythonScriptで実行するときのフォルダ
        /// </summary>
        public static string AssemblyFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        /*
        /// <summary>
        /// Pythonスクリプトファイルを指定して実行する（フルパスで指定する時）
        /// </summary>
        /// <param name="aScriptFileName">pythonスクリプトのファイル名</param>
        /// <param name="aEchoMode">マクロのコマンドをコマンドヒストリウィンドウに表示するかどうか</param>
        /// <returns></returns>
        public static bool RunPythonScript( string aScriptFileName, bool bEcho = true)
        {
//            RhUtil.RhinoApp().RunScript(@"-RunPythonScript "C:\Users\sae\AppData\Roaming\McNeel\Rhinoceros\5.0\scripts\Hello python.py" ");          
            // RunPythonScriptでファイル名を指定するには、-RunPythonScriptとする必要がある。ファイル名はダブルクオーテーションでくくる必要がある。
            return RhinoApp.RunScript(@"-RunPythonScript """+aScriptFileName+@"""", bEcho);
        }


        /// <summary>
        /// Pythonスクリプトファイルを指定して実行する　スクリプトの場所は、デフォルトではRHPと同じ場所。(ScriptFolderで変更可能、プラグインの開発時は、プロジェクト特定のフォルダとする)
        /// </summary>
        /// <param name="aScriptFileName"></param>
        /// <returns></returns>
        public static bool RunPluginPythonScript(string aScriptFileName, string aParam=null, bool bEcho=true)
        {
            aScriptFileName = ScriptFolder + "\\"+ aScriptFileName;
            string cmd = @"-RunPythonScript """ + aScriptFileName + @"""";
            if (aParam != null)
            {
                cmd = cmd + " " + aParam;
            }
            return RhinoApp.RunScript(cmd, bEcho);
        }

        /// <summary>
        /// デバッグファイル用のフォルダ文字列を取得する（現在は、MyDocumentフォルダのQProWork） 最後の￥はない。
        /// </summary>
        /// <returns></returns>
        
        public static string GetDebugFileFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\\SKWork";
        }
        */


        /// <summary>
        /// ConstructionPlaneの名前を ComboBoxのItemsに入れる
        /// </summary>
        /// <param name="aItems"> ComboBoxのItems </param>
        public static void ConstructionPlaneNamesToComboBoxItem(RhinoDoc aDoc, ComboBox.ObjectCollection aItems)
        {
            aItems.Clear();
            foreach (var cplane in aDoc.NamedConstructionPlanes)
                aItems.Add(cplane.Name);
    }

    /// <summary>
    /// 指定した名前のCPlaneを取得する
    /// </summary>
    /// <param name="aName">CPlaneの名前</param>
    /// <returns></returns>
    /*
    public static IOn3dmConstructionPlane GetCPlaneByName(string aName)
    {
        IOn3dmConstructionPlane cp=null;
        RhinoDoc doc = RhUtil.RhinoApp().ActiveDoc();
        int i = 0;
        while (true)
        {
            cp = doc.Properties().NamedConstructionPlane(i);
            if (cp==null)
                break;
            else
            {
                if (cp.m_name == aName)
                    break;
            }
        }

        return cp;
    }
    */
    /// <summary>
    /// 2つの平面が同じかどうか判定をする（現在のところ原点、XY軸が同じかどうかで判定）
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    /*
    public static bool IsPlanesEqual(IOnPlane a, IOnPlane b)
    {
        return a.origin.Equals(b.origin) && a.xaxis.Equals(b.xaxis) && a.yaxis.Equals(b.yaxis);
    }
    */
    /// <summary>
    /// Rhinoの環境（＝オプション設定で可能）がユニバーサル作業平面になっているか
    /// </summary>
    /// <returns></returns>
    /*
    public static bool IsUniversalCPlaneEnabled()
    {
        return RhUtil.RhinoApp().AppSettings().ModelAidSettings().m_uplane_mode;
    }
    */
    /// <summary>
    /// 曲線の端点のY座標が上から下へ行くようにする
    /// </summary>
    /// <param name="aDoc"></param>
    /// <param name="aCrv"></param>
    /// <returns>右まわりに変更したときにのみtrue</returns>
    public static bool CrvTopToDownXY(RhinoDoc aDoc, ObjRef aCrv)
        {
            Debug.Assert(aCrv != null, "aCrvが指定されていません。");
            if (aCrv == null)
            {
                return false;
            }
            double[] domains = new double[2];

            Curve crv = aCrv.Curve();
            domains[0] = crv.Domain.Min;
            domains[1] = crv.Domain.Max;
            double d_len = domains[1] - domains[0];

            double[] t = new double[2];
            Point3d[] p = new Point3d[2];

            for (int i = 0; i < 2; i++)
            {
                p[i]= crv.PointAt(domains[i]);
            }
            if (p[0].Y < p[1].Y)
            {
                Curve revCrv = crv.DuplicateCurve();
                revCrv.Reverse();
                aDoc.Objects.Replace(aCrv, revCrv);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 指定されたXY上の曲線オブジェクトを右回りに変更する（閉じているかどうかのチェックはしない）
        /// </summary>
        /// <param name="aDoc"></param>
        /// <param name="aCrv"></param>
        /// <returns>右まわりに変更したときにのみtrue</returns>
        public static bool DirToRightOnXY(RhinoDoc aDoc, ObjRef aCrv)
        {
            Debug.Assert(aCrv != null, "aCrvが指定されていません。");
            if (aCrv == null)
            {
                return false;
            }
            double[] domains = new double[2];
            double d_len;

            Curve crv = aCrv.Curve();
            domains[0] = crv.Domain.Min;
            domains[1] = crv.Domain.Max;

            d_len = domains[1] - domains[0];

            double[] t = new double[3];
            t[0] = domains[0];
            t[1] = domains[0] + 0.33 * d_len;
            t[2] = domains[0] + 0.66 * d_len;
            Point3d[] p = new Point3d[3];
            for (int i = 0; i < 3; i++)
            {
                //p[i] = new Point3d(0, 0, 0);
                p[i] = crv.PointAt(t[i]);
            }

            //始点からのベクトルを生成
            Vector3d[] v = new Vector3d[2];
            for (int i = 0; i < 2; i++)
            {
                v[i] = new Vector3d(0, 0, 0);
                v[i] = p[i + 1] - p[0];
            }

            //外積
            Vector3d outerVec = Vector3d.CrossProduct(v[0], v[1]);
            if (outerVec.Z > 0)
            {
                Curve c = crv.DuplicateCurve();
                c.Reverse();
                aDoc.Objects.Replace(aCrv, c);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Closedカーブかどうか
        /// </summary>
        /// <param name="aCrv"></param>
        /// <returns>閉曲線ならtrue</returns>
        public static bool IsClosedCrv(ObjRef aCrv)
        {
            Debug.Assert(aCrv != null, "aCrvが指定されていません。");
            Curve crv = aCrv.Curve();
            return crv.IsClosed;
        }

        /// <summary>
        /// ユーザーが複数のオブジェクトを指定する。ObjRefsのリスト形式で選択を返す。
        /// </summary>
        /// <param name="aDoc">ライノのドキュメント</param>
        /// <param name="aMsg">選択するときのメッセージ</param>
        /// <returns>ユーザーが指定したオブジェクト</returns>
        public static List<ObjRef> PickObjects(RhinoDoc aDoc, string aMsg, ObjectType GeometryFilter = 0)
        {
            var ret = new List<ObjRef>();
            var go = new GetObject();
            go.SetCommandPrompt(aMsg);
            go.GeometryFilter = GeometryFilter;
            go.GetMultiple(1,0);
            go.SetCommandPrompt("");

            if (go.CommandResult() != Result.Success)
            {
                if (go.CommandResult() == Result.Cancel)
                    DebugPrint("選択されませんでした。");
                else
                    DebugPrint("選択に失敗しました。");

            }
            else
            {
                DebugPrint("選択された数=" + go.Objects().Length.ToString());
                foreach (ObjRef obj in go.Objects())
                    ret.Add(obj);
            }
            aDoc.Objects.UnselectAll();
            aDoc.Views.Redraw();
            return ret;
        }


        /// <summary>
        /// ユーザーが１つのObjectを指定する。Obj形式で選択を返す。 todo AfterUnselectはfalseにして削除するほうが良いように思う 
        /// </summary>
        /// <param name="aDoc">ライノのドキュメント</param>
        /// <param name="aMsg">選択するときのメッセージ</param>
        /// <returns>ユーザーが指定したオブジェクト</returns>
        /// 
        public static ObjRef PickOne(RhinoDoc aDoc, string aMsg, ObjectType GeometryFilter = 0, bool bAfterUnselect = true)
        {
            ObjRef ret = null;
            var go = new GetObject();
            go.SetCommandPrompt(aMsg);
            go.GeometryFilter = GeometryFilter;

            /* これらのオプションがあるが・・ （どのように提供するか？）
            go.AlreadySelectedObjectSelect = true;
            go.DeselectAllBeforePostSelect = false;
            go.EnableUnselectObjectsOnExit(false);
            go.EnableClearObjectsOnEntry(false);
            go.EnablePreSelect(true, true);
            */

            go.Get();

            if (go.CommandResult() != Result.Success)
            {
                if (go.CommandResult() == Result.Cancel)
                    DebugPrint("中止しました。");
                else
                    DebugPrint("選択に失敗しました。");
                return ret;
            }
            ret = go.Object(0);
            DebugPrint("選択=>" + ret.ObjectId.ToString());

            if ( bAfterUnselect)
            {
                aDoc.Objects.UnselectAll();
                aDoc.Views.Redraw();
            }
            return ret;
        }
        
        /// <summary>
        /// Rhinoオブジェクトの名前を指定したものに設定する
        /// </summary>
        public static void SetObjectName(RhinoDoc aDoc, ObjRef aObjRef, string aName)
        {
            RhinoObject obj= aObjRef.Object();
            if (obj != null)
                SetObjectName(aDoc, obj, aName);
        }

        public static void SetObjectName(RhinoDoc aDoc, RhinoObject aObj, string aName)
        {
            ObjectAttributes obj_attribs = aObj.Attributes;
            obj_attribs.Name = aName;
            aObj.CommitChanges();
        }

        public static void SetObjectName(RhinoDoc aDoc, Guid aGuid, string aName)
        {
            RhinoObject obj = aDoc.Objects.Find(aGuid);
            if (obj!= null)
                SetObjectName(aDoc, obj, aName);
        }

        /// <summary>
        /// リスト内のRhinoオブジェクトの名前を指定したものに設定する
        /// </summary>
        /// <param name="aDoc">ライノのドキュメント</param>
        /// <param name="aObjs">名前を変更するオブジェクトの配列</param>
        /// <param name="aName">新しい名前</param>
        public static void SetObjectsName(RhinoDoc aDoc, RhinoObject[] aObjs, string aName)
        {
            foreach (var obj in aObjs) 
            {
                //CopyObjectAttributes
                ObjectAttributes obj_attribs = obj.Attributes;
                obj_attribs.Name = aName;
                obj.CommitChanges();
            }
        }

        /// <summary>
        /// リスト内のRhinoオブジェクトの名前を指定したものに設定する
        /// </summary>
        /// <param name="aDoc">ライノのドキュメント</param>
        /// <param name="aObjs">名前を変更するオブジェクトの配列</param>
        /// <param name="aName">新しい名前</param>
        public static void SetObjectsName(RhinoDoc aDoc, ObjRef[] aObjs, string aName)
        {
            foreach (var objref in aObjs)
            {
                //CopyObjectAttributes
                var obj = objref.Object();
                ObjectAttributes obj_attribs = obj.Attributes;
                obj_attribs.Name = aName;
                obj.CommitChanges();
            }
        }

        /// <summary>
        /// 指定されたオブジェクトの名前をクリアする　複数のオブジェクトに同じ名前があっても動作する
        /// </summary>
        /// <param name="aDoc">RhinoDoc</param>
        /// <param name="aOldName">古い名前</param>
        /// <returns>変更した名前の数（該当するものがなければ０）</returns>
        public static int ClearObjectsName(RhinoDoc aDoc, string aOldName)
        {
            return ChangeObjectsName(aDoc, aOldName, "");
        }

        /// <summary>
        /// 指定されたオブジェクトの名前を変更する　複数のオブジェクトに同じ名前があっても動作する
        /// </summary>
        /// <param name="aDoc">RhinoDoc</param>
        /// <param name="aOldName">古い名前</param>
        /// <param name="aNewName">新しい名前</param>
        /// <returns>変更した名前の数（該当するものがなければ０）</returns>
        public static int ChangeObjectsName(RhinoDoc aDoc, string aOldName, string aNewName)
        {
            var oldNameObjects = GetObjectsByName(aDoc, aOldName);
            if (oldNameObjects.Count > 0)
            {
                SetObjectsName(aDoc, oldNameObjects.ToArray(), aNewName);
            }
            return oldNameObjects.Count;
        }

        /// <summary>
        /// オブジェクトに指定された名前を付けて、他に同じ名前があればクリアする。
        /// aObjRef=nullの場合はクリアのみ
        /// </summary>
        /// <param name="aDoc"></param>
        /// <param name="aObjRef"></param>
        /// <param name="aName"></param>
        public static void ClearSetObjectName(RhinoDoc aDoc, ObjRef aObjRef, string aName)
        {
            if (aObjRef != null)
            {
                ChangeObjectsName(aDoc, aName, "");
                SetObjectName(aDoc, aObjRef, aName);
            }
        }



        public static RhinoObject GetObjectByID(RhinoDoc aDoc, string aObjectID)
        {
            Guid guid;
            RhinoObject ret = null;
            if (Guid.TryParse(aObjectID, out guid))
            {
                ret = aDoc.Objects.Find(guid);
            }
            return ret;
        }
        /// <summary>
        /// Rhinoオブジェクトを名前で取得する。複数同じ名前があるときは、１つしか返さないので要注意。
        /// 大文字、小文字の区別はしない
        /// </summary>
        /// <param name="aName">名前</param>
        /// <returns>RhinoObject 名前がない時はNULLを返す</returns>
        public static RhinoObject GetObjectByName(RhinoDoc aDoc, string aName)
        {
            var objlist = GetObjectsByName(aDoc, aName);
            if (objlist.Count > 0)
                return objlist[0];
            else
                return null;
        }


        /// <summary>
        /// Rhinoオブジェクトを名前で取得する。同じ名前のオブジェクトは全て返す
        /// 大文字、小文字の区別はしない
        /// </summary>
        /// <param name="aName">名前</param>
        /// <returns>RhinoObjectのリスト (nullはありえない）</returns>
        public static List<RhinoObject> GetObjectsByName(RhinoDoc aDoc, string aName)
        {
            var ret = new List<RhinoObject>();
            var enumset = new ObjectEnumeratorSettings();

            //Enumlateするオブジェクトを設定
            enumset.NormalObjects = true;
            enumset.HiddenObjects = true;
            enumset.LockedObjects = true;

            var rhino_objects = aDoc.Objects.GetObjectList(enumset);

            //全オブジェクトを列挙する
            foreach (RhinoObject obj in rhino_objects)
            {
                if (string.IsNullOrEmpty(obj.Attributes.Name))
                    continue;

                //大文字小文字の区別なし
                if (String.Compare(obj.Attributes.Name, aName, true) == 0)
                {
                    ret.Add(obj);
                }
            }
            return ret;
        }

        public static int DeleteObjectsByName(RhinoDoc aDoc, string aName)
        {
            var objs = GetObjectsByName(aDoc, aName);
            int ret = 0;
            foreach (var obj in objs)
            {
                aDoc.Objects.Delete(obj, true);
                ret = ret + 1;
            }
            return ret;
        }

        public static string[] ObjectNamesInDocument(RhinoDoc aDoc, bool withInfo = false)
        {
            List<string> slist = new List<string>();
            foreach( var obj in aDoc.Objects)
            {
                string name = obj.Attributes.Name;
                if (String.IsNullOrEmpty(name))
                    continue;
                if (withInfo)
                {
                    string sItem;
                    string guid = obj.Id.ToString();
                    int nLayer = obj.Attributes.LayerIndex;
                    string LayerName = "";
                    foreach (var layer in aDoc.Layers)
                    {
                        //if (layer.LayerIndex == nLayer) // for Rhino5
                        if (layer.Index == nLayer) // for Rhino7
                        {
                            LayerName = layer.FullPath;
                            break;
                        }
                    }
                    sItem = name + "," + LayerName + "," + guid;
                    slist.Add(sItem);
                }
                else
                {
                    if (!slist.Contains(name))
                        slist.Add(name);
                }                   
            }
            return slist.ToArray();
        }

        // ========================= UserString ==============================================================
        /// <summary>
        /// リスト内のRhinoオブジェクトのUserStringを指定したものに設定する
        /// </summary>
        /// <param name="aDoc">ライノのドキュメント</param>
        /// <param name="aObjs">名前を変更するオブジェクトの配列</param>
        /// <param name="aKey">キー</param>
        /// <param name="aText">テキスト</param>
        public static void SetUserString(RhinoDoc aDoc, RhinoObject[] aObjs, string aKey, string aText)
        {
            foreach (var obj in aObjs)
            {
                //CopyObjectAttributes
                ObjectAttributes obj_attribs = obj.Attributes;
                obj_attribs.SetUserString(aKey, aText);
                obj.CommitChanges();
            }
        }

        public static void SetUserString(RhinoDoc aDoc, ObjRef[] aObjs, string aKey, string aText)
        {
            foreach (var objref in aObjs)
            {
                //CopyObjectAttributes
                var obj = objref.Object();
                ObjectAttributes obj_attribs = obj.Attributes;
                obj_attribs.SetUserString(aKey, aText);
                obj.CommitChanges();
            }
        }

        public static void SetUserString(RhinoDoc aDoc, RhinoObject aObj, string aKey, string aText)
        {
            var obj = aObj;
            ObjectAttributes obj_attribs = obj.Attributes;
            obj_attribs.SetUserString(aKey, aText);
            obj.CommitChanges();
        }

        public static int ChangeUserStringInDocument(RhinoDoc aDoc, string aKey, string aString, string aNewString)
        {
            var TargetObjects = GetObjectsByUserString(aDoc, aKey, aString);
            if (TargetObjects.Count > 0)
            {
                SetUserString(aDoc, TargetObjects.ToArray(), aKey, aNewString);
            }
            return TargetObjects.Count;
        }

        public static void ClearUserStringInDocument(RhinoDoc aDoc, string aKey, string aString)
        {
            ChangeUserStringInDocument(aDoc, aKey, aString, "");
        }


        public static void RemoveKeyFromAll(RhinoDoc aDoc, string aKey)
        {
            var enumset = new ObjectEnumeratorSettings();

            //Enumlateするオブジェクトを設定
            enumset.NormalObjects = true;
            enumset.HiddenObjects = true;
            enumset.LockedObjects = true;

            var rhino_objects = aDoc.Objects.GetObjectList(enumset);

            //全オブジェクトを列挙する
            foreach (RhinoObject obj in rhino_objects)
            {
                //var user_text = obj.Attributes.GetUserString(aKey);
                //if (string.IsNullOrEmpty(user_text))
                //    continue;
                //
                obj.Attributes.UserDictionary.Remove(aKey);
            }

        }

        /// <summary>
        /// RhinoオブジェクトをUserStringで取得する。
        /// 大文字、小文字の区別はしない
        /// </summary>
        /// <param name="aKey">キー</param>
        /// <param name="aText">文字列</param>
        /// <returns>RhinoObjectのリスト (nullはありえない）</returns>
        public static List<RhinoObject> GetObjectsByUserString(RhinoDoc aDoc, string aKey, string aText, bool aOne = false)
        {
            var ret = new List<RhinoObject>();
            var enumset = new ObjectEnumeratorSettings();

            //Enumlateするオブジェクトを設定
            enumset.NormalObjects = true;
            enumset.HiddenObjects = true;
            enumset.LockedObjects = true;

            var rhino_objects = aDoc.Objects.GetObjectList(enumset);

            //全オブジェクトを列挙する
            foreach (RhinoObject obj in rhino_objects)
            {
                var user_text = obj.Attributes.GetUserString(aKey);
                if (string.IsNullOrEmpty(user_text))
                    continue;

                //大文字小文字の区別なし
                if (String.Compare(user_text, aText, true) == 0)
                {
                    ret.Add(obj);
                    //1つで十分なら終了
                    if (aOne)
                        return ret;
                }
            }
            return ret;
        }

        public static RhinoObject GetObjectByUserString(RhinoDoc aDoc, string aKey, string aName)
        {
            var objlist = GetObjectsByUserString(aDoc, aKey, aName, true);
            if (objlist.Count > 0)
                return objlist[0];
            else
                return null;
        }

        /// <summary>
        /// ドキュメント内の指定のUserStringを一旦全てクリアして、指定のオブジェクトのみになるようにする
        /// 大文字、小文字の区別はしない
        /// 指定のオブジェクトがnullの時は、クリアのみとなる。
        /// </summary>
        /// <param name="aKey">キー</param>
        /// <param name="aText">文字列</param>
        /// <returns>RhinoObjectのリスト (nullはありえない）</returns>
        public static void UnifyUserString(RhinoDoc aDoc, ObjRef aObjRef, string aKey, string aText)
        {
            ClearUserStringInDocument(aDoc, aText, aKey);
            if (aObjRef != null)
            {
                SetUserString(aDoc, new ObjRef[] { aObjRef }, aKey, aText);
            }
        }

        public static void UnifyUserString(RhinoDoc aDoc, ObjRef[] aObjRefs, string aKey, string aText)
        {
            ClearUserStringInDocument(aDoc, aText, aKey);
            if (aObjRefs != null)
            {
                SetUserString(aDoc, aObjRefs, aKey, aText);
            }
        }

        public static string[] ObjectUserStringInDocument(RhinoDoc aDoc, string aKey, bool withInfo = false)
        {
            List<string> slist = new List<string>();
            foreach (var obj in aDoc.Objects)
            {
                //string name = obj.Attributes.Name;

                var user_text = obj.Attributes.GetUserString(aKey);
                if (string.IsNullOrEmpty(user_text))
                    continue;

                string name = user_text;

                if (withInfo)
                {
                    string sItem;
                    string guid = obj.Id.ToString();
                    int nLayer = obj.Attributes.LayerIndex;
                    string LayerName = "";
                    foreach (var layer in aDoc.Layers)
                    {
                        //  if (layer.LayerIndex == nLayer) // for Rhino5
                        if (layer.Index == nLayer) // for Rhino7
                        {
                            LayerName = layer.FullPath;
                            break;
                        }
                    }
                    sItem = name + "," + LayerName + "," + guid;
                    slist.Add(sItem);
                }
                else
                {
                    if (!slist.Contains(name))
                        slist.Add(name);
                }
            }
            return slist.ToArray();
        }

        public static int SelectObjects(List<RhinoObject> objects, bool SelectOn)
        {
            int ret = 0;
            foreach(var obj in objects)
            {
                if (SelectOn)
                {
                    obj.Attributes.Visible = true;
                    obj.CommitChanges();
                }
                obj.Select(SelectOn);
                ret++;
            }
            return ret;
        }

        public static void VisibleObjects(List<RhinoObject> objects, bool VisibleOn)
        {
            foreach (var obj in objects)
            {
                obj.Attributes.Visible = VisibleOn;
                obj.CommitChanges();
            }
        }
    }
}
