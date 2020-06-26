using EntFrm.TicketConsole.MPublicUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public partial class ContextDlg : Form
    {
        public ContextDlg()
        {
            InitializeComponent();
        }

        private void ContextDlg_Load(object sender, EventArgs e)
        {
            string ShutAtHour = IPublicHelper.GetConfigValue("ShutAtHour");
            string ShutAtMinute = IPublicHelper.GetConfigValue("ShutAtMinute");
            dpHours.SelectedItem = ShutAtHour;
            dpMinutes.SelectedItem = ShutAtMinute;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        } 

        private void btnNetwork_Click(object sender, EventArgs e)
        {
            SettingDlg dlg = new SettingDlg();
            dlg.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出系统吗？", "确认对话框", MessageBoxButtons.OKCancel) == DialogResult.OK)
            { 
                Application.Exit();
            }
        }

        private void btnReboot_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出系统并重启主机吗？", "确认对话框", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
                WindowsUtils.DoExitWindows(WindowsUtils.ExitWindows.Reboot);
            }
        }

        private void btnShutdown_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出系统并关闭主机吗？", "确认对话框", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
                WindowsUtils.DoExitWindows(WindowsUtils.ExitWindows.ShutDown);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            IPublicHelper.SetConfigValue("ShutAtHour", dpHours.SelectedItem.ToString());
            IPublicHelper.SetConfigValue("ShutAtMinute", dpMinutes.SelectedItem.ToString());

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
