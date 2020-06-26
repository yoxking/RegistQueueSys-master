using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.ExploreConsole
{
    class PublicHelper
    {

        public static void SetConfigValue(string Name, string Value)
        {
            ConfigurationManager.AppSettings.Set(Name, Value);

            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings[Name].Value = Value;
            config.Save(ConfigurationSaveMode.Modified);
            config = null;
        }

        public static string GetConfigValue(string Name)
        {
            try
            {
                return ConfigurationManager.AppSettings[Name].ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string GetHomeUrl()
        {
            return ConfigurationManager.AppSettings["HomeUrl"].ToString();
        }

        public static string GetIsFull()
        {
            return ConfigurationManager.AppSettings["IsFull"].ToString();
        }

        public static void DoAutoUpdate()
        {
            new Thread(new ThreadStart(new Action(() =>
            {
                string updateFile = AppDomain.CurrentDomain.BaseDirectory + "\\AutoUpdate\\EntFrm.AutoUpdate.exe";
                //判断文件的存在
                if (File.Exists(updateFile))
                {
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.FileName = updateFile;
                    startInfo.Arguments = "";
                    System.Diagnostics.Process.Start(startInfo);
                }

            }))).Start();
        }

    }
}
