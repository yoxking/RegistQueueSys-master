using EntFrm.Framework.Utility;
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
    public partial class SettingDlg : Form
    {
        public SettingDlg()
        {
            InitializeComponent();
        }

        private void SettingDlg_Load(object sender, EventArgs e)
        {

            Init_Printer();
            Init_RegMode();

            txtIpAddress.Text = IPublicHelper.GetConfigValue("ServerIp");
            txtPort.Text = IPublicHelper.GetConfigValue("WTcpPort");
            ckPrintTicket.Checked = bool.Parse(IPublicHelper.GetConfigValue("PrintTicket"));
            ckPrintRecipe.Checked = bool.Parse(IPublicHelper.GetConfigValue("PrintRecipe"));
            dpPrinters.SelectedItem = IPublicHelper.GetConfigValue("PrinterName");
            dpPrinters2.SelectedItem = IPublicHelper.GetConfigValue("Printer2Name");
            dpRegisteMode.SelectedValue = IPublicHelper.GetConfigValue("RegisteMode");
            txtServiceName.Text = IPublicHelper.GetConfigValue("ServiceName");
            txtStafferName.Text = IPublicHelper.GetConfigValue("StafferName");
        }

        private void Init_Printer()
        {
            List<String> printlist = PrinterHelper.GetPrinterList();

            dpPrinters.Items.AddRange(printlist.ToArray());
            dpPrinters2.Items.AddRange(printlist.ToArray());
        }

        private void Init_RegMode()
        {
            //string sResult = IUserContext.OnExecuteCommand_Xp("getAllCounters", null);
            List<ItemObject> InfoList = new List<ItemObject>();
            InfoList.Add(new ItemObject("RegisteFlows", "预约报到模式"));
            InfoList.Add(new ItemObject("RusersInfo", "挂号报到模式"));
            InfoList.Add(new ItemObject("RecipeFlows", "取药报到模式"));

            dpRegisteMode.DataSource = InfoList;
            dpRegisteMode.ValueMember = "Name";
            dpRegisteMode.DisplayMember = "Value";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                IPublicHelper.SetConfigValue("ServerIp", txtIpAddress.Text.Trim());
                IPublicHelper.SetConfigValue("WTcpPort", txtPort.Text.Trim());
                IPublicHelper.SetConfigValue("PrintTicket", ckPrintTicket.Checked.ToString());
                IPublicHelper.SetConfigValue("PrintRecipe", ckPrintRecipe.Checked.ToString());
                IPublicHelper.SetConfigValue("PrinterName", dpPrinters.SelectedItem.ToString());
                IPublicHelper.SetConfigValue("Printer2Name", dpPrinters2.SelectedItem.ToString());
                IPublicHelper.SetConfigValue("RegisteMode", dpRegisteMode.SelectedValue.ToString());
                IPublicHelper.SetConfigValue("ServiceName", txtServiceName.Text.Trim());
                IPublicHelper.SetConfigValue("StafferName", txtStafferName.Text.Trim());

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
            this.DialogResult = DialogResult.Cancel; //失败
            this.Close(); //关闭登陆窗体
        }
    }
}
