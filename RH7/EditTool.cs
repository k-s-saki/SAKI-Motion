using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SakiMotion
{
    public partial class EditTool : SakiMotion.EditBaseForm
    {
        ToolDataList DataList;

        public EditTool()
        {
            InitializeComponent();
            //DataList = new ToolDataList();
        }


        public string D3(double val)
        {
            return string.Format("{0:0.000}", val);
        }
        public void DataToForm(ToolData data)
        {
            CbxName.Text = data.Name;
            TxtDia.Text = D3(data.Dia);
            TxtZPos.Text = D3(data.ZPos);
            TxtSpin.Text = D3(data.Spin);
            TxtFeed.Text = D3(data.Feed);
            TxtAprZFeed.Text = D3(data.AprZFeed);
            TxtAprXYFeed.Text = D3(data.AprXYFeed);
            TxtToolNo.Text = data.ToolNo;
            TxtFixD.Text = data.FixD;
            TxtFixH.Text = data.FixH;
            TxtStartCode.Text = data.StartCode;
            TxtStopCode.Text = data.StopCode;
        }

        public double toVal(string s)
        {
            return double.Parse(s);
        }

        public bool FormToData(ToolData data)
        {
            try
            {
                data.Name = CbxName.Text;
                data.Dia =  toVal(TxtDia.Text);
                data.ZPos = toVal(TxtZPos.Text);
                data.Spin = toVal(TxtSpin.Text);
                data.Feed = toVal(TxtFeed.Text);
                data.AprZFeed = toVal(TxtAprZFeed.Text);
                data.AprXYFeed = toVal(TxtAprXYFeed.Text);

                data.ToolNo = TxtToolNo.Text;
                data.FixD = TxtFixD.Text;
                data.FixH = TxtFixH.Text;
                data.StartCode = TxtStartCode.Text;
                data.StopCode = TxtStopCode.Text;

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
            var ToolData = DataList.GetDataByName(aName);
            if (ToolData != null)
            {
                DataToForm(ToolData);
            }
        }

        public override void EditDataToDataList(bool aIsNew)
        {
            // dummy data check
            ToolData data = new ToolData();
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
            ToolData data = GetDataInList(name);
            if (data != null)
            {
                DataList.Items.Remove(data);
                MessageBox.Show("削除しました:" + name);
                //次のデータへ
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
            DataList = ToolDataList.Load();
        }

        public ToolData GetDataInList(string aName)
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

        private void PnlEditor_Paint(object sender, PaintEventArgs e)
        {

        }
    }


    public class ToolData
    {
        public string Name;

        public double Dia;
        public double ZPos;
        public double Spin;
        public double Feed;
        public double AprZFeed;
        public double AprXYFeed;

        public string ToolNo;
        public string FixD;
        public string FixH;
        public string StartCode;
        public string StopCode;
        // 
        public static ToolData LoadByName(string aName)
        {
            ToolDataList DataList = ToolDataList.Load();
            return DataList.GetDataByName(aName);
        }
    }

    public class ToolDataList
    {
        public List<ToolData> Items = new List<ToolData>();

        public ToolData GetDataByName(string aName)
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
            return DataStore.DataFolder() + @"\EditTool.dat";
        }

        public void Save()
        {
            System.Xml.Serialization.XmlSerializer serializer = new
                System.Xml.Serialization.XmlSerializer(typeof(ToolDataList));

            System.IO.FileStream fs = new System.IO.FileStream(FileName(), System.IO.FileMode.Create);
            serializer.Serialize(fs, this);
            fs.Close();
        }

        public static ToolDataList Load()
        {
            System.Xml.Serialization.XmlSerializer serializer = new
                System.Xml.Serialization.XmlSerializer(typeof(ToolDataList));

            ToolDataList DataList = null;
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(FileName(), System.IO.FileMode.Open);
                DataList = (ToolDataList)serializer.Deserialize(fs);
                fs.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in ToolDataList.Load: " + e.Message);
            }

            if (DataList == null)
            {
                DataList = new ToolDataList();
            }

            return DataList;

        }

    }
}
