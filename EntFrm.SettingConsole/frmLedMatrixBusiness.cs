using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EntFrm.SettingConsole
{
    public partial class frmLedMatrixBusiness : Form
    {
        private LEDMatrixBLL myBoss;
        private string sCurrentNo;
        private int iSelectedRow;
        private UpdateType updateType = UpdateType.Upt;
        private string sServiceNos="";

        public frmLedMatrixBusiness()
        {
            InitializeComponent();
        }

        private void frmLedMatrixBusiness_Load(object sender, EventArgs e)
        {
            myBoss = new LEDMatrixBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            sCurrentNo = "0";
            iSelectedRow = 0;
            InfoList.MultiSelect = false;

            dpFontAlign.SelectedIndex = 0;
            dpSerialPort.SelectedIndex = 0;
            dpMatrixModel.SelectedIndex = 0; 

            DoBindVariableSource();
            DoBindDataList();
            DoRefreshForm();
        }

        private void DoBindDataList()
        {
            int count = 0;
            string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
            LEDMatrixCollections InfoColl = myBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

            if (InfoColl != null && InfoColl.Count > 0)
            {
                sCurrentNo = InfoColl[0].sMatrixNo; 
            }

            InfoList.AutoGenerateColumns = false;
            InfoList.DataSource = InfoColl;
        }

        private void DoBindVariableSource()
        {
            List<ItemObject> varsList = IPublicConsts.GetPublicVariables();

            dpVariables.DataSource = varsList;
            dpVariables.ValueMember = "Value";
            dpVariables.DisplayMember = "Name";
        }

        private void DoRefreshForm()
        {
            try
            {
                LEDMatrix info = myBoss.GetRecordByNo(sCurrentNo);
                if (info != null)
                {
                    sCurrentNo = info.sMatrixNo;

                    txtMatrixName.Text = info.sMatrixName;
                    dpMatrixModel.SelectedItem = info.sMatrixModel ;
                    txtPhyAddr.Text = info.iPhyAddr.ToString();
                    sServiceNos = info.sServiceNos;
                    txtServiceNames.Text = GetServiceNameList(info.sServiceNos);
                    dpSerialPort.SelectedItem = info.sSerialPort;
                    txtBaudrate.Text = info.iBaudrate.ToString();
                    txtIpAddress.Text = info.sIpAddress;
                    txtLocalPort.Text = info.sLocalPort;

                    if (info.sProtocol.Equals("NET"))
                    {
                        ckNetProtocol.Checked = true;
                        ckComProtocol.Checked = false;
                    }
                    else
                    {
                        ckComProtocol.Checked = true;
                        ckNetProtocol.Checked = false;
                    }


                    string[] sparam = info.sParamFormat.Split(';');
                    if (sparam.Length == 8)
                    {
                        txtScreenWidth.Text = sparam[0];
                        txtScreenHeight.Text = sparam[1];
                        txtPosX.Text = sparam[2];
                        txtPosY.Text = sparam[3];
                        txtWidth.Text = sparam[4];
                        txtHeight.Text = sparam[5];
                        txtFontSize.Text = sparam[6];
                        dpFontAlign.SelectedIndex = int.Parse(sparam[7]);
                    }
                    else
                    {
                        txtScreenWidth.Text = "128";
                        txtScreenHeight.Text = "64";
                        txtPosX.Text = "0";
                        txtPosY.Text = "0";
                        txtWidth.Text = "128";
                        txtHeight.Text = "64";
                        txtFontSize.Text = "12";
                        dpFontAlign.SelectedIndex = 1;
                    }

                    txtDisplayFormat.Text = info.sDisplayFormat;
                    txtDisplayRows.Text = info.iDisplayRows.ToString();

                }
                SetFocusedColumn();

                updateType = UpdateType.Upt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示:" + ex.Message);
            }
        }

        private string GetServiceNameList(string servieNoList)
        {
            string sResult = "";

            if (!string.IsNullOrEmpty(servieNoList))
            {
                string[] serviceNos = servieNoList.Split(';');
                foreach (string serviceNo in serviceNos)
                {
                    sResult += IPublicHelper.GetServiceNameByNo(serviceNo) + ";";
                }

                if (!string.IsNullOrEmpty(sResult))
                {
                    sResult = sResult.Substring(0, sResult.Length - 1);
                }
            }

            return sResult;
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
            string sNo = GetFocusedColumnValue("MatrixNo");
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

                txtMatrixName.Text = "综合屏" + count;
                txtPhyAddr.Text = "" + count;

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
                    string sNo = GetFocusedColumnValue("MatrixNo");
                    if (myBoss.HardDeleteRecord(sNo))
                    {
                        MessageBox.Show("删除操作成功!");
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

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            string sNo = GetFocusedColumnValue("MatrixNo");
        }
         

        private void btnAddVar_Click(object sender, EventArgs e)
        {
            string s = txtDisplayFormat.Text;
            int idx = txtDisplayFormat.SelectionStart;
            txtDisplayFormat.Text = s.Insert(idx, dpVariables.SelectedValue.ToString());

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (updateType == UpdateType.Add)
                {
                    LEDMatrix info = new LEDMatrix();
                    info.sMatrixNo = CommonHelper.Get_New12ByteGuid();

                    info.sMatrixName = txtMatrixName.Text.Trim(); 
                    info.sMatrixModel = dpMatrixModel.SelectedItem.ToString();
                    info.iPhyAddr = int.Parse(txtPhyAddr.Text.Trim());
                    info.sProtocol = ckComProtocol.Checked ? "COM" : "NET";
                    info.sSerialPort = dpSerialPort.SelectedItem.ToString();
                    info.iBaudrate = int.Parse(txtBaudrate.Text.Trim());
                    info.sIpAddress = txtIpAddress.Text.Trim();
                    info.sLocalPort = txtLocalPort.Text.Trim();
                    info.sParamFormat = txtScreenWidth.Text.Trim() + ";" + txtScreenHeight.Text.Trim() + ";" + txtPosX.Text.Trim() + ";" + txtPosY.Text.Trim() + ";" + txtWidth.Text.Trim() + ";" + txtHeight.Text.Trim() + ";" + txtFontSize.Text.Trim() + ";" + dpFontAlign.SelectedIndex;
                    info.iTimeoutSec = 30;

                    info.sServiceNos = sServiceNos;
                    info.iDisplayRows = int.Parse(txtDisplayRows.Text.Trim());
                    info.sDisplayFormat = txtDisplayFormat.Text;
                    info.sBranchNo = IUserContext.GetBranchNo();

                    info.sAddOptor = "00000000";
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = "00000000";
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sAppCode = IUserContext.GetAppCode() + ";";

                    if (myBoss.AddNewRecord(info))
                    {
                        if (info.sMatrixModel.Equals("Eq2013"))
                        {
                            setEq2013_IniFile();
                        }
                        //MessageBox.Show("操作成功!");
                        DoBindDataList();
                        DoRefreshForm();
                        return;
                    }

                    MessageBox.Show("新增操作失败!");
                }
                else
                {
                    LEDMatrix info = myBoss.GetRecordByNo(this.sCurrentNo);
                    if (info != null)
                    {

                        info.sMatrixName = txtMatrixName.Text.Trim();
                        info.sMatrixModel = dpMatrixModel.SelectedItem.ToString();
                        info.iPhyAddr = int.Parse(txtPhyAddr.Text.Trim());
                        info.sProtocol = ckComProtocol.Checked ? "COM" : "NET";
                        info.sSerialPort = dpSerialPort.SelectedItem.ToString();
                        info.iBaudrate = int.Parse(txtBaudrate.Text.Trim());
                        info.sIpAddress = txtIpAddress.Text.Trim();
                        info.sLocalPort = txtLocalPort.Text.Trim();
                        info.sParamFormat = txtScreenWidth.Text.Trim() + ";" + txtScreenHeight.Text.Trim() + ";" + txtPosX.Text.Trim() + ";" + txtPosY.Text.Trim() + ";" + txtWidth.Text.Trim() + ";" + txtHeight.Text.Trim() + ";" + txtFontSize.Text.Trim() + ";" + dpFontAlign.SelectedIndex;
                        info.iTimeoutSec = 30;

                        info.sServiceNos = sServiceNos;
                        info.iDisplayRows = int.Parse(txtDisplayRows.Text.Trim());
                        info.sDisplayFormat = txtDisplayFormat.Text;

                        info.sModOptor = "00000000";
                        info.dModDate = DateTime.Now;

                        if (myBoss.UpdateRecord(info))
                        {
                            if (info.sMatrixModel.Equals("Eq2013"))
                            {
                                setEq2013_IniFile();
                            }
                            else if (info.sMatrixModel.Equals("Eq2023"))
                            {
                                setEq2023_IniFile();
                            }

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

        private void setEq2013_IniFile()
        {
            try
            {
                int phyAddr = int.Parse(txtPhyAddr.Text.Trim());
                string protocol = ckNetProtocol.Checked ? "NET" : "COM";
                string baudrate = txtBaudrate.Text.Trim();
                string serialPort = dpSerialPort.SelectedItem.ToString();
                string netPort = txtLocalPort.Text.Trim();
                string ipAddress = txtIpAddress.Text.Trim();
                string screenWidth = txtScreenWidth.Text.Trim();
                string screenHeight = txtScreenHeight.Text.Trim();

                string iniFile = System.Windows.Forms.Application.StartupPath + "\\EQ2008_Dll_Set.ini";
                string addstr = "地址：" + (phyAddr - 1);

                IniFileHelper.SetValue(addstr, "CardType", "21", iniFile);
                IniFileHelper.SetValue(addstr, "CardAddress", (phyAddr - 1) + "", iniFile);

                if (protocol.Equals("NET"))
                {
                    IniFileHelper.SetValue(addstr, "CommunicationMode", "1", iniFile);
                }
                else
                {
                    IniFileHelper.SetValue(addstr, "CommunicationMode", "0", iniFile);
                }

                IniFileHelper.SetValue(addstr, "SerialBaud", baudrate, iniFile);
                IniFileHelper.SetValue(addstr, "SerialNum", serialPort.Substring(3), iniFile);
                IniFileHelper.SetValue(addstr, "NetPort", netPort, iniFile);
                string[] ipList = ipAddress.Split('.');
                if (ipList.Length == 4)
                {
                    IniFileHelper.SetValue(addstr, "IpAddress0", ipList[0], iniFile);
                    IniFileHelper.SetValue(addstr, "IpAddress1", ipList[1], iniFile);
                    IniFileHelper.SetValue(addstr, "IpAddress2", ipList[2], iniFile);
                    IniFileHelper.SetValue(addstr, "IpAddress3", ipList[3], iniFile);
                }
                IniFileHelper.SetValue(addstr, "ColorStyle", "0", iniFile);

                IniFileHelper.SetValue(addstr, "ScreemWidth", screenWidth, iniFile);
                IniFileHelper.SetValue(addstr, "ScreemHeight", screenHeight, iniFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void setEq2023_IniFile()
        {
            try
            {
                int phyAddr = int.Parse(txtPhyAddr.Text.Trim());
                string protocol = ckNetProtocol.Checked ? "NET" : "COM";
                string baudrate = txtBaudrate.Text.Trim();
                string serialPort = dpSerialPort.SelectedItem.ToString();
                string netPort = txtLocalPort.Text.Trim();
                string ipAddress = txtIpAddress.Text.Trim();
                string screenWidth = txtScreenWidth.Text.Trim();
                string screenHeight = txtScreenHeight.Text.Trim();

                string iniFile = System.Windows.Forms.Application.StartupPath + "\\EQ2008_Dll_Set.ini";
                string addstr = "地址：" + (phyAddr - 1);

                IniFileHelper.SetValue(addstr, "CardType", "22", iniFile);
                IniFileHelper.SetValue(addstr, "CardAddress", (phyAddr - 1) + "", iniFile);

                if (protocol.Equals("NET"))
                {
                    IniFileHelper.SetValue(addstr, "CommunicationMode", "1", iniFile);
                }
                else
                {
                    IniFileHelper.SetValue(addstr, "CommunicationMode", "0", iniFile);
                }

                IniFileHelper.SetValue(addstr, "SerialBaud", baudrate, iniFile);
                IniFileHelper.SetValue(addstr, "SerialNum", serialPort.Substring(3), iniFile);
                IniFileHelper.SetValue(addstr, "NetPort", netPort, iniFile);
                string[] ipList = ipAddress.Split('.');
                if (ipList.Length == 4)
                {
                    IniFileHelper.SetValue(addstr, "IpAddress0", ipList[0], iniFile);
                    IniFileHelper.SetValue(addstr, "IpAddress1", ipList[1], iniFile);
                    IniFileHelper.SetValue(addstr, "IpAddress2", ipList[2], iniFile);
                    IniFileHelper.SetValue(addstr, "IpAddress3", ipList[3], iniFile);
                }
                IniFileHelper.SetValue(addstr, "ColorStyle", "1", iniFile);

                IniFileHelper.SetValue(addstr, "ScreemWidth", screenWidth, iniFile);
                IniFileHelper.SetValue(addstr, "ScreemHeight", screenHeight, iniFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                int port = int.Parse(dpSerialPort.SelectedItem.ToString().Substring(3));
                int addr = int.Parse(txtPhyAddr.Text.Trim());
                string sText = txtDisplayFormat.Text;

                if (dpMatrixModel.SelectedItem.ToString().Equals("Eq2013"))
                {
                    setEq2013_IniFile();
                    Eq2008LedModel.SendDatafun(int.Parse(txtPhyAddr.Text.Trim()), sText, int.Parse(txtPosX.Text.Trim()), int.Parse(txtPosY.Text.Trim()), int.Parse(txtWidth.Text.Trim()), int.Parse(txtHeight.Text.Trim()), int.Parse(txtFontSize.Text.Trim()), dpFontAlign.SelectedIndex);

                }
                else if (dpMatrixModel.SelectedItem.ToString().Equals("Eq2023"))
                {
                    setEq2023_IniFile();
                    Eq2008LedModel.SendDatafun(int.Parse(txtPhyAddr.Text.Trim()), sText, int.Parse(txtPosX.Text.Trim()), int.Parse(txtPosY.Text.Trim()), int.Parse(txtWidth.Text.Trim()), int.Parse(txtHeight.Text.Trim()), int.Parse(txtFontSize.Text.Trim()), dpFontAlign.SelectedIndex);

                }
                else if (dpMatrixModel.SelectedItem.ToString().Equals("Pdc101"))
                {
                    //Pd101LederModel.SendDataText(port, 9600, addr, sText);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
         
        private void ckComProtocol_CheckBoxClicked(object sender)
        {
            if (ckComProtocol.Checked)
            {
                ckNetProtocol.Checked = false;
            }
            else
            {
                ckNetProtocol.Checked = true;
            }
        }

        private void ckNetProtocol_CheckBoxClicked(object sender)
        {
            if (ckNetProtocol.Checked)
            {
                ckComProtocol.Checked = false;
            }
            else
            {
                ckComProtocol.Checked = true;
            }
        }

        private void btnSelectService_Click(object sender, EventArgs e)
        {
            ServiceSelectDialog dlg = new ServiceSelectDialog();
            dlg.sServiceNos = sServiceNos;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sServiceNos = dlg.sServiceNos;
                txtServiceNames.Text = dlg.sServiceNames;
            }
        }
    }
}
