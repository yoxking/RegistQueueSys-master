using EntFrm.DataAdapter.Business;
using EntFrm.DataAdapter.Dialogs;
using EntFrm.DataAdapter.Services;
using System;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.DataAdapter
{
    public partial class MainFrame :  Form
    {
        public delegate void SetMessageCallback(string message);
        public delegate void DoExitMainService();
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

            OnStart_AdapterService();

            //try
            //{
            //    //this.btnRegister.Visible = false;
            //    string activeDate = DateTime.Now.ToString("yyyy-MM-dd");

            //    int ret = IUserContext.doCheck_EncryptDogStatus(ref activeDate);
            //    if (ret == 0)
            //    {
            //        OnStart_AdapterService();

            //        DateTime dt = DateTime.Parse(activeDate);

            //        if (dt > DateTime.Parse("2029-12-31"))
            //        {
            //            this.lbRegInfo.Text = "产品授权：正式版本";
            //            this.btnRegister.Text = "更改注册码";
            //            //this.btnRegister.Visible = false;
            //        }
            //        else
            //        {
            //            this.lbRegInfo.Text = "产品授权：授权至" + activeDate;
            //            this.btnRegister.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        ret = IUserContext.doCheck_TrialRegistStatus();
            //        if (ret > 0)
            //        {
            //            if (ret < 5)
            //            {
            //                MessageBox.Show("当前授权期限少于5天，请及时注册正式版本！");
            //            } 


            //            OnStart_AdapterService();

            //            this.lbRegInfo.Text = "产品授权：试用版本（限用" + ret + "天）";
            //            this.btnRegister.Visible = true;
            //        }
            //        else
            //        {
            //            if (MessageBox.Show("软件授权试用限期已过，是否注册软件？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            //            {
            //                RegSoftware dlg = new RegSoftware();
            //                dlg.ShowDialog();
            //            }

            //            this.Close();
            //            this.Dispose();
            //            Application.Exit();
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("出错提示：" + ex.Message);

            //    this.Close();
            //    this.Dispose();
            //    Application.Exit();
            //}
        }

        private void OnStart_AdapterService()
        {
            try
            {
                //节目发布服务
                Thread thread1 = new Thread(PgmTaskService.CreateInstance().StartProgramTask);
                thread1.Start();

                //网络监听服务
                Thread thread2 = new Thread(NettyHostService.CreateInstance().StartHostService);
                thread2.Start();

                //WebSocket监听服务
                //Thread thread3 = new Thread(WebSocketService.CreateInstance().StartWebSocket);
                //thread3.Start();

                //HIS数据更新
                Thread thread4 = new Thread(UpdateDataService.CreateInstance().StartUpdateTask);
                thread4.Start();


                //HIS数据清理
                Thread thread5 = new Thread(ScheduleService.CreateInstance().StartSchedule);
                thread5.Start();
            }
            catch (Exception ex)
            { 
                MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "服务启动失败...");
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

        private void doExitSerivce()
        {  
            BeginInvoke(new Action(() =>
            {
                //WebSocketService.CreateInstance().StopWebSocket();
                NettyHostService.CreateInstance().StopHostService();
                PgmTaskService.CreateInstance().StopProgramTask();
                UpdateDataService.CreateInstance().StopUpdateTask();
                ScheduleService.CreateInstance().StopSchedule();
                System.Environment.Exit(0);

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

                //WebSocketService.CreateInstance().StopWebSocket();
                NettyHostService.CreateInstance().StopHostService();
                PgmTaskService.CreateInstance().StopProgramTask();
                UpdateDataService.CreateInstance().StopUpdateTask();
                ScheduleService.CreateInstance().StopSchedule();
                System.Environment.Exit(0);
            }
        }

        private void txtResult_TextChanged(object sender, EventArgs e)
        {
            this.txtResult.SelectionStart = this.txtResult.Text.Length;
            this.txtResult.SelectionLength = 0;
            this.txtResult.ScrollToCaret();
        } 

        private void btnDbsetting_Click(object sender, EventArgs e)
        {
            SettingDialog dlg = new SettingDialog();
            dlg.ShowDialog();

        }

        private void btnClearlog_Click(object sender, EventArgs e)
        {
            txtResult.Text = ""; 
        }
         

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegSoftware dlg = new RegSoftware();
            dlg.ShowDialog();
        }

        private void btnUptBranchs_Click(object sender, EventArgs e)
        {
            try
            {
                IAdapterBusiness adapterBoss = AdapterFactory.Create();

                //if (adapterBoss != null && adapterBoss.updateBranchList())
                //{
                //    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "门诊科室信息更新完成.");
                //}
                //else
                //{
                //    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "门诊科室信息更新失败.");
                //}

                if (adapterBoss != null && adapterBoss.updateStafferList())
                {
                    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "医生基本信息更新完成.");
                }
                else
                {
                    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "医生基本信息更新失败.");
                }

                //if (adapterBoss != null && adapterBoss.updateStaffRota())
                //{
                //    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "医生排班信息更新完成.");
                //}
                //else
                //{
                //    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "医生排班信息更新失败.");
                //}

                //if (adapterBoss != null && adapterBoss.updateCounterList())
                //{
                //    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "门诊科室信息更新完成.");
                //}
                //else
                //{
                //    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "门诊科室信息更新失败.");
                //}

                //if (adapterBoss != null && adapterBoss.updateServiceList())
                //{
                //    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "门诊业务信息更新完成.");
                //}
                //else
                //{
                //    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "门诊业务信息更新失败.");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUptStaffs_Click(object sender, EventArgs e)
        {
            try
            {
                //IAdapterBusiness adapterBoss = AdapterFactory.Create();
                //if (adapterBoss!=null&&adapterBoss.updateStaffList())
                //{
                //    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "医生基本信息更新完成.");
                //}
                //else
                //{
                //    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "医生基本信息更新失败.");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUptCounters_Click(object sender, EventArgs e)
        {
            //IAdapterBusiness adapterBoss = AdapterFactory.Create();
            //if (adapterBoss!=null&&adapterBoss.updateCounterList())
            //{
            //    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "医技科室信息更新完成.");
            //}
            //else
            //{
            //    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "医技科室信息更新失败.");
            //}
        }

        private void btnUptRUsers_Click(object sender, EventArgs e)
        {
            IAdapterBusiness adapterBoss = AdapterFactory.Create();
            if (adapterBoss != null && adapterBoss.updatePatientList())
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "就诊病人信息更新完成.");
            }
            else
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "就诊病人信息更新失败.");
            }

            if (adapterBoss != null && adapterBoss.updatePhexamList())
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "检查病人信息更新完成.");
            }
            else
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "检查病人信息更新失败.");
            }
        }

        private void btnUptStaffRota_Click(object sender, EventArgs e)
        {
            //IAdapterBusiness adapterBoss = AdapterFactory.Create();
            //if (adapterBoss!=null&&adapterBoss.updateDoctorRota())
            //{
            //    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "医生排班信息更新完成.");
            //}
            //else
            //{
            //    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "医生排班信息更新失败.");
            //}
        }

        private void btnWipeData_Click(object sender, EventArgs e)
        {
            IAdapterBusiness adapterBoss = AdapterFactory.Create();
            if (adapterBoss != null && adapterBoss.wipeHrtbeatFlows())
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "清除心跳信息更新完成...");
            }
            else
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "清除心跳信息更新失败...");
            }
        } 
    }
}

