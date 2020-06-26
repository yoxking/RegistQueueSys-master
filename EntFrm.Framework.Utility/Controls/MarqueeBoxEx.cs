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
    public partial class MarqueeBoxEx : UserControl
    {
        private PointF pp;
        private string temp; 
        private int TimeInterval;

        public int iTimeInterval
        {
            get { return TimeInterval; }
            set { TimeInterval = value; }
        }

        public MarqueeBoxEx()
        {
            InitializeComponent();

            pp = new PointF(lbContent.Size.Width, 0);
            temp = lbContent.Text;
            TimeInterval = 100;

            myTimer.Interval = TimeInterval;
            myTimer.Enabled = true; 
        }

        public void SetProperty(string text,int speed)
        {
            lbContent.Text = text;
            myTimer.Interval = speed;
            this.Refresh();
        }

        private void myTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Graphics g = lbContent.CreateGraphics();
                SizeF s = new SizeF();
                s = g.MeasureString(lbContent.Text, lbContent.Font);//测量文字长度  
                Brush brush = new SolidBrush(lbContent.ForeColor);
                g.Clear(this.BackColor);//清除背景 

                if (temp != lbContent.Text)//文字改变时,重新显示  
                {
                    pp = new PointF(lbContent.Size.Width, (this.Size.Height - s.Height - 20) / 2);
                    temp = lbContent.Text;
                }
                else
                    pp = new PointF(pp.X - 10, (this.Size.Height - s.Height - 20) / 2);//每次偏移10  

                if (pp.X <= -s.Width)
                    pp = new PointF(lbContent.Size.Width, (this.Size.Height - s.Height - 20) / 2);

                g.DrawString(lbContent.Text, lbContent.Font, brush, pp);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
