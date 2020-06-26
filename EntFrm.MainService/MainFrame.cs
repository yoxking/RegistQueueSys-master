using EntFrm.MainService.Dialogs;
using EntFrm.MainService.Services;
using EntFrm.MainService.Speechs;
using System;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.MainService
{
    public partial class MainFrame : Form
    {
        public delegate void SpeechTextCallback(string text, string voice, int volume, int rate); 
        public delegate void SetMessageCallback(string message); 
        public delegate void DoExitMainService();
        public static SpeechTextCallback DoSpeechText;
        public static SetMessageCallback PrintMessage; 
        public static DoExitMainService ExitService;

        public MainFrame()
        {
            InitializeComponent();
        }

        private void MainFrame_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();

            PrintMessage += doPrintMessage;
            ExitService += doExitSerivce;
            DoSpeechText += doSpeechText;
            OnStart_QueueService(); 
        }

        private void OnStart_QueueService()
        {
            try
            {
                //初始化主机服务
                Thread thread1 = new Thread(NetHostService.CreateInstance().OpenService);
                thread1.Start();

                //自动报到服务
                Thread thread2 = new Thread(IRegistService.CreateInstance().StartRegistFlows);
                thread2.Start();

                ////数据处理服务
                Thread thread6 = new Thread(IUpdateService.CreateInstance().StartUpdateFlows);
                thread6.Start();

                //初始化硬件呼叫监听                
                Thread thread3 = new Thread(HdcallerService.CreateInstance().doInitialCaller);
                thread3.Start();

                //初始化综合屏显示
                //Eq2008LedMatrix eq2008 = new Eq2008LedMatrix();
                //eq2008.doStart_LedMatrix();

                //初始化LED屏信息
                Thread thread4 = new Thread(DisplayService.CreateInstance().doInitialLedTip);
                thread4.Start();

                //清空员工登录
                Thread thread5 = new Thread(ITicketService.CreateInstance().doSignoutAll);
                thread5.Start(); 
            }
            catch (Exception ex)
            {
                NetHostService.CreateInstance().CloseService();
                IRegistService.CreateInstance().StopRegistFlows();
                IUpdateService.CreateInstance().StopUpdateFlows();

                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "服务启动失败...");
            }
        }

        /// <summary>
        /// 更新文本框内容的方法
        /// </summary>
        /// <param name="text"></param>
        private void doPrintMessage(string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (this.txtResult.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.txtResult.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.txtResult.Disposing || this.txtResult.IsDisposed)
                        return;
                }

                SetMessageCallback d = new SetMessageCallback(doPrintMessage);
                this.txtResult.Invoke(d, new object[] { text });
            }
            else
            {
                txtResult.AppendText(text + Environment.NewLine);
            }
        }

        private void doSpeechText(string text, string voice, int volume, int rate)
        {
            BeginInvoke(new Action(() =>
            {
                CustomSpeech cs = new CustomSpeech();
                cs.SpeakText(text, voice, volume, rate);
            }));
        }

        private void doExitSerivce()
        {
            try
            {
                NetHostService.CreateInstance().CloseService();
                IRegistService.CreateInstance().StopRegistFlows();
                IUpdateService.CreateInstance().StopUpdateFlows();
            }
            catch(Exception ex) { }

            BeginInvoke(new Action(() =>
            { 
                this.Close();
                this.Dispose();
                Application.Exit();

            }));
        }

        private void frmMainFrame_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide(); //或者是this.Visible = false;
                this.myNotifyIcon.Visible = true;
            }
        }

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

        private void frmMainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            onExitSystem();
        }

        private void showMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            onExitSystem();
        }

        private void onExitSystem()
        {
            if (MessageBox.Show("你确定要退出程序吗？", "确认", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                myNotifyIcon.Visible = false;

                try
                {
                    NetHostService.CreateInstance().CloseService();
                    IRegistService.CreateInstance().StopRegistFlows();
                    IUpdateService.CreateInstance().StopUpdateFlows();
                }
                catch (Exception ex) { }

                this.Close();
                this.Dispose();
                Application.Exit();
            }
        }

        private void txtResult_TextChanged(object sender, EventArgs e)
        {
            this.txtResult.SelectionStart = this.txtResult.Text.Length;
            this.txtResult.SelectionLength = 0;
            this.txtResult.ScrollToCaret();
        }
        
        private void btnSysetting_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "EntFrm.SettingConsole.exe";
            process.StartInfo.WorkingDirectory = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            // process.WaitForExit();//无限制的等待，看自己情况要不要加

            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void btnDbsetting_Click(object sender, EventArgs e)
        {
            Setting dlg = new Setting();
            dlg.ShowDialog();
        }

        private void btnClearlog_Click(object sender, EventArgs e)
        {
            txtResult.Text = "";
            //LogClass info=JsonConvert.DeserializeObject<LogClass>(txtResult.Text);

            //MessageBox.Show(info.sClassName);
        }
          
        private void btnBrowselog_Click(object sender, EventArgs e)
        {
            string logFile = System.Windows.Forms.Application.StartupPath + "\\AppLogs";
            System.Diagnostics.Process.Start("Explorer.exe", logFile);
        } 
        private void myTimer_Tick(object sender, EventArgs e)
        {

            //Thread thread2 = new Thread(DisplayService.CreateInstance().doRefreshLedTip);
            //thread2.Start();
        } 
         
        private void btnClearAllData_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(DbaseService.CreateInstance().doClearAllData);
            thread.Start();
        }

        private void btnClearFlowsData_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(DbaseService.CreateInstance().doClearQueueData);
            thread.Start();
        }

        private void btnBakupDb_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(DbaseService.CreateInstance().doBakupData);
            thread.Start();
        }

        private void btnRecoverDb_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(DbaseService.CreateInstance().doRecoverData);
            thread.Start();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //RegSoft dlg = new RegSoft();
            //dlg.ShowDialog();
        }
    }
}
