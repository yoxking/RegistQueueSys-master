using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EntFrm.Framework.Utility
{
    public class ListViewEx : ListView
    {
        private struct EmbeddedControl
        {
            public Control Control;

            public int Column;

            public int Row;

            public DockStyle Dock;

            public ListViewItem Item;

            public bool Hide;
        }

        private const int LVM_FIRST = 4096;

        private const int LVM_GETCOLUMNORDERARRAY = 4155;

        private const int WM_PAINT = 15;

        private ImageList ImgList = new ImageList();

        private decimal rowHeight = 16m;

        private bool JustSelectedShow;

        public ArrayList _embeddedControls = new ArrayList();

        [DefaultValue(View.LargeIcon)]
        public new View View
        {
            get
            {
                return base.View;
            }
            set
            {
                foreach (ListViewEx.EmbeddedControl embeddedControl in this._embeddedControls)
                {
                    embeddedControl.Control.Visible = (value == View.Details);
                }
                base.View = value;
            }
        }

        [Category("Appearance"), DefaultValue(16), Description("设置ListView的行高，范围：16~256")]
        public decimal RowHeight
        {
            get
            {
                return this.rowHeight;
            }
            set
            {
                this.rowHeight = value;
                this.ImgList.ImageSize = new Size(1, (int)this.rowHeight);
                if (base.SmallImageList == null)
                {
                    base.SmallImageList = this.ImgList;
                }
                this.Refresh();
            }
        }

        [Category("Appearance"), DefaultValue(false), Description("是否只有选中项显示控件")]
        public bool OnlySelectedShow
        {
            get
            {
                return this.JustSelectedShow;
            }
            set
            {
                this.JustSelectedShow = value;
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wPar, IntPtr lPar);

        public ListViewEx()
        {
            this.ImgList.ImageSize = new Size(1, (int)this.rowHeight);
        }

        protected int[] GetColumnOrder()
        {
            IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)) * base.Columns.Count);
            if (ListViewEx.SendMessage(base.Handle, 4155, new IntPtr(base.Columns.Count), intPtr).ToInt32() == 0)
            {
                Marshal.FreeHGlobal(intPtr);
                return null;
            }
            int[] array = new int[base.Columns.Count];
            Marshal.Copy(intPtr, array, 0, base.Columns.Count);
            Marshal.FreeHGlobal(intPtr);
            return array;
        }

        protected Rectangle GetSubItemBounds(ListViewItem Item, int SubItem)
        {
            Rectangle empty = Rectangle.Empty;
            if (Item == null)
            {
                throw new ArgumentNullException("Item");
            }
            int[] columnOrder = this.GetColumnOrder();
            if (columnOrder == null)
            {
                return empty;
            }
            if (SubItem >= columnOrder.Length)
            {
                throw new IndexOutOfRangeException("SubItem " + SubItem + " 超出范围");
            }
            Rectangle bounds = Item.GetBounds(ItemBoundsPortion.Entire);
            int num = bounds.Left;
            int i;
            for (i = 0; i < columnOrder.Length; i++)
            {
                ColumnHeader columnHeader = base.Columns[columnOrder[i]];
                if (columnHeader.Index == SubItem)
                {
                    break;
                }
                num += columnHeader.Width;
            }
            empty = new Rectangle(num, bounds.Top, base.Columns[columnOrder[i]].Width, bounds.Height);
            return empty;
        }

        public void AddControl(Control c, int col, int row)
        {
            this.AddControl(c, col, row, DockStyle.Fill);
        }

        public void AddControl(Control c, int col, int row, DockStyle dock)
        {
            if (c == null)
            {
                throw new ArgumentNullException();
            }
            if (col >= base.Columns.Count || row >= base.Items.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            ListViewEx.EmbeddedControl embeddedControl;
            embeddedControl.Control = c;
            embeddedControl.Column = col;
            embeddedControl.Row = row;
            embeddedControl.Dock = dock;
            embeddedControl.Item = base.Items[row];
            embeddedControl.Hide = false;
            this._embeddedControls.Add(embeddedControl);
            c.Click += new EventHandler(this._embeddedControl_Click);
            base.Controls.Add(c);
            for (int i = 0; i < this._embeddedControls.Count; i++)
            {
                ListViewEx.EmbeddedControl embeddedControl2 = (ListViewEx.EmbeddedControl)this._embeddedControls[i];
                embeddedControl2.Row = embeddedControl2.Item.Index;
                this._embeddedControls.RemoveAt(i);
                this._embeddedControls.Insert(i, embeddedControl2);
            }
        }

        public void RemoveControl(Control c)
        {
            if (c == null)
            {
                throw new ArgumentNullException();
            }
            for (int i = 0; i < this._embeddedControls.Count; i++)
            {
                ListViewEx.EmbeddedControl embeddedControl = (ListViewEx.EmbeddedControl)this._embeddedControls[i];
                embeddedControl.Row = embeddedControl.Item.Index;
                this._embeddedControls.RemoveAt(i);
                this._embeddedControls.Insert(i, embeddedControl);
            }
            for (int j = 0; j < this._embeddedControls.Count; j++)
            {
                if (((ListViewEx.EmbeddedControl)this._embeddedControls[j]).Control == c)
                {
                    c.Click -= new EventHandler(this._embeddedControl_Click);
                    base.Controls.Remove(c);
                    this._embeddedControls.RemoveAt(j);
                    return;
                }
            }
            throw new Exception("该控件没有添加");
        }

        public Control GetControl(int col, int row)
        {
            foreach (ListViewEx.EmbeddedControl embeddedControl in this._embeddedControls)
            {
                if (embeddedControl.Row == row && embeddedControl.Column == col)
                {
                    return embeddedControl.Control;
                }
            }
            return null;
        }

        public void SetControlVisible(Control c, bool visible)
        {
            if (c == null)
            {
                throw new ArgumentNullException();
            }
            for (int i = 0; i < this._embeddedControls.Count; i++)
            {
                ListViewEx.EmbeddedControl embeddedControl = (ListViewEx.EmbeddedControl)this._embeddedControls[i];
                if (embeddedControl.Control == c)
                {
                    this._embeddedControls.RemoveAt(i);
                    embeddedControl.Hide = !visible;
                    this._embeddedControls.Insert(i, embeddedControl);
                    embeddedControl.Control.Visible = visible;
                    return;
                }
            }
        }

        public ListViewItem GetItemByControl(Control ctrl)
        {
            foreach (ListViewEx.EmbeddedControl embeddedControl in this._embeddedControls)
            {
                if (embeddedControl.Control == ctrl)
                {
                    return embeddedControl.Item;
                }
            }
            return null;
        }

        protected override void WndProc(ref Message m)
        {
            int msg = m.Msg;
            if (msg == 15 && this.View == View.Details)
            {
                int i = 0;
                while (i < this._embeddedControls.Count)
                {
                    ListViewEx.EmbeddedControl embeddedControl = (ListViewEx.EmbeddedControl)this._embeddedControls[i];
                    embeddedControl.Row = embeddedControl.Item.Index;
                    this._embeddedControls.RemoveAt(i);
                    this._embeddedControls.Insert(i, embeddedControl);
                    Rectangle subItemBounds;
                    try
                    {
                        subItemBounds = this.GetSubItemBounds(embeddedControl.Item, embeddedControl.Column);
                    }
                    catch (Exception)
                    {
                        embeddedControl.Control.Visible = false;
                        goto IL_2A2;
                    }
                    goto IL_8F;
                IL_2A2:
                    i++;
                    continue;
                IL_8F:
                    if ((base.HeaderStyle != ColumnHeaderStyle.None && subItemBounds.Top < this.Font.Height) || (this.JustSelectedShow && (base.SelectedItems.Count != 1 || !embeddedControl.Item.Selected)) || embeddedControl.Hide)
                    {
                        embeddedControl.Control.Visible = false;
                        goto IL_2A2;
                    }
                    embeddedControl.Control.Visible = true;
                    embeddedControl.Control.Refresh();
                    subItemBounds.Inflate(-1, -1);
                    subItemBounds.Offset(0, -1);
                    subItemBounds.Width++;
                    subItemBounds.Height++;
                    if (embeddedControl.Control.Width > subItemBounds.Width)
                    {
                        embeddedControl.Control.Width = subItemBounds.Width;
                    }
                    if (embeddedControl.Control.Height > subItemBounds.Height)
                    {
                        embeddedControl.Control.Height = subItemBounds.Height;
                    }
                    switch (embeddedControl.Dock)
                    {
                        case DockStyle.None:
                            subItemBounds.Offset((subItemBounds.Width - embeddedControl.Control.Width) / 2, (subItemBounds.Height - embeddedControl.Control.Height) / 2);
                            subItemBounds.Size = embeddedControl.Control.Size;
                            break;
                        case DockStyle.Top:
                            subItemBounds.Height = embeddedControl.Control.Height;
                            break;
                        case DockStyle.Bottom:
                            subItemBounds.Offset(0, subItemBounds.Height - embeddedControl.Control.Height);
                            subItemBounds.Height = embeddedControl.Control.Height;
                            break;
                        case DockStyle.Left:
                            subItemBounds.Width = embeddedControl.Control.Width;
                            break;
                        case DockStyle.Right:
                            subItemBounds.Offset(subItemBounds.Width - embeddedControl.Control.Width, 0);
                            subItemBounds.Width = embeddedControl.Control.Width;
                            break;
                    }
                    embeddedControl.Control.Dock = DockStyle.None;
                    embeddedControl.Control.Bounds = subItemBounds;
                    goto IL_2A2;
                }
            }
            base.WndProc(ref m);
        }

        private void _embeddedControl_Click(object sender, EventArgs e)
        {
            foreach (ListViewEx.EmbeddedControl embeddedControl in this._embeddedControls)
            {
                if (embeddedControl.Control == (Control)sender)
                {
                    if (!embeddedControl.Item.Selected)
                    {
                        base.SelectedItems.Clear();
                        embeddedControl.Item.Selected = true;
                    }
                    break;
                }
            }
        }
    }
}