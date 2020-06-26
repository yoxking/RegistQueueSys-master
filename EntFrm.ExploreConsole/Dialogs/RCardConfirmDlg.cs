using EntFrm.ExploreConsole.Models;
using EntFrm.ExploreConsole.Pubutils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntFrm.ExploreConsole.Dialogs
{
    public partial class RCardConfirmDlg : Form
    {
        public string sStafferNo
        {
            set; get;
        }
        public string sServiceNo
        {
            set;get;
        }
        public string sRiCardNo
        {
            set; get;
        }

        public delegate void SetRUserCallback(RUserData ruserData); 
        public delegate void SetServiceCallback(string serviceData);
        public static SetRUserCallback PrintRUserInfo;
        public static SetServiceCallback PrintService;

        public RCardConfirmDlg()
        {
            InitializeComponent();
        }

        private void RCardConfirmDlg_Load(object sender, EventArgs e)
        {
            PrintService += doPrintService;
            PrintRUserInfo += doPrintRUserInfo;

            Task.Factory.StartNew(() =>
            {
                try
                {
                    //从接口调取科室信息
                    string serviceInfo = getServiceInfo(sServiceNo);
                    doPrintService(serviceInfo);

                    //从接口调取用户信息，
                    RUserData ruserData = WServHelper.getPatientInfoByRicardId(sRiCardNo);
                    PrintRUserInfo(ruserData);
                }
                catch(Exception ex) { }
            });
        }

        /// <summary>
        /// 更新文本框内容的方法
        /// </summary>
        /// <param name="text"></param>
        private void doPrintRUserInfo(RUserData ruserData)
        {
            if (this.txtName.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.txtName.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.txtName.Disposing || this.txtName.IsDisposed)
                        return;
                }

                SetRUserCallback d = new SetRUserCallback(doPrintRUserInfo);
                this.txtName.Invoke(d, new object[] { ruserData });
            }
            else
            {
                if (ruserData != null)
                {
                    txtUserId.Text = ruserData.Id;
                    txtName.Text = ruserData.Name;
                    txtSex.Text = ruserData.Sex;
                    txtAge.Text = ruserData.Age;
                    txtIdNo.Text = ruserData.IdNo;
                    txtRicardId.Text = ruserData.RiCardNo;
                    txtTelphone.Text = ruserData.Telphone;
                }
                else
                {
                    txtRicardId.Text = "未知卡号";
                }
            }
        }
        
        /// <param name="text"></param>
        private void doPrintService(string data)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (this.txtItemName.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.txtItemName.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.txtItemName.Disposing || this.txtItemName.IsDisposed)
                        return;
                }

                SetServiceCallback d = new SetServiceCallback(doPrintService);
                this.txtItemName.Invoke(d, new object[] { data });
            }
            else
            {
                txtItemName.Text = data;
            }
        }
        private string getServiceInfo(string serviceNo)
        {
            string baseUrl = PublicHelper.GetHomeUrl();
            baseUrl = baseUrl.Substring(0, baseUrl.LastIndexOf('/')) + "/GetServiceInfo";

            string result = MyHttpUtils.HttpGet(baseUrl + "?serviceNo="+serviceNo);
            return result;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string serviceNo = sServiceNo;
            string workTime = "1";
            string userid = txtUserId.Text.Trim();
            string name = txtName.Text.Trim();
            string age = txtAge.Text.Trim();
            string sex = (txtSex.Text.Trim() == "男" ? "1" : "0");
            string idNo = txtIdNo.Text.Trim();
            string ricardNo = txtRicardId.Text.Trim();
            string telphone = txtTelphone.Text.Trim();

            string baseUrl = PublicHelper.GetHomeUrl();
            baseUrl = baseUrl.Substring(0, baseUrl.LastIndexOf('/')) + "/RegisteReadCardByService";

            string sbody = "{ serviceno:'" + serviceNo + "',worktime:'" + workTime + "',userno:'"+ userid + "',username:'" + name + "',age:'" + age + "',sex:'" + sex + "',idno:'" + idNo + "',ricardno:'" + ricardNo + "',telphone:'" + telphone + "'}";

            string result = MyHttpUtils.HttpPost(baseUrl, sbody);

            if (result.Equals("Success"))
            {
                MessageBox.Show("挂号成功！");         
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("挂号失败！");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
