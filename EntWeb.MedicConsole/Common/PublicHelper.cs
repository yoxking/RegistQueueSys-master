using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntWeb.MedicConsole.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Configuration;

namespace EntWeb.MedicConsole.Common
{
    public class PublicHelper
    {

        public static string Get_AppCode()
        {
            return GetConfigValue("AppCode");
        }

        public static string Get_ConnStr()
        {
            string connStr = ConfigurationManager.AppSettings["SqlServer"].ToString();

            return EnconfigHelper.Decrypt(connStr);
        }

        public static string Get_BranchNo()
        {
            return GetConfigValue("BranchNo");
        } 

        public static string Get_AppName()
        {
            return GetConfigValue("AppName");
        }

        public static void SetConfigValue(string Name, string Value)
        {
            try
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
                var appSetting = (AppSettingsSection)config.GetSection("appSettings");
                if (appSetting.Settings[Name] == null) //如果不存在此节点，则添加   
                {
                    appSetting.Settings.Add(Name, Value);
                }
                else //如果存在此节点，则修改   
                {
                    appSetting.Settings[Name].Value = Value;
                }
                config.Save(ConfigurationSaveMode.Modified);
                config = null;
            }
            catch (Exception ex) { }
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


        public static string getBranchNameById(string Id)
        {
            BranchInfoBLL infoBLL = new BranchInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetRecordNameByNo(Id);
        }


        public  static string getCounterNameById(string Id)
        {
            CounterInfoBLL infoBLL = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetRecordNameByNo(Id);
        }

        public static bool SetParamValue(string sName, string sValue, string sType = "Others")
        {
            if (sName.Length > 0 && sValue.Length > 0)
            {
                try
                {
                    string sSuNo = "00000000";

                    int count = 100;
                    SysParams info = null;

                    SysParamsBLL infoBLL = new SysParamsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                    SysParamsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, " BranchNo='" + PublicHelper.Get_BranchNo() + "'  And  KeyName='" + sName + "' And KeyType='" + sType + "' ");

                    if (infoColl != null && infoColl.Count > 0)
                    {
                        info = infoColl.GetFirstOne();

                        info.sKeyValue = sValue;
                        info.sModOptor = sSuNo;
                        info.dModDate = DateTime.Now;

                        return infoBLL.UpdateRecord(info);
                    }
                    else
                    {
                        info = new SysParams();

                        info.sParamNo = CommonHelper.Get_New12ByteGuid();
                        info.sKeyName = sName;
                        info.sKeyValue = sValue;
                        info.sKeyType = sType;
                        info.sBranchNo = Get_BranchNo();

                        info.sAddOptor = sSuNo;
                        info.dAddDate = DateTime.Now;
                        info.sModOptor = sSuNo;
                        info.dModDate = DateTime.Now;
                        info.iValidityState = 1;
                        info.sComments = "";

                        info.sAppCode = Get_AppCode() + ";";

                        return infoBLL.AddNewRecord(info);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return false;
        }

        public static string GetParamValue(string sName, string sType = "Others")
        {
            if (sName.Length > 0)
            {
                try
                {
                    int count = 0;


                    SysParamsBLL infoBLL = new SysParamsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                    string sWhere = " BranchNo='" + Get_BranchNo() + "' And  KeyName='" + sName + "' And KeyType='" + sType + "' ";
                    SysParamsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, sWhere);
                    if (infoColl != null && infoColl.Count > 0)
                    {
                        return infoColl.GetFirstOne().sKeyValue;
                    }

                    return "";
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return "";
        }

        public static string GetRecipeState(int state)
        {
            string result = "未知";
            switch (state)
            {
                case PublicConsts.RECIPESTATE1: result = "等候中"; break;
                case PublicConsts.RECIPESTATE2: result = "配药中"; break;
                case PublicConsts.RECIPESTATE3: result = "已完成"; break; 
                default:break;
            }
            return result;
        }

        public static string GetProcessState(int state)
        {
            string result = "未知";
            switch (state)
            {
                case PublicConsts.RROCESSSTATE1: result = "等候中"; break;
                case PublicConsts.RROCESSSTATE2: result = "叫号中"; break;
                case PublicConsts.RROCESSSTATE3: result = "已过号"; break;
                case PublicConsts.RROCESSSTATE4: result = "已完成"; break;
                default: break;
            }
            return result;
        }


        public static List<TicketFlow> ConvertTicketFlows(ViewRecipeFlowsCollections infoColl)
        {

            if (infoColl != null && infoColl.Count > 0)
            {
                List<TicketFlow> tflowList = new List<TicketFlow>();
                TicketFlow tflow = null;

                foreach (ViewRecipeFlows info in infoColl)
                {
                    tflow = new TicketFlow();
                    tflow.RFlowNo = info.sRFlowNo;
                    tflow.RegistNo = info.sRegistNo;
                    tflow.TicketNo = info.sTicketNo;
                    tflow.RUserNo = info.sRUserNo;
                    tflow.RUserName = info.sCnName;
                    tflow.CounterNo = info.sCounterNo;
                    tflow.CounterName = info.sCounterNo;
                    tflow.DataFrom = info.sDataFrom;
                    tflow.RecipeState = GetRecipeState(info.iRecipeState);
                    tflow.ProcessState = GetProcessState(info.iProcessState);
                    tflow.EnqueueTime = info.dEnqueueTime;
                    tflow.ProcessedTime = info.dProcessedTime;

                    tflowList.Add(tflow);
                }

                return tflowList;
            }
            else
            {
                return null;
            }
        }

        public static List<RecipeData> ConvertRecipeData(RecipeDetailsCollections infoColl)
        {
            if (infoColl != null && infoColl.Count > 0)
            {
                List<RecipeData> rdataList = new List<RecipeData>();
                RecipeData rdata = null;

                foreach (RecipeDetails info in infoColl)
                {
                    rdata = new RecipeData();
                    rdata.RecipeNo = info.sRecipeNo;
                    rdata.RecipeName = info.sRecipeName;
                    rdata.RecipeSpec = info.sRecipeSpec; 
                    rdata.Standard = info.sStandard;
                    rdata.Price = info.dPrice;
                    rdata.Amount = info.dAmount;
                    rdata.SQuantity = info.sSQuantity;
                    rdata.TQuantity = info.sTQuantity;
                    rdata.Direction = info.sDirection;
                    rdata.Frequency = info.sFrequency;

                    rdataList.Add(rdata);
                }

                return rdataList;
            }
            else
            {
                return null;
            }
        }
    }
}