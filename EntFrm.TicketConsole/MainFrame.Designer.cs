namespace EntFrm.TicketConsole
{
    partial class MainFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrame));
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.txtTicketStyle = new EntFrm.Framework.Utility.RichTextBoxEx();
            this.btnRegistUser = new System.Windows.Forms.Button();
            this.lbTimeStr = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.txtTicketStyle);
            this.pnlContainer.Controls.Add(this.btnRegistUser);
            this.pnlContainer.Controls.Add(this.lbTimeStr);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(643, 493);
            this.pnlContainer.TabIndex = 0;
            // 
            // txtTicketStyle
            // 
            this.txtTicketStyle.Location = new System.Drawing.Point(30, 385);
            this.txtTicketStyle.Name = "txtTicketStyle";
            this.txtTicketStyle.Size = new System.Drawing.Size(1, 1);
            this.txtTicketStyle.TabIndex = 2;
            this.txtTicketStyle.Text = "";
            // 
            // btnRegistUser
            // 
            this.btnRegistUser.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRegistUser.Location = new System.Drawing.Point(50, 97);
            this.btnRegistUser.Name = "btnRegistUser";
            this.btnRegistUser.Size = new System.Drawing.Size(334, 99);
            this.btnRegistUser.TabIndex = 1;
            this.btnRegistUser.Text = "扫码报到";
            this.btnRegistUser.UseVisualStyleBackColor = true;
            this.btnRegistUser.Click += new System.EventHandler(this.btnRegistUser_Click);
            // 
            // lbTimeStr
            // 
            this.lbTimeStr.AutoSize = true;
            this.lbTimeStr.BackColor = System.Drawing.Color.Transparent;
            this.lbTimeStr.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTimeStr.ForeColor = System.Drawing.Color.Red;
            this.lbTimeStr.Location = new System.Drawing.Point(45, 26);
            this.lbTimeStr.Name = "lbTimeStr";
            this.lbTimeStr.Size = new System.Drawing.Size(236, 27);
            this.lbTimeStr.TabIndex = 0;
            this.lbTimeStr.Text = "2018-08-08 12:20";
            this.lbTimeStr.Click += new System.EventHandler(this.lbTimeStr_Click);
            // 
            // MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 493);
            this.Controls.Add(this.pnlContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainFrame";
            this.Text = "MainFrame";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrame_FormClosing);
            this.Load += new System.EventHandler(this.MainFrame_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Label lbTimeStr;
        private System.Windows.Forms.Button btnRegistUser;
        private Framework.Utility.RichTextBoxEx txtTicketStyle;
    }
}