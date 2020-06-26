namespace EntFrm.SettingConsole
{
    partial class CounterGroupDialog
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.InfoList = new System.Windows.Forms.DataGridView();
            this.CheckedFlag = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CounterNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CounterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CounterAlias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtLedName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InfoList)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.InfoList);
            this.panel2.Controls.Add(this.txtLedName);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(531, 337);
            this.panel2.TabIndex = 3;
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
            this.CheckedFlag,
            this.CounterNo,
            this.CounterName,
            this.CounterAlias});
            this.InfoList.Location = new System.Drawing.Point(19, 73);
            this.InfoList.Name = "InfoList";
            this.InfoList.RowTemplate.Height = 27;
            this.InfoList.Size = new System.Drawing.Size(500, 260);
            this.InfoList.TabIndex = 6;
            // 
            // CheckedFlag
            // 
            this.CheckedFlag.DataPropertyName = "iCheckedFlag";
            this.CheckedFlag.FalseValue = "0";
            this.CheckedFlag.HeaderText = "选择";
            this.CheckedFlag.Name = "CheckedFlag";
            this.CheckedFlag.TrueValue = "1";
            this.CheckedFlag.Width = 60;
            // 
            // CounterNo
            // 
            this.CounterNo.DataPropertyName = "sCounterNo";
            this.CounterNo.HeaderText = "编号";
            this.CounterNo.Name = "CounterNo";
            this.CounterNo.ReadOnly = true;
            this.CounterNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CounterNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CounterNo.Visible = false;
            // 
            // CounterName
            // 
            this.CounterName.DataPropertyName = "sCounterName";
            this.CounterName.HeaderText = "窗口";
            this.CounterName.Name = "CounterName";
            this.CounterName.ReadOnly = true;
            this.CounterName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CounterName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CounterAlias
            // 
            this.CounterAlias.DataPropertyName = "sCounterAlias";
            this.CounterAlias.HeaderText = "窗口别名";
            this.CounterAlias.Name = "CounterAlias";
            this.CounterAlias.ReadOnly = true;
            this.CounterAlias.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CounterAlias.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CounterAlias.Width = 150;
            // 
            // txtLedName
            // 
            this.txtLedName.Location = new System.Drawing.Point(106, 23);
            this.txtLedName.Name = "txtLedName";
            this.txtLedName.ReadOnly = true;
            this.txtLedName.Size = new System.Drawing.Size(416, 25);
            this.txtLedName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "LED屏名称:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 337);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(531, 50);
            this.panel1.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnOk);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(313, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(218, 50);
            this.panel3.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(100, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(4, 7);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 30);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // CounterGroupDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 387);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CounterGroupDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "窗口组选择";
            this.Load += new System.EventHandler(this.CounterGroupDialog_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InfoList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtLedName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridView InfoList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckedFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn CounterNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CounterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CounterAlias;
    }
}