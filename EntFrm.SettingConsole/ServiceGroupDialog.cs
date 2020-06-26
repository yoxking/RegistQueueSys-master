using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EntFrm.SettingConsole
{
    public partial class ServiceGroupDialog : Form
    {
        private string counterName;

        public string CounterName
        {
            get { return counterName; }
            set { counterName = value; }
        }


        private string serviceGroupText;

        public string ServiceGroupText
        {
            get { return serviceGroupText; }
            set { serviceGroupText = value; }
        }

        private string serviceGroupValue;

        public string ServiceGroupValue
        {
            get { return serviceGroupValue; }
            set { serviceGroupValue = value; }
        } 

        public ServiceGroupDialog()
        {
            InitializeComponent();
        }

        private void ServiceGroupDialog_Load(object sender, EventArgs e)
        {
            txtCounterName.Text = this.counterName;
            DoBindDataSource();
        }

        private void DoBindDataSource()
        {
            int count=0;
            ServiceInfoBLL myBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            ServiceInfoCollections InfoColl = myBoss.GetRecordsByPaging(ref count, 1, 100, " BranchNo = '" + IUserContext.GetBranchNo() + "' And HaveChild=0");

            List<ServiceGroup> serviceList = null;
             ServiceGroup service=null;

            if(InfoColl!=null&&InfoColl.Count>0)
            {
                serviceList = new List<ServiceGroup>();
                foreach(ServiceInfo info in InfoColl)
                {
                    service = new ServiceGroup();
                    service.iCheckedFlag = 0;
                    service.sServiceNo = info.sServiceNo;
                    service.sServiceName = info.sServiceName;
                    service.sServiceAlias = info.sServiceAlias;
                    service.iPriority = 0;
                    int i=serviceGroupValue.IndexOf(info.sServiceNo);
                    if(i>=0)
                    {
                        try
                        {
                            i = int.Parse(serviceGroupValue.Substring(i + info.sServiceNo.Length + 1, 1));
                        }
                        catch(Exception ex)
                        {
                            i = 0;
                        }
                        service.iCheckedFlag = 1;
                        service.iPriority = i;                        
                    }
                    serviceList.Add(service);
                }
            }

            InfoList.AutoGenerateColumns = false;
            InfoList.DataSource = serviceList;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                serviceGroupText = "";
                serviceGroupValue = "";
                int priority = 0;

                for (int i = 0; i < InfoList.RowCount; i++)
                {
                    if (InfoList.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                    {
                        try
                        {
                            priority = int.Parse(InfoList.Rows[i].Cells["Priority"].Value.ToString());
                            if (priority > 9)
                            {
                                priority = 9;
                            }
                            else if (priority < 0)
                            {
                                priority = 0;
                            }
                        }
                        catch (Exception ex)
                        { }
                        serviceGroupText += InfoList.Rows[i].Cells["ServiceName"].Value.ToString() + ":" + priority.ToString() + ";";
                        serviceGroupValue += InfoList.Rows[i].Cells["ServiceNo"].Value.ToString() + ":" + priority.ToString() + ";";
                    }
                }

                if (serviceGroupText.Length > 0)
                {
                    serviceGroupText = serviceGroupText.Substring(0, serviceGroupText.Length - 1);
                    serviceGroupValue = serviceGroupValue.Substring(0, serviceGroupValue.Length - 1);

                    this.DialogResult = DialogResult.OK; //ok
                    this.Close();
                }
                else
                {
                    MessageBox.Show("请至少选择一项业务");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示：" + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; //取消
            this.Close();
        }
    }
}
