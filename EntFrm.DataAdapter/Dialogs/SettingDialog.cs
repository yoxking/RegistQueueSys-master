using EntFrm.Framework.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EntFrm.DataAdapter.Dialogs
{
    public partial class SettingDialog : Form
    {
        public SettingDialog()
        {
            InitializeComponent();
        }

        private void Setting_Load(object sender, EventArgs e)
        { 
            txtServerIp.Text = IUserContext.GetConfigValue("ServerIp");
            txtWTcpPort.Text = IUserContext.GetConfigValue("WTcpPort");
            txtSTcpPort.Text = IUserContext.GetConfigValue("STcpPort");
            txtWHttpPort.Text = IUserContext.GetConfigValue("WHttpPort"); 

            txtConnStr.Text = IUserContext.GetConnStr();
        } 

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtServerIp.Text.Trim().Length > 0 && txtWTcpPort.Text.Trim().Length > 0)
            {

                string connStr = EnconfigHelper.Encrypt(txtConnStr.Text.Trim()); 

                IUserContext.SetConfigValue("ServerIp", txtServerIp.Text.Trim());
                IUserContext.SetConfigValue("WTcpPort", txtWTcpPort.Text.Trim());
                IUserContext.SetConfigValue("STcpPort", txtSTcpPort.Text.Trim());
                IUserContext.SetConfigValue("WHttpPort", txtWHttpPort.Text.Trim()); 

                IUserContext.SetConfigValue("SqlServer", connStr); 

                MessageBox.Show("系统设置保存成功，请重新启动服务！");
            }
            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTestConn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtConnStr.Text.Trim()))
                {
                    if (IDbaseHelper.ConnectionTest(txtConnStr.Text.Trim()))
                    {
                        MessageBox.Show("数据库连接成功!");
                    }
                    else
                    {
                        MessageBox.Show("数据库连接失败!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 


    }
}

