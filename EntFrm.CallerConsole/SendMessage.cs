using EntFrm.Business.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EntFrm.CallerConsole
{
    public partial class SendMessage : Form
    {
        public SendMessage()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string s = txtContent.Text.Trim();

            if (!string.IsNullOrEmpty(s))
            {
                MessageInfo info = new MessageInfo();

                info.iID = 0;
                info.sMessNo = "0";
                info.sMSender = ILoginHelper.CurrentUser.sStafferNo;
                info.sMReceiver = "0";
                info.iMType = 1;
                info.sMTitle = "自定义消息";
                info.sMContent = s;
                info.sAttachFile = "";
                info.dSendDate = DateTime.Now;
                info.dReceiveDate = DateTime.Parse("1970-01-01");
                info.iReadState = 0;
                info.sAddOptor = "";
                info.dAddDate = DateTime.Parse("1970-01-01");
                info.sModOptor = "";
                info.dModDate = DateTime.Parse("1970-01-01");
                info.iValidityState = 1;
                info.sComments = "";
                info.sBranchNo = "";
                info.sAppCode = "";
                info.sVersion = "";

                string msg = JsonConvert.SerializeObject(info);

                bool b = bool.Parse(IUserContext.OnExecuteCommand_Xp("postMessage", new string[] { msg }));
                if (b)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("发送失败!");
                }
            }
            else
            {
                MessageBox.Show("内容不能为空!");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
