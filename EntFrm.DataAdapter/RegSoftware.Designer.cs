namespace EntFrm.DataAdapter
{
    partial class RegSoftware
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
            this.lbStatus = new System.Windows.Forms.Label();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnGenDog = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtActValCode = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtActiveCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDogType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDogID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.ForeColor = System.Drawing.Color.Red;
            this.lbStatus.Location = new System.Drawing.Point(155, 207);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(55, 15);
            this.lbStatus.TabIndex = 37;
            this.lbStatus.Text = "label3";
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(384, 241);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(124, 35);
            this.btnQuit.TabIndex = 35;
            this.btnQuit.Text = "关闭";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnGenDog
            // 
            this.btnGenDog.Location = new System.Drawing.Point(155, 241);
            this.btnGenDog.Name = "btnGenDog";
            this.btnGenDog.Size = new System.Drawing.Size(212, 35);
            this.btnGenDog.TabIndex = 36;
            this.btnGenDog.Text = "注册软件";
            this.btnGenDog.UseVisualStyleBackColor = true;
            this.btnGenDog.Click += new System.EventHandler(this.btnGenDog_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(486, 129);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 15);
            this.label7.TabIndex = 34;
            this.label7.Text = "个";
            // 
            // txtActValCode
            // 
            this.txtActValCode.Location = new System.Drawing.Point(155, 168);
            this.txtActValCode.Name = "txtActValCode";
            this.txtActValCode.Size = new System.Drawing.Size(353, 25);
            this.txtActValCode.TabIndex = 30;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(45, 171);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 15);
            this.label9.TabIndex = 26;
            this.label9.Text = "注册码：";
            // 
            // txtActiveCount
            // 
            this.txtActiveCount.Location = new System.Drawing.Point(155, 126);
            this.txtActiveCount.Name = "txtActiveCount";
            this.txtActiveCount.Size = new System.Drawing.Size(299, 25);
            this.txtActiveCount.TabIndex = 31;
            this.txtActiveCount.Text = "3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 15);
            this.label4.TabIndex = 27;
            this.label4.Text = "授权分诊台：";
            // 
            // txtDogType
            // 
            this.txtDogType.Location = new System.Drawing.Point(155, 80);
            this.txtDogType.Name = "txtDogType";
            this.txtDogType.ReadOnly = true;
            this.txtDogType.Size = new System.Drawing.Size(353, 25);
            this.txtDogType.TabIndex = 32;
            this.txtDogType.Text = "分诊排队系统";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 28;
            this.label2.Text = "类别：";
            // 
            // txtDogID
            // 
            this.txtDogID.Enabled = false;
            this.txtDogID.Location = new System.Drawing.Point(155, 37);
            this.txtDogID.Name = "txtDogID";
            this.txtDogID.Size = new System.Drawing.Size(299, 25);
            this.txtDogID.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 29;
            this.label1.Text = "加密锁编号：";
            // 
            // RegSoftware
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 313);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnGenDog);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtActValCode);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtActiveCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDogType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDogID);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegSoftware";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "软件注册";
            this.Load += new System.EventHandler(this.RegSoftware_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnGenDog;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtActValCode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtActiveCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDogType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDogID;
        private System.Windows.Forms.Label label1;
    }
}