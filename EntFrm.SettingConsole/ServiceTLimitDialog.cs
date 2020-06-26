using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using System;
using System.Linq;
using System.Windows.Forms;

namespace EntFrm.SettingConsole
{
    public partial class ServiceTLimitDialog : Form
    {
        private string ServiceNo;
        public string sServiceNo
        {
            set { this.ServiceNo = value; }
            get { return this.ServiceNo; }
        }

        public ServiceTLimitDialog()
        {
            InitializeComponent();
        }

        private void ServiceTLimitDialog_Load(object sender, EventArgs e)
        {
            ServiceInfoBLL myBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            ServiceInfo info = myBoss.GetRecordByNo(ServiceNo);

            if(info!=null)
            {
                txtServiceName.Text = info.sServiceName;

                ckAMLimit.Checked = info.iAMLimit == 0 ? false : true;
                txtAMStartTime.Text = info.dAMStartTime.ToString("HH:mm:ss");
                txtAMEndTime.Text = info.dAMEndTime.ToString("HH:mm:ss");
                txtAMTotal.Value = info.iAMTotal;
                ckPMLimit.Checked = info.iPMLimit == 0 ? false : true;
                txtPMStartTime.Text = info.dPMStartTime.ToString("HH:mm:ss");
                txtPMEndTime.Text = info.dPMEndTime.ToString("HH:mm:ss");
                txtPMTotal.Value = info.iPMTotal;
                ckWeekLimit.Checked = info.iWeekLimit == 0 ? false : true;
                ckWeek1.Checked = info.sWeekDays.Contains("1;");
                ckWeek2.Checked = info.sWeekDays.Contains("2;");
                ckWeek3.Checked = info.sWeekDays.Contains("3;");
                ckWeek4.Checked = info.sWeekDays.Contains("4;");
                ckWeek5.Checked = info.sWeekDays.Contains("5;");
                ckWeek6.Checked = info.sWeekDays.Contains("6;");
                ckWeek7.Checked = info.sWeekDays.Contains("0;"); 

                if(!string.IsNullOrEmpty(info.sComments))
                {
                    ckSCardLimit.Checked = true;
                    string[] stemp = info.sComments.Split(';');
                    txtSCardNum.Text = stemp[0];
                    ckIsOne.Checked = stemp[1].Equals("1");
                    txtSCardInterval.Text = stemp[2];
                }
            }
            else
            {
                txtServiceName.Text = "N/A";
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
             ServiceInfoBLL myBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            ServiceInfo info = myBoss.GetRecordByNo(ServiceNo);

            if (info != null)
            {
                info.iAMLimit = 0;
                info.iPMLimit = 0;
                info.iWeekLimit = 0;

                if (ckAMLimit.Checked)
                {
                    info.iAMLimit = 1;
                    info.dAMStartTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd ") + txtAMStartTime.Text);
                    info.dAMEndTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd ") + txtAMEndTime.Text);
                    info.iAMTotal = (int)txtAMTotal.Value;
                }  

                if (ckPMLimit.Checked)
                {
                    info.iPMLimit = 1;
                    info.dPMStartTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd ") + txtPMStartTime.Text);
                    info.dPMEndTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd ") + txtPMEndTime.Text);
                    info.iPMTotal = (int)txtPMTotal.Value;
                } 
                if (ckWeekLimit.Checked)
                {
                    info.iWeekLimit = 1;
                    string stemp = "";
                    stemp += ckWeek1.Checked ? "1;" : "";
                    stemp += ckWeek2.Checked ? "2;" : "";
                    stemp += ckWeek3.Checked ? "3;" : "";
                    stemp += ckWeek4.Checked ? "4;" : "";
                    stemp += ckWeek5.Checked ? "5;" : "";
                    stemp += ckWeek6.Checked ? "6;" : "";
                    stemp += ckWeek7.Checked ? "0;" : "";
                    info.sWeekDays = stemp;
                } 

                if(ckSCardLimit.Checked)
                {
                    string stemp = "";
                    stemp += txtSCardNum.Text.Trim() + ";";
                    stemp += ckIsOne.Checked ? "1;" : "0;";
                    stemp += txtSCardInterval.Text.Trim() ;

                    info.sComments = stemp;
                }

                if(myBoss.UpdateRecord(info))
                {
                    MessageBox.Show("设置时间限制成功!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("设置时间限制失败!");
                }
            }
            else
            {
                MessageBox.Show("请先添加业务，再设置时间限制!");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
