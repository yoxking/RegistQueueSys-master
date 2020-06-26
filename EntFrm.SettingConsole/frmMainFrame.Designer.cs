namespace EntFrm.SettingConsole
{
    partial class frmMainFrame
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnService = new System.Windows.Forms.Button();
            this.btnCounter = new System.Windows.Forms.Button();
            this.btnCaller = new System.Windows.Forms.Button();
            this.btnTicketStyle = new System.Windows.Forms.Button();
            this.btnLed = new System.Windows.Forms.Button();
            this.btnVoice = new System.Windows.Forms.Button();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.btnEvaluator = new System.Windows.Forms.Button();
            this.btnStaff = new System.Windows.Forms.Button();
            this.btnSettting = new System.Windows.Forms.Button();
            this.btnMatrix = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.Controls.Add(this.btnMatrix);
            this.panel2.Controls.Add(this.btnSettting);
            this.panel2.Controls.Add(this.btnStaff);
            this.panel2.Controls.Add(this.btnService);
            this.panel2.Controls.Add(this.btnCounter);
            this.panel2.Controls.Add(this.btnCaller);
            this.panel2.Controls.Add(this.btnEvaluator);
            this.panel2.Controls.Add(this.btnTicketStyle);
            this.panel2.Controls.Add(this.btnLed);
            this.panel2.Controls.Add(this.btnVoice);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(140, 633);
            this.panel2.TabIndex = 1;
            // 
            // btnService
            // 
            this.btnService.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnService.Location = new System.Drawing.Point(12, 216);
            this.btnService.Name = "btnService";
            this.btnService.Size = new System.Drawing.Size(120, 45);
            this.btnService.TabIndex = 0;
            this.btnService.Tag = "ServPage";
            this.btnService.Text = "5.业务设置";
            this.btnService.UseVisualStyleBackColor = false;
            this.btnService.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnCounter
            // 
            this.btnCounter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCounter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCounter.Location = new System.Drawing.Point(12, 267);
            this.btnCounter.Name = "btnCounter";
            this.btnCounter.Size = new System.Drawing.Size(120, 45);
            this.btnCounter.TabIndex = 0;
            this.btnCounter.Tag = "CuntPage";
            this.btnCounter.Text = "6.科室设置";
            this.btnCounter.UseVisualStyleBackColor = false;
            this.btnCounter.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnCaller
            // 
            this.btnCaller.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCaller.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaller.Location = new System.Drawing.Point(12, 114);
            this.btnCaller.Name = "btnCaller";
            this.btnCaller.Size = new System.Drawing.Size(120, 45);
            this.btnCaller.TabIndex = 0;
            this.btnCaller.Tag = "CallerPage";
            this.btnCaller.Text = "3.呼叫器设置";
            this.btnCaller.UseVisualStyleBackColor = false;
            this.btnCaller.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnTicketStyle
            // 
            this.btnTicketStyle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnTicketStyle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTicketStyle.Location = new System.Drawing.Point(12, 165);
            this.btnTicketStyle.Name = "btnTicketStyle";
            this.btnTicketStyle.Size = new System.Drawing.Size(120, 45);
            this.btnTicketStyle.TabIndex = 0;
            this.btnTicketStyle.Tag = "TkStylePage";
            this.btnTicketStyle.Text = "4.号票样式";
            this.btnTicketStyle.UseVisualStyleBackColor = false;
            this.btnTicketStyle.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnLed
            // 
            this.btnLed.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLed.Location = new System.Drawing.Point(12, 63);
            this.btnLed.Name = "btnLed";
            this.btnLed.Size = new System.Drawing.Size(120, 45);
            this.btnLed.TabIndex = 0;
            this.btnLed.Tag = "LedPage";
            this.btnLed.Text = "2.LED设置";
            this.btnLed.UseVisualStyleBackColor = false;
            this.btnLed.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnVoice
            // 
            this.btnVoice.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnVoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoice.Location = new System.Drawing.Point(12, 12);
            this.btnVoice.Name = "btnVoice";
            this.btnVoice.Size = new System.Drawing.Size(120, 45);
            this.btnVoice.TabIndex = 0;
            this.btnVoice.Tag = "TtsPage";
            this.btnVoice.Text = "1.语音设置";
            this.btnVoice.UseVisualStyleBackColor = false;
            this.btnVoice.Click += new System.EventHandler(this.btn_Click);
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.White;
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(140, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(542, 633);
            this.pnlContainer.TabIndex = 2;
            // 
            // btnEvaluator
            // 
            this.btnEvaluator.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnEvaluator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEvaluator.Location = new System.Drawing.Point(12, 373);
            this.btnEvaluator.Name = "btnEvaluator";
            this.btnEvaluator.Size = new System.Drawing.Size(120, 45);
            this.btnEvaluator.TabIndex = 0;
            this.btnEvaluator.Tag = "EvaluatorPage";
            this.btnEvaluator.Text = "8.评价器";
            this.btnEvaluator.UseVisualStyleBackColor = false;
            this.btnEvaluator.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnStaff
            // 
            this.btnStaff.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnStaff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStaff.Location = new System.Drawing.Point(12, 322);
            this.btnStaff.Name = "btnStaff";
            this.btnStaff.Size = new System.Drawing.Size(120, 45);
            this.btnStaff.TabIndex = 0;
            this.btnStaff.Tag = "StaffPage";
            this.btnStaff.Text = "7.医生管理";
            this.btnStaff.UseVisualStyleBackColor = false;
            this.btnStaff.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnSettting
            // 
            this.btnSettting.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSettting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettting.Location = new System.Drawing.Point(14, 475);
            this.btnSettting.Name = "btnSettting";
            this.btnSettting.Size = new System.Drawing.Size(120, 45);
            this.btnSettting.TabIndex = 0;
            this.btnSettting.Tag = "SetPage";
            this.btnSettting.Text = "10.其他设置";
            this.btnSettting.UseVisualStyleBackColor = false;
            this.btnSettting.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnMatrix
            // 
            this.btnMatrix.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnMatrix.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMatrix.Location = new System.Drawing.Point(12, 424);
            this.btnMatrix.Name = "btnMatrix";
            this.btnMatrix.Size = new System.Drawing.Size(120, 45);
            this.btnMatrix.TabIndex = 0;
            this.btnMatrix.Tag = "MatrixPage";
            this.btnMatrix.Text = "9.综合屏";
            this.btnMatrix.UseVisualStyleBackColor = false;
            this.btnMatrix.Click += new System.EventHandler(this.btn_Click);
            // 
            // frmMainFrame
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(682, 633);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMainFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "排队设置";
            this.Load += new System.EventHandler(this.frmMainFrame_Load);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnVoice;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Button btnCounter;
        private System.Windows.Forms.Button btnTicketStyle;
        private System.Windows.Forms.Button btnLed;
        private System.Windows.Forms.Button btnService;
        private System.Windows.Forms.Button btnCaller;
        private System.Windows.Forms.Button btnMatrix;
        private System.Windows.Forms.Button btnSettting;
        private System.Windows.Forms.Button btnStaff;
        private System.Windows.Forms.Button btnEvaluator;
    }
}