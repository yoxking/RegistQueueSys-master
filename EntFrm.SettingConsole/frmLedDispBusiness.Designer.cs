using EntFrm.Framework.Utility;

namespace EntFrm.SettingConsole
{
    partial class frmLedDispBusiness
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbTxtFormat = new System.Windows.Forms.TabControl();
            this.tabDisplayFormat = new System.Windows.Forms.TabPage();
            this.txtDisplayFormat = new System.Windows.Forms.TextBox();
            this.tabPowerOnTip = new System.Windows.Forms.TabPage();
            this.txtPowerOnTip = new System.Windows.Forms.TextBox();
            this.tabInServiceTip = new System.Windows.Forms.TabPage();
            this.txtInServiceTip = new System.Windows.Forms.TextBox();
            this.tabOnPauseTip = new System.Windows.Forms.TabPage();
            this.txtOnPauseTip = new System.Windows.Forms.TextBox();
            this.tabTimeoutTip = new System.Windows.Forms.TabPage();
            this.txtTimeoutTip = new System.Windows.Forms.TextBox();
            this.txtScreenHeight = new System.Windows.Forms.TextBox();
            this.txtScreenWidth = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtTimeoutSec = new System.Windows.Forms.TextBox();
            this.txtDisplayLength = new System.Windows.Forms.TextBox();
            this.txtFontSize = new System.Windows.Forms.TextBox();
            this.ckNetProtocol = new EntFrm.Framework.Utility.CkGroupBoxEx();
            this.txtLocalPort = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.ckComProtocol = new EntFrm.Framework.Utility.CkGroupBoxEx();
            this.txtBaudrate = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dpSerialPort = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dpVariables = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtPosY = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPosX = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnAddVar = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnUptAll = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dpFontAlign = new System.Windows.Forms.ComboBox();
            this.dpLedModel = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.txtPhyAddr = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.InfoList = new System.Windows.Forms.DataGridView();
            this.DisplayNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhyAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DisplayLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tbTxtFormat.SuspendLayout();
            this.tabDisplayFormat.SuspendLayout();
            this.tabPowerOnTip.SuspendLayout();
            this.tabInServiceTip.SuspendLayout();
            this.tabOnPauseTip.SuspendLayout();
            this.tabTimeoutTip.SuspendLayout();
            this.ckNetProtocol.SuspendLayout();
            this.ckComProtocol.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InfoList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(725, 705);
            this.panel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbTxtFormat);
            this.groupBox1.Controls.Add(this.txtScreenHeight);
            this.groupBox1.Controls.Add(this.txtScreenWidth);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txtTimeoutSec);
            this.groupBox1.Controls.Add(this.txtDisplayLength);
            this.groupBox1.Controls.Add(this.txtFontSize);
            this.groupBox1.Controls.Add(this.ckNetProtocol);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.ckComProtocol);
            this.groupBox1.Controls.Add(this.dpVariables);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.btnAddVar);
            this.groupBox1.Controls.Add(this.btnTest);
            this.groupBox1.Controls.Add(this.btnUptAll);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.dpFontAlign);
            this.groupBox1.Controls.Add(this.dpLedModel);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDisplayName);
            this.groupBox1.Controls.Add(this.txtPhyAddr);
            this.groupBox1.Location = new System.Drawing.Point(14, 256);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(701, 438);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LED设置";
            // 
            // tbTxtFormat
            // 
            this.tbTxtFormat.Controls.Add(this.tabDisplayFormat);
            this.tbTxtFormat.Controls.Add(this.tabPowerOnTip);
            this.tbTxtFormat.Controls.Add(this.tabInServiceTip);
            this.tbTxtFormat.Controls.Add(this.tabOnPauseTip);
            this.tbTxtFormat.Controls.Add(this.tabTimeoutTip);
            this.tbTxtFormat.Location = new System.Drawing.Point(18, 266);
            this.tbTxtFormat.Name = "tbTxtFormat";
            this.tbTxtFormat.SelectedIndex = 0;
            this.tbTxtFormat.Size = new System.Drawing.Size(507, 124);
            this.tbTxtFormat.TabIndex = 247;
            this.tbTxtFormat.Selected += new System.Windows.Forms.TabControlEventHandler(this.tbTxtFormat_Selected);
            // 
            // tabDisplayFormat
            // 
            this.tabDisplayFormat.Controls.Add(this.txtDisplayFormat);
            this.tabDisplayFormat.Location = new System.Drawing.Point(4, 25);
            this.tabDisplayFormat.Name = "tabDisplayFormat";
            this.tabDisplayFormat.Padding = new System.Windows.Forms.Padding(3);
            this.tabDisplayFormat.Size = new System.Drawing.Size(499, 95);
            this.tabDisplayFormat.TabIndex = 0;
            this.tabDisplayFormat.Text = "叫号显示";
            this.tabDisplayFormat.UseVisualStyleBackColor = true;
            // 
            // txtDisplayFormat
            // 
            this.txtDisplayFormat.Location = new System.Drawing.Point(4, 6);
            this.txtDisplayFormat.Multiline = true;
            this.txtDisplayFormat.Name = "txtDisplayFormat";
            this.txtDisplayFormat.Size = new System.Drawing.Size(490, 83);
            this.txtDisplayFormat.TabIndex = 1;
            // 
            // tabPowerOnTip
            // 
            this.tabPowerOnTip.Controls.Add(this.txtPowerOnTip);
            this.tabPowerOnTip.Location = new System.Drawing.Point(4, 25);
            this.tabPowerOnTip.Name = "tabPowerOnTip";
            this.tabPowerOnTip.Padding = new System.Windows.Forms.Padding(3);
            this.tabPowerOnTip.Size = new System.Drawing.Size(499, 95);
            this.tabPowerOnTip.TabIndex = 1;
            this.tabPowerOnTip.Text = "开机显示";
            this.tabPowerOnTip.UseVisualStyleBackColor = true;
            // 
            // txtPowerOnTip
            // 
            this.txtPowerOnTip.Location = new System.Drawing.Point(4, 6);
            this.txtPowerOnTip.Multiline = true;
            this.txtPowerOnTip.Name = "txtPowerOnTip";
            this.txtPowerOnTip.Size = new System.Drawing.Size(490, 83);
            this.txtPowerOnTip.TabIndex = 1;
            // 
            // tabInServiceTip
            // 
            this.tabInServiceTip.Controls.Add(this.txtInServiceTip);
            this.tabInServiceTip.Location = new System.Drawing.Point(4, 25);
            this.tabInServiceTip.Name = "tabInServiceTip";
            this.tabInServiceTip.Padding = new System.Windows.Forms.Padding(3);
            this.tabInServiceTip.Size = new System.Drawing.Size(499, 95);
            this.tabInServiceTip.TabIndex = 2;
            this.tabInServiceTip.Text = "办理显示";
            this.tabInServiceTip.UseVisualStyleBackColor = true;
            // 
            // txtInServiceTip
            // 
            this.txtInServiceTip.Location = new System.Drawing.Point(4, 6);
            this.txtInServiceTip.Multiline = true;
            this.txtInServiceTip.Name = "txtInServiceTip";
            this.txtInServiceTip.Size = new System.Drawing.Size(490, 83);
            this.txtInServiceTip.TabIndex = 1;
            // 
            // tabOnPauseTip
            // 
            this.tabOnPauseTip.Controls.Add(this.txtOnPauseTip);
            this.tabOnPauseTip.Location = new System.Drawing.Point(4, 25);
            this.tabOnPauseTip.Name = "tabOnPauseTip";
            this.tabOnPauseTip.Size = new System.Drawing.Size(499, 95);
            this.tabOnPauseTip.TabIndex = 3;
            this.tabOnPauseTip.Text = "暂停显示";
            this.tabOnPauseTip.UseVisualStyleBackColor = true;
            // 
            // txtOnPauseTip
            // 
            this.txtOnPauseTip.Location = new System.Drawing.Point(4, 6);
            this.txtOnPauseTip.Multiline = true;
            this.txtOnPauseTip.Name = "txtOnPauseTip";
            this.txtOnPauseTip.Size = new System.Drawing.Size(490, 83);
            this.txtOnPauseTip.TabIndex = 1;
            // 
            // tabTimeoutTip
            // 
            this.tabTimeoutTip.Controls.Add(this.txtTimeoutTip);
            this.tabTimeoutTip.Location = new System.Drawing.Point(4, 25);
            this.tabTimeoutTip.Name = "tabTimeoutTip";
            this.tabTimeoutTip.Size = new System.Drawing.Size(499, 95);
            this.tabTimeoutTip.TabIndex = 4;
            this.tabTimeoutTip.Text = "超时显示";
            this.tabTimeoutTip.UseVisualStyleBackColor = true;
            // 
            // txtTimeoutTip
            // 
            this.txtTimeoutTip.Location = new System.Drawing.Point(4, 6);
            this.txtTimeoutTip.Multiline = true;
            this.txtTimeoutTip.Name = "txtTimeoutTip";
            this.txtTimeoutTip.Size = new System.Drawing.Size(490, 83);
            this.txtTimeoutTip.TabIndex = 1;
            // 
            // txtScreenHeight
            // 
            this.txtScreenHeight.Location = new System.Drawing.Point(579, 171);
            this.txtScreenHeight.Name = "txtScreenHeight";
            this.txtScreenHeight.Size = new System.Drawing.Size(76, 25);
            this.txtScreenHeight.TabIndex = 226;
            this.txtScreenHeight.Text = "64";
            // 
            // txtScreenWidth
            // 
            this.txtScreenWidth.Location = new System.Drawing.Point(579, 137);
            this.txtScreenWidth.Name = "txtScreenWidth";
            this.txtScreenWidth.Size = new System.Drawing.Size(76, 25);
            this.txtScreenWidth.TabIndex = 226;
            this.txtScreenWidth.Text = "128";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(528, 174);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(45, 15);
            this.label16.TabIndex = 227;
            this.label16.Text = "屏高:";
            // 
            // txtTimeoutSec
            // 
            this.txtTimeoutSec.Location = new System.Drawing.Point(343, 400);
            this.txtTimeoutSec.Name = "txtTimeoutSec";
            this.txtTimeoutSec.Size = new System.Drawing.Size(57, 25);
            this.txtTimeoutSec.TabIndex = 226;
            this.txtTimeoutSec.Text = "12";
            // 
            // txtDisplayLength
            // 
            this.txtDisplayLength.Location = new System.Drawing.Point(602, 261);
            this.txtDisplayLength.Name = "txtDisplayLength";
            this.txtDisplayLength.Size = new System.Drawing.Size(71, 25);
            this.txtDisplayLength.TabIndex = 226;
            this.txtDisplayLength.Text = "12";
            // 
            // txtFontSize
            // 
            this.txtFontSize.Location = new System.Drawing.Point(96, 400);
            this.txtFontSize.Name = "txtFontSize";
            this.txtFontSize.Size = new System.Drawing.Size(37, 25);
            this.txtFontSize.TabIndex = 226;
            this.txtFontSize.Text = "12";
            // 
            // ckNetProtocol
            // 
            this.ckNetProtocol.Controls.Add(this.txtLocalPort);
            this.ckNetProtocol.Controls.Add(this.label9);
            this.ckNetProtocol.Controls.Add(this.label10);
            this.ckNetProtocol.Controls.Add(this.txtIpAddress);
            this.ckNetProtocol.Location = new System.Drawing.Point(18, 133);
            this.ckNetProtocol.Name = "ckNetProtocol";
            this.ckNetProtocol.Size = new System.Drawing.Size(471, 57);
            this.ckNetProtocol.TabIndex = 246;
            this.ckNetProtocol.TabStop = false;
            this.ckNetProtocol.Text = "网络通讯";
            this.ckNetProtocol.CheckBoxClicked += new EntFrm.Framework.Utility.CkGroupBoxEx.GroupBoxCheckBoxClickHandle(this.ckNetProtocol_CheckBoxClicked);
            // 
            // txtLocalPort
            // 
            this.txtLocalPort.Location = new System.Drawing.Point(388, 24);
            this.txtLocalPort.Name = "txtLocalPort";
            this.txtLocalPort.Size = new System.Drawing.Size(72, 25);
            this.txtLocalPort.TabIndex = 226;
            this.txtLocalPort.Text = "5005";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(13, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 15);
            this.label9.TabIndex = 227;
            this.label9.Text = "IP地址:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(337, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 15);
            this.label10.TabIndex = 227;
            this.label10.Text = "端口:";
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Location = new System.Drawing.Point(79, 24);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(226, 25);
            this.txtIpAddress.TabIndex = 226;
            this.txtIpAddress.Text = "192.168.1.236";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(528, 140);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 15);
            this.label15.TabIndex = 227;
            this.label15.Text = "屏宽：";
            // 
            // ckComProtocol
            // 
            this.ckComProtocol.Controls.Add(this.txtBaudrate);
            this.ckComProtocol.Controls.Add(this.label7);
            this.ckComProtocol.Controls.Add(this.dpSerialPort);
            this.ckComProtocol.Controls.Add(this.label8);
            this.ckComProtocol.Location = new System.Drawing.Point(18, 63);
            this.ckComProtocol.Name = "ckComProtocol";
            this.ckComProtocol.Size = new System.Drawing.Size(655, 60);
            this.ckComProtocol.TabIndex = 245;
            this.ckComProtocol.TabStop = false;
            this.ckComProtocol.Text = "串口通讯";
            this.ckComProtocol.CheckBoxClicked += new EntFrm.Framework.Utility.CkGroupBoxEx.GroupBoxCheckBoxClickHandle(this.ckComProtocol_CheckBoxClicked);
            // 
            // txtBaudrate
            // 
            this.txtBaudrate.Location = new System.Drawing.Point(388, 24);
            this.txtBaudrate.Name = "txtBaudrate";
            this.txtBaudrate.Size = new System.Drawing.Size(249, 25);
            this.txtBaudrate.TabIndex = 226;
            this.txtBaudrate.Text = "9600";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(322, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 15);
            this.label7.TabIndex = 227;
            this.label7.Text = "比特率:";
            // 
            // dpSerialPort
            // 
            this.dpSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpSerialPort.FormattingEnabled = true;
            this.dpSerialPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15",
            "COM16",
            "COM17",
            "COM18",
            "COM19",
            "COM20"});
            this.dpSerialPort.Location = new System.Drawing.Point(79, 24);
            this.dpSerialPort.Name = "dpSerialPort";
            this.dpSerialPort.Size = new System.Drawing.Size(226, 23);
            this.dpSerialPort.TabIndex = 233;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(29, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 15);
            this.label8.TabIndex = 231;
            this.label8.Text = "串口:";
            // 
            // dpVariables
            // 
            this.dpVariables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpVariables.FormattingEnabled = true;
            this.dpVariables.Location = new System.Drawing.Point(531, 292);
            this.dpVariables.Name = "dpVariables";
            this.dpVariables.Size = new System.Drawing.Size(141, 23);
            this.dpVariables.TabIndex = 211;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtHeight);
            this.groupBox3.Controls.Add(this.txtPosY);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtWidth);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtPosX);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(18, 199);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(655, 59);
            this.groupBox3.TabIndex = 243;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "显示位置";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(561, 24);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(76, 25);
            this.txtHeight.TabIndex = 226;
            this.txtHeight.Text = "64";
            // 
            // txtPosY
            // 
            this.txtPosY.Location = new System.Drawing.Point(79, 24);
            this.txtPosY.Name = "txtPosY";
            this.txtPosY.Size = new System.Drawing.Size(76, 25);
            this.txtPosY.TabIndex = 226;
            this.txtPosY.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(500, 31);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 15);
            this.label14.TabIndex = 227;
            this.label14.Text = "高度:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(178, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 15);
            this.label12.TabIndex = 227;
            this.label12.Text = "左边:";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(388, 24);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(72, 25);
            this.txtWidth.TabIndex = 226;
            this.txtWidth.Text = "128";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(337, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 15);
            this.label13.TabIndex = 227;
            this.label13.Text = "宽度:";
            // 
            // txtPosX
            // 
            this.txtPosX.Location = new System.Drawing.Point(226, 24);
            this.txtPosX.Name = "txtPosX";
            this.txtPosX.Size = new System.Drawing.Size(79, 25);
            this.txtPosX.TabIndex = 226;
            this.txtPosX.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(21, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 15);
            this.label11.TabIndex = 227;
            this.label11.Text = "顶部:";
            // 
            // btnAddVar
            // 
            this.btnAddVar.ForeColor = System.Drawing.Color.Black;
            this.btnAddVar.Location = new System.Drawing.Point(531, 323);
            this.btnAddVar.Name = "btnAddVar";
            this.btnAddVar.Size = new System.Drawing.Size(141, 30);
            this.btnAddVar.TabIndex = 211;
            this.btnAddVar.Text = "插入变量";
            this.btnAddVar.UseVisualStyleBackColor = true;
            this.btnAddVar.Click += new System.EventHandler(this.btnAddVar_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(531, 360);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(141, 30);
            this.btnTest.TabIndex = 238;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnUptAll
            // 
            this.btnUptAll.ForeColor = System.Drawing.Color.Black;
            this.btnUptAll.Location = new System.Drawing.Point(440, 398);
            this.btnUptAll.Name = "btnUptAll";
            this.btnUptAll.Size = new System.Drawing.Size(81, 30);
            this.btnUptAll.TabIndex = 237;
            this.btnUptAll.Text = "批量修改";
            this.btnUptAll.UseVisualStyleBackColor = true;
            this.btnUptAll.Click += new System.EventHandler(this.btnUptAll_Click);
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(532, 398);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(141, 30);
            this.btnSave.TabIndex = 237;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dpFontAlign
            // 
            this.dpFontAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpFontAlign.FormattingEnabled = true;
            this.dpFontAlign.Items.AddRange(new object[] {
            "左对齐",
            "居中",
            "右对齐"});
            this.dpFontAlign.Location = new System.Drawing.Point(224, 402);
            this.dpFontAlign.Name = "dpFontAlign";
            this.dpFontAlign.Size = new System.Drawing.Size(67, 23);
            this.dpFontAlign.TabIndex = 233;
            // 
            // dpLedModel
            // 
            this.dpLedModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpLedModel.FormattingEnabled = true;
            this.dpLedModel.Items.AddRange(new object[] {
            "Hzhx13",
            "Eq2013",
            "Pdc101"});
            this.dpLedModel.Location = new System.Drawing.Point(287, 27);
            this.dpLedModel.Name = "dpLedModel";
            this.dpLedModel.Size = new System.Drawing.Size(126, 23);
            this.dpLedModel.TabIndex = 233;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(403, 406);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(22, 15);
            this.label18.TabIndex = 227;
            this.label18.Text = "秒";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(292, 405);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(45, 15);
            this.label17.TabIndex = 227;
            this.label17.Text = "超时:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(531, 266);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(75, 15);
            this.label19.TabIndex = 227;
            this.label19.Text = "显示长度:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(119, 405);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 15);
            this.label4.TabIndex = 227;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(15, 405);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 227;
            this.label2.Text = "字体大小:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(15, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 227;
            this.label3.Text = "名称:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(143, 406);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 15);
            this.label5.TabIndex = 227;
            this.label5.Text = "对齐方式:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(236, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 15);
            this.label6.TabIndex = 231;
            this.label6.Text = "型号:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(444, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 227;
            this.label1.Text = "地址:";
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(63, 26);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(155, 25);
            this.txtDisplayName.TabIndex = 226;
            // 
            // txtPhyAddr
            // 
            this.txtPhyAddr.Location = new System.Drawing.Point(495, 26);
            this.txtPhyAddr.Name = "txtPhyAddr";
            this.txtPhyAddr.Size = new System.Drawing.Size(177, 25);
            this.txtPhyAddr.TabIndex = 226;
            this.txtPhyAddr.Text = "1";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.InfoList);
            this.groupBox5.Controls.Add(this.btnDel);
            this.groupBox5.Controls.Add(this.btnAdd);
            this.groupBox5.Location = new System.Drawing.Point(6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(707, 244);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "LED列表";
            // 
            // InfoList
            // 
            this.InfoList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InfoList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.InfoList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InfoList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DisplayNo,
            this.DisplayName,
            this.SerialPort,
            this.PhyAddr,
            this.DisplayLength});
            this.InfoList.Location = new System.Drawing.Point(11, 25);
            this.InfoList.Name = "InfoList";
            this.InfoList.RowHeadersVisible = false;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.InfoList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.InfoList.RowTemplate.Height = 27;
            this.InfoList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.InfoList.Size = new System.Drawing.Size(670, 172);
            this.InfoList.TabIndex = 228;
            this.InfoList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InfoList_CellClick);
            // 
            // DisplayNo
            // 
            this.DisplayNo.DataPropertyName = "sDisplayNo";
            this.DisplayNo.HeaderText = "编号";
            this.DisplayNo.Name = "DisplayNo";
            this.DisplayNo.ReadOnly = true;
            this.DisplayNo.Visible = false;
            // 
            // DisplayName
            // 
            this.DisplayName.DataPropertyName = "sDisplayName";
            this.DisplayName.HeaderText = "LED屏名称";
            this.DisplayName.Name = "DisplayName";
            this.DisplayName.ReadOnly = true;
            this.DisplayName.Width = 200;
            // 
            // SerialPort
            // 
            this.SerialPort.DataPropertyName = "sSerialPort";
            this.SerialPort.HeaderText = "串口";
            this.SerialPort.Name = "SerialPort";
            this.SerialPort.ReadOnly = true;
            this.SerialPort.Width = 150;
            // 
            // PhyAddr
            // 
            this.PhyAddr.DataPropertyName = "iPhyAddr";
            this.PhyAddr.HeaderText = "物理地址";
            this.PhyAddr.Name = "PhyAddr";
            this.PhyAddr.ReadOnly = true;
            this.PhyAddr.Width = 150;
            // 
            // DisplayLength
            // 
            this.DisplayLength.DataPropertyName = "iDisplayLength";
            this.DisplayLength.HeaderText = "显示长度";
            this.DisplayLength.Name = "DisplayLength";
            this.DisplayLength.ReadOnly = true;
            this.DisplayLength.Width = 200;
            // 
            // btnDel
            // 
            this.btnDel.ForeColor = System.Drawing.Color.Black;
            this.btnDel.Location = new System.Drawing.Point(610, 203);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(70, 30);
            this.btnDel.TabIndex = 227;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(517, 203);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(82, 30);
            this.btnAdd.TabIndex = 227;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // frmLedDispBusiness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 705);
            this.Controls.Add(this.panel1);
            this.Name = "frmLedDispBusiness";
            this.Text = "LED屏";
            this.Load += new System.EventHandler(this.frmLedDispBusiness_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tbTxtFormat.ResumeLayout(false);
            this.tabDisplayFormat.ResumeLayout(false);
            this.tabDisplayFormat.PerformLayout();
            this.tabPowerOnTip.ResumeLayout(false);
            this.tabPowerOnTip.PerformLayout();
            this.tabInServiceTip.ResumeLayout(false);
            this.tabInServiceTip.PerformLayout();
            this.tabOnPauseTip.ResumeLayout(false);
            this.tabOnPauseTip.PerformLayout();
            this.tabTimeoutTip.ResumeLayout(false);
            this.tabTimeoutTip.PerformLayout();
            this.ckNetProtocol.ResumeLayout(false);
            this.ckNetProtocol.PerformLayout();
            this.ckComProtocol.ResumeLayout(false);
            this.ckComProtocol.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InfoList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPhyAddr;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnAddVar;
        private System.Windows.Forms.ComboBox dpVariables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView InfoList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnUptAll;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtPosY;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPosX;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtLocalPort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox dpSerialPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBaudrate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox dpLedModel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox dpFontAlign;
        private CkGroupBoxEx ckNetProtocol;
        private CkGroupBoxEx ckComProtocol;
        private System.Windows.Forms.TabControl tbTxtFormat;
        private System.Windows.Forms.TabPage tabDisplayFormat;
        private System.Windows.Forms.TabPage tabPowerOnTip;
        private System.Windows.Forms.TabPage tabInServiceTip;
        private System.Windows.Forms.TabPage tabOnPauseTip;
        private System.Windows.Forms.TabPage tabTimeoutTip;
        private System.Windows.Forms.TextBox txtScreenHeight;
        private System.Windows.Forms.TextBox txtScreenWidth;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtFontSize;
        private System.Windows.Forms.TextBox txtDisplayFormat;
        private System.Windows.Forms.TextBox txtPowerOnTip;
        private System.Windows.Forms.TextBox txtInServiceTip;
        private System.Windows.Forms.TextBox txtOnPauseTip;
        private System.Windows.Forms.TextBox txtTimeoutTip;
        private System.Windows.Forms.TextBox txtTimeoutSec;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtDisplayLength;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisplayNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisplayName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhyAddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisplayLength;
    }
}