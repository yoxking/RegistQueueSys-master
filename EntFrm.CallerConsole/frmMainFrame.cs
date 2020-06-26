using EntFrm.Business.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntFrm.CallerConsole
{
    public partial class frmMainFrame : Form
    {
        /*
         * 
         处理状态
         0;//等待办理中...
         1;//正在办理中...
         2;//完成流程...，终止流程...
         3;//顾客未到...
         4;//票号滞后...
         5;//窗口转移...
         * **/
          
        public delegate void SetMessageCallback(string ticketNo, string statusMssg);
        public delegate void SetQueunumCallback(int queuingNum, int passedNum,int finishedNum);
        public delegate void SetQueuingCallback(List<ItemTicket> waitingList);
        public delegate void SetPassedCallback(List<ItemTicket> passedList);
        public delegate void SetFinishedCallback(List<ItemTicket> finishedList); 
        public static SetMessageCallback RefreshMessage;
        public static SetQueunumCallback RefreshQueunum;
        public static SetQueuingCallback RefreshQueuing;
        public static SetPassedCallback RefreshPassed;
        public static SetFinishedCallback RefreshFinished;
        private ViewTicketFlows ticket = null; 
        private bool quitFlag = false;
        private int tabsIndex = 0;//QUEUINGTAB,PASSEDTAB,FINISHEDTAB

        frmMiniFrame miniForm = null;


        #region   
        internal AnchorStyles StopAanhor = AnchorStyles.None;
        private Point mPoint = new Point();
        #endregion

        public frmMainFrame()
        {
            InitializeComponent();
        }

        #region 
        //这篇文章是在互联网搜索到的，但是很多文章都没有给 WM_QUERYENDSESSION赋值这句话，所以重新整理了一下
        /// <summary>
        /// 窗口过程的回调函数
        /// </summary>
        ///<param name="m">
        private const int WM_QUERYENDSESSION = 0x0011;
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                //此消息在OnFormClosing之前
                case WM_QUERYENDSESSION:

                    IUserContext.OnExecuteCommand_Xp("doSignOut", new string[] { ILoginHelper.CurrentUser.sCounterNo });

                    this.Dispose();
                    Application.Exit();

                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
        }

        #endregion

        private void frmMainFrame_Load(object sender, EventArgs e)
        {             
            RefreshMessage += doRefreshMessage;
            RefreshQueunum += doRefreshQueunum;
            RefreshQueuing += doRefreshQueuing;
            RefreshPassed += doRefreshPassed;
            RefreshFinished += doRefreshFinished;

            myTimer.Start();

            InitializeForm();
            DoRefreshForm();
            RefreshService();
        }

        private void InitializeForm()
        {
            System.Windows.Forms.Timer StopRectTimer = new System.Windows.Forms.Timer();
            StopRectTimer.Tick += new EventHandler(myTimerTick);
            StopRectTimer.Interval = 200;
            StopRectTimer.Enabled = true;
            this.TopMost = true;
            StopRectTimer.Start();

            int x = Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width - 20;
            int y = 50;
            this.SetDesktopLocation(x, y);

            lbStafferName.Text = ILoginHelper.CurrentUser.sStafferName;
            this.Text = ILoginHelper.CurrentUser.sCounterName;
        }

        private void DoRefreshForm()
        {
            btnCallRepeat.Enabled = false;
            //btnCallGoto.Enabled = true;
            
            btnPassFlow.Enabled = false;
            btnFinishFlow.Enabled = false;

            if (ticket == null)
            {
                //btnCallGoto.Enabled = true;
            }
            else if (ticket != null)
            {
                btnCallRepeat.Enabled = true;
                
                btnPassFlow.Enabled = true;
                btnFinishFlow.Enabled = true;
            }

            doBind_GridList();
        }

        private void RefreshService()
        { 
            //刷新等候病人信息
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    doBind_GridList();

                    Thread.Sleep(30000);
                    if (quitFlag)
                    {
                        break;
                    }
                }

            });

        }
    
        private void doRefreshMessage( string ticketNo, string statusMssg)
        { 
            if (this.lbTicketNo.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.lbTicketNo.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.lbTicketNo.Disposing || this.lbTicketNo.IsDisposed)
                        return;
                }

                SetMessageCallback d = new SetMessageCallback(doRefreshMessage);
                this.lbTicketNo.Invoke(d, new object[] { ticketNo, statusMssg });
            }
            else
            {
                if (!string.IsNullOrEmpty(ticketNo))
                {
                    this.lbTicketNo.Text = ticketNo;
                }
                if (!string.IsNullOrEmpty(statusMssg))
                {
                    this.lbMessage.Text = statusMssg;
                } 
            }
        }

        private void doRefreshQueunum(int queuingNum, int passedNum, int finishedNum)
        {
            if (this.lbWaiterNum.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.lbWaiterNum.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.lbWaiterNum.Disposing || this.lbWaiterNum.IsDisposed)
                        return;
                }

                SetQueunumCallback d = new SetQueunumCallback(doRefreshQueunum);
                this.lbWaiterNum.Invoke(d, new object[] { queuingNum, passedNum, finishedNum });
            }
            else
            {
                lbWaiterNum.Text = queuingNum.ToString();
                tabPages.TabPages[0].Text = "等候中("+queuingNum+")";
                tabPages.TabPages[1].Text = "过号(" + passedNum + ")";
                tabPages.TabPages[2].Text = "完成(" + finishedNum + ")";
            }
        }
        private void doRefreshQueuing(List<ItemTicket> queuingList)
        {
            if (this.dgQueuingList.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.dgQueuingList.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.dgQueuingList.Disposing || this.dgQueuingList.IsDisposed)
                        return;
                }

                SetQueuingCallback d = new SetQueuingCallback(doRefreshQueuing);
                this.dgQueuingList.Invoke(d, new object[] { queuingList });
            }
            else
            {
                dgQueuingList.AutoGenerateColumns = false;
                dgQueuingList.DataSource = queuingList; 
            }
        }

        private void doRefreshPassed(List<ItemTicket> passedList)
        {
            if (this.dgPassedList.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.dgPassedList.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.dgPassedList.Disposing || this.dgPassedList.IsDisposed)
                        return;
                }

                SetPassedCallback d = new SetPassedCallback(doRefreshPassed);
                this.dgPassedList.Invoke(d, new object[] { passedList });
            }
            else
            {
                dgPassedList.AutoGenerateColumns = false;
                dgPassedList.DataSource = passedList; 
            }
        }

        private void doRefreshFinished(List<ItemTicket> finishedList)
        {
            if (this.dgFinishedList.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.dgFinishedList.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.dgFinishedList.Disposing || this.dgFinishedList.IsDisposed)
                        return;
                }

                SetFinishedCallback d = new SetFinishedCallback(doRefreshFinished);
                this.dgFinishedList.Invoke(d, new object[] { finishedList });
            }
            else
            {
                dgFinishedList.AutoGenerateColumns = false;
                dgFinishedList.DataSource = finishedList; 
            }
        }

        #region 窗口停靠
        private void myTimerTick(object sender, EventArgs e)
        {
            if (this.Bounds.Contains(Cursor.Position))
            {
                switch (this.StopAanhor)
                {
                    case AnchorStyles.Top:
                        this.Location = new Point(this.Location.X, 0);
                        break;
                    case AnchorStyles.Left:
                        this.Location = new Point(0, this.Location.Y);
                        break;
                    case AnchorStyles.Right:
                        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, this.Location.Y);
                        break;
                    case AnchorStyles.Bottom:
                        this.Location = new Point(this.Location.X, Screen.PrimaryScreen.Bounds.Height - this.Height);
                        break;
                }
            }
            else
            {
                switch (this.StopAanhor)
                {
                    case AnchorStyles.Top:
                        this.Location = new Point(this.Location.X, (this.Height - 8) * (-1));
                        break;
                    case AnchorStyles.Left:
                        this.Location = new Point((-1) * (this.Width - 8), this.Location.Y);
                        break;
                    case AnchorStyles.Right:
                        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 8, this.Location.Y);
                        break;
                    case AnchorStyles.Bottom:
                        this.Location = new Point(this.Location.X, (Screen.PrimaryScreen.Bounds.Height - 8));
                        break;
                }
            }

        }

        private void mStopAnhor()
        {
            if (this.Top <= 0 && this.Left <= 0)
            {
                StopAanhor = AnchorStyles.None;
            }
            else if (this.Top <= 0)
            {
                StopAanhor = AnchorStyles.Top;
            }
            else if (this.Left <= 0)
            {
                StopAanhor = AnchorStyles.Left;
            }
            else if (this.Left >= Screen.PrimaryScreen.Bounds.Width - this.Width)
            {
                StopAanhor = AnchorStyles.Right;
            }
            else if (this.Top >= Screen.PrimaryScreen.Bounds.Height - this.Height)
            {
                StopAanhor = AnchorStyles.Bottom;
            }
            else
            {
                StopAanhor = AnchorStyles.None;
            }
        }
 
        private void frmMainFrame_LocationChanged(object sender, EventArgs e)
        {
            this.mStopAnhor();
        }

        private void frmMainFrame_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint.X = e.X;
            mPoint.Y = e.Y;
        }

        private void frmMainFrame_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point myPosittion = MousePosition;
                myPosittion.Offset(-mPoint.X, -mPoint.Y);
                Location = myPosittion;
            }
        }

        private void frmMainFrame_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Right)
            {
                myContextMenu.Show(this, e.Location);
            }
        }
        #endregion

        #region 托盘操作
        private void myNotifyIcon_DoubleClick(object sender, EventArgs e)
        { 
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
            else if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }

        private void frmMainFrame_SizeChanged(object sender, EventArgs e)
        { 
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide(); //或者是this.Visible = false;
                this.myNotifyIcon.Visible = true;
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            onExitSystem();
        }

        private void onExitSystem()
        {
            if (MessageBox.Show("你确定要退出程序吗？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                quitFlag = true;

                IUserContext.OnExecuteCommand_Xp("doSignOut", new string[] { ILoginHelper.CurrentUser.sCounterNo });

                myNotifyIcon.Visible = false;
                 
                this.Dispose();
                Application.Exit();
            }
        }

        private void frmMainFrame_FormClosing(object sender, FormClosingEventArgs e)
        { 
            e.Cancel = true;

            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        #endregion

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
                        lbTicketNo.Text = ticket.sCnName;
                        lbMessage.Text = "呼叫下一位操作成功！";
                    }
                }
                else
                {
                    ticket = null;
                    lbTicketNo.Text = "";
                    lbMessage.Text = "暂无办理人员!";
                } 
                DoRefreshForm();
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
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
                        lbMessage.Text = "重复呼叫操作成功!";
                        return;
                    }
                }
                lbMessage.Text = "重复呼叫操作失败!";
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        }

        private void btnPassFlow_Click(object sender, EventArgs e)
        {
            try
            {
                if (ticket != null)
                {
                    string sResult = IUserContext.OnExecuteCommand_Xp("doNotcomeTicket", new string[] { ILoginHelper.CurrentUser.sCounterNo });

                    if (!string.IsNullOrEmpty(sResult))
                    {
                        ticket = null;
                        lbTicketNo.Text = "";
                        lbMessage.Text = "未到过号操作成功！";

                        DoRefreshForm();
                        return;
                    }
                }
                lbMessage.Text = "未到过号操作失败!";
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        } 

        private void btnFinishFlow_Click(object sender, EventArgs e)
        {
            try
            {
                if (ticket != null)
                {
                    string sResult = IUserContext.OnExecuteCommand_Xp("doFinishTicket", new string[] { ILoginHelper.CurrentUser.sCounterNo });

                    if (!string.IsNullOrEmpty(sResult))
                    {
                        ticket = null;
                        lbTicketNo.Text = "";
                        lbMessage.Text = "完成业务操作成功！";

                        DoRefreshForm();
                        return;
                    }
                }
                lbMessage.Text = "完成业务操作失败!";
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        }

        private void btnCallHelper_Click(object sender, EventArgs e)
        {
            try
            {
                string sResult = IUserContext.OnExecuteCommand_Xp("doSeekHelp", new string[] { ILoginHelper.CurrentUser.sCounterNo });

                if (bool.Parse(sResult))
                {
                    lbMessage.Text = "呼叫护士操作成功！"; 
                }
                else
                {
                    lbMessage.Text = "呼叫护士操作失败!";
                }
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            SettingDialog dlg = new SettingDialog();
            dlg.ShowDialog();
        }
         
        private void btnQuitCaller_Click(object sender, EventArgs e)
        {
            onExitSystem();
        }
          
        private void myTimer_Tick(object sender, EventArgs e)
        {
            //刷新等候病人数
            Task.Factory.StartNew(() =>
            {
                try
                {
                    string queuingCount = IUserContext.OnExecuteCommand_Xp("getQueuingCountByCounterNo", new string[] { ILoginHelper.CurrentUser.sCounterNo });
                     
                    string passedCount = IUserContext.OnExecuteCommand_Xp("getPassedCountByCounterNo", new string[] { ILoginHelper.CurrentUser.sCounterNo });
                    
                    string finishedCount = IUserContext.OnExecuteCommand_Xp("getFinishedCountByCounterNo", new string[] { ILoginHelper.CurrentUser.sCounterNo });
                    frmMainFrame.RefreshQueunum(int.Parse(queuingCount), int.Parse(passedCount), int.Parse(finishedCount));
                }
                catch (Exception ex)
                {
                    frmMainFrame.RefreshMessage("", ex.Message);
                }
            });            
        } 

        private void btnMinimal_Click(object sender, EventArgs e)
        {
            this.Hide();
            if(miniForm == null || miniForm.IsDisposed)
            {
                miniForm = new frmMiniFrame();
                miniForm.Show();//未打开，直接打开。
            }
            else
            {
                miniForm.Activate();//已打开，获得焦点，置顶。
            } 
        }

        private void tabPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabsIndex = tabPages.SelectedIndex;

            doBind_GridList();
        }

        private void doBind_GridList()
        {
            //刷新等候病人信息
            Task.Factory.StartNew(() =>
            {

                if (tabsIndex == 0)
                {
                    try
                    {
                        string sResult = IUserContext.OnExecuteCommand_Xp("getQueuingListByCounterNo", new string[] { ILoginHelper.CurrentUser.sCounterNo, "100" });

                        if (!string.IsNullOrEmpty(sResult))
                        {
                            List<ItemTicket> ticketList = JsonConvert.DeserializeObject<List<ItemTicket>>(sResult);

                            frmMainFrame.RefreshQueuing(ticketList);
                        }
                        else
                        {
                            frmMainFrame.RefreshQueuing(null);
                        }
                    }
                    catch (Exception ex)
                    {
                        frmMainFrame.RefreshMessage("", ex.Message);
                    }
                }
                else if (tabsIndex == 1)
                {
                    try
                    {
                        string sResult = IUserContext.OnExecuteCommand_Xp("getNonarrivalListByCounterNo", new string[] { ILoginHelper.CurrentUser.sCounterNo, "100" });

                        if (!string.IsNullOrEmpty(sResult))
                        {

                            List<ItemTicket> ticketList = JsonConvert.DeserializeObject<List<ItemTicket>>(sResult);

                            frmMainFrame.RefreshPassed(ticketList);
                        }
                        else
                        {
                            frmMainFrame.RefreshPassed(null);
                        }
                    }
                    catch (Exception ex)
                    {
                        frmMainFrame.RefreshMessage("", ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        string sResult = IUserContext.OnExecuteCommand_Xp("getFinishedListByCounterNo", new string[] { ILoginHelper.CurrentUser.sCounterNo, "100" });

                        if (!string.IsNullOrEmpty(sResult))
                        {

                            List<ItemTicket> ticketList = JsonConvert.DeserializeObject<List<ItemTicket>>(sResult);

                            frmMainFrame.RefreshFinished(ticketList);
                        }
                        else
                        {
                            frmMainFrame.RefreshFinished(null);
                        }
                    }
                    catch (Exception ex)
                    {
                        frmMainFrame.RefreshMessage("", ex.Message);
                    }
                }

            });
        }

        private void btnMenuGoto_Click(object sender, EventArgs e)
        {
            try
            {
                string PFlowNo = "";
                if (tabsIndex==0&& this.dgQueuingList.SelectedRows.Count > 0)
                {
                    PFlowNo = this.dgQueuingList.SelectedRows[0].Cells["qPFlowNo"].Value.ToString();
                }
                else if(tabsIndex == 1 && this.dgPassedList.SelectedRows.Count > 0)
                {
                    PFlowNo = this.dgPassedList.SelectedRows[0].Cells["pPFlowNo"].Value.ToString();
                }
                else if(tabsIndex == 2 && this.dgFinishedList.SelectedRows.Count > 0)
                {
                    PFlowNo = this.dgFinishedList.SelectedRows[0].Cells["fPFlowNo"].Value.ToString();
                }

                if (!string.IsNullOrEmpty(PFlowNo))
                {
                    IUserContext.OnExecuteCommand_Xp("doSpecialTicket", new string[] { ILoginHelper.CurrentUser.sCounterNo, PFlowNo });

                    string sResult = IUserContext.OnExecuteCommand_Xp("getVTicketFlowByPFlowNo", new string[] { PFlowNo });
                    ticket = JsonConvert.DeserializeObject<ViewTicketFlows>(sResult);

                    if (ticket != null)
                    {
                        lbTicketNo.Text = ticket.sCnName;
                        lbMessage.Text = "指定呼叫操作成功！";
                    }

                    DoRefreshForm();
                }
                else
                {
                    lbMessage.Text = "指定呼叫操作失败!";
                }
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        }
        private void btnMenuPass_Click(object sender, EventArgs e)
        {
            try
            {
                string PFlowNo = "";
                if (tabsIndex == 0 && this.dgQueuingList.SelectedRows.Count > 0)
                {
                    PFlowNo = this.dgQueuingList.SelectedRows[0].Cells["qPFlowNo"].Value.ToString();
                }
                else if (tabsIndex == 1 && this.dgPassedList.SelectedRows.Count > 0)
                {
                    PFlowNo = this.dgPassedList.SelectedRows[0].Cells["pPFlowNo"].Value.ToString();
                }
                else if (tabsIndex == 2 && this.dgFinishedList.SelectedRows.Count > 0)
                {
                    PFlowNo = this.dgFinishedList.SelectedRows[0].Cells["fPFlowNo"].Value.ToString();
                }

                if (!string.IsNullOrEmpty(PFlowNo))
                {
                    lbMessage.Text = PFlowNo;

                    string sResult = IUserContext.OnExecuteCommand_Xp("doPassedTicket", new string[] { ILoginHelper.CurrentUser.sCounterNo, PFlowNo });

                    if (!string.IsNullOrEmpty(sResult))
                    {
                        ticket = null;
                        lbTicketNo.Text = "";
                        lbMessage.Text = "未到过号操作成功！";

                        DoRefreshForm(); 
                    }
                }
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        } 
        private void btnMenuRediag_Click(object sender, EventArgs e)
        {
            try
            {
                string PFlowNo = "";
                if (tabsIndex == 0 && this.dgQueuingList.SelectedRows.Count > 0)
                {
                    PFlowNo = this.dgQueuingList.SelectedRows[0].Cells["qPFlowNo"].Value.ToString();
                }
                else if (tabsIndex == 1 && this.dgPassedList.SelectedRows.Count > 0)
                {
                    PFlowNo = this.dgPassedList.SelectedRows[0].Cells["pPFlowNo"].Value.ToString();
                }
                else if (tabsIndex == 2 && this.dgFinishedList.SelectedRows.Count > 0)
                {
                    PFlowNo = this.dgFinishedList.SelectedRows[0].Cells["fPFlowNo"].Value.ToString();
                }

                if (!string.IsNullOrEmpty(PFlowNo))
                {
                    lbMessage.Text = PFlowNo;

                    string sResult = IUserContext.OnExecuteCommand_Xp("doRediagTicket", new string[] { ILoginHelper.CurrentUser.sCounterNo, PFlowNo });

                    if (!string.IsNullOrEmpty(sResult))
                    {
                        ticket = null;
                        lbTicketNo.Text = "";
                        lbMessage.Text = "复诊操作成功！";

                        DoRefreshForm(); 
                    }
                }
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        }

    }
}
