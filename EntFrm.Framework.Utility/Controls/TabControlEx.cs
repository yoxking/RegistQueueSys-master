using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace EntFrm.Framework.Utility
{    
    public class tabImage
    {
        private Image image;

        public Image Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
            }
        }
    }

    [ToolboxBitmap(typeof(TabControl))]
    public class TabControlEx : TabControl
    {

        List<tabImage> _ImageList; 

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [DefaultValue(typeof(bool), "false")]
        [CategoryAttribute("自定义属性")]
        public List<tabImage> ImageList
        {
            get
            {
                if (_ImageList == null)
                {
                    _ImageList = new List<tabImage>();
                }
                return this._ImageList;
            }
            set
            {
                this._ImageList = value;
                base.Invalidate(true);
            }
        }
        private Point _ImagePoint;


        #region 变量
        private Color _backColor = Color.Transparent;

        private bool _DrawBack = false;
        [DefaultValue(typeof(bool), "false")]
        [CategoryAttribute("自定义属性")]
        public bool DrawBack
        {
            get { return this._DrawBack; }
            set
            {
                this._DrawBack = value;
                base.Invalidate(true);
            }
        }

        private int _Width = 2;
        [DefaultValue(typeof(int), "2")]
        [CategoryAttribute("自定义属性")]
        public int Width
        {
            get { return this._Width; }
            set
            {
                this._Width = value;
                base.Invalidate(true);
            }
        }

        private Color _checkColor = Color.FromArgb(45, 151, 222);
        [DefaultValue(typeof(Color), "45, 151, 222")]
        [CategoryAttribute("自定义属性")]
        public Color CheckColor
        {
            get { return this._checkColor; }
            set
            {
                this._checkColor = value;
                base.Invalidate(true);
            }
        }
        private Color _checkBackColor = Color.FromArgb(45, 151, 222);
        [DefaultValue(typeof(Color), "45, 151, 222")]
        [CategoryAttribute("自定义属性")]
        public Color CheckBackColor
        {
            get { return this._checkBackColor; }
            set
            {
                this._checkBackColor = value;
                base.Invalidate(true);
            }
        }

        private Color _hoverColor = Color.FromArgb(196, 203, 207);
        [DefaultValue(typeof(Color), "196, 203, 207")]
        [CategoryAttribute("自定义属性")]
        public Color HoverColor
        {
            get { return this._hoverColor; }
            set
            {
                this._hoverColor = value;
                base.Invalidate(true);
            }
        }
        private Color _hoverBackColor = Color.FromArgb(196, 203, 207);
        [DefaultValue(typeof(Color), "196, 203, 207")]
        [CategoryAttribute("自定义属性")]
        public Color HoverBackColor
        {
            get { return this._hoverBackColor; }
            set
            {
                this._hoverBackColor = value;
                base.Invalidate(true);
            }
        }

        private Color _normalColor = Color.FromArgb(217, 225, 229);
        [DefaultValue(typeof(Color), "217, 225, 229")]
        [CategoryAttribute("自定义属性")]
        public Color NormalColor
        {
            get { return this._normalColor; }
            set
            {
                this._normalColor = value;
                base.Invalidate(true);
            }
        }


        private Color _normalBackColor = Color.FromArgb(217, 225, 229);
        [DefaultValue(typeof(Color), "217, 225, 229")]
        [CategoryAttribute("鼠标移到tabpage的背景")]
        public Color NormalBackColor
        {
            get { return this._normalBackColor; }
            set
            {
                this._normalBackColor = value;
                base.Invalidate(true);
            }
        }
        
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public TabControlEx(): base()
        {
            base.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.DoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
            base.SizeMode = TabSizeMode.Fixed;
            base.Alignment = TabAlignment.Left;
            base.ItemSize = new Size(60, 180);
            base.UpdateStyles();
        }
        #endregion

        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(typeof(Color), "234, 247, 254")]
        [CategoryAttribute("自定义属性")]
        public override Color BackColor
        {
            get { return this._backColor; }
            set
            {
                this._backColor = value;
                base.Invalidate(true);
            }
        }

        public Point ImagePoint
        {
            get
            {
                return _ImagePoint;
            }

            set
            {
                _ImagePoint = value;
            }
        }



        #endregion

        #region Override Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.DrawTabPages(g);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!this.DesignMode)
                base.Invalidate();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!this.DesignMode)
                base.Invalidate();
        }
        public const int WM_CONTEXTMENU = 0x007B;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg != (int)WM_CONTEXTMENU)//0x007B鼠标右键
            {
                base.WndProc(ref m);
            }
        }
        #endregion


        #region Draw Methods
        
        private void DrawTabPages(Graphics g)
        {
            Rectangle tabRect = Rectangle.Empty;
            Point cursorPoint = this.PointToClient(MousePosition);
            for (int i = 0; i < base.TabCount; i++)
            {
                TabPage page = this.TabPages[i];
                tabRect = this.GetTabRect(i);
                Rectangle work = new Rectangle(tabRect.X+ tabRect.Width - _Width, tabRect.Y, _Width, tabRect.Height-1);
                Rectangle Nwork = new Rectangle(tabRect.X, tabRect.Y, tabRect.Width, tabRect.Height);
                if (this.SelectedIndex == i)//是否选中
                {
                    
                    if (_DrawBack)
                    {
                        using (SolidBrush brush = new SolidBrush(_checkBackColor))
                        {
                            g.FillRectangle(brush, Nwork);
                        }
                    } 
                  using(SolidBrush brush = new SolidBrush(_checkColor))
                    {
                        g.FillRectangle(brush, work);
                    }
                }
                else if (tabRect.Contains(cursorPoint))//鼠标滑过
                {
                    
                    if (_DrawBack)
                    {
                        using (SolidBrush brush = new SolidBrush(_hoverBackColor))
                        {
                            g.FillRectangle(brush, Nwork);
                        }
                    }
                    using (SolidBrush brush = new SolidBrush(_hoverColor))
                    {
                        g.FillRectangle(brush, work);
                    }
                }
                else
                {
                    if (_DrawBack)
                    {
                        using (SolidBrush brush = new SolidBrush(_normalBackColor))
                        {
                            g.FillRectangle(brush, Nwork);
                        }
                    }
                    using (SolidBrush brush = new SolidBrush(_normalColor))
                    {
                        g.FillRectangle(brush, work);
                    }
                    
                }
              

                if (ImageList!=null&&ImageList.Count>i)
                {
                    if (ImageList[i].Image!=null)
                    {
                        g.DrawImage(ImageList[i].Image, tabRect.X + ImagePoint.X, tabRect.Y + ImagePoint.Y);
                    }
                }

            
                TextRenderer.DrawText(g, page.Text, page.Font,Nwork, page.ForeColor);
            }
        }
        #endregion
    }
}

