using System;
using System.IO;
using System.Threading;

namespace EntFrm.AutoUpdate.Service
{
    public class UpdateWorker
    { 
        public static void UpdateVerCode(string verCode)
        {
            IPublicHelper.Set_ConfigValue("VerCode", verCode);
        }

        /// <summary>
        /// 由Updater升级完毕后调用，运行托管exe
        /// </summary>
        public static void StartExecApps()
        {
            string programName = IPublicHelper.Get_ConfigValue("AppName");
             
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName + "\\"+ programName+".exe";
            startInfo.Arguments = "";
            System.Diagnostics.Process.Start(startInfo);
        }
    }
}