using EntFrm.Business.Model;
using EntFrm.CallerConsole.IMyPublicUtils;
using Newtonsoft.Json;
using System;
using System.Windows.Forms;

namespace EntFrm.CallerConsole
{
    public partial class frmLoginForm : Form
    {
        public frmLoginForm()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                bool isRemember = bool.Parse(IPublicHelper.Get_ConfigValue("IsRemember")); 
                if (isRemember)
                {
                    ckRemember.Checked = isRemember;
                    txtLoginId.Text = IPublicHelper.Get_ConfigValue("UserName");
                    txtPsword.Text = IPublicHelper.Get_ConfigValue("PassWord");
                }
            }
            catch(Exception ex)
            {

            }
        } 

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {  
                if(string.IsNullOrEmpty(txtLoginId.Text.Trim())||string.IsNullOrEmpty(txtPsword.Text.Trim()))
                {
                    MessageBox.Show("账号和密码不能为空!");
                    return;
                } 

                string loginId = txtLoginId.Text.Trim();
                string psword = txtPsword.Text.Trim();
                string counterNo = IPublicHelper.Get_CounterNo();

                //string s = IUserContext.OnExecuteCommand_Xp("doSignIn2", new string[] { counterNo, loginId, psword });
                string s = IUserContext.OnExecuteCommand_Xp("doSignIn", new string[] { loginId, psword, counterNo });
                s = IUserContext.OnExecuteCommand_Xp("getStaffer", new string[] {s});
                StafferInfo info = JsonConvert.DeserializeObject<StafferInfo>(s);

                if (info != null)
                {
                    if (ckRemember.Checked)
                    {
                        IPublicHelper.Set_ConfigValue("IsRemember", "true");
                        IPublicHelper.Set_ConfigValue("UserName", info.sLoginId);
                        IPublicHelper.Set_ConfigValue("PassWord", info.sPassword); 
                    }
                    else
                    {
                        IPublicHelper.Set_ConfigValue("IsRemember", "false"); 
                    }

                    ILoginHelper.CurrentUser.IsLogin = true;
                    ILoginHelper.CurrentUser.sStafferNo = info.sStafferNo;
                    ILoginHelper.CurrentUser.sLoginId = info.sLoginId;
                    ILoginHelper.CurrentUser.sStafferName = info.sStafferName;
                    ILoginHelper.CurrentUser.sPassword = info.sPassword;
                    ILoginHelper.CurrentUser.sCounterNo = counterNo;
                    ILoginHelper.CurrentUser.sCounterName = IPublicHelper.GetCounterNameByNo(counterNo);
                    ILoginHelper.CurrentUser.sWorkingMode = IPublicHelper.GetParamValue(IPublicConsts.DEF_WORKINGMODE);
                    ILoginHelper.CurrentUser.dLoginTime = DateTime.Now;

                    this.DialogResult = DialogResult.OK; //成功
                    this.Close(); //关闭登陆窗体
                }
                else
                {
                    MessageBox.Show("账号或者密码错误，请重试!");
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("账号或者密码错误，请重试!"+ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            SettingDialog dlg = new SettingDialog();
            dlg.ShowDialog();
        }
    }
}
