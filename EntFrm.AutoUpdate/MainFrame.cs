
using EntFrm.AutoUpdate.Service;
using System;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.AutoUpdate
{
    public partial class MainFrame : Form
    {
        delegate void AsynUpdateUI(int step);

        public MainFrame()
        {
            InitializeComponent();
        }

        private void MainFrame_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;   //最小化 

            Thread t1 = new Thread(doUpdate); 
            t1.Start(); 
        }

        private void doUpdate()
        {
            IUpdateService updateService = new UpdateService();
            updateService.UpdateProgressChanged += updateProgressChanged;

            if (updateService.DetectVersion())
            {
                DialogResult result = MessageBox.Show("发现有更新的程序版本，是否现在更新？", "更新提示", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    this.WindowState = FormWindowState.Normal;   //显示

                    if (updateService.TryUpdateNow())
                    {
                        UpdateWorker.UpdateVerCode(updateService.GetVersionCode());
                        UpdateWorker.StartExecApps();
                    }
                }
            }

            Application.Exit();
        } 

        public void updateProgressChanged(object sender, UpdateProgressArgs e)
        {
            int pstep = (int)e.ProgressPercent * 100;
            updateProegess(pstep);
        }

        public void updateProegess(int pos)
        {
            if (InvokeRequired)
            {
                this.Invoke(new AsynUpdateUI(delegate (int step)
                {
                    this.barProgress.Value = step;
                }), pos);
            }
            else
            {
                this.barProgress.Value = pos; 
            }
        }
    }
}
