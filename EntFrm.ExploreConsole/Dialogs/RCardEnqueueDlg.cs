using EntFrm.ExploreConsole.Models;
using EntFrm.ExploreConsole.Pubutils;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntFrm.ExploreConsole.Dialogs
{
    public partial class RCardEnqueueDlg : Form
    {
        public delegate void SetRUserCallback(RUserData ruserData);
        public delegate void SetServiceCallback(string serviceData);
        public static SetRUserCallback PrintRUserInfo;
        public static SetServiceCallback PrintService;

        public string WorkingMode { set; get; }
        public RCardEnqueueDlg()
        {
            InitializeComponent();
        }

        private void RCardEnqueueDlg_Load(object sender, EventArgs e)
        {
            PrintService += doPrintService;
            PrintRUserInfo += doPrintRUserInfo;

            //医生模式
            if (WorkingMode.Equals("STAFF"))
            {
                lbItemName.Text = "挂号医生：";
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        //获取业务科室列表
                        string baseUrl = PublicHelper.GetHomeUrl();
                        String pdata = MyHttpUtils.HttpGet(baseUrl + "/IAdapter/GetStafferList");
                        PrintService(pdata);
                    }
                    catch (Exception ex) { }
                });
            }
            else
            {
                lbItemName.Text = "挂号科室：";
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                    //获取业务科室列表
                    string baseUrl = PublicHelper.GetHomeUrl();
                        String pdata = MyHttpUtils.HttpGet(baseUrl + "/IAdapter/GetServiceList");
                        PrintService(pdata);
                    }
                    catch (Exception ex) { }
                });
            }
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
                    txtSex.Text = ruserData.Sex ;
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
            if (this.dpItemList.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.dpItemList.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.dpItemList.Disposing || this.dpItemList.IsDisposed)
                        return;
                }

                SetServiceCallback d = new SetServiceCallback(doPrintService);
                this.dpItemList.Invoke(d, new object[] { data });
            }
            else
            {
                if (!string.IsNullOrEmpty(data))
                {
                    List<ItemObject> itemList = JsonConvert.DeserializeObject<List<ItemObject>>(data);

                    dpItemList.DataSource = itemList;
                    dpItemList.ValueMember = "Value";
                    dpItemList.DisplayMember = "Name";
                }
            }
        }

        private void btnReadCard_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    //取卡号
                    string cardNo = RCardHelper.ReadRCardNo();
                    if (!string.IsNullOrEmpty(cardNo))
                    {
                        //从接口调取用户信息，
                        RUserData ruserData = WServHelper.getPatientInfoByRicardId(cardNo);
                        PrintRUserInfo(ruserData);
                    }
                }
                catch (Exception ex) { }
            });

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //取卡号
            string ricardNo = txtRicardId.Text;
            string itemNo = dpItemList.SelectedValue.ToString();

            try
            { 
                if (!string.IsNullOrEmpty(ricardNo)&&!string.IsNullOrEmpty(itemNo))
                {
                    string baseUrl = PublicHelper.GetHomeUrl();
                    string sbody = "";
                    string result = "";

                    //医生模式
                    if (WorkingMode.Equals("STAFF"))
                    {
                        sbody = "{stafferno:'" + itemNo + "',ricardno:'" + ricardNo + "'}";

                        result = MyHttpUtils.HttpPost(baseUrl + "/IAdapter/EnqueueReadCardByStaffer", sbody);
                    }
                    else
                    {
                        sbody = "{serviceno:'" + itemNo + "',ricardno:'" + ricardNo + "'}";

                        result = MyHttpUtils.HttpPost(baseUrl + "/IAdapter/EnqueueReadCardByService", sbody);
                    }

                    if (result.Equals("Success"))
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("报到失败!");
                    }
                }
                else
                {
                    MessageBox.Show("报到失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("报到失败!");
            }
        } 

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
