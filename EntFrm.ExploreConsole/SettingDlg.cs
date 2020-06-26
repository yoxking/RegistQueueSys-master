using EntFrm.ExploreConsole.Pubutils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EntFrm.ExploreConsole
{
    public partial class SettingDlg : Form
    {
        public SettingDlg()
        {
            InitializeComponent();
        }

        private void SettingDlg_Load(object sender, EventArgs e)
        {
            string homeUrl = PublicHelper.GetHomeUrl();
            string workMode = PublicHelper.GetConfigValue("WorkMode");
            bool isFull = bool.Parse(PublicHelper.GetIsFull());

            txtHomeUrl.Text = homeUrl;
            dpWorkMode.SelectedItem = workMode;
            ckIsFull.Checked = isFull;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                string workMode = dpWorkMode.SelectedItem.ToString();
                PublicHelper.SetConfigValue("HomeUrl", txtHomeUrl.Text.Trim());
                PublicHelper.SetConfigValue("WorkMode", workMode);
                PublicHelper.SetConfigValue("IsFull", ckIsFull.Checked.ToString());
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
