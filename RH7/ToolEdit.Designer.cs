namespace SakiMotion
{
    partial class ToolEdit
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
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtDia = new System.Windows.Forms.TextBox();
            this.TxtZPos = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtSpin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtFeed = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnOk
            // 
            this.BtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOk.Location = new System.Drawing.Point(312, 249);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(103, 41);
            this.BtnOk.TabIndex = 0;
            this.BtnOk.Text = "Ok";
            this.BtnOk.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(203, 249);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(103, 41);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "キャンセル";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "工具直径";
            // 
            // TxtDia
            // 
            this.TxtDia.Location = new System.Drawing.Point(134, 53);
            this.TxtDia.Name = "TxtDia";
            this.TxtDia.Size = new System.Drawing.Size(100, 22);
            this.TxtDia.TabIndex = 3;
            // 
            // TxtZPos
            // 
            this.TxtZPos.Location = new System.Drawing.Point(134, 90);
            this.TxtZPos.Name = "TxtZPos";
            this.TxtZPos.Size = new System.Drawing.Size(100, 22);
            this.TxtZPos.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "工具加工位置";
            // 
            // TxtSpin
            // 
            this.TxtSpin.Location = new System.Drawing.Point(134, 127);
            this.TxtSpin.Name = "TxtSpin";
            this.TxtSpin.Size = new System.Drawing.Size(100, 22);
            this.TxtSpin.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "加工回転数";
            // 
            // TxtFeed
            // 
            this.TxtFeed.Location = new System.Drawing.Point(134, 165);
            this.TxtFeed.Name = "TxtFeed";
            this.TxtFeed.Size = new System.Drawing.Size(100, 22);
            this.TxtFeed.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "加工送り速度";
            // 
            // ToolEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 302);
            this.Controls.Add(this.TxtFeed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TxtSpin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtZPos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtDia);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.Name = "ToolEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "加工工具の設定";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtDia;
        private System.Windows.Forms.TextBox TxtZPos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtSpin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtFeed;
        private System.Windows.Forms.Label label4;
    }
}