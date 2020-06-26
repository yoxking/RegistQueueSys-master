using System.Drawing;
namespace EntFrm.SettingConsole
{
    partial class frmWorkttsBusiness
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
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnAddVar = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtFormatCalling = new System.Windows.Forms.TextBox();
            this.dpVariables = new System.Windows.Forms.ComboBox();
            this.dpVolume = new System.Windows.Forms.ComboBox();
            this.dpRate = new System.Windows.Forms.ComboBox();
            this.dpVoice = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtStyleName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.InfoList = new System.Windows.Forms.DataGridView();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFormatWaiting = new System.Windows.Forms.TextBox();
            this.TtsNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TtsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormatCalling = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormatWaiting = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.panel1.Size = new System.Drawing.Size(727, 596);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPlay);
            this.groupBox1.Controls.Add(this.btnAddVar);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.txtFormatWaiting);
            this.groupBox1.Controls.Add(this.txtFormatCalling);
            this.groupBox1.Controls.Add(this.dpVariables);
            this.groupBox1.Controls.Add(this.dpVolume);
            this.groupBox1.Controls.Add(this.dpRate);
            this.groupBox1.Controls.Add(this.dpVoice);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtStyleName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(17, 332);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(696, 252);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "语音设置";
            // 
            // btnPlay
            // 
            this.btnPlay.ForeColor = System.Drawing.Color.Black;
            this.btnPlay.Location = new System.Drawing.Point(568, 91);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(102, 30);
            this.btnPlay.TabIndex = 235;
            this.btnPlay.Text = "试听";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnAddVar
            // 
            this.btnAddVar.ForeColor = System.Drawing.Color.Black;
            this.btnAddVar.Location = new System.Drawing.Point(456, 91);
            this.btnAddVar.Name = "btnAddVar";
            this.btnAddVar.Size = new System.Drawing.Size(102, 30);
            this.btnAddVar.TabIndex = 236;
            this.btnAddVar.Text = "插入变量";
            this.btnAddVar.UseVisualStyleBackColor = true;
            this.btnAddVar.Click += new System.EventHandler(this.btnAddVar_Click);
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(568, 210);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 227;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtFormatCalling
            // 
            this.txtFormatCalling.Location = new System.Drawing.Point(86, 130);
            this.txtFormatCalling.Name = "txtFormatCalling";
            this.txtFormatCalling.Size = new System.Drawing.Size(582, 25);
            this.txtFormatCalling.TabIndex = 234;
            // 
            // dpVariables
            // 
            this.dpVariables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpVariables.FormattingEnabled = true;
            this.dpVariables.Location = new System.Drawing.Point(87, 96);
            this.dpVariables.Name = "dpVariables";
            this.dpVariables.Size = new System.Drawing.Size(344, 23);
            this.dpVariables.TabIndex = 233;
            // 
            // dpVolume
            // 
            this.dpVolume.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpVolume.FormattingEnabled = true;
            this.dpVolume.Location = new System.Drawing.Point(86, 61);
            this.dpVolume.Name = "dpVolume";
            this.dpVolume.Size = new System.Drawing.Size(120, 23);
            this.dpVolume.TabIndex = 231;
            // 
            // dpRate
            // 
            this.dpRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpRate.FormattingEnabled = true;
            this.dpRate.Location = new System.Drawing.Point(310, 61);
            this.dpRate.Name = "dpRate";
            this.dpRate.Size = new System.Drawing.Size(120, 23);
            this.dpRate.TabIndex = 232;
            // 
            // dpVoice
            // 
            this.dpVoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpVoice.FormattingEnabled = true;
            this.dpVoice.Location = new System.Drawing.Point(310, 32);
            this.dpVoice.Name = "dpVoice";
            this.dpVoice.Size = new System.Drawing.Size(360, 23);
            this.dpVoice.TabIndex = 230;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(259, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 15);
            this.label11.TabIndex = 227;
            this.label11.Text = "语速:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(35, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 228;
            this.label2.Text = "变量:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(37, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 15);
            this.label9.TabIndex = 228;
            this.label9.Text = "音量:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(260, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 15);
            this.label7.TabIndex = 229;
            this.label7.Text = "语音:";
            // 
            // txtStyleName
            // 
            this.txtStyleName.Location = new System.Drawing.Point(87, 27);
            this.txtStyleName.Name = "txtStyleName";
            this.txtStyleName.Size = new System.Drawing.Size(120, 25);
            this.txtStyleName.TabIndex = 226;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(5, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 223;
            this.label1.Text = "叫号格式:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(37, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 15);
            this.label6.TabIndex = 223;
            this.label6.Text = "名称:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.InfoList);
            this.groupBox5.Controls.Add(this.btnDel);
            this.groupBox5.Controls.Add(this.btnAdd);
            this.groupBox5.Location = new System.Drawing.Point(6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(707, 320);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "语音列表";
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
            this.TtsNo,
            this.TtsName,
            this.FormatCalling,
            this.FormatWaiting});
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
            this.btnDel.ForeColor = System.Drawing.Color.Black;
            this.btnDel.Location = new System.Drawing.Point(612, 274);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(67, 30);
            this.btnDel.TabIndex = 227;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(537, 274);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(67, 30);
            this.btnAdd.TabIndex = 227;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(5, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 223;
            this.label3.Text = "等候格式:";
            // 
            // txtFormatWaiting
            // 
            this.txtFormatWaiting.Location = new System.Drawing.Point(86, 161);
            this.txtFormatWaiting.Name = "txtFormatWaiting";
            this.txtFormatWaiting.Size = new System.Drawing.Size(582, 25);
            this.txtFormatWaiting.TabIndex = 234;
            // 
            // TtsNo
            // 
            this.TtsNo.DataPropertyName = "sTtsNo";
            this.TtsNo.HeaderText = "编号";
            this.TtsNo.Name = "TtsNo";
            this.TtsNo.ReadOnly = true;
            this.TtsNo.Visible = false;
            // 
            // TtsName
            // 
            this.TtsName.DataPropertyName = "sTtsName";
            this.TtsName.HeaderText = "语音名称";
            this.TtsName.Name = "TtsName";
            this.TtsName.ReadOnly = true;
            this.TtsName.Width = 125;
            // 
            // FormatCalling
            // 
            this.FormatCalling.DataPropertyName = "sFormatCalling";
            this.FormatCalling.HeaderText = "叫号格式";
            this.FormatCalling.Name = "FormatCalling";
            this.FormatCalling.ReadOnly = true;
            this.FormatCalling.Width = 200;
            // 
            // FormatWaiting
            // 
            this.FormatWaiting.DataPropertyName = "sFormatWaiting";
            this.FormatWaiting.HeaderText = "等候格式";
            this.FormatWaiting.Name = "FormatWaiting";
            this.FormatWaiting.ReadOnly = true;
            this.FormatWaiting.Width = 200;
            // 
            // frmWorkttsBusiness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 596);
            this.Controls.Add(this.panel1);
            this.Name = "frmWorkttsBusiness";
            this.Text = "frmWorkttsBusiness";
            this.Load += new System.EventHandler(this.frmWorkttsBusiness_Load);
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
        private System.Windows.Forms.TextBox txtStyleName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnAddVar;
        private System.Windows.Forms.TextBox txtFormatCalling;
        private System.Windows.Forms.ComboBox dpVariables;
        private System.Windows.Forms.ComboBox dpVolume;
        private System.Windows.Forms.ComboBox dpRate;
        private System.Windows.Forms.ComboBox dpVoice;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView InfoList;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtFormatWaiting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn TtsNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TtsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormatCalling;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormatWaiting;
    }
}