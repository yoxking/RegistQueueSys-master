using EntFrm.Business.BLL;
using EntFrm.Business.Model.Collections;
using System;
using System.Windows.Forms;

namespace EntFrm.SettingConsole
{
    public partial class ServiceSelectDialog : Form
    {
        private string ServiceNos;

        public string sServiceNos
        {
            get { return ServiceNos; }
            set { ServiceNos = value; }
        }
        private string ServiceNames;

        public string sServiceNames
        {
            get { return ServiceNames; }
            set { ServiceNames = value; }
        }


        public ServiceSelectDialog()
        {
            InitializeComponent();
        }

        private void ServiceSelectDialog_Load(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                ServiceInfoBLL myBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ServiceInfoCollections infoColl = myBoss.GetRecordsByPaging(ref i, 1, 1000, " BranchNo = '" + IUserContext.GetBranchNo() + "'  And HaveChild=0");
                 
                InfoList.AutoGenerateColumns = false;
                InfoList.DataSource = infoColl;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceNos = "";
                ServiceNames = "";
                for (int i = 0; i < InfoList.Rows.Count; i++)
                {
                    if (InfoList.Rows[i].Cells[0].Value != null)
                    {
                        ServiceNos += InfoList.Rows[i].Cells[1].Value.ToString() + ";";
                        ServiceNames += InfoList.Rows[i].Cells[3].Value.ToString() + ";";
                    }
                }

                if (!string.IsNullOrEmpty(ServiceNos))
                {
                    ServiceNos = ServiceNos.Substring(0, ServiceNos.Length - 1);
                }

                if (!string.IsNullOrEmpty(ServiceNames))
                {
                    ServiceNames = ServiceNames.Substring(0, ServiceNames.Length - 1);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示:" + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        } 
    }
}
