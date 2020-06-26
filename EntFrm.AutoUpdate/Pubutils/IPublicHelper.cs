using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EntFrm.AutoUpdate
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

    }
}
