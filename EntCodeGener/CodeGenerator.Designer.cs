namespace EntCodeGener
{
    partial class CodeGenerator
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtConnString = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbTables = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btExecute = new System.Windows.Forms.Button();
            this.btConnectDb = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNameSpace = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbClassType = new System.Windows.Forms.ComboBox();
            this.lbResult = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.rbSingle = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.btnGenSqlite = new System.Windows.Forms.Button();
            this.btnGenBuss = new System.Windows.Forms.Button();
            this.btnWBuss = new System.Windows.Forms.Button();
            this.btnGenBuss2 = new System.Windows.Forms.Button();
            this.btnGenPager = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库连接：";
            // 
            // txtConnString
            // 
            this.txtConnString.Location = new System.Drawing.Point(168, 52);
            this.txtConnString.Margin = new System.Windows.Forms.Padding(4);
            this.txtConnString.Name = "txtConnString";
            this.txtConnString.Size = new System.Drawing.Size(808, 25);
            this.txtConnString.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 100);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "数据表：";
            // 
            // cbTables
            // 
            this.cbTables.FormattingEnabled = true;
            this.cbTables.Location = new System.Drawing.Point(168, 96);
            this.cbTables.Margin = new System.Windows.Forms.Padding(4);
            this.cbTables.Name = "cbTables";
            this.cbTables.Size = new System.Drawing.Size(393, 23);
            this.cbTables.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(105, 204);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "结果：";
            // 
            // btExecute
            // 
            this.btExecute.Location = new System.Drawing.Point(168, 556);
            this.btExecute.Margin = new System.Windows.Forms.Padding(4);
            this.btExecute.Name = "btExecute";
            this.btExecute.Size = new System.Drawing.Size(155, 35);
            this.btExecute.TabIndex = 6;
            this.btExecute.Text = "生成代码";
            this.btExecute.UseVisualStyleBackColor = true;
            this.btExecute.Click += new System.EventHandler(this.btExecute_Click);
            // 
            // btConnectDb
            // 
            this.btConnectDb.Location = new System.Drawing.Point(985, 50);
            this.btConnectDb.Margin = new System.Windows.Forms.Padding(4);
            this.btConnectDb.Name = "btConnectDb";
            this.btConnectDb.Size = new System.Drawing.Size(159, 29);
            this.btConnectDb.TabIndex = 7;
            this.btConnectDb.Text = "连接数据库";
            this.btConnectDb.UseVisualStyleBackColor = true;
            this.btConnectDb.Click += new System.EventHandler(this.btConnectDb_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 149);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "命名空间前缀：";
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Location = new System.Drawing.Point(168, 145);
            this.txtNameSpace.Margin = new System.Windows.Forms.Padding(4);
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new System.Drawing.Size(393, 25);
            this.txtNameSpace.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(641, 145);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "类型：";
            // 
            // cbClassType
            // 
            this.cbClassType.FormattingEnabled = true;
            this.cbClassType.Items.AddRange(new object[] {
            "Model",
            "Collections",
            "IDAL",
            "SQLServerDAL",
            "MyCloudDAL",
            "DALFactory",
            "BLL",
            "IService",
            "ServiceInstruction",
            "Business"});
            this.cbClassType.Location = new System.Drawing.Point(709, 141);
            this.cbClassType.Margin = new System.Windows.Forms.Padding(4);
            this.cbClassType.Name = "cbClassType";
            this.cbClassType.Size = new System.Drawing.Size(401, 23);
            this.cbClassType.TabIndex = 11;
            // 
            // lbResult
            // 
            this.lbResult.FormattingEnabled = true;
            this.lbResult.ItemHeight = 15;
            this.lbResult.Location = new System.Drawing.Point(168, 199);
            this.lbResult.Margin = new System.Windows.Forms.Padding(4);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(975, 334);
            this.lbResult.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(641, 106);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "模式：";
            // 
            // rbSingle
            // 
            this.rbSingle.AutoSize = true;
            this.rbSingle.Checked = true;
            this.rbSingle.Location = new System.Drawing.Point(709, 104);
            this.rbSingle.Margin = new System.Windows.Forms.Padding(4);
            this.rbSingle.Name = "rbSingle";
            this.rbSingle.Size = new System.Drawing.Size(103, 19);
            this.rbSingle.TabIndex = 13;
            this.rbSingle.TabStop = true;
            this.rbSingle.Text = "当前选定表";
            this.rbSingle.UseVisualStyleBackColor = true;
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Location = new System.Drawing.Point(880, 104);
            this.rbAll.Margin = new System.Windows.Forms.Padding(4);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(73, 19);
            this.rbAll.TabIndex = 13;
            this.rbAll.Text = "所有表";
            this.rbAll.UseVisualStyleBackColor = true;
            // 
            // btnGenSqlite
            // 
            this.btnGenSqlite.Location = new System.Drawing.Point(345, 556);
            this.btnGenSqlite.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenSqlite.Name = "btnGenSqlite";
            this.btnGenSqlite.Size = new System.Drawing.Size(155, 35);
            this.btnGenSqlite.TabIndex = 6;
            this.btnGenSqlite.Text = "生成Sqlite";
            this.btnGenSqlite.UseVisualStyleBackColor = true;
            this.btnGenSqlite.Click += new System.EventHandler(this.btnGenSqlite_Click);
            // 
            // btnGenBuss
            // 
            this.btnGenBuss.Location = new System.Drawing.Point(521, 556);
            this.btnGenBuss.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenBuss.Name = "btnGenBuss";
            this.btnGenBuss.Size = new System.Drawing.Size(155, 35);
            this.btnGenBuss.TabIndex = 6;
            this.btnGenBuss.Text = "生成Business";
            this.btnGenBuss.UseVisualStyleBackColor = true;
            this.btnGenBuss.Click += new System.EventHandler(this.btnGenBuss_Click);
            // 
            // btnWBuss
            // 
            this.btnWBuss.Location = new System.Drawing.Point(697, 556);
            this.btnWBuss.Margin = new System.Windows.Forms.Padding(4);
            this.btnWBuss.Name = "btnWBuss";
            this.btnWBuss.Size = new System.Drawing.Size(155, 35);
            this.btnWBuss.TabIndex = 6;
            this.btnWBuss.Text = "生成WBusiness";
            this.btnWBuss.UseVisualStyleBackColor = true;
            this.btnWBuss.Click += new System.EventHandler(this.btnWBuss_Click);
            // 
            // btnGenBuss2
            // 
            this.btnGenBuss2.Location = new System.Drawing.Point(880, 556);
            this.btnGenBuss2.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenBuss2.Name = "btnGenBuss2";
            this.btnGenBuss2.Size = new System.Drawing.Size(140, 35);
            this.btnGenBuss2.TabIndex = 6;
            this.btnGenBuss2.Text = "生成Business2";
            this.btnGenBuss2.UseVisualStyleBackColor = true;
            this.btnGenBuss2.Click += new System.EventHandler(this.btnGenBuss2_Click);
            // 
            // btnGenPager
            // 
            this.btnGenPager.Location = new System.Drawing.Point(1045, 556);
            this.btnGenPager.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenPager.Name = "btnGenPager";
            this.btnGenPager.Size = new System.Drawing.Size(126, 35);
            this.btnGenPager.TabIndex = 6;
            this.btnGenPager.Text = "生成PageHelper";
            this.btnGenPager.UseVisualStyleBackColor = true;
            this.btnGenPager.Click += new System.EventHandler(this.btnGenPager_Click);
            // 
            // CodeGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 619);
            this.Controls.Add(this.rbAll);
            this.Controls.Add(this.rbSingle);
            this.Controls.Add(this.lbResult);
            this.Controls.Add(this.cbClassType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNameSpace);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btConnectDb);
            this.Controls.Add(this.btnWBuss);
            this.Controls.Add(this.btnGenPager);
            this.Controls.Add(this.btnGenBuss2);
            this.Controls.Add(this.btnGenBuss);
            this.Controls.Add(this.btnGenSqlite);
            this.Controls.Add(this.btExecute);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbTables);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtConnString);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CodeGenerator";
            this.Text = "CodeGenerator";
            this.Load += new System.EventHandler(this.CodeGenerator_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConnString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbTables;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btExecute;
        private System.Windows.Forms.Button btConnectDb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNameSpace;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbClassType;
        private System.Windows.Forms.ListBox lbResult;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rbSingle;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.Button btnGenSqlite;
        private System.Windows.Forms.Button btnGenBuss;
        private System.Windows.Forms.Button btnWBuss;
        private System.Windows.Forms.Button btnGenBuss2;
        private System.Windows.Forms.Button btnGenPager;
    }
}