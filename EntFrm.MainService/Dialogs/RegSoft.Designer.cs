namespace EntFrm.MainService.Dialogs
{
    partial class RegSoft
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbStatus = new System.Windows.Forms.Label();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnGenDog = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtActValCode = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtActiveCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOrganizName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDogID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbStatus);
            this.panel1.Controls.Add(this.btnQuit);
            this.panel1.Controls.Add(this.btnGenDog);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtActValCode);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtActiveCount);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtOrganizName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtDogID);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(552, 313);
            this.panel1.TabIndex = 0;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.ForeColor = System.Drawing.Color.Red;
            this.lbStatus.Location = new System.Drawing.Point(137, 203);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(55, 15);
            this.lbStatus.TabIndex = 25;
            this.lbStatus.Text = "label3";
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(347, 237);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(143, 35);
            this.btnQuit.TabIndex = 23;
            this.btnQuit.Text = "关闭";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnGenDog
            // 
            this.btnGenDog.Location = new System.Drawing.Point(137, 237);
            this.btnGenDog.Name = "btnGenDog";
            this.btnGenDog.Size = new System.Drawing.Size(156, 35);
            this.btnGenDog.TabIndex = 24;
            this.btnGenDog.Text = "注册软件";
            this.btnGenDog.UseVisualStyleBackColor = true;
            this.btnGenDog.Click += new System.EventHandler(this.btnGenDog_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(468, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 15);
            this.label7.TabIndex = 22;
            this.label7.Text = "个";
            // 
            // txtActValCode
            // 
            this.txtActValCode.Location = new System.Drawing.Point(137, 164);
            this.txtActValCode.Name = "txtActValCode";
            this.txtActValCode.Size = new System.Drawing.Size(353, 25);
            this.txtActValCode.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 167);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 15);
            this.label9.TabIndex = 14;
            this.label9.Text = "注册码：";
            // 
            // txtActiveCount
            // 
            this.txtActiveCount.Location = new System.Drawing.Point(137, 122);
            this.txtActiveCount.Name = "txtActiveCount";
            this.txtActiveCount.Size = new System.Drawing.Size(299, 25);
            this.txtActiveCount.TabIndex = 19;
            this.txtActiveCount.Text = "3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "授权窗口：";
            // 
            // txtOrganizName
            // 
            this.txtOrganizName.Location = new System.Drawing.Point(137, 76);
            this.txtOrganizName.Name = "txtOrganizName";
            this.txtOrganizName.Size = new System.Drawing.Size(353, 25);
            this.txtOrganizName.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 16;
            this.label2.Text = "单位名称：";
            // 
            // txtDogID
            // 
            this.txtDogID.Enabled = false;
            this.txtDogID.Location = new System.Drawing.Point(137, 33);
            this.txtDogID.Name = "txtDogID";
            this.txtDogID.Size = new System.Drawing.Size(299, 25);
            this.txtDogID.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 17;
            this.label1.Text = "加密锁编号：";
            // 
            // RegSoft
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 313);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegSoft";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RegSoft";
            this.Load += new System.EventHandler(this.RegSoft_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnGenDog;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtActValCode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtActiveCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOrganizName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDogID;
        private System.Windows.Forms.Label label1;
    }
}