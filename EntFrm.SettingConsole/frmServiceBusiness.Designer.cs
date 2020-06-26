using System.Drawing;
namespace EntFrm.SettingConsole
{
    partial class frmServiceBusiness
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.InfoList = new System.Windows.Forms.DataGridView();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.dpTicketStyle = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dpServiceClass = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServiceType = new System.Windows.Forms.TextBox();
            this.txtServiceAlias = new System.Windows.Forms.TextBox();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTimeLimit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDigitLength = new System.Windows.Forms.NumericUpDown();
            this.txtStartNum = new System.Windows.Forms.NumericUpDown();
            this.ServiceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceAlias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DigitLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InfoList)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDigitLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartNum)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(727, 553);
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.groupBox5);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(727, 553);
            this.panel3.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.InfoList);
            this.groupBox5.Controls.Add(this.btnDel);
            this.groupBox5.Controls.Add(this.btnAdd);
            this.groupBox5.Location = new System.Drawing.Point(6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(707, 317);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "业务列表";
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
            this.ServiceNo,
            this.ServiceAlias,
            this.ServiceName,
            this.ServiceType,
            this.StartNum,
            this.DigitLength});
            this.InfoList.Location = new System.Drawing.Point(12, 25);
            this.InfoList.Name = "InfoList";
            this.InfoList.RowHeadersVisible = false;
            this.InfoList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
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
            this.btnDel.ForeColor = System.Drawing.Color.Black;
            this.btnDel.Location = new System.Drawing.Point(610, 271);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(72, 30);
            this.btnDel.TabIndex = 227;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(533, 271);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(71, 30);
            this.btnAdd.TabIndex = 227;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.dpTicketStyle);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.dpServiceClass);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtServiceType);
            this.groupBox1.Controls.Add(this.txtServiceAlias);
            this.groupBox1.Controls.Add(this.txtServiceName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnTimeLimit);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtDigitLength);
            this.groupBox1.Controls.Add(this.txtStartNum);
            this.groupBox1.Location = new System.Drawing.Point(6, 339);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(707, 184);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "业务设置";
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(562, 135);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 30);
            this.btnSave.TabIndex = 229;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dpTicketStyle
            // 
            this.dpTicketStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpTicketStyle.FormattingEnabled = true;
            this.dpTicketStyle.Items.AddRange(new object[] {
            "顶级业务",
            "普通业务"});
            this.dpTicketStyle.Location = new System.Drawing.Point(102, 101);
            this.dpTicketStyle.Name = "dpTicketStyle";
            this.dpTicketStyle.Size = new System.Drawing.Size(121, 23);
            this.dpTicketStyle.TabIndex = 227;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(22, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 15);
            this.label6.TabIndex = 220;
            this.label6.Text = "号票样式:";
            // 
            // dpServiceClass
            // 
            this.dpServiceClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpServiceClass.FormattingEnabled = true;
            this.dpServiceClass.Items.AddRange(new object[] {
            "顶级业务",
            "子级业务"});
            this.dpServiceClass.Location = new System.Drawing.Point(562, 25);
            this.dpServiceClass.Name = "dpServiceClass";
            this.dpServiceClass.Size = new System.Drawing.Size(121, 23);
            this.dpServiceClass.TabIndex = 227;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(512, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 220;
            this.label3.Text = "类型:";
            // 
            // txtServiceType
            // 
            this.txtServiceType.Location = new System.Drawing.Point(103, 62);
            this.txtServiceType.Name = "txtServiceType";
            this.txtServiceType.Size = new System.Drawing.Size(120, 25);
            this.txtServiceType.TabIndex = 226;
            // 
            // txtServiceAlias
            // 
            this.txtServiceAlias.Location = new System.Drawing.Point(102, 28);
            this.txtServiceAlias.Name = "txtServiceAlias";
            this.txtServiceAlias.Size = new System.Drawing.Size(120, 25);
            this.txtServiceAlias.TabIndex = 226;
            // 
            // txtServiceName
            // 
            this.txtServiceName.Location = new System.Drawing.Point(322, 25);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(120, 25);
            this.txtServiceName.TabIndex = 226;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(507, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 220;
            this.label2.Text = "长度:";
            // 
            // btnTimeLimit
            // 
            this.btnTimeLimit.ForeColor = System.Drawing.Color.Black;
            this.btnTimeLimit.Location = new System.Drawing.Point(562, 96);
            this.btnTimeLimit.Name = "btnTimeLimit";
            this.btnTimeLimit.Size = new System.Drawing.Size(120, 30);
            this.btnTimeLimit.TabIndex = 6;
            this.btnTimeLimit.Text = "限制设置";
            this.btnTimeLimit.UseVisualStyleBackColor = true;
            this.btnTimeLimit.Click += new System.EventHandler(this.btnTimeLimit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(256, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 220;
            this.label1.Text = "起始号:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(21, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 15);
            this.label5.TabIndex = 224;
            this.label5.Text = "业务编号:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(22, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 224;
            this.label4.Text = "业务前缀:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(241, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 15);
            this.label10.TabIndex = 224;
            this.label10.Text = "业务名称:";
            // 
            // txtDigitLength
            // 
            this.txtDigitLength.Location = new System.Drawing.Point(562, 57);
            this.txtDigitLength.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtDigitLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.txtDigitLength.Name = "txtDigitLength";
            this.txtDigitLength.Size = new System.Drawing.Size(120, 25);
            this.txtDigitLength.TabIndex = 206;
            this.txtDigitLength.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // txtStartNum
            // 
            this.txtStartNum.Location = new System.Drawing.Point(322, 59);
            this.txtStartNum.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtStartNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.txtStartNum.Name = "txtStartNum";
            this.txtStartNum.Size = new System.Drawing.Size(120, 25);
            this.txtStartNum.TabIndex = 206;
            this.txtStartNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ServiceNo
            // 
            this.ServiceNo.DataPropertyName = "sServiceNo";
            this.ServiceNo.HeaderText = "编号";
            this.ServiceNo.Name = "ServiceNo";
            this.ServiceNo.ReadOnly = true;
            this.ServiceNo.Visible = false;
            this.ServiceNo.Width = 80;
            // 
            // ServiceAlias
            // 
            this.ServiceAlias.DataPropertyName = "sServiceAlias";
            this.ServiceAlias.HeaderText = "业务编号";
            this.ServiceAlias.Name = "ServiceAlias";
            this.ServiceAlias.ReadOnly = true;
            this.ServiceAlias.Width = 120;
            // 
            // ServiceName
            // 
            this.ServiceName.DataPropertyName = "sServiceName";
            this.ServiceName.HeaderText = "业务名称";
            this.ServiceName.Name = "ServiceName";
            this.ServiceName.ReadOnly = true;
            this.ServiceName.Width = 200;
            // 
            // ServiceType
            // 
            this.ServiceType.DataPropertyName = "sServiceType";
            this.ServiceType.HeaderText = "业务前缀";
            this.ServiceType.Name = "ServiceType";
            this.ServiceType.ReadOnly = true;
            this.ServiceType.Width = 120;
            // 
            // StartNum
            // 
            this.StartNum.DataPropertyName = "iStartNum";
            this.StartNum.HeaderText = "起始号";
            this.StartNum.Name = "StartNum";
            this.StartNum.ReadOnly = true;
            // 
            // DigitLength
            // 
            this.DigitLength.DataPropertyName = "iDigitLength";
            this.DigitLength.HeaderText = "长度";
            this.DigitLength.Name = "DigitLength";
            this.DigitLength.ReadOnly = true;
            // 
            // frmServiceBusiness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 553);
            this.Controls.Add(this.panel1);
            this.Name = "frmServiceBusiness";
            this.Text = "frmServiceBusiness";
            this.Load += new System.EventHandler(this.frmServiceBusiness_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InfoList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDigitLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartNum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtDigitLength;
        private System.Windows.Forms.NumericUpDown txtStartNum;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnTimeLimit;
        private System.Windows.Forms.ComboBox dpServiceClass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView InfoList;
        private System.Windows.Forms.TextBox txtServiceType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox dpTicketStyle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtServiceAlias;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceAlias;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn DigitLength;
    }
}