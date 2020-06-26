using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EntFrm.AutoUpdate
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

            mutex = new System.Threading.Mutex(true, "AutoUpdate");
            if (mutex.WaitOne(0, false))
            {
                Application.Run(new MainFrame());
            }
            //else
            //{
            //    MessageBox.Show("程序已经在运行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    Application.Exit();
            //} 
        }
    }
}
