using EntFrm.Framework.Utility;

namespace EntFrm.SettingConsole
{
    partial class ServiceTLimitDialog
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.ckSCardLimit = new CkGroupBoxEx();
            this.txtSCardInterval = new System.Windows.Forms.TextBox();
            this.txtSCardNum = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ckIsOne = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ckWeekLimit = new CkGroupBoxEx();
            this.ckWeek7 = new System.Windows.Forms.CheckBox();
            this.ckWeek6 = new System.Windows.Forms.CheckBox();
            this.ckWeek5 = new System.Windows.Forms.CheckBox();
            this.ckWeek4 = new System.Windows.Forms.CheckBox();
            this.ckWeek3 = new System.Windows.Forms.CheckBox();
            this.ckWeek2 = new System.Windows.Forms.CheckBox();
            this.ckWeek1 = new System.Windows.Forms.CheckBox();
            this.ckPMLimit = new CkGroupBoxEx();
            this.txtPMEndTime = new System.Windows.Forms.TextBox();
            this.txtPMStartTime = new System.Windows.Forms.TextBox();
            this.txtPMTotal = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ckAMLimit = new CkGroupBoxEx();
            this.txtAMEndTime = new System.Windows.Forms.TextBox();
            this.txtAMStartTime = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtAMTotal = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.ckSCardLimit.SuspendLayout();
            this.ckWeekLimit.SuspendLayout();
            this.ckPMLimit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPMTotal)).BeginInit();
            this.ckAMLimit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAMTotal)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.ckSCardLimit);
            this.panel2.Controls.Add(this.txtServiceName);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.ckWeekLimit);
            this.panel2.Controls.Add(this.ckPMLimit);
            this.panel2.Controls.Add(this.ckAMLimit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(613, 507);
            this.panel2.TabIndex = 3;
            // 
            // ckSCardLimit
            // 
            this.ckSCardLimit.Controls.Add(this.txtSCardInterval);
            this.ckSCardLimit.Controls.Add(this.txtSCardNum);
            this.ckSCardLimit.Controls.Add(this.label6);
            this.ckSCardLimit.Controls.Add(this.label8);
            this.ckSCardLimit.Controls.Add(this.label7);
            this.ckSCardLimit.Controls.Add(this.ckIsOne);
            this.ckSCardLimit.Controls.Add(this.label5);
            this.ckSCardLimit.Location = new System.Drawing.Point(13, 354);
            this.ckSCardLimit.Name = "ckSCardLimit";
            this.ckSCardLimit.Size = new System.Drawing.Size(585, 141);
            this.ckSCardLimit.TabIndex = 3;
            this.ckSCardLimit.TabStop = false;
            this.ckSCardLimit.Text = "刷卡限制";
            // 
            // txtSCardInterval
            // 
            this.txtSCardInterval.Location = new System.Drawing.Point(149, 100);
            this.txtSCardInterval.Name = "txtSCardInterval";
            this.txtSCardInterval.Size = new System.Drawing.Size(206, 25);
            this.txtSCardInterval.TabIndex = 215;
            // 
            // txtSCardNum
            // 
            this.txtSCardNum.Location = new System.Drawing.Point(149, 30);
            this.txtSCardNum.Name = "txtSCardNum";
            this.txtSCardNum.Size = new System.Drawing.Size(206, 25);
            this.txtSCardNum.TabIndex = 215;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 15);
            this.label6.TabIndex = 211;
            this.label6.Text = "再次刷卡间隔：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(361, 106);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 15);
            this.label8.TabIndex = 211;
            this.label8.Text = "分钟";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(361, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 15);
            this.label7.TabIndex = 211;
            this.label7.Text = "次/天";
            // 
            // ckIsOne
            // 
            this.ckIsOne.AutoSize = true;
            this.ckIsOne.Location = new System.Drawing.Point(149, 69);
            this.ckIsOne.Name = "ckIsOne";
            this.ckIsOne.Size = new System.Drawing.Size(119, 19);
            this.ckIsOne.TabIndex = 7;
            this.ckIsOne.Tag = "2";
            this.ckIsOne.Text = "限制一卡多取";
            this.ckIsOne.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 15);
            this.label5.TabIndex = 211;
            this.label5.Text = "总刷卡次数：";
            // 
            // txtServiceName
            // 
            this.txtServiceName.Location = new System.Drawing.Point(125, 15);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.ReadOnly = true;
            this.txtServiceName.Size = new System.Drawing.Size(435, 25);
            this.txtServiceName.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "业务名称：";
            // 
            // ckWeekLimit
            // 
            this.ckWeekLimit.Controls.Add(this.ckWeek7);
            this.ckWeekLimit.Controls.Add(this.ckWeek6);
            this.ckWeekLimit.Controls.Add(this.ckWeek5);
            this.ckWeekLimit.Controls.Add(this.ckWeek4);
            this.ckWeekLimit.Controls.Add(this.ckWeek3);
            this.ckWeekLimit.Controls.Add(this.ckWeek2);
            this.ckWeekLimit.Controls.Add(this.ckWeek1);
            this.ckWeekLimit.Location = new System.Drawing.Point(13, 264);
            this.ckWeekLimit.Name = "ckWeekLimit";
            this.ckWeekLimit.Size = new System.Drawing.Size(585, 78);
            this.ckWeekLimit.TabIndex = 0;
            this.ckWeekLimit.TabStop = false;
            this.ckWeekLimit.Text = "周次限制";
            // 
            // ckWeek7
            // 
            this.ckWeek7.AutoSize = true;
            this.ckWeek7.Location = new System.Drawing.Point(509, 38);
            this.ckWeek7.Name = "ckWeek7";
            this.ckWeek7.Size = new System.Drawing.Size(59, 19);
            this.ckWeek7.TabIndex = 2;
            this.ckWeek7.Tag = "0";
            this.ckWeek7.Text = "周日";
            this.ckWeek7.UseVisualStyleBackColor = true;
            // 
            // ckWeek6
            // 
            this.ckWeek6.AutoSize = true;
            this.ckWeek6.Location = new System.Drawing.Point(427, 38);
            this.ckWeek6.Name = "ckWeek6";
            this.ckWeek6.Size = new System.Drawing.Size(59, 19);
            this.ckWeek6.TabIndex = 3;
            this.ckWeek6.Tag = "6";
            this.ckWeek6.Text = "周六";
            this.ckWeek6.UseVisualStyleBackColor = true;
            // 
            // ckWeek5
            // 
            this.ckWeek5.AutoSize = true;
            this.ckWeek5.Location = new System.Drawing.Point(347, 38);
            this.ckWeek5.Name = "ckWeek5";
            this.ckWeek5.Size = new System.Drawing.Size(59, 19);
            this.ckWeek5.TabIndex = 4;
            this.ckWeek5.Tag = "5";
            this.ckWeek5.Text = "周五";
            this.ckWeek5.UseVisualStyleBackColor = true;
            // 
            // ckWeek4
            // 
            this.ckWeek4.AutoSize = true;
            this.ckWeek4.Location = new System.Drawing.Point(266, 38);
            this.ckWeek4.Name = "ckWeek4";
            this.ckWeek4.Size = new System.Drawing.Size(59, 19);
            this.ckWeek4.TabIndex = 5;
            this.ckWeek4.Tag = "4";
            this.ckWeek4.Text = "周四";
            this.ckWeek4.UseVisualStyleBackColor = true;
            // 
            // ckWeek3
            // 
            this.ckWeek3.AutoSize = true;
            this.ckWeek3.Location = new System.Drawing.Point(190, 38);
            this.ckWeek3.Name = "ckWeek3";
            this.ckWeek3.Size = new System.Drawing.Size(59, 19);
            this.ckWeek3.TabIndex = 6;
            this.ckWeek3.Tag = "3";
            this.ckWeek3.Text = "周三";
            this.ckWeek3.UseVisualStyleBackColor = true;
            // 
            // ckWeek2
            // 
            this.ckWeek2.AutoSize = true;
            this.ckWeek2.Location = new System.Drawing.Point(112, 38);
            this.ckWeek2.Name = "ckWeek2";
            this.ckWeek2.Size = new System.Drawing.Size(59, 19);
            this.ckWeek2.TabIndex = 7;
            this.ckWeek2.Tag = "2";
            this.ckWeek2.Text = "周二";
            this.ckWeek2.UseVisualStyleBackColor = true;
            // 
            // ckWeek1
            // 
            this.ckWeek1.AutoSize = true;
            this.ckWeek1.Location = new System.Drawing.Point(26, 38);
            this.ckWeek1.Name = "ckWeek1";
            this.ckWeek1.Size = new System.Drawing.Size(59, 19);
            this.ckWeek1.TabIndex = 8;
            this.ckWeek1.Tag = "1";
            this.ckWeek1.Text = "周一";
            this.ckWeek1.UseVisualStyleBackColor = true;
            // 
            // ckPMLimit
            // 
            this.ckPMLimit.Controls.Add(this.txtPMEndTime);
            this.ckPMLimit.Controls.Add(this.txtPMStartTime);
            this.ckPMLimit.Controls.Add(this.txtPMTotal);
            this.ckPMLimit.Controls.Add(this.label3);
            this.ckPMLimit.Controls.Add(this.label1);
            this.ckPMLimit.Controls.Add(this.label2);
            this.ckPMLimit.Location = new System.Drawing.Point(13, 163);
            this.ckPMLimit.Name = "ckPMLimit";
            this.ckPMLimit.Size = new System.Drawing.Size(585, 85);
            this.ckPMLimit.TabIndex = 0;
            this.ckPMLimit.TabStop = false;
            this.ckPMLimit.Text = "下午时间限制 ";
            // 
            // txtPMEndTime
            // 
            this.txtPMEndTime.Location = new System.Drawing.Point(238, 37);
            this.txtPMEndTime.Name = "txtPMEndTime";
            this.txtPMEndTime.Size = new System.Drawing.Size(100, 25);
            this.txtPMEndTime.TabIndex = 215;
            // 
            // txtPMStartTime
            // 
            this.txtPMStartTime.Location = new System.Drawing.Point(71, 39);
            this.txtPMStartTime.Name = "txtPMStartTime";
            this.txtPMStartTime.Size = new System.Drawing.Size(100, 25);
            this.txtPMStartTime.TabIndex = 214;
            // 
            // txtPMTotal
            // 
            this.txtPMTotal.Location = new System.Drawing.Point(427, 38);
            this.txtPMTotal.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtPMTotal.Name = "txtPMTotal";
            this.txtPMTotal.Size = new System.Drawing.Size(120, 25);
            this.txtPMTotal.TabIndex = 210;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(363, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 213;
            this.label3.Text = "总票数:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 211;
            this.label1.Text = "开始:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 212;
            this.label2.Text = "结束:";
            // 
            // ckAMLimit
            // 
            this.ckAMLimit.Controls.Add(this.txtAMEndTime);
            this.ckAMLimit.Controls.Add(this.txtAMStartTime);
            this.ckAMLimit.Controls.Add(this.label13);
            this.ckAMLimit.Controls.Add(this.label12);
            this.ckAMLimit.Controls.Add(this.label11);
            this.ckAMLimit.Controls.Add(this.txtAMTotal);
            this.ckAMLimit.Location = new System.Drawing.Point(13, 59);
            this.ckAMLimit.Name = "ckAMLimit";
            this.ckAMLimit.Size = new System.Drawing.Size(585, 85);
            this.ckAMLimit.TabIndex = 0;
            this.ckAMLimit.TabStop = false;
            this.ckAMLimit.Text = "上午时间限制";
            // 
            // txtAMEndTime
            // 
            this.txtAMEndTime.Location = new System.Drawing.Point(238, 34);
            this.txtAMEndTime.Name = "txtAMEndTime";
            this.txtAMEndTime.Size = new System.Drawing.Size(100, 25);
            this.txtAMEndTime.TabIndex = 215;
            // 
            // txtAMStartTime
            // 
            this.txtAMStartTime.Location = new System.Drawing.Point(71, 36);
            this.txtAMStartTime.Name = "txtAMStartTime";
            this.txtAMStartTime.Size = new System.Drawing.Size(100, 25);
            this.txtAMStartTime.TabIndex = 214;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(363, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 15);
            this.label13.TabIndex = 213;
            this.label13.Text = "总票数:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(187, 39);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 15);
            this.label12.TabIndex = 212;
            this.label12.Text = "结束:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 15);
            this.label11.TabIndex = 211;
            this.label11.Text = "开始:";
            // 
            // txtAMTotal
            // 
            this.txtAMTotal.Location = new System.Drawing.Point(427, 35);
            this.txtAMTotal.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtAMTotal.Name = "txtAMTotal";
            this.txtAMTotal.Size = new System.Drawing.Size(120, 25);
            this.txtAMTotal.TabIndex = 210;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 507);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(613, 50);
            this.panel1.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnOk);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(395, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(218, 50);
            this.panel3.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(100, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(4, 7);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 30);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // ServiceTLimitDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 557);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceTLimitDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "业务时间限制";
            this.Load += new System.EventHandler(this.ServiceTLimitDialog_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ckSCardLimit.ResumeLayout(false);
            this.ckSCardLimit.PerformLayout();
            this.ckWeekLimit.ResumeLayout(false);
            this.ckWeekLimit.PerformLayout();
            this.ckPMLimit.ResumeLayout(false);
            this.ckPMLimit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPMTotal)).EndInit();
            this.ckAMLimit.ResumeLayout(false);
            this.ckAMLimit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAMTotal)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private CkGroupBoxEx ckWeekLimit;
        private System.Windows.Forms.CheckBox ckWeek7;
        private System.Windows.Forms.CheckBox ckWeek6;
        private System.Windows.Forms.CheckBox ckWeek5;
        private System.Windows.Forms.CheckBox ckWeek4;
        private System.Windows.Forms.CheckBox ckWeek3;
        private System.Windows.Forms.CheckBox ckWeek2;
        private System.Windows.Forms.CheckBox ckWeek1;
        private CkGroupBoxEx ckPMLimit;
        private System.Windows.Forms.TextBox txtPMEndTime;
        private System.Windows.Forms.TextBox txtPMStartTime;
        private System.Windows.Forms.NumericUpDown txtPMTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private CkGroupBoxEx ckAMLimit;
        private System.Windows.Forms.TextBox txtAMEndTime;
        private System.Windows.Forms.TextBox txtAMStartTime;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown txtAMTotal;
        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.Label label4;
        private CkGroupBoxEx ckSCardLimit;
        private System.Windows.Forms.TextBox txtSCardInterval;
        private System.Windows.Forms.TextBox txtSCardNum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox ckIsOne;
        private System.Windows.Forms.Label label5;
    }
}