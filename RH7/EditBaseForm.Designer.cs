namespace SakiMotion
{
    partial class EditBaseForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PnlButton = new System.Windows.Forms.Panel();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.PnlEditor = new System.Windows.Forms.Panel();
            this.PnlOperate = new System.Windows.Forms.Panel();
            this.BtnEditCancel = new System.Windows.Forms.Button();
            this.BtnEditFinish = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.BtnDelFromList = new System.Windows.Forms.Button();
            this.BtnEditStart = new System.Windows.Forms.Button();
            this.CbxName = new System.Windows.Forms.ComboBox();
            this.PnlButton.SuspendLayout();
            this.PnlOperate.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlButton
            // 
            this.PnlButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlButton.Controls.Add(this.BtnCancel);
            this.PnlButton.Controls.Add(this.BtnOk);
            this.PnlButton.Location = new System.Drawing.Point(12, 384);
            this.PnlButton.Name = "PnlButton";
            this.PnlButton.Size = new System.Drawing.Size(708, 79);
            this.PnlButton.TabIndex = 19;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(474, 20);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(103, 41);
            this.BtnCancel.TabIndex = 6;
            this.BtnCancel.Text = "キャンセル";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // BtnOk
            // 
            this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOk.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOk.Location = new System.Drawing.Point(583, 20);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(103, 41);
            this.BtnOk.TabIndex = 5;
            this.BtnOk.Text = "Ok";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // PnlEditor
            // 
            this.PnlEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlEditor.AutoScroll = true;
            this.PnlEditor.Location = new System.Drawing.Point(12, 79);
            this.PnlEditor.Name = "PnlEditor";
            this.PnlEditor.Size = new System.Drawing.Size(708, 299);
            this.PnlEditor.TabIndex = 18;
            // 
            // PnlOperate
            // 
            this.PnlOperate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlOperate.Controls.Add(this.BtnEditCancel);
            this.PnlOperate.Controls.Add(this.BtnEditFinish);
            this.PnlOperate.Controls.Add(this.label12);
            this.PnlOperate.Controls.Add(this.BtnDelFromList);
            this.PnlOperate.Controls.Add(this.BtnEditStart);
            this.PnlOperate.Controls.Add(this.CbxName);
            this.PnlOperate.Location = new System.Drawing.Point(12, 8);
            this.PnlOperate.Name = "PnlOperate";
            this.PnlOperate.Size = new System.Drawing.Size(708, 65);
            this.PnlOperate.TabIndex = 17;
            // 
            // BtnEditCancel
            // 
            this.BtnEditCancel.Location = new System.Drawing.Point(507, 22);
            this.BtnEditCancel.Name = "BtnEditCancel";
            this.BtnEditCancel.Size = new System.Drawing.Size(57, 40);
            this.BtnEditCancel.TabIndex = 4;
            this.BtnEditCancel.Text = "中止";
            this.BtnEditCancel.UseVisualStyleBackColor = true;
            this.BtnEditCancel.Click += new System.EventHandler(this.BtnEditCancel_Click);
            // 
            // BtnEditFinish
            // 
            this.BtnEditFinish.Location = new System.Drawing.Point(444, 22);
            this.BtnEditFinish.Name = "BtnEditFinish";
            this.BtnEditFinish.Size = new System.Drawing.Size(57, 40);
            this.BtnEditFinish.TabIndex = 3;
            this.BtnEditFinish.Text = "完了";
            this.BtnEditFinish.UseVisualStyleBackColor = true;
            this.BtnEditFinish.Click += new System.EventHandler(this.BtnEditFinish_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(22, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 15);
            this.label12.TabIndex = 17;
            this.label12.Text = "名前";
            // 
            // BtnDelFromList
            // 
            this.BtnDelFromList.Location = new System.Drawing.Point(498, 8);
            this.BtnDelFromList.Name = "BtnDelFromList";
            this.BtnDelFromList.Size = new System.Drawing.Size(57, 40);
            this.BtnDelFromList.TabIndex = 2;
            this.BtnDelFromList.Text = "削除";
            this.BtnDelFromList.UseVisualStyleBackColor = true;
            this.BtnDelFromList.Click += new System.EventHandler(this.BtnDelFromList_Click);
            // 
            // BtnEditStart
            // 
            this.BtnEditStart.Location = new System.Drawing.Point(435, 8);
            this.BtnEditStart.Name = "BtnEditStart";
            this.BtnEditStart.Size = new System.Drawing.Size(57, 40);
            this.BtnEditStart.TabIndex = 1;
            this.BtnEditStart.Text = "変更";
            this.BtnEditStart.UseVisualStyleBackColor = true;
            this.BtnEditStart.Click += new System.EventHandler(this.BtnEditStart_Click);
            // 
            // CbxName
            // 
            this.CbxName.FormattingEnabled = true;
            this.CbxName.Location = new System.Drawing.Point(87, 23);
            this.CbxName.Name = "CbxName";
            this.CbxName.Size = new System.Drawing.Size(339, 23);
            this.CbxName.TabIndex = 0;
            this.CbxName.DropDown += new System.EventHandler(this.CbxName_DropDown);
            this.CbxName.SelectedIndexChanged += new System.EventHandler(this.CbxName_SelectedIndexChanged);
            // 
            // EditBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 475);
            this.Controls.Add(this.PnlButton);
            this.Controls.Add(this.PnlEditor);
            this.Controls.Add(this.PnlOperate);
            this.Name = "EditBaseForm";
            this.Text = "EditBaseForm";
            this.Load += new System.EventHandler(this.EditBaseForm_Load);
            this.Shown += new System.EventHandler(this.EditBaseForm_Shown);
            this.PnlButton.ResumeLayout(false);
            this.PnlOperate.ResumeLayout(false);
            this.PnlOperate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BtnEditFinish;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button BtnDelFromList;
        private System.Windows.Forms.Button BtnEditStart;
        public System.Windows.Forms.Panel PnlEditor;
        public System.Windows.Forms.Panel PnlOperate;
        public System.Windows.Forms.Panel PnlButton;
        public System.Windows.Forms.ComboBox CbxName;
        private System.Windows.Forms.Button BtnEditCancel;
        public System.Windows.Forms.Button BtnOk;
        public System.Windows.Forms.Button BtnCancel;
    }
}