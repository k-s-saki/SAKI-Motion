namespace ProfileCut7
{
    partial class ProfileCutPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnTool = new System.Windows.Forms.Button();
            this.BtnPost = new System.Windows.Forms.Button();
            this.LblToolName = new System.Windows.Forms.Label();
            this.LblPathName = new System.Windows.Forms.Label();
            this.BtnPathGen = new System.Windows.Forms.Button();
            this.BtnProfileCut = new System.Windows.Forms.Button();
            this.BtnDebugUserString = new System.Windows.Forms.Button();
            this.BtnSetOrder = new System.Windows.Forms.Button();
            this.LblPostName = new System.Windows.Forms.Label();
            this.BtnOutput = new System.Windows.Forms.Button();
            this.cbxOutputInfo = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnDelPath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnTool
            // 
            this.BtnTool.Location = new System.Drawing.Point(24, 152);
            this.BtnTool.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnTool.Name = "BtnTool";
            this.BtnTool.Size = new System.Drawing.Size(160, 32);
            this.BtnTool.TabIndex = 2;
            this.BtnTool.Text = "工具を設定";
            this.BtnTool.UseVisualStyleBackColor = true;
            this.BtnTool.Click += new System.EventHandler(this.BtnTool_Click);
            // 
            // BtnPost
            // 
            this.BtnPost.Location = new System.Drawing.Point(24, 116);
            this.BtnPost.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnPost.Name = "BtnPost";
            this.BtnPost.Size = new System.Drawing.Size(160, 32);
            this.BtnPost.TabIndex = 5;
            this.BtnPost.Text = "機械の設定";
            this.BtnPost.UseVisualStyleBackColor = true;
            this.BtnPost.Click += new System.EventHandler(this.BtnPost_Click);
            // 
            // LblToolName
            // 
            this.LblToolName.AutoSize = true;
            this.LblToolName.Location = new System.Drawing.Point(191, 161);
            this.LblToolName.Name = "LblToolName";
            this.LblToolName.Size = new System.Drawing.Size(31, 15);
            this.LblToolName.TabIndex = 11;
            this.LblToolName.Text = "---";
            // 
            // LblPathName
            // 
            this.LblPathName.AutoSize = true;
            this.LblPathName.Location = new System.Drawing.Point(190, 197);
            this.LblPathName.Name = "LblPathName";
            this.LblPathName.Size = new System.Drawing.Size(31, 15);
            this.LblPathName.TabIndex = 13;
            this.LblPathName.Text = "---";
            // 
            // BtnPathGen
            // 
            this.BtnPathGen.Location = new System.Drawing.Point(24, 188);
            this.BtnPathGen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnPathGen.Name = "BtnPathGen";
            this.BtnPathGen.Size = new System.Drawing.Size(160, 32);
            this.BtnPathGen.TabIndex = 12;
            this.BtnPathGen.Text = "加工パスの設定";
            this.BtnPathGen.UseVisualStyleBackColor = true;
            this.BtnPathGen.Click += new System.EventHandler(this.BtnPathGen_Click);
            // 
            // BtnProfileCut
            // 
            this.BtnProfileCut.Location = new System.Drawing.Point(24, 274);
            this.BtnProfileCut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnProfileCut.Name = "BtnProfileCut";
            this.BtnProfileCut.Size = new System.Drawing.Size(160, 32);
            this.BtnProfileCut.TabIndex = 14;
            this.BtnProfileCut.Text = "加工曲線の生成";
            this.BtnProfileCut.UseVisualStyleBackColor = true;
            this.BtnProfileCut.Click += new System.EventHandler(this.BtnProfileCut_Click);
            // 
            // BtnDebugUserString
            // 
            this.BtnDebugUserString.Location = new System.Drawing.Point(122, 490);
            this.BtnDebugUserString.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnDebugUserString.Name = "BtnDebugUserString";
            this.BtnDebugUserString.Size = new System.Drawing.Size(134, 30);
            this.BtnDebugUserString.TabIndex = 15;
            this.BtnDebugUserString.Text = "CheckData";
            this.BtnDebugUserString.UseVisualStyleBackColor = true;
            this.BtnDebugUserString.Click += new System.EventHandler(this.BtnDebugUserString_Click);
            // 
            // BtnSetOrder
            // 
            this.BtnSetOrder.Location = new System.Drawing.Point(24, 310);
            this.BtnSetOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnSetOrder.Name = "BtnSetOrder";
            this.BtnSetOrder.Size = new System.Drawing.Size(160, 32);
            this.BtnSetOrder.TabIndex = 16;
            this.BtnSetOrder.Text = "加工順";
            this.BtnSetOrder.UseVisualStyleBackColor = true;
            this.BtnSetOrder.Click += new System.EventHandler(this.BtnSetOrder_Click);
            // 
            // LblPostName
            // 
            this.LblPostName.AutoSize = true;
            this.LblPostName.Location = new System.Drawing.Point(191, 126);
            this.LblPostName.Name = "LblPostName";
            this.LblPostName.Size = new System.Drawing.Size(31, 15);
            this.LblPostName.TabIndex = 17;
            this.LblPostName.Text = "---";
            // 
            // BtnOutput
            // 
            this.BtnOutput.Location = new System.Drawing.Point(24, 347);
            this.BtnOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnOutput.Name = "BtnOutput";
            this.BtnOutput.Size = new System.Drawing.Size(160, 32);
            this.BtnOutput.TabIndex = 18;
            this.BtnOutput.Text = "加工データ出力";
            this.BtnOutput.UseVisualStyleBackColor = true;
            this.BtnOutput.Click += new System.EventHandler(this.BtnOutput_Click);
            // 
            // cbxOutputInfo
            // 
            this.cbxOutputInfo.AutoSize = true;
            this.cbxOutputInfo.Location = new System.Drawing.Point(19, 457);
            this.cbxOutputInfo.Name = "cbxOutputInfo";
            this.cbxOutputInfo.Size = new System.Drawing.Size(122, 19);
            this.cbxOutputInfo.TabIndex = 19;
            this.cbxOutputInfo.Text = "コード分析情報";
            this.cbxOutputInfo.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 27);
            this.label2.TabIndex = 20;
            this.label2.Text = "設定";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 27);
            this.label3.TabIndex = 21;
            this.label3.Text = "操作";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 427);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 27);
            this.label4.TabIndex = 22;
            this.label4.Text = "分析";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(19, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(210, 27);
            this.label5.TabIndex = 23;
            this.label5.Text = "Profile Cut (RML1)";
            // 
            // BtnDelPath
            // 
            this.BtnDelPath.Location = new System.Drawing.Point(122, 386);
            this.BtnDelPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnDelPath.Name = "BtnDelPath";
            this.BtnDelPath.Size = new System.Drawing.Size(134, 32);
            this.BtnDelPath.TabIndex = 24;
            this.BtnDelPath.Text = "計算パス消去";
            this.BtnDelPath.UseVisualStyleBackColor = true;
            this.BtnDelPath.Click += new System.EventHandler(this.BtnDelPath_Click);
            // 
            // ProfileCutPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnDelPath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxOutputInfo);
            this.Controls.Add(this.BtnOutput);
            this.Controls.Add(this.LblPostName);
            this.Controls.Add(this.BtnSetOrder);
            this.Controls.Add(this.BtnDebugUserString);
            this.Controls.Add(this.BtnProfileCut);
            this.Controls.Add(this.LblPathName);
            this.Controls.Add(this.BtnPathGen);
            this.Controls.Add(this.LblToolName);
            this.Controls.Add(this.BtnPost);
            this.Controls.Add(this.BtnTool);
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "ProfileCutPanel";
            this.Size = new System.Drawing.Size(277, 545);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnTool;
        private System.Windows.Forms.Button BtnPost;
        private System.Windows.Forms.Label LblToolName;
        private System.Windows.Forms.Label LblPathName;
        private System.Windows.Forms.Button BtnPathGen;
        private System.Windows.Forms.Button BtnProfileCut;
        private System.Windows.Forms.Button BtnDebugUserString;
        private System.Windows.Forms.Button BtnSetOrder;
        private System.Windows.Forms.Label LblPostName;
        private System.Windows.Forms.Button BtnOutput;
        private System.Windows.Forms.CheckBox cbxOutputInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnDelPath;
    }
}
