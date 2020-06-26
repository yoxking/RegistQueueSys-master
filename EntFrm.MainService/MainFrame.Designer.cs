namespace EntFrm.MainService
{
    partial class MainFrame
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrame));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lbRegInfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRecoverDb = new System.Windows.Forms.Button();
            this.btnBakupDb = new System.Windows.Forms.Button();
            this.btnClearFlowsData = new System.Windows.Forms.Button();
            this.btnClearAllData = new System.Windows.Forms.Button();
            this.btnDbsetting = new System.Windows.Forms.Button();
            this.btnSysetting = new System.Windows.Forms.Button();
            this.btnClearlog = new System.Windows.Forms.Button();
            this.btnBrowselog = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.myTimer = new System.Windows.Forms.Timer(this.components);
            this.myContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.myContextMenu.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(10, 10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(20, 5);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(572, 583);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel4);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(564, 545);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "操作日志";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(558, 539);
            this.panel4.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtResult);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox2.Size = new System.Drawing.Size(558, 539);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "日志";
            // 
            // txtResult
            // 
            this.txtResult.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Location = new System.Drawing.Point(10, 28);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(538, 501);
            this.txtResult.TabIndex = 0;
            this.txtResult.TextChanged += new System.EventHandler(this.txtResult_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(564, 545);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "基本操作";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(558, 539);
            this.panel1.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnRegister);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.lbRegInfo);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(8, 324);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(545, 162);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "关于软件";
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(331, 35);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(130, 30);
            this.btnRegister.TabIndex = 1;
            this.btnRegister.Text = "注册软件";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "版本信息：v3.30";
            // 
            // lbRegInfo
            // 
            this.lbRegInfo.AutoSize = true;
            this.lbRegInfo.Location = new System.Drawing.Point(26, 41);
            this.lbRegInfo.Name = "lbRegInfo";
            this.lbRegInfo.Size = new System.Drawing.Size(142, 15);
            this.lbRegInfo.TabIndex = 0;
            this.lbRegInfo.Text = "产品授权：正式版本";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "版权所有：©2019 中灏科技 ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRecoverDb);
            this.groupBox1.Controls.Add(this.btnBakupDb);
            this.groupBox1.Controls.Add(this.btnClearFlowsData);
            this.groupBox1.Controls.Add(this.btnClearAllData);
            this.groupBox1.Controls.Add(this.btnDbsetting);
            this.groupBox1.Controls.Add(this.btnSysetting);
            this.groupBox1.Controls.Add(this.btnClearlog);
            this.groupBox1.Controls.Add(this.btnBrowselog);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(8, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(545, 279);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作";
            // 
            // btnRecoverDb
            // 
            this.btnRecoverDb.Location = new System.Drawing.Point(176, 148);
            this.btnRecoverDb.Name = "btnRecoverDb";
            this.btnRecoverDb.Size = new System.Drawing.Size(130, 45);
            this.btnRecoverDb.TabIndex = 0;
            this.btnRecoverDb.Text = "还原数据";
            this.btnRecoverDb.UseVisualStyleBackColor = true;
            this.btnRecoverDb.Click += new System.EventHandler(this.btnRecoverDb_Click);
            // 
            // btnBakupDb
            // 
            this.btnBakupDb.Location = new System.Drawing.Point(23, 148);
            this.btnBakupDb.Name = "btnBakupDb";
            this.btnBakupDb.Size = new System.Drawing.Size(130, 45);
            this.btnBakupDb.TabIndex = 0;
            this.btnBakupDb.Text = "备份数据";
            this.btnBakupDb.UseVisualStyleBackColor = true;
            this.btnBakupDb.Click += new System.EventHandler(this.btnBakupDb_Click);
            // 
            // btnClearFlowsData
            // 
            this.btnClearFlowsData.Location = new System.Drawing.Point(176, 88);
            this.btnClearFlowsData.Name = "btnClearFlowsData";
            this.btnClearFlowsData.Size = new System.Drawing.Size(130, 45);
            this.btnClearFlowsData.TabIndex = 0;
            this.btnClearFlowsData.Text = "清空营业数据";
            this.btnClearFlowsData.UseVisualStyleBackColor = true;
            this.btnClearFlowsData.Click += new System.EventHandler(this.btnClearFlowsData_Click);
            // 
            // btnClearAllData
            // 
            this.btnClearAllData.Location = new System.Drawing.Point(23, 88);
            this.btnClearAllData.Name = "btnClearAllData";
            this.btnClearAllData.Size = new System.Drawing.Size(130, 45);
            this.btnClearAllData.TabIndex = 0;
            this.btnClearAllData.Text = "清空所有数据";
            this.btnClearAllData.UseVisualStyleBackColor = true;
            this.btnClearAllData.Click += new System.EventHandler(this.btnClearAllData_Click);
            // 
            // btnDbsetting
            // 
            this.btnDbsetting.Location = new System.Drawing.Point(176, 28);
            this.btnDbsetting.Name = "btnDbsetting";
            this.btnDbsetting.Size = new System.Drawing.Size(130, 45);
            this.btnDbsetting.TabIndex = 0;
            this.btnDbsetting.Text = "参数设置";
            this.btnDbsetting.UseVisualStyleBackColor = true;
            this.btnDbsetting.Click += new System.EventHandler(this.btnDbsetting_Click);
            // 
            // btnSysetting
            // 
            this.btnSysetting.Location = new System.Drawing.Point(23, 28);
            this.btnSysetting.Name = "btnSysetting";
            this.btnSysetting.Size = new System.Drawing.Size(130, 45);
            this.btnSysetting.TabIndex = 0;
            this.btnSysetting.Text = "排队设置";
            this.btnSysetting.UseVisualStyleBackColor = true;
            this.btnSysetting.Click += new System.EventHandler(this.btnSysetting_Click);
            // 
            // btnClearlog
            // 
            this.btnClearlog.Location = new System.Drawing.Point(176, 209);
            this.btnClearlog.Name = "btnClearlog";
            this.btnClearlog.Size = new System.Drawing.Size(130, 45);
            this.btnClearlog.TabIndex = 0;
            this.btnClearlog.Text = "清空日志";
            this.btnClearlog.UseVisualStyleBackColor = true;
            this.btnClearlog.Click += new System.EventHandler(this.btnClearlog_Click);
            // 
            // btnBrowselog
            // 
            this.btnBrowselog.Location = new System.Drawing.Point(23, 209);
            this.btnBrowselog.Name = "btnBrowselog";
            this.btnBrowselog.Size = new System.Drawing.Size(130, 45);
            this.btnBrowselog.TabIndex = 0;
            this.btnBrowselog.Text = "查看日志";
            this.btnBrowselog.UseVisualStyleBackColor = true;
            this.btnBrowselog.Click += new System.EventHandler(this.btnBrowselog_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(331, 28);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(130, 45);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "退出服务";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // myTimer
            // 
            this.myTimer.Interval = 10000;
            this.myTimer.Tick += new System.EventHandler(this.myTimer_Tick);
            // 
            // myContextMenu
            // 
            this.myContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.myContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMenuItem,
            this.exitMenuItem});
            this.myContextMenu.Name = "myContextMenu";
            this.myContextMenu.Size = new System.Drawing.Size(154, 52);
            // 
            // showMenuItem
            // 
            this.showMenuItem.Name = "showMenuItem";
            this.showMenuItem.Size = new System.Drawing.Size(153, 24);
            this.showMenuItem.Text = "打开主服务";
            this.showMenuItem.Click += new System.EventHandler(this.showMenuItem_Click);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(153, 24);
            this.exitMenuItem.Text = "退出主服务";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // myNotifyIcon
            // 
            this.myNotifyIcon.ContextMenuStrip = this.myContextMenu;
            this.myNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("myNotifyIcon.Icon")));
            this.myNotifyIcon.Text = "主服务程序";
            this.myNotifyIcon.Visible = true;
            this.myNotifyIcon.DoubleClick += new System.EventHandler(this.myNotifyIcon_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10);
            this.panel2.Size = new System.Drawing.Size(592, 603);
            this.panel2.TabIndex = 6;
            // 
            // MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 603);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainFrame";
            this.Text = "主服务程序";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainFrame_FormClosing);
            this.Load += new System.EventHandler(this.MainFrame_Load);
            this.SizeChanged += new System.EventHandler(this.frmMainFrame_SizeChanged);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.myContextMenu.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbRegInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRecoverDb;
        private System.Windows.Forms.Button btnBakupDb;
        private System.Windows.Forms.Button btnClearFlowsData;
        private System.Windows.Forms.Button btnClearAllData;
        private System.Windows.Forms.Button btnDbsetting;
        private System.Windows.Forms.Button btnSysetting;
        private System.Windows.Forms.Button btnClearlog;
        private System.Windows.Forms.Button btnBrowselog;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Timer myTimer;
        private System.Windows.Forms.ContextMenuStrip myContextMenu;
        private System.Windows.Forms.ToolStripMenuItem showMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.NotifyIcon myNotifyIcon;
        private System.Windows.Forms.Panel panel2;
    }
}