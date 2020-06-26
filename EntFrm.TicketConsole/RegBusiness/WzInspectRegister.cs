using EntFrm.Framework.Utility;
using EntFrm.TicketConsole.MPublicUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.TicketConsole.RegBusiness
{
    public class WzInspectRegister : IRegisterBusiness
    {
        private string registeMode = "RegisteFlows";
        private string serviceName = "";
        private string StafferName = "";

        public WzInspectRegister()
        {
            registeMode = IPublicHelper.GetConfigValue("RegisteMode");
            serviceName = IPublicHelper.GetConfigValue("ServiceName");
            StafferName = IPublicHelper.GetConfigValue("StafferName");
        }

        public string RegisterScanCode(string strCode)
        {
            string result = ""; 
            if (registeMode.Equals("RegisteFlows"))
            { 
                result = IUserContext.OnExecuteCommand_Xp("doRegistScanByRFlowNo", new string[] { strCode });
            }
            else
            {
                result = IUserContext.OnExecuteCommand_Xp("doRegistScanByRuserNo", new string[] { strCode, serviceName, StafferName });
            }

            return result;
        }
    }
}
