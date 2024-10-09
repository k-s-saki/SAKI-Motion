namespace SakiMotion
{
    partial class EditPost
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
            this.label6 = new System.Windows.Forms.Label();
            this.TxtStartCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtEndCode = new System.Windows.Forms.TextBox();
            this.TxtSaveFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnSaveFileName = new System.Windows.Forms.Button();
            this.BtnExecFileName = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtExecFileName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtCodeType = new System.Windows.Forms.TextBox();
            this.PnlEditor.SuspendLayout();
            this.PnlOperate.SuspendLayout();
            this.PnlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlEditor
            // 
            this.PnlEditor.Controls.Add(this.TxtCodeType);
            this.PnlEditor.Controls.Add(this.label4);
            this.PnlEditor.Controls.Add(this.BtnExecFileName);
            this.PnlEditor.Controls.Add(this.label3);
            this.PnlEditor.Controls.Add(this.TxtExecFileName);
            this.PnlEditor.Controls.Add(this.BtnSaveFileName);
            this.PnlEditor.Controls.Add(this.label2);
            this.PnlEditor.Controls.Add(this.TxtSaveFileName);
            this.PnlEditor.Controls.Add(this.TxtEndCode);
            this.PnlEditor.Controls.Add(this.label1);
            this.PnlEditor.Controls.Add(this.label6);
            this.PnlEditor.Controls.Add(this.TxtStartCode);
            this.PnlEditor.Size = new System.Drawing.Size(556, 311);
            this.PnlEditor.TabIndex = 1;
            // 
            // PnlOperate
            // 
            this.PnlOperate.Size = new System.Drawing.Size(556, 52);
            // 
            // PnlButton
            // 
            this.PnlButton.Location = new System.Drawing.Point(9, 379);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 59);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "初期化コード";
            // 
            // TxtStartCode
            // 
            this.TxtStartCode.Location = new System.Drawing.Point(114, 56);
            this.TxtStartCode.Margin = new System.Windows.Forms.Padding(2);
            this.TxtStartCode.Multiline = true;
            this.TxtStartCode.Name = "TxtStartCode";
            this.TxtStartCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtStartCode.Size = new System.Drawing.Size(390, 70);
            this.TxtStartCode.TabIndex = 0;
            this.TxtStartCode.Text = "0.000";
            this.TxtStartCode.TextChanged += new System.EventHandler(this.TxtPathPrec_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 156);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "終了時コード";
            // 
            // TxtEndCode
            // 
            this.TxtEndCode.Location = new System.Drawing.Point(114, 147);
            this.TxtEndCode.Margin = new System.Windows.Forms.Padding(2);
            this.TxtEndCode.Multiline = true;
            this.TxtEndCode.Name = "TxtEndCode";
            this.TxtEndCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtEndCode.Size = new System.Drawing.Size(390, 70);
            this.TxtEndCode.TabIndex = 1;
            this.TxtEndCode.Text = "0.000";
            // 
            // TxtSaveFileName
            // 
            this.TxtSaveFileName.Location = new System.Drawing.Point(114, 239);
            this.TxtSaveFileName.Margin = new System.Windows.Forms.Padding(2);
            this.TxtSaveFileName.Name = "TxtSaveFileName";
            this.TxtSaveFileName.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtSaveFileName.Size = new System.Drawing.Size(301, 19);
            this.TxtSaveFileName.TabIndex = 3;
            this.TxtSaveFileName.Text = "0.000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 242);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "保存ファイル名";
            // 
            // BtnSaveFileName
            // 
            this.BtnSaveFileName.Location = new System.Drawing.Point(427, 239);
            this.BtnSaveFileName.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSaveFileName.Name = "BtnSaveFileName";
            this.BtnSaveFileName.Size = new System.Drawing.Size(60, 21);
            this.BtnSaveFileName.TabIndex = 19;
            this.BtnSaveFileName.Text = "指定";
            this.BtnSaveFileName.UseVisualStyleBackColor = true;
            this.BtnSaveFileName.Click += new System.EventHandler(this.BtnSaveFileName_Click);
            // 
            // BtnExecFileName
            // 
            this.BtnExecFileName.Location = new System.Drawing.Point(427, 271);
            this.BtnExecFileName.Margin = new System.Windows.Forms.Padding(2);
            this.BtnExecFileName.Name = "BtnExecFileName";
            this.BtnExecFileName.Size = new System.Drawing.Size(60, 21);
            this.BtnExecFileName.TabIndex = 22;
            this.BtnExecFileName.Text = "指定";
            this.BtnExecFileName.UseVisualStyleBackColor = true;
            this.BtnExecFileName.Click += new System.EventHandler(this.BtnExecFileName_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 273);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "実行ファイル名";
            // 
            // TxtExecFileName
            // 
            this.TxtExecFileName.Location = new System.Drawing.Point(114, 271);
            this.TxtExecFileName.Margin = new System.Windows.Forms.Padding(2);
            this.TxtExecFileName.Name = "TxtExecFileName";
            this.TxtExecFileName.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtExecFileName.Size = new System.Drawing.Size(301, 19);
            this.TxtExecFileName.TabIndex = 20;
            this.TxtExecFileName.Text = "0.000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 29);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "コード種別";
            // 
            // TxtCodeType
            // 
            this.TxtCodeType.Location = new System.Drawing.Point(114, 22);
            this.TxtCodeType.Margin = new System.Windows.Forms.Padding(2);
            this.TxtCodeType.Name = "TxtCodeType";
            this.TxtCodeType.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtCodeType.Size = new System.Drawing.Size(150, 19);
            this.TxtCodeType.TabIndex = 24;
            // 
            // EditPost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(583, 452);
            this.Name = "EditPost";
            this.Text = "機械設定";
            this.PnlEditor.ResumeLayout(false);
            this.PnlEditor.PerformLayout();
            this.PnlOperate.ResumeLayout(false);
            this.PnlOperate.PerformLayout();
            this.PnlButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox TxtStartCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtEndCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtSaveFileName;
        private System.Windows.Forms.Button BtnSaveFileName;
        private System.Windows.Forms.Button BtnExecFileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtExecFileName;
        private System.Windows.Forms.TextBox TxtCodeType;
        private System.Windows.Forms.Label label4;
    }
}
