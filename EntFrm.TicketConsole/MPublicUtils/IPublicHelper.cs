using EntFrm.Business.Model;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public class IPublicHelper
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

        public static string Get_ServerIp()
        {
            return ConfigurationManager.AppSettings["ServerIp"].ToString();
        }

        public static string Get_WTcpPort()
        {
            return ConfigurationManager.AppSettings["WTcpPort"].ToString();
        }

        public static string Get_PrinterName()
        {
            return ConfigurationManager.AppSettings["PrinterName"].ToString();
        }

        public static string Get_Printer2Name()
        {
            return ConfigurationManager.AppSettings["Printer2Name"].ToString();
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

        public static CounterInfo GetCounterByNo(string sNo)
        {

            string s = IUserContext.OnExecuteCommand_Xp("getCounter", new string[] { sNo });
            CounterInfo info = JsonConvert.DeserializeObject<CounterInfo>(s);

            return info;
        }
        public static StafferInfo GetStafferByNo(string sNo)
        {

            string s = IUserContext.OnExecuteCommand_Xp("getStaffer", new string[] { sNo });
            StafferInfo info = JsonConvert.DeserializeObject<StafferInfo>(s);

            return info;
        }
        public static RUsersInfo GetRUserByNo(string sNo)
        {

            string s = IUserContext.OnExecuteCommand_Xp("getRuser", new string[] { sNo });
            RUsersInfo info = JsonConvert.DeserializeObject<RUsersInfo>(s);

            return info;
        }

        public static ServiceInfo GetServiceByNo(string sNo)
        {

            string s = IUserContext.OnExecuteCommand_Xp("getService", new string[] { sNo });
            ServiceInfo info = JsonConvert.DeserializeObject<ServiceInfo>(s);
             
            return info;
        }

        public static string ReplaceVariables(string sFormatStr, string sPFlowNo)
        {
            try
            {
                string waiterNum =  "0"; 

                string nextTicketNo = "";
                sFormatStr = sFormatStr.Replace("[", "");
                sFormatStr = sFormatStr.Replace("]", "");

                ViewTicketFlows vTicketFlow = JsonConvert.DeserializeObject<ViewTicketFlows>(IUserContext.OnExecuteCommand_Xp("getVTicketFlowByPFlowNo", new string[] { sPFlowNo }));
                if (vTicketFlow != null)
                {
                    CounterInfo counter = GetCounterByNo(vTicketFlow.sProcessedCounterNo);
                    StafferInfo staff = GetStafferByNo(vTicketFlow.sStafferNo);
                    RUsersInfo ruser = GetRUserByNo(vTicketFlow.sRUserNo);
                    ServiceInfo service = GetServiceByNo(vTicketFlow.sServiceNo);

                    if (counter != null)
                    {
                        sFormatStr = sFormatStr.Replace("CounterName", counter.sCounterName);
                        sFormatStr = sFormatStr.Replace("CounterAlias", counter.sCounterAlias);
                        nextTicketNo = IUserContext.OnExecuteCommand_Xp("getNextTicketNo", new string[] { counter.sCounterNo });

                        waiterNum = IUserContext.OnExecuteCommand_Xp("getQueuingCountByCounterNo", new string[] { counter.sCounterNo });
                        //allWaiterNum = IUserContext.OnExecuteCommand_Xp("getQueuingCountByCounterNo", new string[] { "" });
                        
                        sFormatStr = sFormatStr.Replace("CounterWaiterNumber", waiterNum);
                        //sFormatStr = sFormatStr.Replace("AllWaitingNumber", allWaiterNum);
                    }

                    if (staff != null)
                    {
                        sFormatStr = sFormatStr.Replace("StafferName", staff.sStafferName);
                        sFormatStr = sFormatStr.Replace("StaffRank", staff.sRanks); 

                        waiterNum = IUserContext.OnExecuteCommand_Xp("getQueuingCountByStafferNo", new string[] { staff.sStafferNo });
                        
                        sFormatStr = sFormatStr.Replace("StaffWaiterNumber", waiterNum);
                    }

                    if (service != null)
                    {
                        sFormatStr = sFormatStr.Replace("ServiceName", service.sServiceName);
                        sFormatStr = sFormatStr.Replace("ServiceAlias", service.sServiceAlias); 

                        waiterNum = IUserContext.OnExecuteCommand_Xp("getQueuingCountByServiceNo", new string[] { service.sServiceNo });
                        
                        sFormatStr = sFormatStr.Replace("ServiceWaiterNumber", waiterNum);
                    }

                    if (ruser != null)
                    {
                        sFormatStr = sFormatStr.Replace("FullName", ruser.sCnName);
                        sFormatStr = sFormatStr.Replace("UserSex", ruser.iSex==1?"男":"女");
                        sFormatStr = sFormatStr.Replace("IdNumber", ruser.sIdCardNo);
                        sFormatStr = sFormatStr.Replace("CardNumber", ruser.sIdCardNo);
                        sFormatStr = sFormatStr.Replace("Telephone", ruser.sTelphone);
                        sFormatStr = sFormatStr.Replace("Summary", ruser.sSummary);
                    }
                      
                    sFormatStr = sFormatStr.Replace("TicketNo", vTicketFlow.sTicketNo); 
                    sFormatStr = sFormatStr.Replace("yyyy-MM-dd-HH:mm:ss", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sFormatStr = sFormatStr.Replace("yyyy/MM/dd-HH:mm:ss", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    sFormatStr = sFormatStr.Replace("HH:mm:ss", DateTime.Now.ToString("HH:mm:ss"));
                    sFormatStr = sFormatStr.Replace("hh:mm:ss", DateTime.Now.ToString("hh:mm:ss"));

                }
                return sFormatStr;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string ReplaceVariables_Recipe(string sFormatStr, string sRFlowNo)
        {
            try
            {
                string waiterNum = "0";

                string nextTicketNo = "";
                sFormatStr = sFormatStr.Replace("[", "");
                sFormatStr = sFormatStr.Replace("]", "");
                sRFlowNo = sRFlowNo.Split(',')[0];

                ViewRecipeFlows vRecipeFlow = JsonConvert.DeserializeObject<ViewRecipeFlows>(IUserContext.OnExecuteCommand_Xp("getVRecipeFlowByRFlowNo", new string[] { sRFlowNo }));
                if (vRecipeFlow != null)
                {
                    CounterInfo counter = GetCounterByNo(vRecipeFlow.sCounterNo);

                    if (counter != null)
                    {
                        sFormatStr = sFormatStr.Replace("CounterName", counter.sCounterName);
                        sFormatStr = sFormatStr.Replace("CounterAlias", counter.sCounterAlias);
                        //nextTicketNo = IUserContext.OnExecuteCommand_Xp("getNextTicketNo", new string[] { counter.sCounterNo });

                        //waiterNum = IUserContext.OnExecuteCommand_Xp("getQueuingCountByCounterNo", new string[] { counter.sCounterNo });
                        ////allWaiterNum = IUserContext.OnExecuteCommand_Xp("getQueuingCountByCounterNo", new string[] { "" });

                        //sFormatStr = sFormatStr.Replace("CounterWaiterNumber", waiterNum);
                        //sFormatStr = sFormatStr.Replace("AllWaitingNumber", allWaiterNum);
                    }

                    sFormatStr = sFormatStr.Replace("FullName", vRecipeFlow.sCnName);
                    sFormatStr = sFormatStr.Replace("UserSex", vRecipeFlow.iSex == 1 ? "男" : "女");
                    sFormatStr = sFormatStr.Replace("IdNumber", vRecipeFlow.sIdCardNo);
                    sFormatStr = sFormatStr.Replace("CardNumber", vRecipeFlow.sIdCardNo);
                    sFormatStr = sFormatStr.Replace("Telephone", vRecipeFlow.sTelphone);
                    sFormatStr = sFormatStr.Replace("Summary", vRecipeFlow.sSummary);


                    sFormatStr = sFormatStr.Replace("TicketNo", vRecipeFlow.sTicketNo);
                    sFormatStr = sFormatStr.Replace("yyyy-MM-dd-HH:mm:ss", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sFormatStr = sFormatStr.Replace("yyyy/MM/dd-HH:mm:ss", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    sFormatStr = sFormatStr.Replace("HH:mm:ss", DateTime.Now.ToString("HH:mm:ss"));
                    sFormatStr = sFormatStr.Replace("hh:mm:ss", DateTime.Now.ToString("hh:mm:ss"));

                }
                return sFormatStr;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
