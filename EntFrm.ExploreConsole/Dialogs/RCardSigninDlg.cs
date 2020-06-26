using EntFrm.ExploreConsole.Pubutils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.ExploreConsole.Dialogs
{
    public partial class RCardSigninDlg : Form
    { 
        private bool bResult = false;
        private int clockTime;
        private BackgroundWorker bkWorker = new BackgroundWorker();
        private string StrInput = "";

        public string sStrInput
        {
            get { return StrInput; }
            set { StrInput = value; }
        }

        public RCardSigninDlg()
        {
            InitializeComponent();
        }

        private void RCardSigninDlg_Load(object sender, EventArgs e)
        { 
            clockTime = 15; 

            bkWorker.WorkerReportsProgress = true;
            bkWorker.WorkerSupportsCancellation = true;
            bkWorker.DoWork += new DoWorkEventHandler(DoWork);
            bkWorker.ProgressChanged += new ProgressChangedEventHandler(ProgessChanged);
            bkWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteWork);

            bkWorker.RunWorkerAsync();
        }

        public void DoWork(object sender, DoWorkEventArgs e)
        {
            // 事件处理，指定处理函数  
            e.Result = ProcessProgress(bkWorker, e);
        }


        public void ProgessChanged(object sender, ProgressChangedEventArgs e)
        {
            lbTimeStr.Text = clockTime + "秒";
        }

        public void CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        {
            DialogResult = bResult ? DialogResult.OK : DialogResult.Cancel; 
            this.Close();
        }

        private int ProcessProgress(object sender, DoWorkEventArgs e)
        {
            //判断是否请求了取消后台操作
            if (bkWorker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                while (true)
                {
                    clockTime--;

                    if (clockTime < 1 || bResult)
                    {
                        break;
                    }
                    bkWorker.ReportProgress(clockTime);

                    //读卡号
                    sStrInput = RCardHelper.ReadRCardNo();

                    if (!string.IsNullOrEmpty(sStrInput))
                    { bResult = true; }

                    Thread.Sleep(1000);
                }
            }

            return -1;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                //读卡号
                sStrInput = RCardHelper.ReadRCardNo();

                if (!string.IsNullOrEmpty(sStrInput))
                { bResult = true; }

                //bResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("读卡失败!");
                bResult = false;
            }
        }
    }
}
