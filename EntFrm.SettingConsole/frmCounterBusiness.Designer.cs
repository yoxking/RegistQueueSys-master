using System.Drawing;
namespace EntFrm.SettingConsole
{
    partial class frmCounterBusiness
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
            this.dpLedList = new System.Windows.Forms.ComboBox();
            this.dpVoiceList = new System.Windows.Forms.ComboBox();
            this.dpCallerList = new System.Windows.Forms.ComboBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.txtCounterAlias = new System.Windows.Forms.TextBox();
            this.btnChooseServices = new System.Windows.Forms.Button();
            this.txtServiceGroupText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCounterName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.InfoList = new System.Windows.Forms.DataGridView();
            this.CounterNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CounterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CounterAlias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceGroupText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
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
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(727, 580);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.dpLedList);
            this.groupBox1.Controls.Add(this.dpVoiceList);
            this.groupBox1.Controls.Add(this.dpCallerList);
            this.groupBox1.Controls.Add(this.txtComment);
            this.groupBox1.Controls.Add(this.txtCounterAlias);
            this.groupBox1.Controls.Add(this.btnChooseServices);
            this.groupBox1.Controls.Add(this.txtServiceGroupText);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCounterName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(6, 345);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(707, 209);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "科室设置";
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(560, 171);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(121, 30);
            this.btnSave.TabIndex = 228;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dpLedList
            // 
            this.dpLedList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpLedList.FormattingEnabled = true;
            this.dpLedList.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6"});
            this.dpLedList.Location = new System.Drawing.Point(351, 65);
            this.dpLedList.Name = "dpLedList";
            this.dpLedList.Size = new System.Drawing.Size(121, 23);
            this.dpLedList.TabIndex = 227;
            // 
            // dpVoiceList
            // 
            this.dpVoiceList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpVoiceList.FormattingEnabled = true;
            this.dpVoiceList.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6"});
            this.dpVoiceList.Location = new System.Drawing.Point(102, 65);
            this.dpVoiceList.Name = "dpVoiceList";
            this.dpVoiceList.Size = new System.Drawing.Size(154, 23);
            this.dpVoiceList.TabIndex = 227;
            // 
            // dpCallerList
            // 
            this.dpCallerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpCallerList.FormattingEnabled = true;
            this.dpCallerList.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6"});
            this.dpCallerList.Location = new System.Drawing.Point(560, 60);
            this.dpCallerList.Name = "dpCallerList";
            this.dpCallerList.Size = new System.Drawing.Size(121, 23);
            this.dpCallerList.TabIndex = 227;
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(103, 140);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(578, 25);
            this.txtComment.TabIndex = 226;
            // 
            // txtCounterAlias
            // 
            this.txtCounterAlias.Location = new System.Drawing.Point(351, 27);
            this.txtCounterAlias.Name = "txtCounterAlias";
            this.txtCounterAlias.Size = new System.Drawing.Size(120, 25);
            this.txtCounterAlias.TabIndex = 226;
            // 
            // btnChooseServices
            // 
            this.btnChooseServices.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnChooseServices.ForeColor = System.Drawing.Color.Black;
            this.btnChooseServices.Location = new System.Drawing.Point(560, 103);
            this.btnChooseServices.Name = "btnChooseServices";
            this.btnChooseServices.Size = new System.Drawing.Size(121, 30);
            this.btnChooseServices.TabIndex = 6;
            this.btnChooseServices.Text = "选择";
            this.btnChooseServices.UseVisualStyleBackColor = true;
            this.btnChooseServices.Click += new System.EventHandler(this.btnChooseServices_Click);
            // 
            // txtServiceGroupText
            // 
            this.txtServiceGroupText.Enabled = false;
            this.txtServiceGroupText.Location = new System.Drawing.Point(103, 105);
            this.txtServiceGroupText.Name = "txtServiceGroupText";
            this.txtServiceGroupText.Size = new System.Drawing.Size(451, 25);
            this.txtServiceGroupText.TabIndex = 226;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(291, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 15);
            this.label5.TabIndex = 223;
            this.label5.Text = "LED屏:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(51, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 15);
            this.label4.TabIndex = 223;
            this.label4.Text = "语音:";
            // 
            // txtCounterName
            // 
            this.txtCounterName.Location = new System.Drawing.Point(103, 27);
            this.txtCounterName.Name = "txtCounterName";
            this.txtCounterName.Size = new System.Drawing.Size(153, 25);
            this.txtCounterName.TabIndex = 226;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(494, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 223;
            this.label2.Text = "呼叫器:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(49, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 15);
            this.label7.TabIndex = 220;
            this.label7.Text = "备注:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(37, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 220;
            this.label1.Text = "业务组:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(270, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 15);
            this.label10.TabIndex = 224;
            this.label10.Text = "科室别名:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(22, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 15);
            this.label6.TabIndex = 223;
            this.label6.Text = "科室编号:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.InfoList);
            this.groupBox5.Controls.Add(this.btnDel);
            this.groupBox5.Controls.Add(this.btnAdd);
            this.groupBox5.Location = new System.Drawing.Point(6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(707, 323);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "科室列表";
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
            this.CounterNo,
            this.CounterName,
            this.CounterAlias,
            this.ServiceGroupText});
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
            // CounterNo
            // 
            this.CounterNo.DataPropertyName = "sCounterNo";
            this.CounterNo.HeaderText = "编号";
            this.CounterNo.Name = "CounterNo";
            this.CounterNo.ReadOnly = true;
            this.CounterNo.Visible = false;
            // 
            // CounterName
            // 
            this.CounterName.DataPropertyName = "sCounterName";
            this.CounterName.HeaderText = "科室编号";
            this.CounterName.Name = "CounterName";
            this.CounterName.ReadOnly = true;
            this.CounterName.Width = 125;
            // 
            // CounterAlias
            // 
            this.CounterAlias.DataPropertyName = "sCounterAlias";
            this.CounterAlias.HeaderText = "科室别名";
            this.CounterAlias.Name = "CounterAlias";
            this.CounterAlias.ReadOnly = true;
            this.CounterAlias.Width = 200;
            // 
            // ServiceGroupText
            // 
            this.ServiceGroupText.DataPropertyName = "sServiceGroupText";
            this.ServiceGroupText.HeaderText = "业务组";
            this.ServiceGroupText.Name = "ServiceGroupText";
            this.ServiceGroupText.ReadOnly = true;
            this.ServiceGroupText.Width = 285;
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
            // frmCounterBusiness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 580);
            this.Controls.Add(this.panel1);
            this.Name = "frmCounterBusiness";
            this.Text = "frmCounterBusiness";
            this.Load += new System.EventHandler(this.frmCounterBusiness_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InfoList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCounterAlias;
        private System.Windows.Forms.Button btnChooseServices;
        private System.Windows.Forms.TextBox txtServiceGroupText;
        private System.Windows.Forms.TextBox txtCounterName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView InfoList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox dpCallerList;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox dpLedList;
        private System.Windows.Forms.ComboBox dpVoiceList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn CounterNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CounterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CounterAlias;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceGroupText;
    }
}