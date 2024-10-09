using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SakiMotion
{
    public partial class UserStringForm : Form
    {
        public UserStringForm()
        {
            InitializeComponent();
        }

        public void AddLine(string s)
        {
            TextBox.AppendText(s + Environment.NewLine);
        }
    }
}
