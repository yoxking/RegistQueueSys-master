namespace EntFrm.ExploreConsole.Dialogs
{
    partial class RCardSigninDlg
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbTimeStr = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F);
            this.label1.Location = new System.Drawing.Point(60, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "请将患者卡放在读卡区域...";
            // 
            // lbTimeStr
            // 
            this.lbTimeStr.AutoSize = true;
            this.lbTimeStr.Font = new System.Drawing.Font("宋体", 16F);
            this.lbTimeStr.ForeColor = System.Drawing.Color.Red;
            this.lbTimeStr.Location = new System.Drawing.Point(170, 98);
            this.lbTimeStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTimeStr.Name = "lbTimeStr";
            this.lbTimeStr.Size = new System.Drawing.Size(54, 22);
            this.lbTimeStr.TabIndex = 1;
            this.lbTimeStr.Text = "15秒";
            // 
            // RCardSigninDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 179);
            this.Controls.Add(this.lbTimeStr);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RCardSigninDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "刷卡";
            this.Load += new System.EventHandler(this.RCardSigninDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbTimeStr;
    }
}