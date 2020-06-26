namespace EntFrm.ExploreConsole
{
    partial class SettingDlg
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
            this.txtHomeUrl = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ckIsFull = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dpWorkMode = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "分诊台地址：";
            // 
            // txtHomeUrl
            // 
            this.txtHomeUrl.Location = new System.Drawing.Point(41, 57);
            this.txtHomeUrl.Name = "txtHomeUrl";
            this.txtHomeUrl.Size = new System.Drawing.Size(452, 25);
            this.txtHomeUrl.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(196, 184);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(131, 47);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "保存";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(362, 184);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(131, 47);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ckIsFull
            // 
            this.ckIsFull.AutoSize = true;
            this.ckIsFull.Location = new System.Drawing.Point(46, 147);
            this.ckIsFull.Name = "ckIsFull";
            this.ckIsFull.Size = new System.Drawing.Size(89, 19);
            this.ckIsFull.TabIndex = 3;
            this.ckIsFull.Text = "全屏显示";
            this.ckIsFull.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "模式：";
            // 
            // dpWorkMode
            // 
            this.dpWorkMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpWorkMode.FormattingEnabled = true;
            this.dpWorkMode.Items.AddRange(new object[] {
            "分诊台模式",
            "虚拟挂号模式",
            "取药报到模式"});
            this.dpWorkMode.Location = new System.Drawing.Point(96, 105);
            this.dpWorkMode.Name = "dpWorkMode";
            this.dpWorkMode.Size = new System.Drawing.Size(397, 23);
            this.dpWorkMode.TabIndex = 4;
            // 
            // SettingDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 256);
            this.Controls.Add(this.dpWorkMode);
            this.Controls.Add(this.ckIsFull);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtHomeUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "基本设置";
            this.Load += new System.EventHandler(this.SettingDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHomeUrl;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox ckIsFull;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox dpWorkMode;
    }
}