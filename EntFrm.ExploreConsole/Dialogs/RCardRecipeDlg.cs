using EntFrm.ExploreConsole.Pubutils;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.ExploreConsole.Dialogs
{
    public partial class RCardRecipeDlg : Form
    {
        private bool bResult = false;
        private int clockTime;
        private BackgroundWorker bkWorker = new BackgroundWorker();
        private string strInput = ""; 

        public RCardRecipeDlg()
        {
            InitializeComponent();
        }

        private void RCardRecipeDlg_Load(object sender, EventArgs e)
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
            if (bResult)
            {
                try
                {
                    string baseUrl = PublicHelper.GetHomeUrl();
                    string sbody = "{ricardno:'" + strInput + "'}";

                    string result = MyHttpUtils.HttpPost(baseUrl + "/IAdapter/EnqueueRCard_TakeRecipe", sbody);

                    if (result.Equals("Success"))
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("报到失败!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("报到失败!");
                }
            }

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
                    strInput = RCardHelper.ReadRCardNo();

                    if (!string.IsNullOrEmpty(strInput))
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
                strInput = RCardHelper.ReadRCardNo();

                if (!string.IsNullOrEmpty(strInput))
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