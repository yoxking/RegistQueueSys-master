using EntFrm.Framework.Utility;

namespace EntFrm.SettingConsole
{
    partial class frmSysettingBusiness
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dpBranchList = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.ckCallVoiceEnable = new EntFrm.Framework.Utility.CkGroupBoxEx();
            this.dpCallVoiceVolume = new System.Windows.Forms.ComboBox();
            this.dpCallVoiceRate = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dpCallVoiceStyle = new System.Windows.Forms.ComboBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnAddVar = new System.Windows.Forms.Button();
            this.dpVariables = new System.Windows.Forms.ComboBox();
            this.txtCallVoiceFormat = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dpReaderModel = new System.Windows.Forms.ComboBox();
            this.dpBaudrate = new System.Windows.Forms.ComboBox();
            this.dpSerialPort = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.ckCallVoiceEnable.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(727, 553);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dpBranchList);
            this.groupBox1.Controls.Add(this.btnOk);
            this.groupBox1.Controls.Add(this.ckCallVoiceEnable);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(709, 476);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本设置";
            // 
            // dpBranchList
            // 
            this.dpBranchList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpBranchList.FormattingEnabled = true;
            this.dpBranchList.Location = new System.Drawing.Point(128, 44);
            this.dpBranchList.Name = "dpBranchList";
            this.dpBranchList.Size = new System.Drawing.Size(372, 23);
            this.dpBranchList.TabIndex = 227;
            // 
            // btnOk
            // 
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.Location = new System.Drawing.Point(596, 430);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 30);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "保存";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // ckCallVoiceEnable
            // 
            this.ckCallVoiceEnable.Controls.Add(this.dpCallVoiceVolume);
            this.ckCallVoiceEnable.Controls.Add(this.dpCallVoiceRate);
            this.ckCallVoiceEnable.Controls.Add(this.label11);
            this.ckCallVoiceEnable.Controls.Add(this.label9);
            this.ckCallVoiceEnable.Controls.Add(this.dpCallVoiceStyle);
            this.ckCallVoiceEnable.Controls.Add(this.btnTest);
            this.ckCallVoiceEnable.Controls.Add(this.btnAddVar);
            this.ckCallVoiceEnable.Controls.Add(this.dpVariables);
            this.ckCallVoiceEnable.Controls.Add(this.txtCallVoiceFormat);
            this.ckCallVoiceEnable.Location = new System.Drawing.Point(24, 106);
            this.ckCallVoiceEnable.Name = "ckCallVoiceEnable";
            this.ckCallVoiceEnable.Size = new System.Drawing.Size(662, 204);
            this.ckCallVoiceEnable.TabIndex = 10;
            this.ckCallVoiceEnable.TabStop = false;
            this.ckCallVoiceEnable.Text = "护士站呼叫";
            // 
            // dpCallVoiceVolume
            // 
            this.dpCallVoiceVolume.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpCallVoiceVolume.FormattingEnabled = true;
            this.dpCallVoiceVolume.Location = new System.Drawing.Point(531, 35);
            this.dpCallVoiceVolume.Name = "dpCallVoiceVolume";
            this.dpCallVoiceVolume.Size = new System.Drawing.Size(120, 23);
            this.dpCallVoiceVolume.TabIndex = 235;
            // 
            // dpCallVoiceRate
            // 
            this.dpCallVoiceRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpCallVoiceRate.FormattingEnabled = true;
            this.dpCallVoiceRate.Location = new System.Drawing.Point(531, 77);
            this.dpCallVoiceRate.Name = "dpCallVoiceRate";
            this.dpCallVoiceRate.Size = new System.Drawing.Size(120, 23);
            this.dpCallVoiceRate.TabIndex = 236;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(480, 81);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 15);
            this.label11.TabIndex = 233;
            this.label11.Text = "语速:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(482, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 15);
            this.label9.TabIndex = 234;
            this.label9.Text = "音量:";
            // 
            // dpCallVoiceStyle
            // 
            this.dpCallVoiceStyle.FormattingEnabled = true;
            this.dpCallVoiceStyle.Location = new System.Drawing.Point(13, 128);
            this.dpCallVoiceStyle.Name = "dpCallVoiceStyle";
            this.dpCallVoiceStyle.Size = new System.Drawing.Size(638, 23);
            this.dpCallVoiceStyle.TabIndex = 10;
            // 
            // btnTest
            // 
            this.btnTest.ForeColor = System.Drawing.Color.Black;
            this.btnTest.Location = new System.Drawing.Point(531, 157);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(120, 30);
            this.btnTest.TabIndex = 9;
            this.btnTest.Text = "试听";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnAddVar
            // 
            this.btnAddVar.ForeColor = System.Drawing.Color.Black;
            this.btnAddVar.Location = new System.Drawing.Point(370, 160);
            this.btnAddVar.Name = "btnAddVar";
            this.btnAddVar.Size = new System.Drawing.Size(106, 30);
            this.btnAddVar.TabIndex = 9;
            this.btnAddVar.Text = "插入变量";
            this.btnAddVar.UseVisualStyleBackColor = true;
            this.btnAddVar.Click += new System.EventHandler(this.btnAddVar_Click);
            // 
            // dpVariables
            // 
            this.dpVariables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpVariables.FormattingEnabled = true;
            this.dpVariables.Items.AddRange(new object[] {
            "[科室名称]",
            "[科室别名]"});
            this.dpVariables.Location = new System.Drawing.Point(13, 164);
            this.dpVariables.Name = "dpVariables";
            this.dpVariables.Size = new System.Drawing.Size(351, 23);
            this.dpVariables.TabIndex = 7;
            // 
            // txtCallVoiceFormat
            // 
            this.txtCallVoiceFormat.Location = new System.Drawing.Point(13, 32);
            this.txtCallVoiceFormat.Multiline = true;
            this.txtCallVoiceFormat.Name = "txtCallVoiceFormat";
            this.txtCallVoiceFormat.Size = new System.Drawing.Size(463, 87);
            this.txtCallVoiceFormat.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(28, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 223;
            this.label1.Text = "分诊科室:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dpReaderModel);
            this.groupBox2.Controls.Add(this.dpBaudrate);
            this.groupBox2.Controls.Add(this.dpSerialPort);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(24, 334);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(662, 79);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "身份证读卡器";
            // 
            // dpReaderModel
            // 
            this.dpReaderModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpReaderModel.FormattingEnabled = true;
            this.dpReaderModel.Items.AddRange(new object[] {
            "华旭"});
            this.dpReaderModel.Location = new System.Drawing.Point(70, 35);
            this.dpReaderModel.Name = "dpReaderModel";
            this.dpReaderModel.Size = new System.Drawing.Size(121, 23);
            this.dpReaderModel.TabIndex = 227;
            // 
            // dpBaudrate
            // 
            this.dpBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpBaudrate.FormattingEnabled = true;
            this.dpBaudrate.Items.AddRange(new object[] {
            "9600",
            "37800"});
            this.dpBaudrate.Location = new System.Drawing.Point(513, 31);
            this.dpBaudrate.Name = "dpBaudrate";
            this.dpBaudrate.Size = new System.Drawing.Size(138, 23);
            this.dpBaudrate.TabIndex = 227;
            // 
            // dpSerialPort
            // 
            this.dpSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpSerialPort.FormattingEnabled = true;
            this.dpSerialPort.Items.AddRange(new object[] {
            "USB1",
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6"});
            this.dpSerialPort.Location = new System.Drawing.Point(301, 36);
            this.dpSerialPort.Name = "dpSerialPort";
            this.dpSerialPort.Size = new System.Drawing.Size(121, 23);
            this.dpSerialPort.TabIndex = 227;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(19, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 15);
            this.label4.TabIndex = 223;
            this.label4.Text = "类型:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(250, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 223;
            this.label2.Text = "端口:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(447, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 15);
            this.label6.TabIndex = 223;
            this.label6.Text = "波特率:";
            // 
            // frmSysettingBusiness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 553);
            this.Controls.Add(this.panel1);
            this.Name = "frmSysettingBusiness";
            this.Text = "frmSysettingBusiness";
            this.Load += new System.EventHandler(this.frmSysettingBusiness_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ckCallVoiceEnable.ResumeLayout(false);
            this.ckCallVoiceEnable.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddVar;
        private System.Windows.Forms.TextBox txtCallVoiceFormat;
        private System.Windows.Forms.ComboBox dpVariables;
        private CkGroupBoxEx ckCallVoiceEnable;
        private System.Windows.Forms.ComboBox dpCallVoiceStyle;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox dpReaderModel;
        private System.Windows.Forms.ComboBox dpBaudrate;
        private System.Windows.Forms.ComboBox dpSerialPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox dpCallVoiceVolume;
        private System.Windows.Forms.ComboBox dpCallVoiceRate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.ComboBox dpBranchList;
        private System.Windows.Forms.Label label1;
    }
}