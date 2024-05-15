namespace ProfileCut7
{
    partial class EditPath
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtZEnd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtZApr = new System.Windows.Forms.TextBox();
            this.TxtZRapid = new System.Windows.Forms.TextBox();
            this.TxtZStep = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtZStart = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.TxtOffsetStep = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.TxtOffsetEnd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtOffsetStart = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtPathPrec = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbOffsetAddCurve = new System.Windows.Forms.RadioButton();
            this.rbOffset = new System.Windows.Forms.RadioButton();
            this.rbDirectCurve = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtRapidFeed = new System.Windows.Forms.TextBox();
            this.PnlEditor.SuspendLayout();
            this.PnlOperate.SuspendLayout();
            this.PnlButton.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlEditor
            // 
            this.PnlEditor.Controls.Add(this.label7);
            this.PnlEditor.Controls.Add(this.TxtRapidFeed);
            this.PnlEditor.Controls.Add(this.groupBox1);
            this.PnlEditor.Controls.Add(this.label6);
            this.PnlEditor.Controls.Add(this.TxtPathPrec);
            this.PnlEditor.Controls.Add(this.groupBox5);
            this.PnlEditor.Controls.Add(this.groupBox3);
            this.PnlEditor.Size = new System.Drawing.Size(627, 342);
            this.PnlEditor.TabIndex = 1;
            // 
            // PnlOperate
            // 
            this.PnlOperate.Size = new System.Drawing.Size(627, 65);
            // 
            // PnlButton
            // 
            this.PnlButton.Location = new System.Drawing.Point(12, 427);
            this.PnlButton.Size = new System.Drawing.Size(627, 79);
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(502, 20);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(389, 20);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.TxtZEnd);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.TxtZApr);
            this.groupBox3.Controls.Add(this.TxtZRapid);
            this.groupBox3.Controls.Add(this.TxtZStep);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.TxtZStart);
            this.groupBox3.Location = new System.Drawing.Point(343, 106);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(267, 222);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Z方向";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 153);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 15);
            this.label8.TabIndex = 17;
            this.label8.Text = "加工終了Z加算";
            // 
            // TxtZEnd
            // 
            this.TxtZEnd.Location = new System.Drawing.Point(155, 149);
            this.TxtZEnd.Name = "TxtZEnd";
            this.TxtZEnd.Size = new System.Drawing.Size(100, 22);
            this.TxtZEnd.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "Ｚアプローチ距離";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 15);
            this.label5.TabIndex = 15;
            this.label5.Text = "移動Z座標";
            // 
            // TxtZApr
            // 
            this.TxtZApr.Location = new System.Drawing.Point(155, 65);
            this.TxtZApr.Name = "TxtZApr";
            this.TxtZApr.Size = new System.Drawing.Size(100, 22);
            this.TxtZApr.TabIndex = 1;
            // 
            // TxtZRapid
            // 
            this.TxtZRapid.Location = new System.Drawing.Point(155, 28);
            this.TxtZRapid.Name = "TxtZRapid";
            this.TxtZRapid.Size = new System.Drawing.Size(100, 22);
            this.TxtZRapid.TabIndex = 0;
            // 
            // TxtZStep
            // 
            this.TxtZStep.Location = new System.Drawing.Point(155, 121);
            this.TxtZStep.Name = "TxtZStep";
            this.TxtZStep.Size = new System.Drawing.Size(100, 22);
            this.TxtZStep.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Z下降ステップ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "加工開始Z加算";
            // 
            // TxtZStart
            // 
            this.TxtZStart.Location = new System.Drawing.Point(155, 93);
            this.TxtZStart.Name = "TxtZStart";
            this.TxtZStart.Size = new System.Drawing.Size(100, 22);
            this.TxtZStart.TabIndex = 2;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.TxtOffsetStep);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.TxtOffsetEnd);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.TxtOffsetStart);
            this.groupBox5.Location = new System.Drawing.Point(41, 227);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(288, 101);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "外形方向";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(65, 49);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(49, 15);
            this.label20.TabIndex = 15;
            this.label20.Text = "ステップ";
            // 
            // TxtOffsetStep
            // 
            this.TxtOffsetStep.Location = new System.Drawing.Point(148, 45);
            this.TxtOffsetStep.Name = "TxtOffsetStep";
            this.TxtOffsetStep.Size = new System.Drawing.Size(100, 22);
            this.TxtOffsetStep.TabIndex = 1;
            this.TxtOffsetStep.Text = "0.000";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(77, 73);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 15);
            this.label13.TabIndex = 13;
            this.label13.Text = "終了";
            // 
            // TxtOffsetEnd
            // 
            this.TxtOffsetEnd.Location = new System.Drawing.Point(148, 70);
            this.TxtOffsetEnd.Name = "TxtOffsetEnd";
            this.TxtOffsetEnd.Size = new System.Drawing.Size(100, 22);
            this.TxtOffsetEnd.TabIndex = 2;
            this.TxtOffsetEnd.Text = "0.000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "開始";
            // 
            // TxtOffsetStart
            // 
            this.TxtOffsetStart.Location = new System.Drawing.Point(148, 21);
            this.TxtOffsetStart.Name = "TxtOffsetStart";
            this.TxtOffsetStart.Size = new System.Drawing.Size(100, 22);
            this.TxtOffsetStart.TabIndex = 0;
            this.TxtOffsetStart.Text = "0.000";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(38, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "機械コード変換許容差";
            // 
            // TxtPathPrec
            // 
            this.TxtPathPrec.Location = new System.Drawing.Point(89, 53);
            this.TxtPathPrec.Name = "TxtPathPrec";
            this.TxtPathPrec.Size = new System.Drawing.Size(100, 22);
            this.TxtPathPrec.TabIndex = 0;
            this.TxtPathPrec.Text = "0.000";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbOffsetAddCurve);
            this.groupBox1.Controls.Add(this.rbOffset);
            this.groupBox1.Controls.Add(this.rbDirectCurve);
            this.groupBox1.Location = new System.Drawing.Point(41, 110);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 111);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "加工方法";
            // 
            // rbOffsetAddCurve
            // 
            this.rbOffsetAddCurve.AutoSize = true;
            this.rbOffsetAddCurve.Location = new System.Drawing.Point(46, 79);
            this.rbOffsetAddCurve.Name = "rbOffsetAddCurve";
            this.rbOffsetAddCurve.Size = new System.Drawing.Size(167, 19);
            this.rbOffsetAddCurve.TabIndex = 2;
            this.rbOffsetAddCurve.TabStop = true;
            this.rbOffsetAddCurve.Text = "サイド加工＋導入円弧";
            this.rbOffsetAddCurve.UseVisualStyleBackColor = true;
            // 
            // rbOffset
            // 
            this.rbOffset.AutoSize = true;
            this.rbOffset.Location = new System.Drawing.Point(46, 54);
            this.rbOffset.Name = "rbOffset";
            this.rbOffset.Size = new System.Drawing.Size(92, 19);
            this.rbOffset.TabIndex = 1;
            this.rbOffset.TabStop = true;
            this.rbOffset.Text = "サイド加工";
            this.rbOffset.UseVisualStyleBackColor = true;
            // 
            // rbDirectCurve
            // 
            this.rbDirectCurve.AutoSize = true;
            this.rbDirectCurve.Location = new System.Drawing.Point(46, 29);
            this.rbDirectCurve.Name = "rbDirectCurve";
            this.rbDirectCurve.Size = new System.Drawing.Size(182, 19);
            this.rbDirectCurve.TabIndex = 0;
            this.rbDirectCurve.TabStop = true;
            this.rbDirectCurve.Text = "直接中心加工　文字など";
            this.rbDirectCurve.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(365, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 18;
            this.label7.Text = "移動速度";
            // 
            // TxtRapidFeed
            // 
            this.TxtRapidFeed.Location = new System.Drawing.Point(489, 32);
            this.TxtRapidFeed.Name = "TxtRapidFeed";
            this.TxtRapidFeed.Size = new System.Drawing.Size(109, 22);
            this.TxtRapidFeed.TabIndex = 1;
            // 
            // EditPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(663, 518);
            this.Name = "EditPath";
            this.Text = "加工パスの設定";
            this.PnlEditor.ResumeLayout(false);
            this.PnlEditor.PerformLayout();
            this.PnlOperate.ResumeLayout(false);
            this.PnlOperate.PerformLayout();
            this.PnlButton.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox TxtZStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtZStep;
        private System.Windows.Forms.TextBox TxtZRapid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox TxtOffsetStep;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox TxtOffsetEnd;
        private System.Windows.Forms.TextBox TxtOffsetStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtPathPrec;
        private System.Windows.Forms.TextBox TxtZApr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rbDirectCurve;
        private System.Windows.Forms.RadioButton rbOffset;
        private System.Windows.Forms.RadioButton rbOffsetAddCurve;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtRapidFeed;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TxtZEnd;
    }
}
