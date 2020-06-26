using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace EntFrm.SettingConsole
{
    public partial class frmServiceBusiness : Form
    {
        private ServiceInfoBLL myBoss;
        private string sCurrentNo;
        private int iSelectedRow;
        private UpdateType updateType = UpdateType.Upt;

        #region
        private StringReader streamToPrint = null;
        //private StreamReader streamToPrint = null;
        private Font printFont;
        private int iCheckPrint;
        //private int iTicketPrintCount = 1;
        private bool bUse80Printer = false;
        private PrintDocument pdTicket = new PrintDocument();
        private PageSetupDialog psdTicket = new PageSetupDialog();
        private int iPageWidth58 = 228;
        private int iPageWidth80 = 314;
        private int iPageHeight = 1169;
        #endregion 

        public frmServiceBusiness()
        {
            InitializeComponent();
        }

        private void frmServiceBusiness_Load(object sender, EventArgs e)
        {
            myBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            sCurrentNo = "0";
            iSelectedRow = 0;
            InfoList.MultiSelect = false;

            DoBindDataList();
            DoBindTStyleList();
            DoServiceClassList();
            DoRefreshForm();  
        } 

        private void DoBindDataList()
        {
            ServiceInfoCollections InfoColl = myBoss.GetAllRecordsByParentNoOrder("00000000", "  ",IUserContext.GetBranchNo());

            if (InfoColl != null && InfoColl.Count > 0&&sCurrentNo=="0")
            {
                sCurrentNo = InfoColl[0].sServiceNo;
            }

            InfoList.AutoGenerateColumns = false;
            InfoList.DataSource = InfoColl;
        }

        private void DoBindTStyleList()
        {
            int count = 0;
            string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
            TicketStyleBLL tstyleBoss = new TicketStyleBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

            TicketStyleCollections infoColl = tstyleBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

            dpTicketStyle.DataSource = infoColl;
            dpTicketStyle.ValueMember = "sStyleNo";
            dpTicketStyle.DisplayMember = "sStyleName";
        }

        private void DoServiceClassList()
        {
            List<ItemObject> serviceClass = new List<ItemObject>();
            ServiceInfoCollections InfoColl = myBoss.GetAllRecordsByParentNoOrder("00000000", "  ", IUserContext.GetBranchNo());

            ItemObject item = new ItemObject("根", "00000000");
            serviceClass.Add(item);

            if (InfoColl != null && InfoColl.Count > 0)
            {
                foreach (ServiceInfo info in InfoColl)
                {
                    item = new ItemObject(info.sServiceName, info.sServiceNo);
                    serviceClass.Add(item);
                }
            }

            dpServiceClass.DataSource = serviceClass;
            dpServiceClass.ValueMember = "Value";
            dpServiceClass.DisplayMember = "Name";
        }

        private void DoRefreshForm()
        {
            try
            {
                ServiceInfo info = myBoss.GetRecordByNo(sCurrentNo);
                if (info != null)
                {
                    //txtServiceNo.Text = info.sServiceNo;
                    txtServiceAlias.Text = info.sServiceAlias;
                    txtServiceName.Text = info.sServiceName;
                    txtServiceType.Text = info.sServiceType;
                    txtStartNum.Value = info.iStartNum;
                    txtDigitLength.Value = info.iDigitLength;
                    dpServiceClass.SelectedValue = info.sParentNo;
                    dpTicketStyle.SelectedValue = info.sTicketStyleNo;
                    btnTimeLimit.Enabled = true;

                }

                SetFocusedColumn();

                updateType = UpdateType.Upt;
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
                this.InfoList.CurrentCell = this.InfoList.Rows[iSelectedRow].Cells[0];
            }
            catch(Exception ex)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int count = myBoss.GetCountByCondition(" BranchNo = '" + IUserContext.GetBranchNo() + "' And HaveChild=0 ") + 1;

                txtServiceName.Text = "业务" + count;
                //txtServiceAlias.Text = "";
                txtServiceType.Text = "";
                //txtStartNum.Value = 1;
                //txtDigitLength.Value = 3;
                dpServiceClass.SelectedValue = "00000000";
                btnTimeLimit.Enabled = false;
                dpTicketStyle.SelectedIndex = 0;

                updateType = UpdateType.Add;
                btnSave_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示:" + ex.Message);
            }
        } 

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("您确定要删除所选信息(将同时删除所有子业务)?", "确认对话框", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string sNo = GetFocusedColumnValue("ServiceNo");
                    DeleteServices(sNo);
                    DoBindDataList();
                    DoServiceClassList();
                    DoRefreshForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示:" + ex.Message);
            }
        }


        private void DeleteServices(string sNo)
        {
            ServiceInfo info = myBoss.GetRecordByNo(sNo);
            if (info.iHaveChild > 0)
            {
                ServiceInfoCollections infoColl = myBoss.GetRecordsByClassNo(sNo);
                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (ServiceInfo serv in infoColl)
                    {
                        DeleteServices(serv.sServiceNo);
                    }
                }
            }
            myBoss.HardDeleteRecord(sNo);
        }

        private void btnTimeLimit_Click(object sender, EventArgs e)
        {
            ServiceTLimitDialog dlg = new ServiceTLimitDialog();
            dlg.sServiceNo = sCurrentNo;
            dlg.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (updateType == UpdateType.Add)
                {
                    ServiceInfo info = new ServiceInfo();
                    string sNo = CommonHelper.Get_New12ByteGuid();
                    info.sServiceNo = sNo;

                    info.sServiceName = txtServiceName.Text.Trim();
                    info.sServiceAlias = txtServiceAlias.Text.Trim();

                    info.sServiceType = txtServiceType.Text.Trim();
                    info.iStartNum = (int)txtStartNum.Value;
                    info.iEndNum = (int)txtDigitLength.Value;
                    info.iAlarmNum = 0;
                    info.iDigitLength = 3;
                    info.sWorkflowText = "";
                    info.sWorkflowValue = "";


                    TicketButton btnModel = new TicketButton();
                    btnModel.iButtonLeft = 10;
                    btnModel.iButtonTop = 10;
                    btnModel.iButtonWidth = 210;
                    btnModel.iButtonHeight = 70;
                    btnModel.sButtonBgColor = Color.White.ToArgb().ToString();
                    btnModel.sButtonBgImage1 = "";
                    btnModel.sButtonBgImage2 = "";
                    btnModel.sButtonBgImage3 = "";

                    btnModel.iTitleFtSize = 18;
                    btnModel.sTitleFtFamily = "宋体";
                    btnModel.sTitleFtStyle = FontStyle.Regular.ToString();
                    btnModel.sTitleFtColor = Color.Black.ToArgb().ToString();
                    btnModel.iSubtitleFtSize = 9;
                    btnModel.sSubtitleFtFamily = "宋体";
                    btnModel.sSubtitleFtStyle = FontStyle.Regular.ToString();
                    btnModel.sSubtitleFtColor = Color.Red.ToArgb().ToString();
                    btnModel.bIsShowTitle = true;
                    btnModel.bIsShowSubtitle = true;

                    info.sTicketButtonFmt = JsonConvert.SerializeObject(btnModel);
                    info.sTicketStyleNo = dpTicketStyle.SelectedValue.ToString();


                    info.iAMLimit = 0;
                    info.dAMStartTime = DateTime.Now;
                    info.dAMEndTime = DateTime.Now;
                    info.iAMTotal = 1;

                    info.iPMLimit = 0;
                    info.dPMStartTime = DateTime.Now;
                    info.dPMEndTime = DateTime.Now;
                    info.iPMTotal = 1;

                    info.iPrintPause = 0;
                    info.sParentNo = dpServiceClass.SelectedValue.ToString();
                    info.iHaveChild = 0; 

                    info.iWeekLimit = 0;
                    info.sWeekDays = "";
                    info.iIsShowDialog = 0;
                    info.sShowDialogName = "";

                    info.sBranchNo = IUserContext.GetBranchNo();

                    info.sComments = "";
                    info.sAddOptor = "00000000";
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = "00000000";
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sAppCode = IUserContext.GetAppCode() + ";";

                    if (myBoss.AddNewRecord(info))
                    {
                        Update_ServiceChild();

                        //MessageBox.Show("操作成功!");
                        DoBindDataList();
                        DoServiceClassList();
                        DoRefreshForm();
                        return;
                    }

                    MessageBox.Show("新增操作失败!");
                }
                else
                {
                    ServiceInfo info = myBoss.GetRecordByNo(this.sCurrentNo);
                    if (info != null)
                    {
                        if (this.sCurrentNo.Equals(dpServiceClass.SelectedValue.ToString()))
                        {
                            MessageBox.Show("业务类型选择不能相同!");
                            return;
                        }

                        info.sServiceAlias = txtServiceAlias.Text.Trim();
                        info.sServiceName = txtServiceName.Text.Trim();
                        info.sServiceType = txtServiceType.Text.Trim();
                        info.iStartNum = (int)txtStartNum.Value;
                        info.iEndNum = 0;
                        info.iDigitLength = (int)txtDigitLength.Value;
                        info.sTicketStyleNo = dpTicketStyle.SelectedValue.ToString();

                        info.sParentNo = dpServiceClass.SelectedValue.ToString();  

                        info.sModOptor = "00000000";
                        info.dModDate = DateTime.Now;

                        if (myBoss.UpdateRecord(info))
                        {
                            Update_ServiceChild();

                            MessageBox.Show("操作成功!");
                            DoBindDataList();
                            DoServiceClassList();
                            DoRefreshForm();
                            return;
                        }
                    }

                    MessageBox.Show("更新操作失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示:" + ex.Message);
            }
        }

        private void Update_ServiceChild()
        {
            int count = 0; 
            string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
            ServiceInfoCollections infoColl = myBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

            if (infoColl != null && infoColl.Count > 0)
            {
                foreach (ServiceInfo info in infoColl)
                {
                    info.iHaveChild = 0;
                    count = myBoss.GetCountByCondition(" BranchNo = '" + IUserContext.GetBranchNo() + "' And  ParentNo='" + info.sServiceNo + "' ");
                    if (count > 0)
                    {
                        info.iHaveChild = 1;
                    }
                    myBoss.UpdateRecord(info);
                }
            }
        }

        private void btnUptAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确认对话框", "您确定要批量修改？", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                int count = 0;
                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
                ServiceInfoCollections infoColl = myBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);
                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (ServiceInfo info in infoColl)
                    {
                        //if (info.sServiceNo.Equals(dpServiceClass.SelectedValue.ToString()))
                        //{
                        //    continue;
                        //}

                        info.sServiceAlias = txtServiceName.Text.Trim();
                        info.sServiceName = txtServiceName.Text.Trim();
                        info.sServiceType = txtServiceType.Text.Trim();
                        info.iStartNum = (int)txtStartNum.Value;
                        info.iEndNum = (int)txtDigitLength.Value;
                        info.sTicketStyleNo = dpTicketStyle.SelectedValue.ToString();

                        //info.sParentNo = dpServiceClass.SelectedValue.ToString();

                        info.sModOptor = "00000000";
                        info.dModDate = DateTime.Now;

                        myBoss.UpdateRecord(info);
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
