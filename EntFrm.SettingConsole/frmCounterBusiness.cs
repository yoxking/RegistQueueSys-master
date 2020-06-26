using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Windows.Forms;

namespace EntFrm.SettingConsole
{
    public partial class frmCounterBusiness : Form
    {
        private CounterInfoBLL myBoss;
        private string sCurrentNo;
        private string serviceGroupValue = "";
        private int iSelectedRow;
        private UpdateType updateType = UpdateType.Upt;

        public frmCounterBusiness()
        {
            InitializeComponent();
        }

        private void frmCounterBusiness_Load(object sender, EventArgs e)
        {
            string s = IUserContext.GetConnStr();
            myBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            sCurrentNo = "0";
            iSelectedRow = 0;
            serviceGroupValue = "";
            InfoList.MultiSelect = false;

            DoBindDataList();
            DoBindVoiceList();
            DoBindLedList();
            DoBindCallerList(); 
            DoRefreshForm();
        }

        private void DoBindVoiceList()
        {
            int count = 0;
            string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
            VoiceInfoBLL ttsBoss = new VoiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

            VoiceInfoCollections infoColl = ttsBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

            dpVoiceList.DataSource = infoColl;
            dpVoiceList.ValueMember = "sTtsNo";
            dpVoiceList.DisplayMember = "sTtsName";
        }

        private void DoBindLedList()
        {
            int count = 0;
            string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
            LEDDisplayBLL ledBoss = new LEDDisplayBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

            LEDDisplayCollections infoColl = ledBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

            dpLedList.DataSource = infoColl;
            dpLedList.ValueMember = "sDisplayNo";
            dpLedList.DisplayMember = "sDisplayName";
        }

        private void DoBindCallerList()
        {
            int count = 0;
            string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
            CallerInfoBLL callBoss = new CallerInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

            CallerInfoCollections infoColl = callBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

            dpCallerList.DataSource = infoColl;
            dpCallerList.ValueMember = "sCallerNo";
            dpCallerList.DisplayMember = "sCallerName";
        } 

        private void DoBindDataList()
        {
            int count = 0;
            string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
            CounterInfoCollections InfoColl = myBoss.GetRecordsByPaging(ref count, 1, 1000, sWhere);

            if (InfoColl != null && InfoColl.Count > 0 && sCurrentNo == "0")
             {
                 sCurrentNo = InfoColl[0].sCounterNo;
             }

             InfoList.AutoGenerateColumns = false;
             InfoList.DataSource = InfoColl;
        } 

        private void DoRefreshForm()
        {
            try
            {
                CounterInfo info = myBoss.GetRecordByNo(sCurrentNo);
                if (info != null)
                {
                    txtCounterName.Text = info.sCounterName;
                    txtCounterAlias.Text = info.sCounterAlias; 
                    txtServiceGroupText.Text = info.sServiceGroupText;
                    serviceGroupValue = info.sServiceGroupValue;

                    dpVoiceList.SelectedValue = info.sVoiceStyleNos;
                    dpLedList.SelectedValue = info.sLedDisplayNo;
                    dpCallerList.SelectedValue = info.sCallerNo; 

                    txtComment.Text = info.sComments;
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
            string sNo = GetFocusedColumnValue("CounterNo");
            if (!sNo.Equals(sCurrentNo))
            {
                sCurrentNo = sNo;
                btnChooseServices.Enabled = true;
                DoRefreshForm();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int count = myBoss.GetCountByCondition(" BranchNo = '" + IUserContext.GetBranchNo() + "' ") + 1;

            txtCounterAlias.Text = "科室" + count;
            txtCounterName.Text = "" + count;
            //txtPhyAddr.Text = "";
            txtServiceGroupText.Text = "";
            btnChooseServices.Enabled = false;

            updateType = UpdateType.Add;
            btnSave_Click(sender, e);
        } 

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("您确定要删除所选信息?", "确认对话框", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string sNo = GetFocusedColumnValue("CounterNo");
                    if (myBoss.HardDeleteRecord(sNo))
                    { 
                        DoBindDataList();
                        DoRefreshForm();
                    }
                    else
                    {
                        MessageBox.Show("删除操作失败!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示:" + ex.Message);
            }
        }

        private void btnChooseServices_Click(object sender, EventArgs e)
        {
            ServiceGroupDialog dlg = new ServiceGroupDialog();

            CounterInfo info = myBoss.GetRecordByNo(this.sCurrentNo);
            dlg.CounterName = info.sCounterName;
            dlg.ServiceGroupText = info.sServiceGroupText;
            dlg.ServiceGroupValue = info.sServiceGroupValue;
            if(dlg.ShowDialog()==DialogResult.OK)
            {
                serviceGroupValue = dlg.ServiceGroupValue; 
                txtServiceGroupText.Text = dlg.ServiceGroupText;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!doCheck_RegStatus())
                //{
                //    MessageBox.Show("加密锁无效或者科室数已超过授权数量，请联系管理员！");
                //    return;
                //}

                    if (updateType == UpdateType.Add)
                {
                    CounterInfo info = new CounterInfo();

                    info.sCounterNo = CommonHelper.Get_New12ByteGuid();
                    info.sCounterName = txtCounterName.Text.Trim();
                    info.sCounterAlias = txtCounterAlias.Text.Trim();
                    info.sServiceGroupValue = serviceGroupValue;
                    info.sServiceGroupText = txtServiceGroupText.Text.Trim();
                    info.sVoiceStyleNos = dpVoiceList.SelectedValue.ToString(); 
                    info.sLedDisplayNo = dpLedList.SelectedValue.ToString();
                    info.iLedAddress = 1;
                    info.sCallerNo = dpCallerList.SelectedValue.ToString();
                    info.iCallerAddress = 1; 
                    info.iIsAutoLogon = 0; 
                    info.iLogonState = 0;
                    info.sLogonStafferNo = "";
                    info.iPauseState = 0;
                    info.iCalledNum = 0;
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
                        //MessageBox.Show("操作成功!");
                        DoBindDataList();
                        DoRefreshForm();
                        return;
                    }

                    MessageBox.Show("新增操作失败!");
                }
                else
                {
                    CounterInfo info = myBoss.GetRecordByNo(this.sCurrentNo);
                    if (info != null)
                    {
                        info.sCounterName = txtCounterName.Text.Trim();
                        info.sCounterAlias = txtCounterAlias.Text.Trim();
                        info.sServiceGroupText = txtServiceGroupText.Text.Trim();
                        info.sServiceGroupValue = serviceGroupValue;
                        info.sVoiceStyleNos = dpVoiceList.SelectedValue.ToString();
                        info.sLedDisplayNo = dpLedList.SelectedValue.ToString(); 
                        info.sCallerNo = dpCallerList.SelectedValue.ToString(); 
                        info.sComments = txtComment.Text.Trim();

                        info.sModOptor = "00000000";
                        info.dModDate = DateTime.Now; 

                        if (myBoss.UpdateRecord(info))
                        {
                            MessageBox.Show("操作成功!");
                            DoBindDataList();
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
                CounterInfoCollections infoColl = myBoss.GetRecordsByPaging(ref count, 1, 1000, sWhere);
                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (CounterInfo info in infoColl)
                    {
                        info.sCounterName = txtCounterName.Text.Trim();
                        info.sCounterAlias = txtCounterAlias.Text.Trim();
                        info.sServiceGroupText = txtServiceGroupText.Text.Trim();
                        info.sServiceGroupValue = serviceGroupValue;
                        //info.sTerminalPhyAddr = "";
                        //info.sTerminalSerialPort = dpSerialPort.SelectedItem.ToString(); 

                        //info.sTtsStylesNo = dpVoiceList.SelectedValue.ToString();
                        //info.sLedDisplayNo = dpLedList.SelectedValue.ToString();
                        //info.sCallerNo = dpCallerList.SelectedValue.ToString();
                        //info.iIsAutoLogon = ckAutoLogon.Checked ? 1 : 0;
                        //info.sStafferNo = "";
                        //if (ckAutoLogon.Checked)
                        //{
                        //    info.sStafferNo = dpStaffList.SelectedValue.ToString();
                        //}
                         
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

        private bool doCheck_RegStatus()
        {
            try
            {
                int count =30;
                int num = myBoss.GetCountByCondition("");

                EncDogModel dogCode = IUserContext.getEncryptDog();
                RegKeyModel regCode = IUserContext.getRegistryKey();

                if(dogCode!=null)
                {
                    count = int.Parse(dogCode.ActiveCount);
                    
                }
                else  if(regCode!=null)
                {
                    count = int.Parse(regCode.ActiveCount); 
                }

                if (num < count)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            { return false; }
        }
    }
}
