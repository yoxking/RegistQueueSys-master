using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntFrm.Framework.Utility
{
    public partial class TicketButtonEx : UserControl
    {
        public TicketButtonEx()
        {
            InitializeComponent();

            ////  TODO:  在  InitComponent  调用后添加任何初始化 
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            ////开启双缓冲
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            ////this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.ResizeRedraw, true);
        } 

        private void RefreshButton()
        {
            lbTitle.Location = new Point((this.Width - this.lbTitle.Width) / 2, 10);
            lbSubtitle.Location = new Point((this.Width - this.lbSubtitle.Width) / 2, this.Height - this.lbSubtitle.Height - 10);
        }

        private void TicketButtonEx_Resize(object sender, EventArgs e)
        {
            RefreshButton();
        }

        private void TicketButtonEx_Load(object sender, EventArgs e)
        {
            RefreshButton();
        }
    }
}
