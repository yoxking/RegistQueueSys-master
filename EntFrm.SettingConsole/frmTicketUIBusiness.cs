using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EntFrm.SettingConsole
{
    public partial class frmTicketUIBusiness : Form
    {
        private ServiceInfoBLL myBoss;
        private string sCurrentNo;
        private int iSelectedRow;

        public frmTicketUIBusiness()
        {
            InitializeComponent();
        }

        private void frmTicketUIBusiness_Load(object sender, EventArgs e)
        {
            myBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            sCurrentNo = "0";
            iSelectedRow = 0;
            InfoList.MultiSelect = false;
                
            DoBindDataList();
            DoBindShowDialogs(); 
            DoRefreshForm();
        }

        private void DoBindDataList()
        {
            ServiceInfoCollections InfoColl = myBoss.GetAllRecordsByParentNoOrder("00000000", "  ",IUserContext.GetBranchNo());

            if (InfoColl != null && InfoColl.Count > 0 && sCurrentNo == "0")
            {
                sCurrentNo = InfoColl[0].sServiceNo;
            }

            InfoList.AutoGenerateColumns = false;
            InfoList.DataSource = InfoColl;
        }

        private void DoBindShowDialogs()
        {
            List<ItemObject> dlgList = IPublicConsts.GetShowDialogs();

            dpServiceRules.DataSource = dlgList;
            dpServiceRules.ValueMember = "Value";
            dpServiceRules.DisplayMember = "Name";
            dpServiceRules.SelectedIndex = 0;
        }
         

        private void DoRefreshForm()
        {
            try
            {
                 ServiceInfo info = myBoss.GetRecordByNo(sCurrentNo);
                 if (info != null)
                 {
                     dpServiceRules.SelectedValue = info.sShowDialogName;
                 }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示:" + ex.Message);
            }
        }

        private void SetFocusedColumn()
        {
            try
            {
                this.InfoList.Rows[iSelectedRow].Selected = true;
            }
            catch (Exception ex)
            { }
        }

        private string GetFocusedColumnValue(string columnName)
        {
            try
            {
                string sResult = "";

                if (this.InfoList.SelectedRows.Count > 0)
                {
                    sResult = this.InfoList.SelectedRows[0].Cells[columnName].Value.ToString();
                    iSelectedRow = this.InfoList.SelectedRows[0].Index;
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private void InfoList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string sNo = GetFocusedColumnValue("ServiceNo");
            if (!sNo.Equals(sCurrentNo))
            {
                sCurrentNo = sNo;
                DoRefreshForm();
            }
        }
         
        private void btnVisualDesign_Click(object sender, EventArgs e)
        { 
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "MyFormDesinger.exe";
            process.StartInfo.WorkingDirectory = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }

        private void btnUpt_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceInfo info = myBoss.GetRecordByNo(this.sCurrentNo);
                if (info != null)
                { 
                    if (dpServiceRules.SelectedValue.ToString().Equals("Null"))
                    {
                        info.iIsShowDialog = 0;
                        info.sShowDialogName = "Null";
                    }
                    else
                    {
                        info.iIsShowDialog = 1;
                        info.sShowDialogName = dpServiceRules.SelectedValue.ToString();
                    }

                    info.sModOptor = "00000000";
                    info.dModDate = DateTime.Now;

                    if (myBoss.UpdateRecord(info))
                    {
                        MessageBox.Show("更新操作成功!");
                        DoBindDataList();
                        DoRefreshForm();
                        return;
                    }
                }

                MessageBox.Show("更新操作失败!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示:" + ex.Message);
            }
        }
         
    }
}
