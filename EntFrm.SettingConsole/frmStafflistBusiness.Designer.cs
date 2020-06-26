using System.Drawing;
namespace EntFrm.SettingConsole
{
    partial class frmStafflistBusiness
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
            this.btnChoicePhoto = new System.Windows.Forms.Button();
            this.dpStarLevel = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.txtStafferName = new System.Windows.Forms.TextBox();
            this.txtPsword = new System.Windows.Forms.TextBox();
            this.txtHeadPhoto = new System.Windows.Forms.TextBox();
            this.txtOrganizName = new System.Windows.Forms.TextBox();
            this.txtLoginId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.InfoList = new System.Windows.Forms.DataGridView();
            this.StafferNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sLoginId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StafferName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrganizName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StarLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.panel1.Size = new System.Drawing.Size(727, 612);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnChoicePhoto);
            this.groupBox1.Controls.Add(this.dpStarLevel);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.txtSummary);
            this.groupBox1.Controls.Add(this.txtStafferName);
            this.groupBox1.Controls.Add(this.txtPsword);
            this.groupBox1.Controls.Add(this.txtHeadPhoto);
            this.groupBox1.Controls.Add(this.txtOrganizName);
            this.groupBox1.Controls.Add(this.txtLoginId);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(8, 334);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(707, 247);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "员工设置";
            // 
            // btnChoicePhoto
            // 
            this.btnChoicePhoto.Location = new System.Drawing.Point(637, 85);
            this.btnChoicePhoto.Name = "btnChoicePhoto";
            this.btnChoicePhoto.Size = new System.Drawing.Size(40, 30);
            this.btnChoicePhoto.TabIndex = 231;
            this.btnChoicePhoto.Text = "...";
            this.btnChoicePhoto.UseVisualStyleBackColor = true;
            this.btnChoicePhoto.Click += new System.EventHandler(this.btnChoicePhoto_Click);
            // 
            // dpStarLevel
            // 
            this.dpStarLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpStarLevel.FormattingEnabled = true;
            this.dpStarLevel.Items.AddRange(new object[] {
            "一星",
            "二星",
            "三星",
            "四星",
            "五星"});
            this.dpStarLevel.Location = new System.Drawing.Point(103, 92);
            this.dpStarLevel.Name = "dpStarLevel";
            this.dpStarLevel.Size = new System.Drawing.Size(121, 23);
            this.dpStarLevel.TabIndex = 230;
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(571, 198);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 30);
            this.btnSave.TabIndex = 229;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(103, 121);
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(573, 71);
            this.txtSummary.TabIndex = 226;
            // 
            // txtStafferName
            // 
            this.txtStafferName.Location = new System.Drawing.Point(103, 54);
            this.txtStafferName.Name = "txtStafferName";
            this.txtStafferName.Size = new System.Drawing.Size(120, 25);
            this.txtStafferName.TabIndex = 226;
            // 
            // txtPsword
            // 
            this.txtPsword.Location = new System.Drawing.Point(322, 20);
            this.txtPsword.Name = "txtPsword";
            this.txtPsword.Size = new System.Drawing.Size(120, 25);
            this.txtPsword.TabIndex = 226;
            // 
            // txtHeadPhoto
            // 
            this.txtHeadPhoto.Location = new System.Drawing.Point(322, 87);
            this.txtHeadPhoto.Name = "txtHeadPhoto";
            this.txtHeadPhoto.ReadOnly = true;
            this.txtHeadPhoto.Size = new System.Drawing.Size(311, 25);
            this.txtHeadPhoto.TabIndex = 226;
            // 
            // txtOrganizName
            // 
            this.txtOrganizName.Location = new System.Drawing.Point(322, 53);
            this.txtOrganizName.Name = "txtOrganizName";
            this.txtOrganizName.Size = new System.Drawing.Size(354, 25);
            this.txtOrganizName.TabIndex = 226;
            // 
            // txtLoginId
            // 
            this.txtLoginId.Location = new System.Drawing.Point(103, 20);
            this.txtLoginId.Name = "txtLoginId";
            this.txtLoginId.Size = new System.Drawing.Size(120, 25);
            this.txtLoginId.TabIndex = 226;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(271, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 220;
            this.label3.Text = "密码:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(271, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 15);
            this.label5.TabIndex = 220;
            this.label5.Text = "照片:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(271, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 220;
            this.label1.Text = "部门:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(52, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 224;
            this.label2.Text = "简介:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(52, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 15);
            this.label4.TabIndex = 224;
            this.label4.Text = "星级:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(22, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 15);
            this.label10.TabIndex = 224;
            this.label10.Text = "员工姓名:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(37, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 15);
            this.label6.TabIndex = 223;
            this.label6.Text = "员工号:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.InfoList);
            this.groupBox5.Controls.Add(this.btnDel);
            this.groupBox5.Controls.Add(this.btnAdd);
            this.groupBox5.Location = new System.Drawing.Point(6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(707, 317);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "员工列表";
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
            this.StafferNo,
            this.sLoginId,
            this.StafferName,
            this.OrganizName,
            this.StarLevel});
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
            // StafferNo
            // 
            this.StafferNo.DataPropertyName = "sStafferNo";
            this.StafferNo.HeaderText = "编号";
            this.StafferNo.Name = "StafferNo";
            this.StafferNo.ReadOnly = true;
            this.StafferNo.Visible = false;
            // 
            // sLoginId
            // 
            this.sLoginId.DataPropertyName = "sLoginId";
            this.sLoginId.HeaderText = "医生号";
            this.sLoginId.Name = "sLoginId";
            this.sLoginId.ReadOnly = true;
            // 
            // StafferName
            // 
            this.StafferName.DataPropertyName = "sStafferName";
            this.StafferName.HeaderText = "医生姓名";
            this.StafferName.Name = "StafferName";
            this.StafferName.ReadOnly = true;
            this.StafferName.Width = 200;
            // 
            // OrganizName
            // 
            this.OrganizName.DataPropertyName = "sOrganizName";
            this.OrganizName.HeaderText = "部门";
            this.OrganizName.Name = "OrganizName";
            this.OrganizName.ReadOnly = true;
            this.OrganizName.Width = 200;
            // 
            // StarLevel
            // 
            this.StarLevel.DataPropertyName = "sStarLevel";
            this.StarLevel.HeaderText = "星级";
            this.StarLevel.Name = "StarLevel";
            this.StarLevel.ReadOnly = true;
            this.StarLevel.Width = 125;
            // 
            // btnDel
            // 
            this.btnDel.ForeColor = System.Drawing.Color.Black;
            this.btnDel.Location = new System.Drawing.Point(617, 271);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(61, 30);
            this.btnDel.TabIndex = 227;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(544, 271);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(67, 30);
            this.btnAdd.TabIndex = 227;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // frmStafflistBusiness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 612);
            this.Controls.Add(this.panel1);
            this.Name = "frmStafflistBusiness";
            this.Text = "frmStafflistBusiness";
            this.Load += new System.EventHandler(this.frmStafflistBusiness_Load);
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
        private System.Windows.Forms.TextBox txtStafferName;
        private System.Windows.Forms.TextBox txtPsword;
        private System.Windows.Forms.TextBox txtOrganizName;
        private System.Windows.Forms.TextBox txtLoginId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView InfoList;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnChoicePhoto;
        private System.Windows.Forms.ComboBox dpStarLevel;
        private System.Windows.Forms.TextBox txtHeadPhoto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn StafferNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn sLoginId;
        private System.Windows.Forms.DataGridViewTextBoxColumn StafferName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrganizName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StarLevel;
    }
}