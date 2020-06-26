using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace EntFrm.Framework.Utility
{
    public class CkGroupBoxEx : GroupBox
    {
        public delegate void GroupBoxCheckHandle(object sender);

        public delegate void GroupBoxCheckBoxClickHandle(object sender);

        private bool bChecked;

        private bool bPressed;

        private CheckBoxState cbsState = CheckBoxState.UncheckedNormal;

        private Point ptPos = default(Point);

        private Size szCheckBox = default(Size);

        private Rectangle rectCheckBoxBg = default(Rectangle);

        private Rectangle rectCheckBoxText = default(Rectangle);

        [Category("属性已更改"), Description("在控件的Checked属性值更改时引发的事件。")]
        public event CkGroupBoxEx.GroupBoxCheckHandle CheckedChanged
        {
            add
            {
                base.Events.AddHandler(this, value);
            }
            remove
            {
                base.Events.RemoveHandler(this, value);
            }
        }

        [Category("Behavior"), Description("在控件的复选框状态更改时引发的事件。")]
        public event CkGroupBoxEx.GroupBoxCheckBoxClickHandle CheckBoxClicked
        {
            add
            {
                base.Events.AddHandler(this, value);
            }
            remove
            {
                base.Events.RemoveHandler(this, value);
            }
        }

        [Category("Appearance"), DefaultValue(false), Description("指示组件是否处于选中状态。")]
        public bool Checked
        {
            get
            {
                return this.bChecked;
            }
            set
            {
                this.bChecked = value;
                if (this.bChecked)
                {
                    this.cbsState = CheckBoxState.CheckedNormal;
                }
                else
                {
                    this.cbsState = CheckBoxState.UncheckedNormal;
                }
                Graphics g = Graphics.FromHwnd(base.Handle);
                CheckBoxRenderer.DrawCheckBox(g, this.ptPos, this.cbsState);
                foreach (Control control in base.Controls)
                {
                    control.Enabled = this.bChecked;
                }
                this.Refresh();
                CkGroupBoxEx.GroupBoxCheckHandle groupBoxCheckHandle = base.Events[this] as CkGroupBoxEx.GroupBoxCheckHandle;
                if (groupBoxCheckHandle != null)
                {
                    groupBoxCheckHandle(this);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (e.ClipRectangle != base.ClientRectangle)
            {
                base.OnPaint(e);
                return;
            }
            this.szCheckBox = CheckBoxRenderer.GetGlyphSize(e.Graphics, this.cbsState);
            Size size = TextRenderer.MeasureText(this.Text, this.Font, new Size(0, 0), TextFormatFlags.NoPadding);
            this.rectCheckBoxBg.X = e.ClipRectangle.Location.X + 8;
            this.rectCheckBoxBg.Y = e.ClipRectangle.Location.Y;
            this.rectCheckBoxBg.Width = size.Width + this.szCheckBox.Width + 5;
            this.rectCheckBoxBg.Height = size.Height;
            string text = Regex.Replace(this.Text, ".", " ");
            int width = TextRenderer.MeasureText(text, this.Font, new Size(0, 0), TextFormatFlags.NoPadding).Width;
            int arg_126_0 = TextRenderer.MeasureText(" ", this.Font, new Size(0, 0), TextFormatFlags.NoPadding).Width;
            do
            {
                text += " ";
                width = TextRenderer.MeasureText(text, this.Font, new Size(0, 0), TextFormatFlags.NoPadding).Width;
            }
            while (width < this.rectCheckBoxBg.Width);
            GroupBoxRenderer.DrawGroupBox(e.Graphics, e.ClipRectangle, text, this.Font, GroupBoxState.Normal);
            this.ptPos.X = e.ClipRectangle.Location.X + 10;
            this.ptPos.Y = e.ClipRectangle.Location.Y + size.Height / 2 - this.szCheckBox.Height / 2;
            if (this.ptPos.Y < 0)
            {
                this.ptPos.Y = 0;
            }
            this.rectCheckBoxText.X = this.ptPos.X + this.szCheckBox.Width + 2;
            this.rectCheckBoxText.Y = e.ClipRectangle.Location.Y;
            this.rectCheckBoxText.Width = size.Width;
            this.rectCheckBoxText.Height = size.Height;
            CheckBoxRenderer.DrawCheckBox(e.Graphics, this.ptPos, this.rectCheckBoxText, this.Text, this.Font, false, this.cbsState);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.rectCheckBoxBg.Contains(e.Location))
            {
                if (this.bChecked)
                {
                    this.cbsState = CheckBoxState.CheckedHot;
                }
                else
                {
                    this.cbsState = CheckBoxState.UncheckedHot;
                }
                Graphics g = Graphics.FromHwnd(base.Handle);
                CheckBoxRenderer.DrawCheckBox(g, this.ptPos, this.cbsState);
                return;
            }
            if (this.cbsState == CheckBoxState.UncheckedHot || this.cbsState == CheckBoxState.CheckedHot)
            {
                if (this.bChecked)
                {
                    this.cbsState = CheckBoxState.CheckedNormal;
                }
                else
                {
                    this.cbsState = CheckBoxState.UncheckedNormal;
                }
                Graphics g2 = Graphics.FromHwnd(base.Handle);
                CheckBoxRenderer.DrawCheckBox(g2, this.ptPos, this.cbsState);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.rectCheckBoxBg.Contains(e.Location))
            {
                this.bPressed = true;
                if (this.bChecked)
                {
                    this.cbsState = CheckBoxState.CheckedPressed;
                }
                else
                {
                    this.cbsState = CheckBoxState.UncheckedPressed;
                }
                Graphics g = Graphics.FromHwnd(base.Handle);
                CheckBoxRenderer.DrawCheckBox(g, this.ptPos, this.cbsState);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (this.rectCheckBoxBg.Contains(e.Location) && this.bPressed)
            {
                this.bChecked = !this.bChecked;
                foreach (Control control in base.Controls)
                {
                    control.Enabled = this.bChecked;
                }
                CkGroupBoxEx.GroupBoxCheckHandle groupBoxCheckHandle = base.Events[this] as CkGroupBoxEx.GroupBoxCheckHandle;
                if (groupBoxCheckHandle != null)
                {
                    groupBoxCheckHandle(this);
                }
                CkGroupBoxEx.GroupBoxCheckBoxClickHandle groupBoxCheckBoxClickHandle = base.Events[this] as CkGroupBoxEx.GroupBoxCheckBoxClickHandle;
                if (groupBoxCheckBoxClickHandle != null)
                {
                    groupBoxCheckBoxClickHandle(this);
                }
            }
            this.bPressed = false;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (this.bChecked)
            {
                this.cbsState = CheckBoxState.CheckedNormal;
            }
            else
            {
                this.cbsState = CheckBoxState.UncheckedNormal;
            }
            Graphics g = Graphics.FromHwnd(base.Handle);
            CheckBoxRenderer.DrawCheckBox(g, this.ptPos, this.cbsState);
        }

        protected override void OnCreateControl()
        {
            foreach (Control control in base.Controls)
            {
                control.Enabled = this.bChecked;
            }
            base.OnCreateControl();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (base.Enabled)
            {
                if (this.bChecked)
                {
                    this.cbsState = CheckBoxState.CheckedNormal;
                }
                else
                {
                    this.cbsState = CheckBoxState.UncheckedNormal;
                }
            }
            else if (this.bChecked)
            {
                this.cbsState = CheckBoxState.CheckedDisabled;
            }
            else
            {
                this.cbsState = CheckBoxState.UncheckedDisabled;
            }
            Graphics g = Graphics.FromHwnd(base.Handle);
            CheckBoxRenderer.DrawCheckBox(g, this.ptPos, this.rectCheckBoxText, this.Text, this.Font, false, this.cbsState);
        }
    }
}
