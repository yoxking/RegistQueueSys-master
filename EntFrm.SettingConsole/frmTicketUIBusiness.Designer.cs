using System.Drawing;
namespace EntFrm.SettingConsole
{
    partial class frmTicketUIBusiness
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
            this.dpServiceRules = new System.Windows.Forms.ComboBox();
            this.btnUpt = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ckButtonShowText = new System.Windows.Forms.CheckBox();
            this.btnVisualDesign = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.InfoList = new System.Windows.Forms.DataGridView();
            this.ServiceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceAlias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DigitLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.panel1.Size = new System.Drawing.Size(727, 553);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dpServiceRules);
            this.groupBox1.Controls.Add(this.btnUpt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ckButtonShowText);
            this.groupBox1.Controls.Add(this.btnVisualDesign);
            this.groupBox1.Location = new System.Drawing.Point(8, 332);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(707, 148);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "按键设置";
            // 
            // dpServiceRules
            // 
            this.dpServiceRules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpServiceRules.FormattingEnabled = true;
            this.dpServiceRules.Location = new System.Drawing.Point(88, 68);
            this.dpServiceRules.Name = "dpServiceRules";
            this.dpServiceRules.Size = new System.Drawing.Size(423, 23);
            this.dpServiceRules.TabIndex = 229;
            // 
            // btnUpt
            // 
            this.btnUpt.ForeColor = System.Drawing.Color.Black;
            this.btnUpt.Location = new System.Drawing.Point(88, 103);
            this.btnUpt.Name = "btnUpt";
            this.btnUpt.Size = new System.Drawing.Size(89, 30);
            this.btnUpt.TabIndex = 227;
            this.btnUpt.Text = "保存";
            this.btnUpt.UseVisualStyleBackColor = true;
            this.btnUpt.Click += new System.EventHandler(this.btnUpt_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(30, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 228;
            this.label3.Text = "规则：";
            // 
            // ckButtonShowText
            // 
            this.ckButtonShowText.AutoSize = true;
            this.ckButtonShowText.ForeColor = System.Drawing.Color.Black;
            this.ckButtonShowText.Location = new System.Drawing.Point(88, 34);
            this.ckButtonShowText.Name = "ckButtonShowText";
            this.ckButtonShowText.Size = new System.Drawing.Size(89, 19);
            this.ckButtonShowText.TabIndex = 227;
            this.ckButtonShowText.Text = "显示文本";
            this.ckButtonShowText.UseVisualStyleBackColor = true;
            // 
            // btnVisualDesign
            // 
            this.btnVisualDesign.ForeColor = System.Drawing.Color.Black;
            this.btnVisualDesign.Location = new System.Drawing.Point(543, 24);
            this.btnVisualDesign.Name = "btnVisualDesign";
            this.btnVisualDesign.Size = new System.Drawing.Size(140, 71);
            this.btnVisualDesign.TabIndex = 227;
            this.btnVisualDesign.Text = "窗口设计";
            this.btnVisualDesign.UseVisualStyleBackColor = true;
            this.btnVisualDesign.Click += new System.EventHandler(this.btnVisualDesign_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.InfoList);
            this.groupBox5.Location = new System.Drawing.Point(6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(707, 312);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "按键列表";
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
            this.InfoList.Location = new System.Drawing.Point(15, 24);
            this.InfoList.Name = "InfoList";
            this.InfoList.RowHeadersVisible = false;
            this.InfoList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.InfoList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.InfoList.RowTemplate.Height = 27;
            this.InfoList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.InfoList.Size = new System.Drawing.Size(670, 240);
            this.InfoList.TabIndex = 229;
            this.InfoList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InfoList_CellClick);
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
            this.ServiceAlias.HeaderText = "业务ID";
            this.ServiceAlias.Name = "ServiceAlias";
            this.ServiceAlias.ReadOnly = true;
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
            // frmTicketUIBusiness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 553);
            this.Controls.Add(this.panel1);
            this.Name = "frmTicketUIBusiness";
            this.Text = "frmTicketUIBusiness";
            this.Load += new System.EventHandler(this.frmTicketUIBusiness_Load);
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
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnVisualDesign;
        private System.Windows.Forms.CheckBox ckButtonShowText;
        private System.Windows.Forms.Button btnUpt;
        private System.Windows.Forms.ComboBox dpServiceRules;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView InfoList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceAlias;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn DigitLength;
    }
}