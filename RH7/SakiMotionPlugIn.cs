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

namespace SakiMotion
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class SakiMotionPlugIn : Rhino.PlugIns.PlugIn

    {
        //Registory Base
        static string RegBase = @"Software\SakiMotion";
        Microsoft.Win32.RegistryKey mRegKey;

        public const string TOOLPATH_LAYER = "TOOLPATH";
        public const string TOOLPATH_CALC_LAYER = "TOOLPATH_CALC";
        public const string TOOLPATH_INFO_LAYER = "TOOLPATH_INFO";

        public SakiMotionPlugIn()
        {
            Instance = this;
        }

        ///<summary>Gets the only instance of the ProfileCut7PlugIn plug-in.</summary>
        public static SakiMotionPlugIn Instance
        {
            get; private set;
        }

        protected override Rhino.PlugIns.LoadReturnCode OnLoad(ref string errorMessage)
        {
            System.Type panelType = typeof(ProfileCutPanel);
            Rhino.UI.Panels.RegisterPanel(this, panelType, "SakiMotionPanel", System.Drawing.SystemIcons.Question,0);

            return Rhino.PlugIns.LoadReturnCode.Success;
        }

        public ProfileCutPanel MainUIPanel
        {
            get;
            set;
        }

        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and maintain plug-in wide options in a document.
        public RhinoDoc Doc
        {
            get
            {
                return RhinoDoc.ActiveDoc;
            }
        }

        public Microsoft.Win32.RegistryKey RegKey()
        {
            if (mRegKey == null)
                mRegKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(RegBase);
            return mRegKey;
        }
        public static string AssemblyFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public static string GetScriptFolder()
        {
            string p0 = AssemblyFolder;
            SKUtil.DebugPrint("AssemblyFolder = "+p0);

            string p1 = System.IO.Directory.GetParent(p0).ToString();
            SKUtil.DebugPrint("ParentFolder = " + p1);

            string ret = p1 + @"\scripts";

            SKUtil.DebugPrint("ScriptFolder = " + ret);

            return ret;
            //string p2 = System.IO.Directory.GetParent(p1).ToString();
            //return p2 + @"\scripts";
        }

        public static bool RunScript(string aScriptName, string aParam = null, bool bEcho = true)
        {
            string run_name = GetScriptFolder() + @"\" + aScriptName;
            string cmd = @"-RunPythonScript """ + run_name + @"""";
            if (aParam != null)
            {
                cmd = cmd + " " + aParam;
            }
            return RhinoApp.RunScript(cmd, bEcho);
        }

        public int GetLayerIndex(string layer_name)
        {
            //string layer_name = "TOOLPATH";
            int layer_index = Doc.Layers.Find(layer_name, true);  // TODO:Rhino5 -> Rhino7では、非推奨になっている。
            if (layer_index < 0)
            {
                layer_index = Doc.Layers.Add(layer_name, System.Drawing.Color.Black);
            }
            return layer_index;
        }

        public void SetObjectLayerAndColor(RhinoObject obj, int LayerIndex, Color col, ObjectColorSource cs = ObjectColorSource.ColorFromObject)
        {
            obj.Attributes.ColorSource = cs;
            obj.Attributes.ObjectColor = col;
            obj.Attributes.LayerIndex = LayerIndex;
            obj.CommitChanges();
        }

        public void SetObjectLayer(RhinoObject obj, int LayerIndex)
        {
            obj.Attributes.LayerIndex = LayerIndex;
            obj.CommitChanges();
        }

        public void SetObjectLayer(RhinoObject obj, string LayerName)
        {
            int layer_index = GetLayerIndex(LayerName);
            SetObjectLayer(obj, layer_index);
        }

        public void SetObjectLayerAndColor(RhinoObject obj, string LayerName, Color col)
        {
            int layer_index = GetLayerIndex(LayerName);
            SetObjectLayerAndColor(obj, layer_index, col);
        }


        public void SetObjectToToolpathLayer(RhinoObject obj, Color col)
        {
            int layer_index = GetLayerIndex("TOOLPATH");
            SetObjectLayerAndColor(obj, layer_index, col);
        }

        public void SetObjectToToolpathLayer(RhinoObject obj)
        {
            int layer_index = GetLayerIndex("TOOLPATH");
            SetObjectLayer(obj, layer_index);
        }

        public ToolData CurrentTool()
        {
            var rk = RegKey();
            var ToolName = (string)rk.GetValue("CurrentTool");
            if (string.IsNullOrEmpty(ToolName))
            {
                SKUtil.DebugPrint("Error CurrentTool == null ");
                return null;
            }

            return ToolData.LoadByName(ToolName);
        }

        public void SetCurrentTool(string aName)
        {
            var rk = RegKey();
            rk.SetValue("CurrentTool", aName);
        }

        public PathData CurrentPath()
        {
            var rk = RegKey();
            var PathName = (string)rk.GetValue("CurrentPath");
            if (string.IsNullOrEmpty(PathName))
            {
                SKUtil.DebugPrint("Error CurrentPath == null ");
                return null;
            }

            return PathData.LoadByName(PathName);
        }

        public void SetCurrentPath(string aName)
        {
            var rk = RegKey();
            rk.SetValue("CurrentPath", aName);
        }

        public PostData CurrentPost()
        {
            var rk = RegKey();
            var PostName = (string)rk.GetValue("CurrentPost");
            if (string.IsNullOrEmpty(PostName))
            {
                SKUtil.DebugPrint("Error CurrentPost == null ");
                return null;
            }

            return PostData.LoadByName(PostName);
        }

        public void SetCurrentPost(string aName)
        {
            var rk = RegKey();
            rk.SetValue("CurrentPost", aName);
        }

        /*
        public void ConvertPrecision(string CurveName)
        {
            // 名前でまとめて変換する
            var path = CurrentPath();
            var prec = path.PathPrec;
            string param = string.Format("{0} {1:0.000}", CurveName, prec);
            ProfileCutPlugIn.RunScript("SKConvert.py", param, false);
        }
        */

        public void ConvertPrecision2()
        {
            // 専用化して一度ですべての変換ができるように 
            // TODO 機械コードクラスへ移動
            var path = CurrentPath();
            if (path != null)
            {
                var prefix = ProfileCutCommand.Internal_Name_Prefix;
                var prec = path.PathPrec;
                string param = string.Format("{0} {1:0.000}", prefix, prec);
                SakiMotionPlugIn.RunScript("SKProfileCutConvert.py", param, true);
            }
        }

        public void ConvertPrecisionAll()
        {
            var Prefix = ProfileCutCommand.Internal_Name_Prefix;
            ConvertPrecision2();
            /*
            ConvertPrecision(Prefix + "_MAIN");
            ConvertPrecision(Prefix + "_Z_PATH");
            ConvertPrecision(Prefix + "_R_PATH");
            //_IN部があれば選択して変換する
            ConvertPrecision(Prefix + "_IN");
            //_OUT部を選択して変換する
            ConvertPrecision(Prefix + "_OUT");
            */
            var NewName = ProfileCutCommand.Final_Name_Prefix;
            SKUtil.ChangeObjectsName(Doc, Prefix + "_MAIN", NewName + "_MAIN");
            SKUtil.ChangeObjectsName(Doc, Prefix + "_Z_PATH", NewName + "_Z_PATH");
            SKUtil.ChangeObjectsName(Doc, Prefix + "_R_PATH", NewName + "_R_PATH");
            SKUtil.ChangeObjectsName(Doc, Prefix + "_IN", NewName + "_IN");
            SKUtil.ChangeObjectsName(Doc, Prefix + "_OUT", NewName + "_OUT");
        }

        public Type MachineCodeType()
        {
            return typeof(SKGCodeModal);
            // return typeof(SKRML1Modal);
        }


        public void DeleteDataObject()
        {
            SakiMotionPlugIn.RunScript("SKProfileCutDelete.py");
        }
    }
}