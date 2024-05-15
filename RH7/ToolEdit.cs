using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProfileCut7
{

    public partial class ToolEdit : Form
    {

        public ToolEdit()
        {
            InitializeComponent();
        }

        public double TxtToValue(TextBox tbox)
        {
            double val;
            if (double.TryParse(tbox.Text, out val))
                return val;
            else
            {
                tbox.Focus();
                throw new Exception("値を修正してください。");
            }
        }

        public void ValueToTool(Tool tool)
        {
            try
            {
                tool.Dia = TxtToValue(TxtDia);
                tool.ZPos = TxtToValue(TxtZPos);
                tool.Feed = TxtToValue(TxtFeed);
                tool.Spin = TxtToValue(TxtSpin);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public void ValueFromTool(Tool tool)
        {
            TxtDia.Text = string.Format("{0:0.000}", tool.Dia);
            TxtZPos.Text = string.Format("{0:0.000}", tool.ZPos);
            TxtFeed.Text = string.Format("{0:0}", tool.Feed);
            TxtSpin.Text = string.Format("{0:0}", tool.Spin);
        }


    }

    public class Tool : Object
    {
        public double Dia;
        public double ZPos;
        public double Feed;
        public double Spin;

        public Tool()
        {
            Dia = 0.0;
            ZPos = 0.0;
            Feed = 0.0;
            Spin = 0.0;
        }

        public override string ToString()
        {
            return string.Format("D{0:0.000} Z{1:0.000} S{2:0} F{3:0}", Dia, ZPos, Spin, Feed);
        }

        public void ReadFromReg()
        {
            var rk = ProfileCut7PlugIn.Instance.RegKey();
            try
            {
                Dia = double.Parse(rk.GetValue("Dia").ToString());
                ZPos = double.Parse(rk.GetValue("ZPos").ToString());
                Feed = double.Parse(rk.GetValue("Feed").ToString());
                Spin = double.Parse(rk.GetValue("Spin").ToString());
            }
            catch( Exception e)
            {
                MessageBox.Show("Error Exception in ToolEdit.cs ReadFromReg():" + e.Message);
            }
        }


        public void SaveToReg()
        {
            var rk = ProfileCut7PlugIn.Instance.RegKey();
            rk.SetValue("Dia", Dia.ToString());
            rk.SetValue("ZPos", ZPos.ToString());
            rk.SetValue("Feed", Feed.ToString());
            rk.SetValue("Spin", Spin.ToString());
        }

        //工具設定 (旧バージョン)
        /*
        ToolEdit toolEdit = new ToolEdit();
        toolEdit.ValueFromTool(tool);
        if (toolEdit.ShowDialog() == DialogResult.OK)
        {
            //工具を設定する
            toolEdit.ValueToTool(tool);
            LblToolInfo.Text = tool.ToString();
            //ファイルに保存する
            tool.SaveToReg();
        }
        */


    }
}
