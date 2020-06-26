namespace EntFrm.CallerConsole
{
    partial class frmMainFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainFrame));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.myNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.myContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnShow = new System.Windows.Forms.ToolStripMenuItem();
            this.btnQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabPages = new System.Windows.Forms.TabControl();
            this.tbQueuingList = new System.Windows.Forms.TabPage();
            this.dgQueuingList = new System.Windows.Forms.DataGridView();
            this.qPFlowNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTicketNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qProcessState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qEnqueueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbPassedList = new System.Windows.Forms.TabPage();
            this.dgPassedList = new System.Windows.Forms.DataGridView();
            this.pPFlowNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pTicketNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pProcessState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pProcessDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbFinishedList = new System.Windows.Forms.TabPage();
            this.dgFinishedList = new System.Windows.Forms.DataGridView();
            this.fPFlowNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fTicketNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fProcessState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fProcessDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnMinimal = new System.Windows.Forms.Button();
            this.lbMessage = new System.Windows.Forms.Label();
            this.btnQuitCaller = new System.Windows.Forms.Button();
            this.btnCallHelper = new System.Windows.Forms.Button();
            this.btnFinishFlow = new System.Windows.Forms.Button();
            this.btnPassFlow = new System.Windows.Forms.Button();
            this.btnCallRepeat = new System.Windows.Forms.Button();
            this.btnCallNext = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbWaiterNum = new System.Windows.Forms.Label();
            this.lbStafferName = new System.Windows.Forms.Label();
            this.lbTicketNo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.myTimer = new System.Windows.Forms.Timer(this.components);
            this.myQueuingMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.QMenu_Goto = new System.Windows.Forms.ToolStripMenuItem();
            this.QMenu_Notcome = new System.Windows.Forms.ToolStripMenuItem();
            this.myPassedMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.myFinishedMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PMenu_Goto = new System.Windows.Forms.ToolStripMenuItem();
            this.QMenu_Rediag = new System.Windows.Forms.ToolStripMenuItem();
            this.FMenu_Goto = new System.Windows.Forms.ToolStripMenuItem();
            this.FMenu_Rediag = new System.Windows.Forms.ToolStripMenuItem();
            this.myContextMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPages.SuspendLayout();
            this.tbQueuingList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgQueuingList)).BeginInit();
            this.tbPassedList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPassedList)).BeginInit();
            this.tbFinishedList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFinishedList)).BeginInit();
            this.myQueuingMenu.SuspendLayout();
            this.myPassedMenu.SuspendLayout();
            this.myFinishedMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // myNotifyIcon
            // 
            this.myNotifyIcon.ContextMenuStrip = this.myContextMenu;
            this.myNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("myNotifyIcon.Icon")));
            this.myNotifyIcon.Text = "虚拟呼叫器";
            this.myNotifyIcon.Visible = true;
            this.myNotifyIcon.DoubleClick += new System.EventHandler(this.myNotifyIcon_DoubleClick);
            // 
            // myContextMenu
            // 
            this.myContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.myContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSetting,
            this.toolStripSeparator1,
            this.btnShow,
            this.btnQuit});
            this.myContextMenu.Name = "myContextMenu";
            this.myContextMenu.Size = new System.Drawing.Size(154, 82);
            // 
            // btnSetting
            // 
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(153, 24);
            this.btnSetting.Text = "系统设置";
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
            // 
            // btnShow
            // 
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(153, 24);
            this.btnShow.Text = "打开呼叫器";
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(153, 24);
            this.btnQuit.Text = "退出呼叫器";
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabPages);
            this.panel1.Controls.Add(this.btnMinimal);
            this.panel1.Controls.Add(this.lbMessage);
            this.panel1.Controls.Add(this.btnQuitCaller);
            this.panel1.Controls.Add(this.btnCallHelper);
            this.panel1.Controls.Add(this.btnFinishFlow);
            this.panel1.Controls.Add(this.btnPassFlow);
            this.panel1.Controls.Add(this.btnCallRepeat);
            this.panel1.Controls.Add(this.btnCallNext);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbWaiterNum);
            this.panel1.Controls.Add(this.lbStafferName);
            this.panel1.Controls.Add(this.lbTicketNo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(392, 725);
            this.panel1.TabIndex = 53;
            // 
            // tabPages
            // 
            this.tabPages.Controls.Add(this.tbQueuingList);
            this.tabPages.Controls.Add(this.tbPassedList);
            this.tabPages.Controls.Add(this.tbFinishedList);
            this.tabPages.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPages.Location = new System.Drawing.Point(14, 303);
            this.tabPages.Name = "tabPages";
            this.tabPages.SelectedIndex = 0;
            this.tabPages.Size = new System.Drawing.Size(366, 369);
            this.tabPages.TabIndex = 57;
            this.tabPages.SelectedIndexChanged += new System.EventHandler(this.tabPages_SelectedIndexChanged);
            // 
            // tbQueuingList
            // 
            this.tbQueuingList.Controls.Add(this.dgQueuingList);
            this.tbQueuingList.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbQueuingList.Location = new System.Drawing.Point(4, 30);
            this.tbQueuingList.Name = "tbQueuingList";
            this.tbQueuingList.Padding = new System.Windows.Forms.Padding(3);
            this.tbQueuingList.Size = new System.Drawing.Size(358, 335);
            this.tbQueuingList.TabIndex = 0;
            this.tbQueuingList.Text = "等候中(0)";
            this.tbQueuingList.UseVisualStyleBackColor = true;
            // 
            // dgQueuingList
            // 
            this.dgQueuingList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgQueuingList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgQueuingList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgQueuingList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.qPFlowNo,
            this.qTicketNo,
            this.qUserName,
            this.qProcessState,
            this.qEnqueueDate});
            this.dgQueuingList.ContextMenuStrip = this.myQueuingMenu;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgQueuingList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgQueuingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgQueuingList.Location = new System.Drawing.Point(3, 3);
            this.dgQueuingList.MultiSelect = false;
            this.dgQueuingList.Name = "dgQueuingList";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgQueuingList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgQueuingList.RowHeadersVisible = false;
            this.dgQueuingList.RowTemplate.Height = 27;
            this.dgQueuingList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgQueuingList.Size = new System.Drawing.Size(352, 329);
            this.dgQueuingList.TabIndex = 0;
            // 
            // qPFlowNo
            // 
            this.qPFlowNo.DataPropertyName = "PFlowNo";
            this.qPFlowNo.HeaderText = "ID";
            this.qPFlowNo.Name = "qPFlowNo";
            this.qPFlowNo.ReadOnly = true;
            this.qPFlowNo.Visible = false;
            // 
            // qTicketNo
            // 
            this.qTicketNo.DataPropertyName = "TicketNo";
            this.qTicketNo.HeaderText = "票号";
            this.qTicketNo.Name = "qTicketNo";
            this.qTicketNo.ReadOnly = true;
            this.qTicketNo.Width = 60;
            // 
            // qUserName
            // 
            this.qUserName.DataPropertyName = "UserName";
            this.qUserName.HeaderText = "姓名";
            this.qUserName.Name = "qUserName";
            this.qUserName.ReadOnly = true;
            this.qUserName.Width = 60;
            // 
            // qProcessState
            // 
            this.qProcessState.DataPropertyName = "ProcessState";
            this.qProcessState.HeaderText = "状态";
            this.qProcessState.Name = "qProcessState";
            this.qProcessState.ReadOnly = true;
            this.qProcessState.Width = 60;
            // 
            // qEnqueueDate
            // 
            this.qEnqueueDate.DataPropertyName = "EnqueueDate";
            this.qEnqueueDate.HeaderText = "挂号时间";
            this.qEnqueueDate.Name = "qEnqueueDate";
            this.qEnqueueDate.ReadOnly = true;
            this.qEnqueueDate.Width = 80;
            // 
            // tbPassedList
            // 
            this.tbPassedList.Controls.Add(this.dgPassedList);
            this.tbPassedList.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPassedList.Location = new System.Drawing.Point(4, 30);
            this.tbPassedList.Name = "tbPassedList";
            this.tbPassedList.Padding = new System.Windows.Forms.Padding(3);
            this.tbPassedList.Size = new System.Drawing.Size(358, 335);
            this.tbPassedList.TabIndex = 1;
            this.tbPassedList.Text = "过号(0)";
            this.tbPassedList.UseVisualStyleBackColor = true;
            // 
            // dgPassedList
            // 
            this.dgPassedList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPassedList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgPassedList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPassedList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pPFlowNo,
            this.pTicketNo,
            this.pUserName,
            this.pProcessState,
            this.pProcessDate});
            this.dgPassedList.ContextMenuStrip = this.myPassedMenu;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPassedList.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgPassedList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPassedList.Location = new System.Drawing.Point(3, 3);
            this.dgPassedList.MultiSelect = false;
            this.dgPassedList.Name = "dgPassedList";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPassedList.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgPassedList.RowHeadersVisible = false;
            this.dgPassedList.RowTemplate.Height = 27;
            this.dgPassedList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPassedList.Size = new System.Drawing.Size(352, 329);
            this.dgPassedList.TabIndex = 1;
            // 
            // pPFlowNo
            // 
            this.pPFlowNo.DataPropertyName = "PFlowNo";
            this.pPFlowNo.HeaderText = "ID";
            this.pPFlowNo.Name = "pPFlowNo";
            this.pPFlowNo.ReadOnly = true;
            this.pPFlowNo.Visible = false;
            // 
            // pTicketNo
            // 
            this.pTicketNo.DataPropertyName = "TicketNo";
            this.pTicketNo.HeaderText = "票号";
            this.pTicketNo.Name = "pTicketNo";
            this.pTicketNo.ReadOnly = true;
            this.pTicketNo.Width = 60;
            // 
            // pUserName
            // 
            this.pUserName.DataPropertyName = "UserName";
            this.pUserName.HeaderText = "姓名";
            this.pUserName.Name = "pUserName";
            this.pUserName.ReadOnly = true;
            this.pUserName.Width = 60;
            // 
            // pProcessState
            // 
            this.pProcessState.DataPropertyName = "ProcessState";
            this.pProcessState.HeaderText = "状态";
            this.pProcessState.Name = "pProcessState";
            this.pProcessState.ReadOnly = true;
            this.pProcessState.Width = 60;
            // 
            // pProcessDate
            // 
            this.pProcessDate.DataPropertyName = "ProcessDate";
            this.pProcessDate.HeaderText = "过号时间";
            this.pProcessDate.Name = "pProcessDate";
            this.pProcessDate.ReadOnly = true;
            this.pProcessDate.Width = 80;
            // 
            // tbFinishedList
            // 
            this.tbFinishedList.Controls.Add(this.dgFinishedList);
            this.tbFinishedList.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbFinishedList.Location = new System.Drawing.Point(4, 30);
            this.tbFinishedList.Name = "tbFinishedList";
            this.tbFinishedList.Size = new System.Drawing.Size(358, 335);
            this.tbFinishedList.TabIndex = 2;
            this.tbFinishedList.Text = "完成(0)";
            this.tbFinishedList.UseVisualStyleBackColor = true;
            // 
            // dgFinishedList
            // 
            this.dgFinishedList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgFinishedList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgFinishedList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFinishedList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fPFlowNo,
            this.fTicketNo,
            this.fUserName,
            this.fProcessState,
            this.fProcessDate});
            this.dgFinishedList.ContextMenuStrip = this.myFinishedMenu;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgFinishedList.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgFinishedList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgFinishedList.Location = new System.Drawing.Point(0, 0);
            this.dgFinishedList.MultiSelect = false;
            this.dgFinishedList.Name = "dgFinishedList";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgFinishedList.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgFinishedList.RowHeadersVisible = false;
            this.dgFinishedList.RowTemplate.Height = 27;
            this.dgFinishedList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFinishedList.Size = new System.Drawing.Size(358, 335);
            this.dgFinishedList.TabIndex = 2;
            // 
            // fPFlowNo
            // 
            this.fPFlowNo.DataPropertyName = "PFlowNo";
            this.fPFlowNo.HeaderText = "ID";
            this.fPFlowNo.Name = "fPFlowNo";
            this.fPFlowNo.ReadOnly = true;
            this.fPFlowNo.Visible = false;
            // 
            // fTicketNo
            // 
            this.fTicketNo.DataPropertyName = "TicketNo";
            this.fTicketNo.HeaderText = "票号";
            this.fTicketNo.Name = "fTicketNo";
            this.fTicketNo.ReadOnly = true;
            this.fTicketNo.Width = 60;
            // 
            // fUserName
            // 
            this.fUserName.DataPropertyName = "UserName";
            this.fUserName.HeaderText = "姓名";
            this.fUserName.Name = "fUserName";
            this.fUserName.ReadOnly = true;
            this.fUserName.Width = 60;
            // 
            // fProcessState
            // 
            this.fProcessState.DataPropertyName = "ProcessState";
            this.fProcessState.HeaderText = "状态";
            this.fProcessState.Name = "fProcessState";
            this.fProcessState.ReadOnly = true;
            this.fProcessState.Width = 60;
            // 
            // fProcessDate
            // 
            this.fProcessDate.DataPropertyName = "ProcessDate";
            this.fProcessDate.HeaderText = "完成时间";
            this.fProcessDate.Name = "fProcessDate";
            this.fProcessDate.ReadOnly = true;
            this.fProcessDate.Width = 80;
            // 
            // btnMinimal
            // 
            this.btnMinimal.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMinimal.Location = new System.Drawing.Point(338, 25);
            this.btnMinimal.Name = "btnMinimal";
            this.btnMinimal.Size = new System.Drawing.Size(33, 33);
            this.btnMinimal.TabIndex = 54;
            this.btnMinimal.Text = "<<";
            this.btnMinimal.UseVisualStyleBackColor = true;
            this.btnMinimal.Click += new System.EventHandler(this.btnMinimal_Click);
            // 
            // lbMessage
            // 
            this.lbMessage.AutoSize = true;
            this.lbMessage.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMessage.ForeColor = System.Drawing.Color.Green;
            this.lbMessage.Location = new System.Drawing.Point(12, 686);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(139, 20);
            this.lbMessage.TabIndex = 27;
            this.lbMessage.Text = "等待呼叫中...";
            // 
            // btnQuitCaller
            // 
            this.btnQuitCaller.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuitCaller.Location = new System.Drawing.Point(196, 237);
            this.btnQuitCaller.Name = "btnQuitCaller";
            this.btnQuitCaller.Size = new System.Drawing.Size(175, 40);
            this.btnQuitCaller.TabIndex = 24;
            this.btnQuitCaller.Text = "退出呼叫器";
            this.btnQuitCaller.UseVisualStyleBackColor = true;
            this.btnQuitCaller.Click += new System.EventHandler(this.btnQuitCaller_Click);
            // 
            // btnCallHelper
            // 
            this.btnCallHelper.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCallHelper.Location = new System.Drawing.Point(18, 237);
            this.btnCallHelper.Name = "btnCallHelper";
            this.btnCallHelper.Size = new System.Drawing.Size(175, 40);
            this.btnCallHelper.TabIndex = 24;
            this.btnCallHelper.Text = "呼叫护士";
            this.btnCallHelper.UseVisualStyleBackColor = true;
            this.btnCallHelper.Click += new System.EventHandler(this.btnCallHelper_Click);
            // 
            // btnFinishFlow
            // 
            this.btnFinishFlow.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFinishFlow.Location = new System.Drawing.Point(196, 191);
            this.btnFinishFlow.Name = "btnFinishFlow";
            this.btnFinishFlow.Size = new System.Drawing.Size(175, 40);
            this.btnFinishFlow.TabIndex = 23;
            this.btnFinishFlow.Text = "就诊完成";
            this.btnFinishFlow.UseVisualStyleBackColor = true;
            this.btnFinishFlow.Click += new System.EventHandler(this.btnFinishFlow_Click);
            // 
            // btnPassFlow
            // 
            this.btnPassFlow.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPassFlow.Location = new System.Drawing.Point(18, 191);
            this.btnPassFlow.Name = "btnPassFlow";
            this.btnPassFlow.Size = new System.Drawing.Size(175, 40);
            this.btnPassFlow.TabIndex = 21;
            this.btnPassFlow.Text = "未到过号";
            this.btnPassFlow.UseVisualStyleBackColor = true;
            this.btnPassFlow.Click += new System.EventHandler(this.btnPassFlow_Click);
            // 
            // btnCallRepeat
            // 
            this.btnCallRepeat.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCallRepeat.Location = new System.Drawing.Point(18, 145);
            this.btnCallRepeat.Name = "btnCallRepeat";
            this.btnCallRepeat.Size = new System.Drawing.Size(175, 40);
            this.btnCallRepeat.TabIndex = 14;
            this.btnCallRepeat.Text = "重复呼叫";
            this.btnCallRepeat.UseVisualStyleBackColor = true;
            this.btnCallRepeat.Click += new System.EventHandler(this.btnCallRepeat_Click);
            // 
            // btnCallNext
            // 
            this.btnCallNext.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCallNext.Location = new System.Drawing.Point(196, 145);
            this.btnCallNext.Name = "btnCallNext";
            this.btnCallNext.Size = new System.Drawing.Size(175, 40);
            this.btnCallNext.TabIndex = 13;
            this.btnCallNext.Text = "呼叫下一位";
            this.btnCallNext.UseVisualStyleBackColor = true;
            this.btnCallNext.Click += new System.EventHandler(this.btnCallNext_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(18, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "等候人数:";
            // 
            // lbWaiterNum
            // 
            this.lbWaiterNum.AutoSize = true;
            this.lbWaiterNum.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbWaiterNum.ForeColor = System.Drawing.Color.Red;
            this.lbWaiterNum.Location = new System.Drawing.Point(142, 98);
            this.lbWaiterNum.Name = "lbWaiterNum";
            this.lbWaiterNum.Size = new System.Drawing.Size(20, 20);
            this.lbWaiterNum.TabIndex = 10;
            this.lbWaiterNum.Text = "0";
            // 
            // lbStafferName
            // 
            this.lbStafferName.AutoSize = true;
            this.lbStafferName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStafferName.ForeColor = System.Drawing.Color.Green;
            this.lbStafferName.Location = new System.Drawing.Point(142, 30);
            this.lbStafferName.Name = "lbStafferName";
            this.lbStafferName.Size = new System.Drawing.Size(42, 20);
            this.lbStafferName.TabIndex = 9;
            this.lbStafferName.Text = "N/A";
            // 
            // lbTicketNo
            // 
            this.lbTicketNo.AutoSize = true;
            this.lbTicketNo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTicketNo.ForeColor = System.Drawing.Color.Red;
            this.lbTicketNo.Location = new System.Drawing.Point(142, 63);
            this.lbTicketNo.Name = "lbTicketNo";
            this.lbTicketNo.Size = new System.Drawing.Size(42, 20);
            this.lbTicketNo.TabIndex = 8;
            this.lbTicketNo.Text = "N/A";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(15, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "当前医生:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(15, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "当前病人:";
            // 
            // myTimer
            // 
            this.myTimer.Interval = 3000;
            this.myTimer.Tick += new System.EventHandler(this.myTimer_Tick);
            // 
            // myQueuingMenu
            // 
            this.myQueuingMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.myQueuingMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.QMenu_Goto,
            this.QMenu_Notcome});
            this.myQueuingMenu.Name = "myQueuingMenu";
            this.myQueuingMenu.Size = new System.Drawing.Size(109, 52);
            // 
            // QMenu_Goto
            // 
            this.QMenu_Goto.Name = "QMenu_Goto";
            this.QMenu_Goto.Size = new System.Drawing.Size(108, 24);
            this.QMenu_Goto.Text = "呼叫";
            this.QMenu_Goto.Click += new System.EventHandler(this.btnMenuGoto_Click);
            // 
            // QMenu_Notcome
            // 
            this.QMenu_Notcome.Name = "QMenu_Notcome";
            this.QMenu_Notcome.Size = new System.Drawing.Size(108, 24);
            this.QMenu_Notcome.Text = "过号";
            this.QMenu_Notcome.Click += new System.EventHandler(this.btnMenuPass_Click);
            // 
            // myPassedMenu
            // 
            this.myPassedMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.myPassedMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PMenu_Goto,
            this.QMenu_Rediag});
            this.myPassedMenu.Name = "myPassedMenu";
            this.myPassedMenu.Size = new System.Drawing.Size(109, 52);
            // 
            // myFinishedMenu
            // 
            this.myFinishedMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.myFinishedMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FMenu_Goto,
            this.FMenu_Rediag});
            this.myFinishedMenu.Name = "myFinishedMenu";
            this.myFinishedMenu.Size = new System.Drawing.Size(109, 52);
            // 
            // PMenu_Goto
            // 
            this.PMenu_Goto.Name = "PMenu_Goto";
            this.PMenu_Goto.Size = new System.Drawing.Size(108, 24);
            this.PMenu_Goto.Text = "呼叫";
            this.PMenu_Goto.Click += new System.EventHandler(this.btnMenuGoto_Click);
            // 
            // QMenu_Rediag
            // 
            this.QMenu_Rediag.Name = "QMenu_Rediag";
            this.QMenu_Rediag.Size = new System.Drawing.Size(108, 24);
            this.QMenu_Rediag.Text = "复诊";
            this.QMenu_Rediag.Click += new System.EventHandler(this.btnMenuRediag_Click);
            // 
            // FMenu_Goto
            // 
            this.FMenu_Goto.Name = "FMenu_Goto";
            this.FMenu_Goto.Size = new System.Drawing.Size(108, 24);
            this.FMenu_Goto.Text = "呼叫";
            this.FMenu_Goto.Click += new System.EventHandler(this.btnMenuGoto_Click);
            // 
            // FMenu_Rediag
            // 
            this.FMenu_Rediag.Name = "FMenu_Rediag";
            this.FMenu_Rediag.Size = new System.Drawing.Size(108, 24);
            this.FMenu_Rediag.Text = "复诊";
            this.FMenu_Rediag.Click += new System.EventHandler(this.btnMenuRediag_Click);
            // 
            // frmMainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 725);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMainFrame";
            this.Text = "虚拟呼叫器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainFrame_FormClosing);
            this.Load += new System.EventHandler(this.frmMainFrame_Load);
            this.LocationChanged += new System.EventHandler(this.frmMainFrame_LocationChanged);
            this.SizeChanged += new System.EventHandler(this.frmMainFrame_SizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmMainFrame_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMainFrame_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmMainFrame_MouseUp);
            this.myContextMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPages.ResumeLayout(false);
            this.tbQueuingList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgQueuingList)).EndInit();
            this.tbPassedList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPassedList)).EndInit();
            this.tbFinishedList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgFinishedList)).EndInit();
            this.myQueuingMenu.ResumeLayout(false);
            this.myPassedMenu.ResumeLayout(false);
            this.myFinishedMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon myNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip myContextMenu;
        private System.Windows.Forms.ToolStripMenuItem btnShow;
        private System.Windows.Forms.ToolStripMenuItem btnQuit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem btnSetting;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.Button btnCallHelper;
        private System.Windows.Forms.Button btnFinishFlow;
        private System.Windows.Forms.Button btnPassFlow;
        private System.Windows.Forms.Button btnCallRepeat;
        private System.Windows.Forms.Button btnCallNext;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbWaiterNum;
        private System.Windows.Forms.Label lbStafferName;
        private System.Windows.Forms.Label lbTicketNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnQuitCaller;
        private System.Windows.Forms.Timer myTimer;
        private System.Windows.Forms.Button btnMinimal;
        private System.Windows.Forms.TabControl tabPages;
        private System.Windows.Forms.TabPage tbQueuingList;
        private System.Windows.Forms.TabPage tbPassedList;
        private System.Windows.Forms.TabPage tbFinishedList;
        private System.Windows.Forms.DataGridView dgQueuingList;
        private System.Windows.Forms.DataGridView dgPassedList;
        private System.Windows.Forms.DataGridView dgFinishedList;
        private System.Windows.Forms.DataGridViewTextBoxColumn qPFlowNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTicketNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn qUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn qProcessState;
        private System.Windows.Forms.DataGridViewTextBoxColumn qEnqueueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn pPFlowNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn pTicketNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn pUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn pProcessState;
        private System.Windows.Forms.DataGridViewTextBoxColumn pProcessDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn fPFlowNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn fTicketNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn fUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn fProcessState;
        private System.Windows.Forms.DataGridViewTextBoxColumn fProcessDate;
        private System.Windows.Forms.ContextMenuStrip myQueuingMenu;
        private System.Windows.Forms.ToolStripMenuItem QMenu_Goto;
        private System.Windows.Forms.ToolStripMenuItem QMenu_Notcome;
        private System.Windows.Forms.ContextMenuStrip myPassedMenu;
        private System.Windows.Forms.ToolStripMenuItem PMenu_Goto;
        private System.Windows.Forms.ToolStripMenuItem QMenu_Rediag;
        private System.Windows.Forms.ContextMenuStrip myFinishedMenu;
        private System.Windows.Forms.ToolStripMenuItem FMenu_Goto;
        private System.Windows.Forms.ToolStripMenuItem FMenu_Rediag;
    }
}