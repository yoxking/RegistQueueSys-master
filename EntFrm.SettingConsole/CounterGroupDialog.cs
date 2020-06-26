using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EntFrm.SettingConsole
{
    public partial class CounterGroupDialog : Form
    {
        private string DisplayNo;
        public string sDisplayNo
        {
            set { this.DisplayNo = value; }
            get { return this.DisplayNo; }
        }

        private string sCounterNos;

        public CounterGroupDialog()
        {
            InitializeComponent();
        }

        private void CounterGroupDialog_Load(object sender, EventArgs e)
        {
            LEDDisplayBLL myBoss = new LEDDisplayBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            LEDDisplay info = myBoss.GetRecordByNo(sDisplayNo);

            if (info != null)
            {
                txtLedName.Text = info.sDisplayName;
                sCounterNos = info.sCounterNos;

                DoRefreshForm();
            }
        }

        private void DoRefreshForm()
        {
            int count = 0;
            string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
            CounterInfoBLL myBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            CounterInfoCollections infoColl = myBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

            List<CounterGroup> infoList = null;
            CounterGroup counter = null;

            if (infoColl != null && infoColl.Count > 0)
            {
                infoList = new List<CounterGroup>();
                foreach (CounterInfo info in infoColl)
                {
                    counter = new CounterGroup();
                    counter.iCheckedFlag = 0;
                    if (sCounterNos.IndexOf(info.sCounterNo) > -1)
                    {
                        counter.iCheckedFlag = 1;
                    }
                    counter.sCounterNo = info.sCounterNo;
                    counter.sCounterName = info.sCounterName;
                    counter.sCounterAlias = info.sCounterAlias;

                    infoList.Add(counter);
                }
            }
            InfoList.AutoGenerateColumns = false;
            InfoList.DataSource = infoList;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                LEDDisplayBLL myBoss = new LEDDisplayBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                LEDDisplay info = myBoss.GetRecordByNo(sDisplayNo);

                if (info != null)
                {
                    info.sCounterNos = GetSelectedCounters();
                    info.dModDate = DateTime.Now;

                    if(myBoss.UpdateRecord(info))
                    {
                        this.DialogResult = DialogResult.OK; //ok
                        this.Close();
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("出错提示:" + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private string GetSelectedCounters()
        {

            string counterGroupValue = "";

            for (int i = 0; i < InfoList.RowCount; i++)
            {
                if (InfoList.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                {
                    counterGroupValue += InfoList.Rows[i].Cells["CounterNo"].Value.ToString() + ";";
                }
            }

            counterGroupValue = counterGroupValue.Substring(0, counterGroupValue.Length - 1);

            return counterGroupValue;
        }
    }
}
