namespace SakiMotion
{
    partial class EditTool
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.TxtFeed = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtSpin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtZPos = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtDia = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtAprZFeed = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtStopCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtStartCode = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtFixD = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtToolNo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TxtFixH = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtAprXYFeed = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.PnlEditor.SuspendLayout();
            this.PnlOperate.SuspendLayout();
            this.PnlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlEditor
            // 
            this.PnlEditor.Controls.Add(this.TxtAprXYFeed);
            this.PnlEditor.Controls.Add(this.label11);
            this.PnlEditor.Controls.Add(this.TxtFixH);
            this.PnlEditor.Controls.Add(this.label6);
            this.PnlEditor.Controls.Add(this.TxtStopCode);
            this.PnlEditor.Controls.Add(this.label7);
            this.PnlEditor.Controls.Add(this.TxtStartCode);
            this.PnlEditor.Controls.Add(this.label8);
            this.PnlEditor.Controls.Add(this.TxtFixD);
            this.PnlEditor.Controls.Add(this.label9);
            this.PnlEditor.Controls.Add(this.TxtToolNo);
            this.PnlEditor.Controls.Add(this.label10);
            this.PnlEditor.Controls.Add(this.TxtAprZFeed);
            this.PnlEditor.Controls.Add(this.label5);
            this.PnlEditor.Controls.Add(this.TxtFeed);
            this.PnlEditor.Controls.Add(this.label4);
            this.PnlEditor.Controls.Add(this.TxtSpin);
            this.PnlEditor.Controls.Add(this.label3);
            this.PnlEditor.Controls.Add(this.TxtZPos);
            this.PnlEditor.Controls.Add(this.label2);
            this.PnlEditor.Controls.Add(this.TxtDia);
            this.PnlEditor.Controls.Add(this.label1);
            this.PnlEditor.Size = new System.Drawing.Size(741, 270);
            this.PnlEditor.TabIndex = 0;
            this.PnlEditor.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlEditor_Paint);
            // 
            // PnlButton
            // 
            this.PnlButton.Location = new System.Drawing.Point(12, 355);
            // 
            // TxtFeed
            // 
            this.TxtFeed.Location = new System.Drawing.Point(150, 150);
            this.TxtFeed.Name = "TxtFeed";
            this.TxtFeed.Size = new System.Drawing.Size(100, 22);
            this.TxtFeed.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 15);
            this.label4.TabIndex = 16;
            this.label4.Text = "加工送り速度";
            // 
            // TxtSpin
            // 
            this.TxtSpin.Location = new System.Drawing.Point(150, 113);
            this.TxtSpin.Name = "TxtSpin";
            this.TxtSpin.Size = new System.Drawing.Size(100, 22);
            this.TxtSpin.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "加工回転数";
            // 
            // TxtZPos
            // 
            this.TxtZPos.Location = new System.Drawing.Point(150, 76);
            this.TxtZPos.Name = "TxtZPos";
            this.TxtZPos.Size = new System.Drawing.Size(100, 22);
            this.TxtZPos.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "工具加工位置";
            // 
            // TxtDia
            // 
            this.TxtDia.Location = new System.Drawing.Point(150, 39);
            this.TxtDia.Name = "TxtDia";
            this.TxtDia.Size = new System.Drawing.Size(100, 22);
            this.TxtDia.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "工具直径";
            // 
            // TxtAprZFeed
            // 
            this.TxtAprZFeed.Location = new System.Drawing.Point(150, 187);
            this.TxtAprZFeed.Name = "TxtAprZFeed";
            this.TxtAprZFeed.Size = new System.Drawing.Size(100, 22);
            this.TxtAprZFeed.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 15);
            this.label5.TabIndex = 18;
            this.label5.Text = "Zアプローチ速度";
            // 
            // TxtStopCode
            // 
            this.TxtStopCode.Location = new System.Drawing.Point(448, 187);
            this.TxtStopCode.Name = "TxtStopCode";
            this.TxtStopCode.Size = new System.Drawing.Size(258, 22);
            this.TxtStopCode.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(329, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 15);
            this.label7.TabIndex = 26;
            this.label7.Text = "工具終了コード";
            // 
            // TxtStartCode
            // 
            this.TxtStartCode.Location = new System.Drawing.Point(448, 150);
            this.TxtStartCode.Name = "TxtStartCode";
            this.TxtStartCode.Size = new System.Drawing.Size(258, 22);
            this.TxtStartCode.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(329, 154);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 15);
            this.label8.TabIndex = 24;
            this.label8.Text = "工具開始コード";
            // 
            // TxtFixD
            // 
            this.TxtFixD.Location = new System.Drawing.Point(448, 76);
            this.TxtFixD.Name = "TxtFixD";
            this.TxtFixD.Size = new System.Drawing.Size(100, 22);
            this.TxtFixD.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(317, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 15);
            this.label9.TabIndex = 22;
            this.label9.Text = "工具径補正番号";
            // 
            // TxtToolNo
            // 
            this.TxtToolNo.Location = new System.Drawing.Point(448, 39);
            this.TxtToolNo.Name = "TxtToolNo";
            this.TxtToolNo.Size = new System.Drawing.Size(100, 22);
            this.TxtToolNo.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(362, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 15);
            this.label10.TabIndex = 20;
            this.label10.Text = "工具番号";
            // 
            // TxtFixH
            // 
            this.TxtFixH.Location = new System.Drawing.Point(448, 113);
            this.TxtFixH.Name = "TxtFixH";
            this.TxtFixH.Size = new System.Drawing.Size(100, 22);
            this.TxtFixH.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(317, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 15);
            this.label6.TabIndex = 28;
            this.label6.Text = "工具長補正番号";
            // 
            // TxtAprXYFeed
            // 
            this.TxtAprXYFeed.Location = new System.Drawing.Point(150, 225);
            this.TxtAprXYFeed.Name = "TxtAprXYFeed";
            this.TxtAprXYFeed.Size = new System.Drawing.Size(100, 22);
            this.TxtAprXYFeed.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(19, 232);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(112, 15);
            this.label11.TabIndex = 30;
            this.label11.Text = "XYアプローチ速度";
            // 
            // EditTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(777, 446);
            this.Name = "EditTool";
            this.Text = "工具の設定";
            this.PnlEditor.ResumeLayout(false);
            this.PnlEditor.PerformLayout();
            this.PnlOperate.ResumeLayout(false);
            this.PnlOperate.PerformLayout();
            this.PnlButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TxtFeed;
        private System.Windows.Forms.TextBox TxtSpin;
        private System.Windows.Forms.TextBox TxtZPos;
        private System.Windows.Forms.TextBox TxtDia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtFixH;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtStopCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtStartCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TxtFixD;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TxtToolNo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TxtAprZFeed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtAprXYFeed;
        private System.Windows.Forms.Label label11;
    }
}
