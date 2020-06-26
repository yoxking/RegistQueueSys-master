using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Windows.Forms;

namespace EntFrm.SettingConsole
{
    public partial class frmPhCallerBusiness : Form
    {
        private CallerInfoBLL myBoss;
        private string sCurrentNo; 
        private int iSelectedRow;
        private UpdateType updateType = UpdateType.Upt;

        public frmPhCallerBusiness()
        {
            InitializeComponent();
        }

        private void frmPhCallerBusiness_Load(object sender, EventArgs e)
        {
            myBoss = new CallerInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            sCurrentNo = "0";
            iSelectedRow = 0;
            dpSerialPort.SelectedIndex = 0;
            txtPhyAddr.Text = "1";
            txtTimeoutSec.Text = "2";

            DoBindDataList();
            DoEvalDataList();
            DoRefreshForm();
        }
         
        private void DoBindDataList()
        {
            int count = 0;
            string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
            CallerInfoCollections InfoColl = myBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

            if (InfoColl != null && InfoColl.Count > 0 && sCurrentNo == "0")
            {
                sCurrentNo = InfoColl[0].sCallerNo;
            }

            InfoList.AutoGenerateColumns = false;
            InfoList.DataSource = InfoColl;
        }

        private void DoEvalDataList()
        {

            int count = 0;
            string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
            EvaluatorInfoBLL evalBoss = new EvaluatorInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            EvaluatorInfoCollections evalColl = evalBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

            dpEvalList.DataSource = evalColl;
            dpEvalList.ValueMember = "sEvalorNo";
            dpEvalList.DisplayMember = "sEvaVCode";
        }

        private void DoRefreshForm()
        {
            try
            {
                CallerInfo info = myBoss.GetRecordByNo(sCurrentNo);
                if (info != null)
                {
                    txtCallerName.Text = info.sCallerName;
                    txtPhyAddr.Text = info.iPhyAddr.ToString();
                    dpSerialPort.SelectedItem = info.sSerialPort;
                    dpEvalList.SelectedItem = info.sEvalorNo;
                    txtTimeoutSec.Text = info.iTimeoutSec.ToString();
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
            string sNo = GetFocusedColumnValue("CallerNo");
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
                int count = myBoss.GetCountByCondition(" BranchNo = '" + IUserContext.GetBranchNo() + "' ") + 1;

                txtCallerName.Text = "呼叫器" + count;
                txtTimeoutSec.Text = "3";
                txtPhyAddr.Text = count + ""; 

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
                if (MessageBox.Show("您确定要删除所选信息?", "确认对话框", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string sNo = GetFocusedColumnValue("CallerNo");
                    if (myBoss.HardDeleteRecord(sNo))
                    {
                        //MessageBox.Show("删除操作成功!");
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (updateType == UpdateType.Add)
                {
                    CallerInfo info = new CallerInfo();

                    info.sCallerNo = CommonHelper.Get_New12ByteGuid();
                    info.sCallerName = txtCallerName.Text.Trim();
                    info.sProtocol = "ZYJH101";
                    info.sSerialPort = dpSerialPort.SelectedItem.ToString();
                    info.sCommMode = "serialport";
                    info.iBaudrate = 9600;
                    info.iPhyAddr = int.Parse(txtPhyAddr.Text.Trim());
                    info.sEvalorNo = dpEvalList.SelectedItem.ToString(); 
                    info.iTimeoutSec = int.Parse(txtTimeoutSec.Text.Trim());
                    info.iUpdateFlag = 0;
                    info.dUpdateTime = DateTime.Now;
                    info.iCheckState = 1;
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
                    CallerInfo info = myBoss.GetRecordByNo(this.sCurrentNo);
                    if (info != null)
                    {
                        info.sCallerName = txtCallerName.Text.Trim();
                        info.sSerialPort = dpSerialPort.SelectedItem.ToString();
                        info.iPhyAddr = int.Parse(txtPhyAddr.Text.Trim());
                        info.sEvalorNo = dpEvalList.SelectedItem.ToString();
                        info.iTimeoutSec = int.Parse(txtTimeoutSec.Text.Trim());

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

                CallerInfoCollections infoColl = myBoss.GetAllRecords();
                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (CallerInfo info in infoColl)
                    {
                        info.sCallerName = txtCallerName.Text.Trim();
                        info.sSerialPort = dpSerialPort.SelectedItem.ToString();
                        info.iPhyAddr = int.Parse(txtPhyAddr.Text.Trim());
                        info.iTimeoutSec = int.Parse(txtTimeoutSec.Text.Trim());
                        info.sEvalorNo = dpEvalList.SelectedItem.ToString();

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
