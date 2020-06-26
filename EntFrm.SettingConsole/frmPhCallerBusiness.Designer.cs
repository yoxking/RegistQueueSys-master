namespace EntFrm.SettingConsole
{
    partial class frmPhCallerBusiness
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
            this.btnSave = new System.Windows.Forms.Button();
            this.dpEvalList = new System.Windows.Forms.ComboBox();
            this.dpSerialPort = new System.Windows.Forms.ComboBox();
            this.txtTimeoutSec = new System.Windows.Forms.TextBox();
            this.txtPhyAddr = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCallerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.InfoList = new System.Windows.Forms.DataGridView();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.CallerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CallerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhyAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeoutSec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(727, 600);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.dpEvalList);
            this.groupBox1.Controls.Add(this.dpSerialPort);
            this.groupBox1.Controls.Add(this.txtTimeoutSec);
            this.groupBox1.Controls.Add(this.txtPhyAddr);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCallerName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(10, 333);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(703, 240);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "呼叫器设置";
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(551, 163);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 30);
            this.btnSave.TabIndex = 228;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dpEvalList
            // 
            this.dpEvalList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpEvalList.FormattingEnabled = true;
            this.dpEvalList.Location = new System.Drawing.Point(121, 112);
            this.dpEvalList.Name = "dpEvalList";
            this.dpEvalList.Size = new System.Drawing.Size(332, 23);
            this.dpEvalList.TabIndex = 227;
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
            this.dpSerialPort.Location = new System.Drawing.Point(121, 67);
            this.dpSerialPort.Name = "dpSerialPort";
            this.dpSerialPort.Size = new System.Drawing.Size(121, 23);
            this.dpSerialPort.TabIndex = 227;
            // 
            // txtTimeoutSec
            // 
            this.txtTimeoutSec.Location = new System.Drawing.Point(551, 65);
            this.txtTimeoutSec.Name = "txtTimeoutSec";
            this.txtTimeoutSec.Size = new System.Drawing.Size(108, 25);
            this.txtTimeoutSec.TabIndex = 226;
            // 
            // txtPhyAddr
            // 
            this.txtPhyAddr.Location = new System.Drawing.Point(333, 67);
            this.txtPhyAddr.Name = "txtPhyAddr";
            this.txtPhyAddr.Size = new System.Drawing.Size(120, 25);
            this.txtPhyAddr.TabIndex = 226;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(55, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 223;
            this.label4.Text = "评价器:";
            // 
            // txtCallerName
            // 
            this.txtCallerName.Location = new System.Drawing.Point(121, 28);
            this.txtCallerName.Name = "txtCallerName";
            this.txtCallerName.Size = new System.Drawing.Size(332, 25);
            this.txtCallerName.TabIndex = 226;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(71, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 223;
            this.label2.Text = "端口:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(282, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 223;
            this.label3.Text = "地址:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(665, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 15);
            this.label1.TabIndex = 224;
            this.label1.Text = "秒";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(470, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 15);
            this.label10.TabIndex = 224;
            this.label10.Text = "间隔时长:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(26, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 15);
            this.label6.TabIndex = 223;
            this.label6.Text = "呼叫器名称:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.InfoList);
            this.groupBox5.Controls.Add(this.btnDel);
            this.groupBox5.Controls.Add(this.btnAdd);
            this.groupBox5.Location = new System.Drawing.Point(6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(707, 313);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "呼叫器列表";
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
            this.CallerNo,
            this.CallerName,
            this.SerialPort,
            this.PhyAddr,
            this.TimeoutSec});
            this.InfoList.Location = new System.Drawing.Point(11, 25);
            this.InfoList.Name = "InfoList";
            this.InfoList.RowHeadersVisible = false;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.InfoList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.InfoList.RowTemplate.Height = 27;
            this.InfoList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.InfoList.Size = new System.Drawing.Size(670, 240);
            this.InfoList.TabIndex = 228;
            this.InfoList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InfoList_CellClick);
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDel.ForeColor = System.Drawing.Color.Black;
            this.btnDel.Location = new System.Drawing.Point(600, 274);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(81, 30);
            this.btnDel.TabIndex = 227;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(520, 274);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(77, 30);
            this.btnAdd.TabIndex = 227;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // CallerNo
            // 
            this.CallerNo.DataPropertyName = "sCallerNo";
            this.CallerNo.HeaderText = "编号";
            this.CallerNo.Name = "CallerNo";
            this.CallerNo.ReadOnly = true;
            this.CallerNo.Visible = false;
            // 
            // CallerName
            // 
            this.CallerName.DataPropertyName = "sCallerName";
            this.CallerName.HeaderText = "呼叫器名称";
            this.CallerName.Name = "CallerName";
            this.CallerName.ReadOnly = true;
            this.CallerName.Width = 125;
            // 
            // SerialPort
            // 
            this.SerialPort.DataPropertyName = "sSerialPort";
            this.SerialPort.HeaderText = "串口";
            this.SerialPort.Name = "SerialPort";
            this.SerialPort.ReadOnly = true;
            // 
            // PhyAddr
            // 
            this.PhyAddr.DataPropertyName = "iPhyAddr";
            this.PhyAddr.HeaderText = "地址";
            this.PhyAddr.Name = "PhyAddr";
            this.PhyAddr.ReadOnly = true;
            // 
            // TimeoutSec
            // 
            this.TimeoutSec.DataPropertyName = "iTimeoutSec";
            this.TimeoutSec.HeaderText = "间隔";
            this.TimeoutSec.Name = "TimeoutSec";
            this.TimeoutSec.ReadOnly = true;
            this.TimeoutSec.Width = 150;
            // 
            // frmPhCallerBusiness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 600);
            this.Controls.Add(this.panel1);
            this.Name = "frmPhCallerBusiness";
            this.Text = "frmPhyCallerBusiness";
            this.Load += new System.EventHandler(this.frmPhCallerBusiness_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InfoList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox dpSerialPort;
        private System.Windows.Forms.TextBox txtTimeoutSec;
        private System.Windows.Forms.TextBox txtPhyAddr;
        private System.Windows.Forms.TextBox txtCallerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView InfoList;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox dpEvalList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn CallerNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CallerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhyAddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeoutSec;
    }
}