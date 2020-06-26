using EntFrm.TicketConsole.IViewModel;
using EntFrm.TicketConsole.MPublicUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public partial class ScanBcode : Form
    {
        private BarcodeHook BarCode = new BarcodeHook();
        private BackgroundWorker bkWorker = new BackgroundWorker();
        private int clockTime;
        private bool bResult = false;

        public string StrInput
        {
            set;
            get;
        }

        public ScanBcode()
        {
            InitializeComponent();
            BarCode.BarCodeEvent += new BarcodeHook.BarCodeDelegate(BarCode_BarCodeEvent);
        }

        private void ScanBcode_Load(object sender, EventArgs e)
        {
            this.pnlContainer.BackgroundImage = EntFrm.TicketConsole.Properties.Resources.barcode;
            this.pnlContainer.BackgroundImageLayout = ImageLayout.Stretch;

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            BarCode.Start();
            clockTime = 15;

            bkWorker.WorkerReportsProgress = true;
            bkWorker.WorkerSupportsCancellation = true;
            bkWorker.DoWork += new DoWorkEventHandler(DoWork);
            bkWorker.ProgressChanged += new ProgressChangedEventHandler(ProgessChanged);
            bkWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteWork);

            bkWorker.RunWorkerAsync();
        }

        private void ScanBcode_FormClosing(object sender, FormClosingEventArgs e)
        {

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

                    Thread.Sleep(1000);
                }
            }

            return -1;
        }

        public void BarCode_BarCodeEvent(BarcodeHook.BarCodes barCode)
        {
            try
            {
                if (barCode.IsValid)
                {
                    StrInput = barCode.BarCode;
                    //MessageBox.Show(StrInput);
                    BarCode.Stop();

                    bResult = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
