using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProfileCut7
{
    public partial class EditPath : EditBaseForm
    {
        PathDataList DataList;


        public EditPath()
        {
            InitializeComponent();
            //DataList = new PathDataList();
        }


        public string D3(double val)
        {
            return string.Format("{0:0.000}", val);
        }
        public void DataToForm(PathData data)
        {
            CbxName.Text = data.Name;
            TxtPathPrec.Text = D3(data.PathPrec);
            TxtOffsetStart.Text = D3(data.OffsetStart);
            TxtOffsetStep.Text = D3(data.OffsetStep);
            TxtOffsetEnd.Text = D3(data.OffsetEnd);
            TxtRapidFeed.Text = D3(data.RapidFeed);

            TxtZRapid.Text = D3(data.ZRapid);
            TxtZStart.Text = D3(data.ZStart);
            TxtZStep.Text = D3(data.ZStep);
            TxtZEnd.Text = D3(data.ZEnd);
            TxtZApr.Text = D3(data.ZApr);

            rbDirectCurve.Checked = (data.Method == ToolPathMethod.DirectCurve);
            rbOffset.Checked = (data.Method == ToolPathMethod.Offset);
            rbOffsetAddCurve.Checked = (data.Method == ToolPathMethod.OffsetAddCurve);

        }

        public double toVal(string s)
        {
            return double.Parse(s);
        }

        public bool FormToData(PathData data)
        {
            try
            {
                data.Name = CbxName.Text;
                data.PathPrec = toVal(TxtPathPrec.Text);
                data.OffsetStart = toVal(TxtOffsetStart.Text);
                data.OffsetStep = toVal(TxtOffsetStep.Text);
                data.OffsetEnd = toVal(TxtOffsetEnd.Text);
                data.ZRapid = toVal(TxtZRapid.Text);
                data.ZStart = toVal(TxtZStart.Text);
                data.ZStep = toVal(TxtZStep.Text);
                data.ZApr = toVal(TxtZApr.Text);
                data.ZEnd = toVal(TxtZEnd.Text);
                data.RapidFeed = toVal(TxtRapidFeed.Text);

                if (rbDirectCurve.Checked)
                    data.Method = ToolPathMethod.DirectCurve;
                if (rbOffset.Checked)
                    data.Method = ToolPathMethod.Offset;
                if (rbOffsetAddCurve.Checked)
                    data.Method = ToolPathMethod.OffsetAddCurve;

                if (data.ZStep < 0)
                {
                    MessageBox.Show("Z加工ステップはゼロ以上で入力してください。");
                    TxtZStep.Focus();
                    return false;
                }
                if (data.ZApr < 0)
                {
                    MessageBox.Show("Zアプローチはゼロ以上で入力してください。");
                    TxtOffsetStep.Focus();
                    return false;
                }
                if (data.ZStart < data.ZEnd)
                {
                    MessageBox.Show("開始Zは終了Z以上で入力してください。");
                    TxtZStart.Focus();
                    return false;
                }
                if (data.OffsetStep < 0)
                {
                    MessageBox.Show("外形ステップはゼロ以上で入力してください。");
                    TxtOffsetStep.Focus();
                    return false;
                }
                if (data.OffsetStart < data.OffsetEnd)
                {
                    MessageBox.Show("外形開始は外形終了以上で入力してください。");
                    TxtOffsetStart.Focus();
                    return false;
                }

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
            var PathData = DataList.GetDataByName(aName);
            if (PathData != null)
            {
                DataToForm(PathData);
            }
        }

        public override void EditDataToDataList(bool aIsNew)
        {
            // dummy data check
            PathData data = new PathData();
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
            PathData data = GetDataInList(name);
            if (data != null)
            {
                DataList.Items.Remove(data);
                MessageBox.Show("削除しました:" + name);
                //次のデータへ
                BrowseStartName = name;
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

            DataList = PathDataList.Load();
        }

        public PathData GetDataInList(string aName)
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
    }


    public class AddCurve
    {
        public double R;
        public double Angle;
        public double ZP;
    }


    public enum ToolPathMethod { DirectCurve=0, Offset=2, OffsetAddCurve=3 };

    public class PathData
    {
        /*
        public PathData()
        {
            StartCurve = new AddCurve();
            EndCurve = new AddCurve();
        }
        */

        public string Name;
        public double PathPrec;
        public double OffsetStart;
        public double OffsetStep;
        public double OffsetEnd;

        // ZCut
        public double ZRapid;
        public double ZStart;
        public double ZStep;
        public double ZEnd;
        public double ZApr;

        public double RapidFeed;

        public ToolPathMethod Method;


        // 
        public static PathData LoadByName(string aName)
        {
            PathDataList DataList = PathDataList.Load();
            return DataList.GetDataByName(aName);
        }
    }

    public class PathDataList
    {
        public List<PathData> Items = new List<PathData>();

        public PathData GetDataByName(string aName)
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
            return DataStore.DataFolder() + @"\EditPath.dat";
        }

        public void Save()
        {
            System.Xml.Serialization.XmlSerializer serializer = new
                System.Xml.Serialization.XmlSerializer(typeof(PathDataList));

            System.IO.FileStream fs = new System.IO.FileStream(FileName(), System.IO.FileMode.Create);
            serializer.Serialize(fs, this);
            fs.Close();
        }

        public static PathDataList Load()
        {
            System.Xml.Serialization.XmlSerializer serializer = new
                System.Xml.Serialization.XmlSerializer(typeof(PathDataList));

            PathDataList DataList=null;
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(FileName(), System.IO.FileMode.Open);
                DataList = (PathDataList)serializer.Deserialize(fs);
                fs.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in PathDataList.Load: " + e.Message);
            }

            if (DataList == null)
            {
                DataList = new PathDataList();
            }

            return DataList;

        }

    }
}
