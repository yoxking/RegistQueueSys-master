namespace EntFrm.CallerConsole
{
    partial class frmMiniFrame
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
            this.btnNotCome = new System.Windows.Forms.Button();
            this.btnCallRepeat = new System.Windows.Forms.Button();
            this.btnCallNext = new System.Windows.Forms.Button();
            this.btnTicketNo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnNotCome
            // 
            this.btnNotCome.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNotCome.Location = new System.Drawing.Point(202, 51);
            this.btnNotCome.Name = "btnNotCome";
            this.btnNotCome.Size = new System.Drawing.Size(92, 33);
            this.btnNotCome.TabIndex = 24;
            this.btnNotCome.Text = "过号";
            this.btnNotCome.UseVisualStyleBackColor = true;
            this.btnNotCome.Click += new System.EventHandler(this.btnNotCome_Click);
            // 
            // btnCallRepeat
            // 
            this.btnCallRepeat.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCallRepeat.Location = new System.Drawing.Point(202, 10);
            this.btnCallRepeat.Name = "btnCallRepeat";
            this.btnCallRepeat.Size = new System.Drawing.Size(92, 35);
            this.btnCallRepeat.TabIndex = 23;
            this.btnCallRepeat.Text = "重呼";
            this.btnCallRepeat.UseVisualStyleBackColor = true;
            this.btnCallRepeat.Click += new System.EventHandler(this.btnCallRepeat_Click);
            // 
            // btnCallNext
            // 
            this.btnCallNext.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCallNext.Location = new System.Drawing.Point(300, 10);
            this.btnCallNext.Name = "btnCallNext";
            this.btnCallNext.Size = new System.Drawing.Size(82, 74);
            this.btnCallNext.TabIndex = 22;
            this.btnCallNext.Text = "呼叫";
            this.btnCallNext.UseVisualStyleBackColor = true;
            this.btnCallNext.Click += new System.EventHandler(this.btnCallNext_Click);
            // 
            // btnTicketNo
            // 
            this.btnTicketNo.BackColor = System.Drawing.SystemColors.Window;
            this.btnTicketNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTicketNo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTicketNo.ForeColor = System.Drawing.Color.Red;
            this.btnTicketNo.Location = new System.Drawing.Point(13, 10);
            this.btnTicketNo.Name = "btnTicketNo";
            this.btnTicketNo.Size = new System.Drawing.Size(183, 74);
            this.btnTicketNo.TabIndex = 25;
            this.btnTicketNo.UseVisualStyleBackColor = false;
            // 
            // frmMiniFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 96);
            this.Controls.Add(this.btnTicketNo);
            this.Controls.Add(this.btnNotCome);
            this.Controls.Add(this.btnCallRepeat);
            this.Controls.Add(this.btnCallNext);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMiniFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "虚拟叫号器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMiniFrame_FormClosing);
            this.Load += new System.EventHandler(this.frmMiniFrame_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnNotCome;
        private System.Windows.Forms.Button btnCallRepeat;
        private System.Windows.Forms.Button btnCallNext;
        private System.Windows.Forms.Button btnTicketNo;
    }
}