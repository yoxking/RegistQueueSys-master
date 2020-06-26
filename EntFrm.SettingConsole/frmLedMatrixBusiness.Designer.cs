using EntFrm.Framework.Utility;

namespace EntFrm.SettingConsole
{
    partial class frmLedMatrixBusiness
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFontSize = new System.Windows.Forms.TextBox();
            this.txtScreenHeight = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDisplayFormat = new System.Windows.Forms.TextBox();
            this.btnAddVar = new System.Windows.Forms.Button();
            this.dpVariables = new System.Windows.Forms.ComboBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtServiceNames = new System.Windows.Forms.TextBox();
            this.txtScreenWidth = new System.Windows.Forms.TextBox();
            this.txtDisplayRows = new System.Windows.Forms.TextBox();
            this.ckNetProtocol = new EntFrm.Framework.Utility.CkGroupBoxEx();
            this.txtLocalPort = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.ckComProtocol = new EntFrm.Framework.Utility.CkGroupBoxEx();
            this.txtBaudrate = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dpSerialPort = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtPosX = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtPosY = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dpFontAlign = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSelectService = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.dpMatrixModel = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMatrixName = new System.Windows.Forms.TextBox();
            this.txtPhyAddr = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.InfoList = new System.Windows.Forms.DataGridView();
            this.MatrixNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatrixName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatrixModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhyAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Protocol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(725, 774);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFontSize);
            this.groupBox1.Controls.Add(this.txtScreenHeight);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtServiceNames);
            this.groupBox1.Controls.Add(this.txtScreenWidth);
            this.groupBox1.Controls.Add(this.txtDisplayRows);
            this.groupBox1.Controls.Add(this.ckNetProtocol);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.ckComProtocol);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.dpFontAlign);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnSelectService);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.dpMatrixModel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtMatrixName);
            this.groupBox1.Controls.Add(this.txtPhyAddr);
            this.groupBox1.Location = new System.Drawing.Point(16, 258);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(701, 504);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "综合屏设置";
            // 
            // txtFontSize
            // 
            this.txtFontSize.Location = new System.Drawing.Point(225, 460);
            this.txtFontSize.Name = "txtFontSize";
            this.txtFontSize.Size = new System.Drawing.Size(70, 25);
            this.txtFontSize.TabIndex = 226;
            this.txtFontSize.Text = "64";
            // 
            // txtScreenHeight
            // 
            this.txtScreenHeight.Location = new System.Drawing.Point(584, 172);
            this.txtScreenHeight.Name = "txtScreenHeight";
            this.txtScreenHeight.Size = new System.Drawing.Size(70, 25);
            this.txtScreenHeight.TabIndex = 226;
            this.txtScreenHeight.Text = "64";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDisplayFormat);
            this.groupBox2.Controls.Add(this.btnAddVar);
            this.groupBox2.Controls.Add(this.dpVariables);
            this.groupBox2.Controls.Add(this.btnTest);
            this.groupBox2.Location = new System.Drawing.Point(19, 310);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(654, 137);
            this.groupBox2.TabIndex = 248;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "显示格式";
            // 
            // txtDisplayFormat
            // 
            this.txtDisplayFormat.Location = new System.Drawing.Point(16, 24);
            this.txtDisplayFormat.Multiline = true;
            this.txtDisplayFormat.Name = "txtDisplayFormat";
            this.txtDisplayFormat.Size = new System.Drawing.Size(472, 99);
            this.txtDisplayFormat.TabIndex = 247;
            // 
            // btnAddVar
            // 
            this.btnAddVar.ForeColor = System.Drawing.Color.Black;
            this.btnAddVar.Location = new System.Drawing.Point(498, 55);
            this.btnAddVar.Name = "btnAddVar";
            this.btnAddVar.Size = new System.Drawing.Size(141, 30);
            this.btnAddVar.TabIndex = 211;
            this.btnAddVar.Text = "插入变量";
            this.btnAddVar.UseVisualStyleBackColor = true;
            this.btnAddVar.Click += new System.EventHandler(this.btnAddVar_Click);
            // 
            // dpVariables
            // 
            this.dpVariables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpVariables.FormattingEnabled = true;
            this.dpVariables.Location = new System.Drawing.Point(498, 24);
            this.dpVariables.Name = "dpVariables";
            this.dpVariables.Size = new System.Drawing.Size(141, 23);
            this.dpVariables.TabIndex = 211;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(498, 93);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(141, 30);
            this.btnTest.TabIndex = 238;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(532, 176);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(45, 15);
            this.label18.TabIndex = 227;
            this.label18.Text = "高度:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(144, 466);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 15);
            this.label11.TabIndex = 227;
            this.label11.Text = "字体大小:";
            // 
            // txtServiceNames
            // 
            this.txtServiceNames.Location = new System.Drawing.Point(97, 272);
            this.txtServiceNames.Name = "txtServiceNames";
            this.txtServiceNames.Size = new System.Drawing.Size(410, 25);
            this.txtServiceNames.TabIndex = 226;
            // 
            // txtScreenWidth
            // 
            this.txtScreenWidth.Location = new System.Drawing.Point(584, 136);
            this.txtScreenWidth.Name = "txtScreenWidth";
            this.txtScreenWidth.Size = new System.Drawing.Size(70, 25);
            this.txtScreenWidth.TabIndex = 226;
            this.txtScreenWidth.Text = "128";
            // 
            // txtDisplayRows
            // 
            this.txtDisplayRows.Location = new System.Drawing.Point(66, 460);
            this.txtDisplayRows.Name = "txtDisplayRows";
            this.txtDisplayRows.Size = new System.Drawing.Size(72, 25);
            this.txtDisplayRows.TabIndex = 226;
            this.txtDisplayRows.Text = "12";
            // 
            // ckNetProtocol
            // 
            this.ckNetProtocol.Controls.Add(this.txtLocalPort);
            this.ckNetProtocol.Controls.Add(this.label9);
            this.ckNetProtocol.Controls.Add(this.label10);
            this.ckNetProtocol.Controls.Add(this.txtIpAddress);
            this.ckNetProtocol.Location = new System.Drawing.Point(18, 133);
            this.ckNetProtocol.Name = "ckNetProtocol";
            this.ckNetProtocol.Size = new System.Drawing.Size(481, 57);
            this.ckNetProtocol.TabIndex = 246;
            this.ckNetProtocol.TabStop = false;
            this.ckNetProtocol.Text = "网络通讯";
            this.ckNetProtocol.CheckBoxClicked += new EntFrm.Framework.Utility.CkGroupBoxEx.GroupBoxCheckBoxClickHandle(this.ckNetProtocol_CheckBoxClicked);
            // 
            // txtLocalPort
            // 
            this.txtLocalPort.Location = new System.Drawing.Point(346, 22);
            this.txtLocalPort.Name = "txtLocalPort";
            this.txtLocalPort.Size = new System.Drawing.Size(112, 25);
            this.txtLocalPort.TabIndex = 226;
            this.txtLocalPort.Text = "1";
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
            this.label10.Location = new System.Drawing.Point(295, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 15);
            this.label10.TabIndex = 227;
            this.label10.Text = "端口:";
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Location = new System.Drawing.Point(79, 24);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(184, 25);
            this.txtIpAddress.TabIndex = 226;
            this.txtIpAddress.Text = "127.0.0.1";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(533, 143);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(45, 15);
            this.label17.TabIndex = 227;
            this.label17.Text = "宽度:";
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
            "COM9"});
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtPosX);
            this.groupBox3.Controls.Add(this.txtHeight);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.txtPosY);
            this.groupBox3.Controls.Add(this.txtWidth);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Location = new System.Drawing.Point(18, 199);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(655, 59);
            this.groupBox3.TabIndex = 243;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "综合屏参数";
            // 
            // txtPosX
            // 
            this.txtPosX.Location = new System.Drawing.Point(222, 25);
            this.txtPosX.Name = "txtPosX";
            this.txtPosX.Size = new System.Drawing.Size(70, 25);
            this.txtPosX.TabIndex = 226;
            this.txtPosX.Text = "64";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(553, 25);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(70, 25);
            this.txtHeight.TabIndex = 226;
            this.txtHeight.Text = "64";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(162, 30);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(45, 15);
            this.label16.TabIndex = 227;
            this.label16.Text = "左侧:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(493, 30);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 15);
            this.label14.TabIndex = 227;
            this.label14.Text = "高度:";
            // 
            // txtPosY
            // 
            this.txtPosY.Location = new System.Drawing.Point(80, 25);
            this.txtPosY.Name = "txtPosY";
            this.txtPosY.Size = new System.Drawing.Size(70, 25);
            this.txtPosY.TabIndex = 226;
            this.txtPosY.Text = "128";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(411, 25);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(70, 25);
            this.txtWidth.TabIndex = 226;
            this.txtWidth.Text = "128";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(29, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 15);
            this.label12.TabIndex = 227;
            this.label12.Text = "顶部:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(360, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 15);
            this.label13.TabIndex = 227;
            this.label13.Text = "宽度:";
            // 
            // dpFontAlign
            // 
            this.dpFontAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpFontAlign.FormattingEnabled = true;
            this.dpFontAlign.Items.AddRange(new object[] {
            "左对齐",
            "居中",
            "右对齐"});
            this.dpFontAlign.Location = new System.Drawing.Point(382, 460);
            this.dpFontAlign.Name = "dpFontAlign";
            this.dpFontAlign.Size = new System.Drawing.Size(125, 23);
            this.dpFontAlign.TabIndex = 233;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(301, 466);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 15);
            this.label5.TabIndex = 227;
            this.label5.Text = "对齐方式:";
            // 
            // btnSelectService
            // 
            this.btnSelectService.Location = new System.Drawing.Point(521, 269);
            this.btnSelectService.Name = "btnSelectService";
            this.btnSelectService.Size = new System.Drawing.Size(137, 30);
            this.btnSelectService.TabIndex = 238;
            this.btnSelectService.Text = "选择业务";
            this.btnSelectService.UseVisualStyleBackColor = true;
            this.btnSelectService.Click += new System.EventHandler(this.btnSelectService_Click);
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(517, 458);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(141, 30);
            this.btnSave.TabIndex = 237;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(31, 277);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(45, 15);
            this.label15.TabIndex = 227;
            this.label15.Text = "业务:";
            // 
            // dpMatrixModel
            // 
            this.dpMatrixModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpMatrixModel.FormattingEnabled = true;
            this.dpMatrixModel.Items.AddRange(new object[] {
            "Eq2013",
            "Eq2023",
            "Pdc101"});
            this.dpMatrixModel.Location = new System.Drawing.Point(287, 27);
            this.dpMatrixModel.Name = "dpMatrixModel";
            this.dpMatrixModel.Size = new System.Drawing.Size(126, 23);
            this.dpMatrixModel.TabIndex = 233;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(119, 458);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 15);
            this.label4.TabIndex = 227;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(15, 463);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 227;
            this.label2.Text = "行数:";
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
            // txtMatrixName
            // 
            this.txtMatrixName.Location = new System.Drawing.Point(63, 26);
            this.txtMatrixName.Name = "txtMatrixName";
            this.txtMatrixName.Size = new System.Drawing.Size(155, 25);
            this.txtMatrixName.TabIndex = 226;
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
            this.groupBox5.Location = new System.Drawing.Point(8, 8);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(707, 244);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "综合屏列表";
            // 
            // InfoList
            // 
            this.InfoList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InfoList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.InfoList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InfoList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MatrixNo,
            this.MatrixName,
            this.MatrixModel,
            this.PhyAddr,
            this.Protocol});
            this.InfoList.Location = new System.Drawing.Point(11, 25);
            this.InfoList.Name = "InfoList";
            this.InfoList.RowHeadersVisible = false;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.InfoList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.InfoList.RowTemplate.Height = 27;
            this.InfoList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.InfoList.Size = new System.Drawing.Size(670, 172);
            this.InfoList.TabIndex = 228;
            this.InfoList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InfoList_CellClick);
            // 
            // MatrixNo
            // 
            this.MatrixNo.DataPropertyName = "sMatrixNo";
            this.MatrixNo.HeaderText = "编号";
            this.MatrixNo.Name = "MatrixNo";
            this.MatrixNo.ReadOnly = true;
            this.MatrixNo.Visible = false;
            // 
            // MatrixName
            // 
            this.MatrixName.DataPropertyName = "sMatrixName";
            this.MatrixName.HeaderText = "综合屏名称";
            this.MatrixName.Name = "MatrixName";
            this.MatrixName.ReadOnly = true;
            this.MatrixName.Width = 200;
            // 
            // MatrixModel
            // 
            this.MatrixModel.DataPropertyName = "sMatrixModel";
            this.MatrixModel.HeaderText = "型号";
            this.MatrixModel.Name = "MatrixModel";
            this.MatrixModel.ReadOnly = true;
            this.MatrixModel.Width = 150;
            // 
            // PhyAddr
            // 
            this.PhyAddr.DataPropertyName = "iPhyAddr";
            this.PhyAddr.HeaderText = "物理地址";
            this.PhyAddr.Name = "PhyAddr";
            this.PhyAddr.ReadOnly = true;
            this.PhyAddr.Width = 150;
            // 
            // Protocol
            // 
            this.Protocol.DataPropertyName = "sProtocol";
            this.Protocol.HeaderText = "协议";
            this.Protocol.Name = "Protocol";
            this.Protocol.ReadOnly = true;
            this.Protocol.Width = 200;
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
            // frmLedMatrixBusiness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 774);
            this.Controls.Add(this.panel1);
            this.Name = "frmLedMatrixBusiness";
            this.Text = "frmLedMatrixBusiness";
            this.Load += new System.EventHandler(this.frmLedMatrixBusiness_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.TextBox txtDisplayRows;
        private CkGroupBoxEx ckNetProtocol;
        private System.Windows.Forms.TextBox txtLocalPort;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtIpAddress;
        private CkGroupBoxEx ckComProtocol;
        private System.Windows.Forms.TextBox txtBaudrate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox dpSerialPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox dpVariables;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnAddVar;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox dpMatrixModel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMatrixName;
        private System.Windows.Forms.TextBox txtPhyAddr;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView InfoList;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtDisplayFormat;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtServiceNames;
        private System.Windows.Forms.Button btnSelectService;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox dpFontAlign;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFontSize;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPosX;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtPosY;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtScreenHeight;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtScreenWidth;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatrixNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatrixName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatrixModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhyAddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Protocol;
    }
}