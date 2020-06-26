using EntFrm.Business.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EntFrm.CallerConsole
{
    public partial class SettingDialog : Form
    {
        public SettingDialog()
        {
            InitializeComponent();
        }

        private void SettingDialog_Load(object sender, EventArgs e)
        {
            DoBindCounterList();
            InitializeForm();
        }

        private void DoBindCounterList()
        {
            try
            {
                string sResult = IUserContext.OnExecuteCommand_Xp("getAllCounters", null);
                List<CounterInfo> InfoList = JsonConvert.DeserializeObject<List<CounterInfo>>(sResult);

                dpCounterList.DataSource = InfoList;
                dpCounterList.ValueMember = "sCounterNo";
                dpCounterList.DisplayMember = "sCounterName";
            }
            catch (Exception ex)
            {
                MessageBox.Show("服务器连接失败!");
            }
        }

        private void InitializeForm()
        {
            try
            {
                txtIpAddress.Text = IPublicHelper.Get_ServerIp();
                txtWTcpPort.Text = IPublicHelper.Get_WTcpPort();
                txtWHttpPort.Text = IPublicHelper.Get_WHttpPort(); 

                if (dpCounterList.Items != null && dpCounterList.Items.Count > 0)
                {
                    dpCounterList.SelectedValue = IPublicHelper.Get_CounterNo();
                } 
            }
            catch(Exception ex)
            { }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                IPublicHelper.Set_ConfigValue("ServerIp", txtIpAddress.Text.Trim());
                IPublicHelper.Set_ConfigValue("WTcpPort", txtWTcpPort.Text.Trim());
                IPublicHelper.Set_ConfigValue("WHttpPort", txtWHttpPort.Text.Trim()); 
                IPublicHelper.Set_ConfigValue("CounterNo", "00000000");

                if (dpCounterList.Items != null && dpCounterList.Items.Count > 0)
                {
                    IPublicHelper.Set_ConfigValue("CounterNo", dpCounterList.SelectedValue.ToString());
                }

                MessageBox.Show("保存参数信息成功，请重新启动程序!");

                this.DialogResult = DialogResult.OK; //成功
                this.Close(); //关闭登陆窗体
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存参数信息出错！详细信息：" + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
