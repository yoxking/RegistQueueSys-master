namespace EntFrm.Framework.Utility
{
    partial class TicketButtonEx
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbSubtitle = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbSubtitle
            // 
            this.lbSubtitle.AllowDrop = true;
            this.lbSubtitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSubtitle.AutoSize = true;
            this.lbSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.lbSubtitle.Location = new System.Drawing.Point(3, 45);
            this.lbSubtitle.Name = "lbSubtitle";
            this.lbSubtitle.Padding = new System.Windows.Forms.Padding(5);
            this.lbSubtitle.Size = new System.Drawing.Size(175, 25);
            this.lbSubtitle.TabIndex = 0;
            this.lbSubtitle.Text = "当前业务中排队等待3人";
            this.lbSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.BackColor = System.Drawing.Color.Transparent;
            this.lbTitle.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.Location = new System.Drawing.Point(30, 10);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(133, 30);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "对公业务";
            // 
            // TicketButtonEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lbSubtitle);
            this.Controls.Add(this.lbTitle);
            this.DoubleBuffered = true;
            this.Name = "TicketButtonEx";
            this.Size = new System.Drawing.Size(210, 70);
            this.Load += new System.EventHandler(this.TicketButtonEx_Load);
            this.Resize += new System.EventHandler(this.TicketButtonEx_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbSubtitle;
        public System.Windows.Forms.Label lbTitle;


    }
}
