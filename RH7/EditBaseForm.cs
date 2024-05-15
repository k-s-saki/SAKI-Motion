using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Collections;

namespace ProfileCut7
{
    public partial class EditBaseForm : Form
    {

        public enum FormState { None, Browse, Edit }
        //public int BrowseStartIndex = 0;
        public string BrowseStartName = "";

        private string mSelectedName;

        private FormState mState;
        public FormState State
        {
            get { return mState; }
            set
            {
                //if (value != mState)
                //{
                mState = value;
                if (mState == FormState.Browse)
                {
                    //PnlEditor.Enabled = false;
                    SetPanelReadonly(PnlEditor,true);
                    PnlButton.Enabled = true;
                    //CbxName.Enabled = true;
                    BtnEditStart.Visible = true;
                    BtnDelFromList.Visible = true;
                    BtnEditCancel.Visible = false;
                    BtnEditFinish.Visible = false;
                    CbxName.DropDownStyle = ComboBoxStyle.DropDownList;
                    UpdateNames();
                    for(int i=0; i<CbxName.Items.Count; i++)
                    {
                        if (CbxName.Items[i].ToString() == BrowseStartName)
                        {
                            CbxName.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else if (mState == FormState.Edit)
                {
                    //PnlEditor.Enabled = true;
                    SetPanelReadonly(PnlEditor, false);
                    PnlButton.Enabled = false;
                    //CbxName.Enabled = false;
                    BtnEditStart.Visible = false;
                    BtnDelFromList.Visible = false;
                    BtnEditCancel.Visible = true;
                    BtnEditFinish.Visible = true;
                    CbxName.DropDownStyle = ComboBoxStyle.DropDown;
                }
                //}
            }
        }
        public string PureError = "Pure virtual method call.";

        public EditBaseForm()
        {
            InitializeComponent();
        }

        //子のコントロールを再帰的に取得する
        public Control[] EnumControlsIn(Control top)
        {
            ArrayList buf = new ArrayList();
            foreach (Control c in top.Controls)
            {
                buf.Add(c);
                buf.AddRange(EnumControlsIn(c));
            }
            return (Control[])buf.ToArray(typeof(Control));
        }

        public void SetPanelReadonly(Panel pnl, bool isReadonly)
        {
            Control[] c_list = EnumControlsIn(pnl);
            foreach(var c in c_list)
            {
                if (c is TextBox)
                {
                    (c as TextBox).ReadOnly = isReadonly;
                }
                if (c is RadioButton)
                {
                    (c as RadioButton).Enabled = ! isReadonly;
                }
                if (c is Button)
                {
                    (c as Button).Enabled = !isReadonly;
                }
            }
        }



        public void InitialCheck()
        {
            /*
            if (!Directory.Exists( DataFolder() ))
            {
                //初期フォルダを生成する
                Directory.CreateDirectory( DataFolder() );
            }

            //ファイルが存在すれば新しく読み込む
            if (File.Exists( FileName() ))
            {
                //LoadDataList();
            }
            */
            //CheckFile();
            LoadDataList();
            State = FormState.Browse;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            //現在選択されている名前
            mSelectedName = CbxName.Text;
            if (CheckFile())
            {
                SaveDataList();
            }

        }

        public string GetSelectedName()
        {
            return mSelectedName;
        }

        public virtual void LoadDataList()
        {
            //シリアライズについては下記を参照
            //http://gurizuri0505.halfmoon.jp/develop/csharp/xmlserialize
            // aType = typeof(SelfClass)
            throw new Exception(PureError + "[" + this.Name + "]" + MethodBase.GetCurrentMethod().Name);
        }


        public virtual void SaveDataList()
        {
            throw new Exception(PureError + "[" + this.Name + "]" + MethodBase.GetCurrentMethod().Name);
        }


        private bool CheckFile()
        {
            /*
            if (!Directory.Exists(DataFolder() ))
            {
                //フォルダエラー
                MessageBox.Show("保存フォルダーが存在しません:" + DataFolder() );
                return false;
            }
            */
            return true;
        }

        public virtual List<string> EnumNames()
        {
            throw new Exception(PureError + "[" + this.Name + "]" + MethodBase.GetCurrentMethod().Name);
        }

        private void CbxName_DropDown(object sender, EventArgs e)
        {
        }

        public virtual void UpdateNames()
        {
            CbxName.Items.Clear();
            foreach (var name in EnumNames())
            {
                CbxName.Items.Add(name);
            }
        }

        public virtual bool IsExistNameInList(string aName)
        {
            throw new Exception(PureError + "[" + this.Name + "]" + MethodBase.GetCurrentMethod().Name);

        }


        private void BtnEditStart_Click(object sender, EventArgs e)
        {
            EditStart();           
        }

        private void BtnEditFinish_Click(object sender, EventArgs e)
        {
            EditFinish();
        }

        private void BtnEditCancel_Click(object sender, EventArgs e)
        {
            BrowseStartName = CbxName.Text;
            State = FormState.Browse;
        }

        private void BtnDelFromList_Click(object sender, EventArgs e)
        {
            DeleteCurrent();
            SaveDataList();
        }

        public virtual void DeleteCurrent()
        {
            throw new Exception(PureError + "[" + this.Name + "]" + MethodBase.GetCurrentMethod().Name);
        }

        public virtual void EditStart()
        {
            State = FormState.Edit;
        }

        public virtual void EditFinish()
        {
            bool isNew = true;
            string Name = CbxName.Text;
            if (string.IsNullOrEmpty(Name))
            {
                MessageBox.Show("名前を入力してください");
                CbxName.Focus();
                return;
            }

            //すでに名前が存在しているか？
            if (IsExistNameInList(Name))
            {
                DialogResult dr = MessageBox.Show("同じ名前が存在しています。上書きしますか？",
                      "確認", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    CbxName.Focus();
                    return;
                }
                else
                    isNew = false;
            }

            EditDataToDataList(isNew);
            SaveDataList();

            State = FormState.Browse;
        }

        public virtual void EditDataToDataList(bool aIsNew)
        {
            throw new Exception(PureError + "[" + this.Name + "]" + MethodBase.GetCurrentMethod().Name);
            /*
            var data = DataList.NewData();
            if (FormToData(data) == true)
            {
                DataList.Items.Add(data);
            }
            */
        }

        private void CbxName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //選択が変更された
            //
            string Name = CbxName.Text;
            //名前でデータを取得する
            EditDataByName(Name);
        }

        public virtual void EditDataByName(string aName)
        {
            throw new Exception(PureError + "[" + this.Name + "]" + MethodBase.GetCurrentMethod().Name);
            /*
            var PathData = DataList.GetDataByName(aName);

            if (PathData != null)
            {
                DataToForm(aPathData);
            }
            */
        }

        private void EditBaseForm_Load(object sender, EventArgs e)
        {
        }

        private void EditBaseForm_Shown(object sender, EventArgs e)
        {
            if (DesignMode) return;
            InitialCheck();
        }

    }

    public class DataStore
    {
        static public string DataFolder()
        {
            // 現在実行しているアセンブリ(.exeのアセンブリ)を取得する
            var assm = Assembly.GetExecutingAssembly();

            // AssemblyNameを取得する
            var AsmName = assm.GetName();

            // アセンブリ名のみを取得
            //var assemblyName = System.IO.Path.GetFileName(fullAssemblyNmae);
            var s= System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\"+ AsmName.Name;
            return s;
        }
    }
}
