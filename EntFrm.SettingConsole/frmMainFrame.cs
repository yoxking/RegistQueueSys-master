using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EntFrm.SettingConsole
{
    public partial class frmMainFrame : Form
    {
        public frmMainFrame()
        {
            InitializeComponent();
        }

        private void frmMainFrame_Load(object sender, EventArgs e)
        {
            try
            {
                pnlContainer.Controls.Clear();

                Form child = new frmWorkttsBusiness();
                child.TopLevel = false;
                child.FormBorderStyle = FormBorderStyle.None;
                child.Dock = DockStyle.Top;
                child.BringToFront();
                pnlContainer.Controls.Add(child);
                child.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show("出错提示:"+ex.Message);
            }
        } 

        private void myTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn=(Button)sender;
        
            string sTabPage = btn.Tag.ToString();
            Form child = null;

            switch (sTabPage)
            {
                case "ServPage":
                    pnlContainer.Controls.Clear();

                    child = new frmServiceBusiness();
                    child.TopLevel = false;
                    child.FormBorderStyle = FormBorderStyle.None;
                    child.Dock = DockStyle.Top;
                    child.BringToFront();
                    pnlContainer.Controls.Add(child);
                    child.Show();

                    break;
                case "CuntPage":
                    pnlContainer.Controls.Clear();
                    child = new frmCounterBusiness();
                    child.TopLevel = false;
                    child.FormBorderStyle = FormBorderStyle.None;
                    child.Dock = DockStyle.Top;
                    child.BringToFront();
                    pnlContainer.Controls.Add(child);
                    child.Show();

                    break;
                case "TkUIPage":
                    pnlContainer.Controls.Clear();
                    child = new frmTicketUIBusiness();
                    child.TopLevel = false;
                    child.FormBorderStyle = FormBorderStyle.None;
                    child.Dock = DockStyle.Top;
                    child.BringToFront();
                    pnlContainer.Controls.Add(child);
                    child.Show();
                    break;
                case "CallerPage":
                    pnlContainer.Controls.Clear();
                    child = new frmPhCallerBusiness();
                    child.TopLevel = false;
                    child.FormBorderStyle = FormBorderStyle.None;
                    child.Dock = DockStyle.Top;
                    child.BringToFront();
                    pnlContainer.Controls.Add(child);
                    child.Show();
                    break;
                case "EvaluatorPage":
                    pnlContainer.Controls.Clear();
                    child = new frmEvaluatorBusiness();
                    child.TopLevel = false;
                    child.FormBorderStyle = FormBorderStyle.None;
                    child.Dock = DockStyle.Top;
                    child.BringToFront();
                    pnlContainer.Controls.Add(child);
                    child.Show();
                    break;

                case "TkStylePage":
                    pnlContainer.Controls.Clear();
                    child = new frmTicketStyleBusiness();
                    child.TopLevel = false;
                    child.FormBorderStyle = FormBorderStyle.None;
                    child.Dock = DockStyle.Top;
                    child.BringToFront();
                    pnlContainer.Controls.Add(child);
                    child.Show();

                    break;
                case "FlowPage":
                    pnlContainer.Controls.Clear();
                    child = new frmWorkflowBusiness();
                    child.TopLevel = false;
                    child.FormBorderStyle = FormBorderStyle.None;
                    child.Dock = DockStyle.Top;
                    child.BringToFront();
                    pnlContainer.Controls.Add(child);
                    child.Show();

                    break;
                case "LedPage":
                    pnlContainer.Controls.Clear();
                    child = new frmLedDispBusiness();
                    child.TopLevel = false;
                    child.FormBorderStyle = FormBorderStyle.None;
                    child.Dock = DockStyle.Top;
                    child.BringToFront();
                    pnlContainer.Controls.Add(child);
                    child.Show();

                    break;
                case "TtsPage":
                    pnlContainer.Controls.Clear();
                    child = new frmWorkttsBusiness();
                    child.TopLevel = false;
                    child.FormBorderStyle = FormBorderStyle.None;
                    child.Dock = DockStyle.Top;
                    child.BringToFront();
                    pnlContainer.Controls.Add(child);
                    child.Show();

                    break;
                case "StaffPage":
                    pnlContainer.Controls.Clear();
                    child = new frmStafflistBusiness();
                    child.TopLevel = false;
                    child.FormBorderStyle = FormBorderStyle.None;
                    child.Dock = DockStyle.Top;
                    child.BringToFront();
                    pnlContainer.Controls.Add(child);
                    child.Show();

                    break; 
                case "MatrixPage":
                    pnlContainer.Controls.Clear();
                    child = new frmLedMatrixBusiness();
                    child.TopLevel = false;
                    child.FormBorderStyle = FormBorderStyle.None;
                    child.Dock = DockStyle.Top;
                    child.BringToFront();
                    pnlContainer.Controls.Add(child);
                    child.Show();

                    break;
                case "SetPage":
                    pnlContainer.Controls.Clear();
                    child = new frmSysettingBusiness();
                    child.TopLevel = false;
                    child.FormBorderStyle = FormBorderStyle.None;
                    child.Dock = DockStyle.Top;
                    child.BringToFront();
                    pnlContainer.Controls.Add(child);
                    child.Show();

                    break;
                default: break;
            }
        }  
    }
}
