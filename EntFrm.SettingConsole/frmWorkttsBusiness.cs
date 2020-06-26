using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.SettingConsole.Speechs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EntFrm.SettingConsole
{
    public partial class frmWorkttsBusiness : Form
    {
        private VoiceInfoBLL myBoss;
        private string sCurrentNo;
        private int iSelectedRow;
        private UpdateType updateType = UpdateType.Upt;

        public frmWorkttsBusiness()
        {
            InitializeComponent();
        }

        private void frmWorkttsBusiness_Load(object sender, EventArgs e)
        { 
            myBoss = new VoiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            sCurrentNo = "0";
            iSelectedRow = 0;
            InfoList.MultiSelect = false;

            DoBindVariableSource(); 
            DoBindTtsSource();
            DoBindVolumeSource();
            DoBindRateSource();
            DoBindDataList();
            DoRefreshForm();
        }

        private void DoBindDataList()
        {
            int count = 0;
            string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
            VoiceInfoCollections InfoColl = myBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

            if (InfoColl != null && InfoColl.Count > 0 )
            {
                if (sCurrentNo == "0")
                {
                    sCurrentNo = InfoColl[0].sTtsNo;
                } 
            }

            InfoList.AutoGenerateColumns = false;
            InfoList.DataSource = InfoColl;
        }

        private void DoBindTtsSource()
        {
            CustomSpeech tts = new CustomSpeech();
            DictionaryEntry[] dict = tts.GetTokens();

            foreach (DictionaryEntry de in dict)
            {
                dpVoice.Items.Add(de.Value.ToString());
            }
            dpVoice.SelectedIndex = 0;
        }

        private void DoBindVariableSource()
        {
            List<ItemObject> varsList = IPublicConsts.GetPublicVariables();

            dpVariables.DataSource = varsList;
            dpVariables.ValueMember = "Value";
            dpVariables.DisplayMember = "Name";
        } 

        private void DoBindVolumeSource()
        {
            List<ItemObject> varsList = IPublicConsts.GetPublicVoiceVolume();

            dpVolume.DataSource = varsList;
            dpVolume.ValueMember = "Value";
            dpVolume.DisplayMember = "Name";
        }

        private void DoBindRateSource()
        {
            List<ItemObject> varsList = IPublicConsts.GetPublicVoiceRate();

            dpRate.DataSource = varsList;
            dpRate.ValueMember = "Value";
            dpRate.DisplayMember = "Name";
        }

        private void DoRefreshForm()
        {
            try
            {
                VoiceInfo info = myBoss.GetRecordByNo(sCurrentNo);
                if (info != null)
                {
                    txtStyleName.Text = info.sTtsName;
                    dpVoice.SelectedItem = info.sVoice; 
                    dpVolume.SelectedValue = info.iVolume;
                    dpRate.SelectedValue = info.iRate;
                    txtFormatCalling.Text = info.sFormatCalling;
                    txtFormatWaiting.Text = info.sFormatWaiting;
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

                updateType = UpdateType.Upt;
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
            string sNo = GetFocusedColumnValue("TtsNo");
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
                int count = myBoss.GetCountByCondition("") + 1;

                txtStyleName.Text = "语音" + count;

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
                    string sNo = GetFocusedColumnValue("TtsNo");
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


        private void btnAddVar_Click(object sender, EventArgs e)
        {
            string strInsertText = dpVariables.SelectedValue.ToString();

            int start = txtFormatCalling.SelectionStart;
            txtFormatCalling.Text = txtFormatCalling.Text.Insert(start, strInsertText);
            txtFormatCalling.Focus();
            txtFormatCalling.SelectionStart = start;
            txtFormatCalling.SelectionLength = strInsertText.Length;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                string s = txtFormatCalling.Text.Trim();

                List<ItemObject> varsList = IPublicConsts.GetPublicVariables();

                foreach (ItemObject temp in varsList)
                {
                    s = s.Replace(temp.Value.ToString(), "01");
                } 
                CustomSpeech tts = new CustomSpeech(); 
                tts.SpeakText(s, dpVoice.SelectedItem.ToString(),int.Parse(dpVolume.SelectedValue.ToString()), int.Parse(dpRate.SelectedValue.ToString()));
            }
            catch(Exception ex)
            {
                MessageBox.Show("出错提示：" + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (updateType == UpdateType.Add)
                {
                    VoiceInfo info = new VoiceInfo();
                    info.sTtsNo = CommonHelper.Get_New12ByteGuid();
                    info.sTtsName = txtStyleName.Text.Trim(); 
                    info.sVoice = dpVoice.SelectedItem.ToString(); 
                    info.iVolume = int.Parse(dpVolume.SelectedValue.ToString());
                    info.iRate = int.Parse(dpRate.SelectedValue.ToString());
                    info.sFormatCalling = txtFormatCalling.Text.Trim();
                    info.sFormatWaiting = txtFormatWaiting.Text.Trim();

                    info.sPreMusic = ".\\Uploads\\dingdong.wav";
                    info.sPostMusic = ".\\Uploads\\dingdong.wav";
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
                    VoiceInfo info = myBoss.GetRecordByNo(this.sCurrentNo);
                    if (info != null)
                    {
                        info.sTtsName = txtStyleName.Text.Trim();
                        info.sVoice = dpVoice.SelectedItem.ToString();
                        info.iVolume = int.Parse(dpVolume.SelectedValue.ToString());
                        info.iRate = int.Parse(dpRate.SelectedValue.ToString());
                        info.sFormatCalling = txtFormatCalling.Text.Trim();
                        info.sFormatWaiting = txtFormatWaiting.Text.Trim(); 

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
    }
}
