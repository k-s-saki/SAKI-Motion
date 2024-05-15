using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProfileCut7
{
    public partial class EditPost : ProfileCut7.EditBaseForm
    {
        PostDataList DataList;


        public EditPost()
        {
            InitializeComponent();
        }


        public string D3(double val)
        {
            return string.Format("{0:0.000}", val);
        }
        public void DataToForm(PostData data)
        {
            CbxName.Text = data.Name;
            TxtCodeType.Text = data.CodeType;
            TxtStartCode.Text = data.StartCode;
            TxtEndCode.Text = data.EndCode;
            TxtSaveFileName.Text = data.SaveFileName;
            TxtExecFileName.Text = data.ExecFileName;
        }

        public double toVal(string s)
        {
            return double.Parse(s);
        }

        public bool FormToData(PostData data)
        {
            try
            {
                data.Name = CbxName.Text;
                data.CodeType = TxtCodeType.Text;
                data.StartCode = TxtStartCode.Text;
                data.EndCode = TxtEndCode.Text;
                data.SaveFileName = TxtSaveFileName.Text;
                data.ExecFileName = TxtExecFileName.Text;
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("入力値の変換エラー:" + e.Message);
                return false;
            }
        }

        public override void EditDataByName(string aName)
        {
            var PostData = DataList.GetDataByName(aName);
            if (PostData != null)
            {
                DataToForm(PostData);
            }
        }

        public override void EditDataToDataList(bool aIsNew)
        {
            // dummy data check
            PostData data = new PostData();
            try 
            {
                FormToData(data);
                if (aIsNew)
                {
                    DataList.Items.Add(data);
                }
                else
                {
                    data = GetDataInList(CbxName.Text);
                    FormToData(data);
                }
                BrowseStartName = data.Name;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public override void DeleteCurrent()
        {
            var name = CbxName.Text;
            int index = CbxName.SelectedIndex;
            PostData data = GetDataInList(name);
            if (data != null)
            {
                DataList.Items.Remove(data);
                MessageBox.Show("削除しました:" + name);
                BrowseStartName = "";
                State = FormState.Browse;
            }
            else
                MessageBox.Show("データが見つかりません");
        }

        public override List<string> EnumNames()
        {
            var slist = new List<string>();
            foreach (var item in DataList.Items)
            {
                slist.Add(item.Name);
            }
            return slist;
        }

        public override void SaveDataList()
        {
            DataList.Save();
        }

        public override void LoadDataList()
        {
            DataList = PostDataList.Load();
        }

        public PostData GetDataInList(string aName)
        {
            foreach (var item in DataList.Items)
            {
                if (item.Name == aName)
                {
                    return item;
                }
            }
            return null;
        }

        public override bool IsExistNameInList(string aName)
        {
            foreach (var item in DataList.Items)
            {
                if (item.Name == aName)
                {
                    return true;
                }
            }
            return false;
        }

        private void TxtPathPrec_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnSaveFileName_Click(object sender, EventArgs e)
        {
            //
            //SaveFileDialogクラスのインスタンスを作成
            SaveFileDialog sfd = new SaveFileDialog();

            //はじめのファイル名を指定する
            sfd.FileName = "path.txt";
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
                TxtSaveFileName.Text = sfd.FileName;
            }
        }

        private void BtnExecFileName_Click(object sender, EventArgs e)
        {
            //SaveFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //はじめのファイル名を指定する
            ofd.FileName = "";
            // デスクトップを指定
            ofd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            //[ファイルの種類]に表示される選択肢を指定する
            ofd.Filter = "exe ファイル(*.exe)|*.exe|すべてのファイル(*.*)|*.*";
            //[ファイルの種類]ではじめに選択されるものを指定する
            ofd.FilterIndex = 1;
            //タイトルを設定する
            ofd.Title = "実行するファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;
            //既に存在するファイル名を指定したとき警告する
            //sfd.OverwritePrompt = true;
            //存在しないパスが指定されたとき警告を表示する
            ofd.CheckPathExists = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                TxtExecFileName.Text = ofd.FileName;
            }

        }
    }


    public class PostData
    {
        public string Name="";
        public string StartCode="";
        public string EndCode="";
        public string SaveFileName="";
        public string CodeType = "GCODE";

        public static PostData LoadByName(string aName)
        {
            PostDataList DataList = PostDataList.Load();
            return DataList.GetDataByName(aName);
        }


        // フォームに追加していない
        public string ExecFileName ="";
    }

    public class PostDataList
    {
        public List<PostData> Items = new List<PostData>();

        public PostData GetDataByName(string aName)
        {
            foreach (var item in Items)
            {
                if (item.Name == aName)
                {
                    return item;
                }
            }
            return null;
        }

        static public string FileName()
        {
            return DataStore.DataFolder() + @"\EditPost.dat";
        }

        public void Save()
        {
            System.Xml.Serialization.XmlSerializer serializer = new
                System.Xml.Serialization.XmlSerializer(typeof(PostDataList));

            System.IO.FileStream fs = new System.IO.FileStream(FileName(), System.IO.FileMode.Create);
            serializer.Serialize(fs, this);
            fs.Close();
        }

        public static PostDataList Load()
        {
            System.Xml.Serialization.XmlSerializer serializer = new
                System.Xml.Serialization.XmlSerializer(typeof(PostDataList));

            PostDataList DataList=null;
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(FileName(), System.IO.FileMode.Open);
                DataList = (PostDataList)serializer.Deserialize(fs);
                fs.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in PostDataList.Load: " + e.Message);
            }

            if (DataList == null)
            {
                DataList = new PostDataList();
            }

            return DataList;

        }

    }
}
