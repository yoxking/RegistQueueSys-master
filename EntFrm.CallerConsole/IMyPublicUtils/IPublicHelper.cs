using EntFrm.Business.Model;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.CallerConsole
{
    public class IPublicHelper
    { 
        public static void Set_ConfigValue(string Name, string Value)
        {
            ConfigurationManager.AppSettings.Set(Name, Value);

            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings[Name].Value = Value;
            config.Save(ConfigurationSaveMode.Modified);
            config = null;
        }
        public static string Get_ConfigValue(string Name)
        {
            return ConfigurationManager.AppSettings[Name].ToString();
        }

        public static string Get_ServerIp()
        {
            return ConfigurationManager.AppSettings["ServerIp"].ToString();
        }

        public static string Get_WTcpPort()
        {
            return ConfigurationManager.AppSettings["WTcpPort"].ToString();
        }

        public static string Get_WHttpPort()
        {
            return ConfigurationManager.AppSettings["WHttpPort"].ToString();
        }

        public static string Get_XPubPort()
        {
            return ConfigurationManager.AppSettings["XPubPort"].ToString();
        }

        public static string Get_XSubPort()
        {
            return ConfigurationManager.AppSettings["XSubPort"].ToString();
        }

        public static string Get_CounterNo()
        {
            return ConfigurationManager.AppSettings["CounterNo"].ToString();
        }

        public static string GetCounterNameByNo(string sNo)
        {
            string sResult = IUserContext.OnExecuteCommand_Xp("getCounter", new string[] { sNo });
            CounterInfo info = JsonConvert.DeserializeObject<CounterInfo>(sResult);

            if(info!=null)
            {
                return info.sCounterName;
            }
            return "";
        }

        public static string GetServiceNameByNo(string sNo)
        {
            string s = IUserContext.OnExecuteCommand_Xp("getService", new string[] { sNo });
            ServiceInfo info = JsonConvert.DeserializeObject<ServiceInfo>(s);

            if (info != null)
            {
                return info.sServiceName;
            }

            return "";
        }

        public static string GetStafferNameByNo(string sNo)
        {
            string s = IUserContext.OnExecuteCommand_Xp("getStaffByNo", new string[] { sNo });
            StafferInfo info = JsonConvert.DeserializeObject<StafferInfo>(s);

            if (info != null)
            {
                return info.sStafferName;
            }

            return "";
        }

        public static string GetParamValue(string sNo)
        {
            try
            {
                return IUserContext.OnExecuteCommand_Xp("getParamValue", new string[] { sNo, "Others" });
            }
            catch (Exception ex)
            {
                return "";
            }
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
