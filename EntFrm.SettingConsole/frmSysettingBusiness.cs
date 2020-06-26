using EntFrm.Business.BLL;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.SettingConsole.Speechs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EntFrm.SettingConsole
{
    public partial class frmSysettingBusiness : Form
    {
        public frmSysettingBusiness()
        {
            InitializeComponent();
        }

        private void frmSysettingBusiness_Load(object sender, EventArgs e)
        {  
            
            dpVariables.SelectedIndex = 0;

            DoBindBranchSource();
            DoBindVolumeSource();
            DoBindRateSource();
            DoBindTtsSource();
            DoRefreshForm();
        }

        private void DoBindBranchSource()
        {
            BranchInfoBLL infoBoss = new BranchInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            BranchInfoCollections infoColl = infoBoss.GetAllRecords();
             
            dpBranchList.DataSource = infoColl;
            dpBranchList.ValueMember = "sBranchNo";
            dpBranchList.DisplayMember = "sBranchName";

        }

        private void DoBindVolumeSource()
        {
            List<ItemObject> varsList = IPublicConsts.GetPublicVoiceVolume();

            dpCallVoiceVolume.DataSource = varsList;
            dpCallVoiceVolume.ValueMember = "Value";
            dpCallVoiceVolume.DisplayMember = "Name";
        }

        private void DoBindRateSource()
        {
            List<ItemObject> varsList = IPublicConsts.GetPublicVoiceRate();

            dpCallVoiceRate.DataSource = varsList;
            dpCallVoiceRate.ValueMember = "Value";
            dpCallVoiceRate.DisplayMember = "Name";
        }

        private void DoBindTtsSource()
        {
            CustomSpeech tts = new CustomSpeech();
            DictionaryEntry[] dict = tts.GetTokens();

            foreach (DictionaryEntry de in dict)
            {
                dpCallVoiceStyle.Items.Add(de.Value.ToString());
            }
            dpCallVoiceStyle.SelectedIndex = 0;
        }

        private void DoRefreshForm()
        {
            string sBranchNo = IUserContext.GetBranchNo();
            int iCallVoiceEnable = int.Parse(IUserContext.GetParamValue(IPublicConsts.DEF_CALLVOICEENABLE, IPublicConsts.TYPE_OTHERS));
            string sCallVoiceFormat = IUserContext.GetParamValue(IPublicConsts.DEF_CALLVOICEFORMAT, IPublicConsts.TYPE_OTHERS);
            string sCallVoiceStyle = IUserContext.GetParamValue(IPublicConsts.DEF_CALLVOICESTYLE, IPublicConsts.TYPE_OTHERS);
            int iCallVoiceVolume = int.Parse(IUserContext.GetParamValue(IPublicConsts.DEF_CALLVOICEVOLUME, IPublicConsts.TYPE_OTHERS));
            int iCallVoiceRate = int.Parse(IUserContext.GetParamValue(IPublicConsts.DEF_CALLVOICERATE, IPublicConsts.TYPE_OTHERS));
            //int iCallInterval = int.Parse(IUserContext.GetParamValue(IPublicConsts.DEF_CALLINTERVAL, IPublicConsts.TYPE_OTHERS));

            string sReaderModel = IUserContext.GetParamValue(IPublicConsts.READER_MODEL, IPublicConsts.TYPE_OTHERS);
            string sSerialPort = IUserContext.GetParamValue(IPublicConsts.READER_SPORT, IPublicConsts.TYPE_OTHERS);
            string sBaudrate = IUserContext.GetParamValue(IPublicConsts.READER_BRATE, IPublicConsts.TYPE_OTHERS);

            dpReaderModel.SelectedItem = sReaderModel;
            dpSerialPort.SelectedItem = sSerialPort;
            dpBaudrate.SelectedItem = sBaudrate;

            dpBranchList.SelectedValue = sBranchNo;
            ckCallVoiceEnable.Checked = (iCallVoiceEnable == 1);
            dpCallVoiceStyle.SelectedItem = sCallVoiceStyle;
            txtCallVoiceFormat.Text = sCallVoiceFormat;
            dpCallVoiceVolume.SelectedValue = iCallVoiceVolume;
            dpCallVoiceRate.SelectedValue = iCallVoiceRate;
            //txtCallInterval.Text = iCallInterval.ToString();

        }

        private void btnAddVar_Click(object sender, EventArgs e)
        { 
            string strInsertText = dpVariables.SelectedItem.ToString();

            int start = txtCallVoiceFormat.SelectionStart;
            txtCallVoiceFormat.Text = txtCallVoiceFormat.Text.Insert(start, strInsertText);
            txtCallVoiceFormat.Focus();
            txtCallVoiceFormat.SelectionStart = start;
            txtCallVoiceFormat.SelectionLength = strInsertText.Length;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                IUserContext.SetConfigValue(IPublicConsts.DEF_BRANCHNO,dpBranchList.SelectedValue.ToString());
                IUserContext.SetParamValue(IPublicConsts.DEF_CALLVOICEENABLE, ckCallVoiceEnable.Checked?"1":"0", IPublicConsts.TYPE_OTHERS);
                 IUserContext.SetParamValue(IPublicConsts.DEF_CALLVOICEFORMAT, txtCallVoiceFormat.Text.Trim(), IPublicConsts.TYPE_OTHERS);
                 IUserContext.SetParamValue(IPublicConsts.DEF_CALLVOICESTYLE,dpCallVoiceStyle.SelectedItem.ToString(), IPublicConsts.TYPE_OTHERS);
                 IUserContext.SetParamValue(IPublicConsts.DEF_CALLVOICEVOLUME,dpCallVoiceVolume.SelectedValue.ToString(), IPublicConsts.TYPE_OTHERS);
                 IUserContext.SetParamValue(IPublicConsts.DEF_CALLVOICERATE,dpCallVoiceRate.SelectedValue.ToString(), IPublicConsts.TYPE_OTHERS);

                IUserContext.SetParamValue(IPublicConsts.READER_MODEL, dpReaderModel.SelectedItem.ToString(), IPublicConsts.TYPE_OTHERS);
                IUserContext.SetParamValue(IPublicConsts.READER_SPORT, dpSerialPort.SelectedItem.ToString(), IPublicConsts.TYPE_OTHERS);
                IUserContext.SetParamValue(IPublicConsts.READER_BRATE, dpBaudrate.SelectedItem.ToString(), IPublicConsts.TYPE_OTHERS);

                //IUserContext.SetParamValue(IPublicConsts.DEF_CALLINTERVAL, txtCallInterval.Text.Trim(), IPublicConsts.TYPE_OTHERS);


                MessageBox.Show("保存成功!");
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
                string s = txtCallVoiceFormat.Text.Trim();

                List<ItemObject> varsList = IPublicConsts.GetPublicVariables();

                foreach (ItemObject temp in varsList)
                {

                    s = s.Replace("[科室名称]", "01");
                    s = s.Replace("[科室别名]", "01");
                }
                CustomSpeech tts = new CustomSpeech();
                tts.SpeakText(s, dpCallVoiceStyle.SelectedItem.ToString(), int.Parse(dpCallVoiceVolume.SelectedValue.ToString()), int.Parse(dpCallVoiceRate.SelectedValue.ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示：" + ex.Message);
            }
        }
    }
}
