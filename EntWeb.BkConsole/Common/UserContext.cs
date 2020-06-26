using EntFrm.Framework.Utility;
using System;
using System.Text;
using System.Web;

namespace EntWeb.BkConsole
{
    public class UserContext
    {
        //public static string getLeftMenuByUserNoAndClassNo(string parentMenuId, string fsubMenuId)
        //{
        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();

        //        string sSuNo = ((LoginInfo)HttpContext.Current.Session["loginUser"]).UserNo;

        //        PermitInfoBLL infoBLL = new PermitInfoBLL(Common.PublicHelper.Get_ConnStr(), Common.PublicHelper.Get_AppCode());
        //        string rootMenuId = infoBLL.GetRecordByCodeNo(parentMenuId).sParentNo; 

        //        PermitInfoCollections lv0Permits = infoBLL.GetRecordsByUserNo(sSuNo, "00000000");
        //        PermitInfoCollections lv1Permits = null;

        //        if (lv0Permits != null && lv0Permits.Count > 0)
        //        {
        //            foreach (PermitInfo info0 in lv0Permits)
        //            {
        //                if (rootMenuId.Equals(info0.sPermitCode))
        //                {
        //                    sb.Append("<li class=\"start active open\">");
        //                }
        //                else
        //                {
        //                    sb.Append("<li>");
        //                }
        //                sb.Append("<a href=\"javascript:;\">");
        //                sb.Append("<i class=\"icon-home\"></i>");
        //                sb.Append("<span class=\"title\">" + info0.sPermitName + "</span>");
        //                sb.Append("<span class=\"selected\"></span>");
        //                sb.Append("<span class=\"arrow open\"></span>");
        //                sb.Append("</a>");

        //                lv1Permits = infoBLL.GetRecordsByUserNo(sSuNo, info0.sPermitCode);

        //                if (lv1Permits != null && lv1Permits.Count > 0)
        //                {
        //                    sb.Append("<ul class=\"sub-menu\">");

        //                    foreach (PermitInfo info1 in lv1Permits)
        //                    {
        //                        if (parentMenuId.Equals(info1.sPermitCode))
        //                        {
        //                            sb.Append("<li class=\"active\">");
        //                        }
        //                        else
        //                        {
        //                            sb.Append("<li>");
        //                        }
        //                        sb.Append("<a href=\"" + info1.sPFunction + "\">");
        //                        sb.Append("<i class=\"icon-bar-chart\"></i>");
        //                        sb.Append(info1.sPermitName);
        //                        sb.Append("</a>");
        //                        sb.Append("</li>");
        //                    }
        //                    sb.Append("</ul>");
        //                }

        //                sb.Append("</li>");
        //            }
        //        }


        //        return sb.ToString();
        //    }
        //    catch(Exception ex)
        //    {
        //        return "";
        //    }
        //}

        //public static string getTabMenuByUserNoAndClassNo(string parentMenuId, string fsubMenuId)
        //{
        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();

        //        string sSuNo = ((LoginInfo)HttpContext.Current.Session["loginUser"]).UserNo;

        //        PermitInfoBLL infoBLL = new PermitInfoBLL(Common.PublicHelper.Get_ConnStr(), Common.PublicHelper.Get_AppCode());
        //        PermitInfoCollections lv2Permits = infoBLL.GetRecordsByUserNo(sSuNo, parentMenuId);

        //        if (lv2Permits != null && lv2Permits.Count > 0)
        //        {
        //            foreach (PermitInfo info2 in lv2Permits)
        //            {
        //                if (fsubMenuId.Equals(info2.sPermitNo))
        //                { 
        //                    sb.Append("<li class=\"active\">");
        //                    sb.Append("<a href=\"#tab_1_1\" data-toggle=\"tab\">"+info2.sPermitName+"</a>");
        //                    sb.Append("</li>");
        //                }
        //                else
        //                {
        //                    sb.Append("<li>");
        //                    sb.Append("<a href=\"#\" data-toggle=\"tab\" onclick=\"goNavigUrl('"+info2.sPFunction+"')\">" + info2.sPermitName + "</a>");
        //                    sb.Append("</li>");
        //                }
        //            }
        //        }

        //        return sb.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        return "";
        //    }
        //}

        //public static bool SetParamValueByName(string sName, string sValue,string sType="2")
        //{
        //    if (sName.Length > 0 &&sValue.Length>0)
        //    {
        //        try
        //        {
        //            string sSuNo = ((LoginInfo)HttpContext.Current.Session["loginUser"]).UserNo;
        //            string sBranchNo = ((LoginInfo)HttpContext.Current.Session["loginUser"]).BranchNo;

        //            int count = 0;
        //            SysParams info = null;

        //            SysParamsBLL infoBLL=new SysParamsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
        //            SysParamsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 10, " KeyName='" + sName + "' And KeyType='"+sType+"' And BranchNo='" + sBranchNo + "' ");

        //            if (infoColl != null && infoColl.Count > 0)
        //            {
        //                info= infoColl.GetFirstOne();

        //                info.sKeyValue = sValue;
        //                info.sModOptor = sSuNo;
        //                info.dModDate = DateTime.Now;

        //                return infoBLL.UpdateRecord(info);
        //            }
        //            else
        //            {
        //                info = new SysParams();

        //                info.sParamNo = CommonHelper.Get_New12ByteGuid();
        //                info.sKeyName = sName;
        //                info.sKeyValue = sValue;
        //                info.sKeyType = sType;
        //                info.sBranchNo = sBranchNo;

        //                info.sAddOptor = sSuNo;
        //                info.dAddDate = DateTime.Now;
        //                info.sModOptor = sSuNo;
        //                info.dModDate = DateTime.Now;
        //                info.iValidityState = 1;
        //                info.sComments = "";

        //                info.sAppCode = PublicHelper.Get_AppCode() + ";";

        //                return infoBLL.AddNewRecord(info);
        //            } 
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message);
        //        }
        //    }
        //    return false;
        //}

        //public static string GetParamValueByName(string sName, string sType = "2",bool branchFlag=true)
        //{
        //    if (sName.Length > 0)
        //    {
        //        try
        //        {
        //            int count = 0;
        //            string sWhere = "";
        //            if(branchFlag)
        //            {
        //            string sBranchNo = ((LoginInfo)HttpContext.Current.Session["loginUser"]).BranchNo;
        //            sWhere = " KeyName='" + sName + "' And KeyType='" + sType + "' And BranchNo='" + sBranchNo + "' ";

        //            }
        //            else
        //            {
        //                sWhere=" KeyName='" + sName + "' And KeyType='" + sType + "' ";
        //            }

        //            SysParamsBLL infoBLL = new SysParamsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
        //            SysParamsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 10, sWhere);
        //            if (infoColl != null && infoColl.Count > 0)
        //            {
        //                return infoColl.GetFirstOne().sKeyValue;
        //            }

        //            return "";
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message);
        //        }
        //    }
        //    return "";
        //}
    }
}