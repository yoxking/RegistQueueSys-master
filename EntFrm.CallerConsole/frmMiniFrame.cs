using EntFrm.Business.Model;
using Newtonsoft.Json;
using System;
using System.Windows.Forms;

namespace EntFrm.CallerConsole
{
    public partial class frmMiniFrame : Form
    { 
        public delegate void SetMessageCallback(string ticketNo, string statusMssg); 
        public static SetMessageCallback RefreshMessage; 
        private ViewTicketFlows ticket = null; 

        public frmMiniFrame()
        {
            InitializeComponent();
        }

        private void frmMiniFrame_Load(object sender, EventArgs e)
        {
            RefreshMessage += doRefreshMessage;
            this.TopMost = true;

            InitializeForm(); 
        }

        private void InitializeForm()
        {
            int x = Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width - 20;
            int y = 50;
            this.SetDesktopLocation(x, y);

            this.Text = ILoginHelper.CurrentUser.sCounterName;
        }
        private void doRefreshMessage(string ticketNo, string statusMssg)
        {
            if (this.btnTicketNo.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.btnTicketNo.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.btnTicketNo.Disposing || this.btnTicketNo.IsDisposed)
                        return;
                }

                SetMessageCallback d = new SetMessageCallback(doRefreshMessage);
                this.btnTicketNo.Invoke(d, new object[] { ticketNo, statusMssg });
            }
            else
            {
                this.btnTicketNo.Text = ticketNo; 
            }
        }
           
        private void btnCallNext_Click(object sender, EventArgs e)
        {
            try
            {
                string sResult = IUserContext.OnExecuteCommand_Xp("doCallNextTicket", new string[] { ILoginHelper.CurrentUser.sCounterNo });

                if (!string.IsNullOrEmpty(sResult))
                {
                    sResult = IUserContext.OnExecuteCommand_Xp("getVTicketFlowByPFlowNo", new string[] { sResult });
                    ticket = JsonConvert.DeserializeObject<ViewTicketFlows>(sResult);

                    if (ticket != null)
                    {
                        btnTicketNo.Text = ticket.sCnName; 
                    }
                }
                else
                {
                    ticket = null;
                    btnTicketNo.Text = ""; 
                } 
            }
            catch (Exception ex)
            { 
            }
        }

        private void btnCallRepeat_Click(object sender, EventArgs e)
        {
            try
            {
                if (ticket != null)
                {
                    string sResult = IUserContext.OnExecuteCommand_Xp("doRecallTicket", new string[] { ILoginHelper.CurrentUser.sCounterNo });

                    if (!string.IsNullOrEmpty(sResult))
                    { 
                        return;
                    }
                } 
            }
            catch (Exception ex)
            { 
            }
        }


        private void btnNotCome_Click(object sender, EventArgs e)
        {
            try
            {
                if (ticket != null)
                {
                    string sResult = IUserContext.OnExecuteCommand_Xp("doNotcomeTicket", new string[] { ILoginHelper.CurrentUser.sCounterNo });

                    if (!string.IsNullOrEmpty(sResult))
                    {
                        ticket = null;
                        btnTicketNo.Text = ""; 
                         
                        return;
                    }
                } 
            }
            catch (Exception ex)
            { 
            }
        }


        private void frmMiniFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
