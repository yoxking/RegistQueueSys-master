using System;
using System.Windows.Forms;

namespace EntFrm.MainService
{
    static class Program
    {
        private static System.Threading.Mutex mutex;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainFrame());

            mutex = new System.Threading.Mutex(true, "MainService");
            if (mutex.WaitOne(0, false))
            {
                IPublicHelper.DoAutoUpdate();
                Application.Run(new MainFrame());
            }
            else
            {
                MessageBox.Show("程序已经在运行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }
    }
}
