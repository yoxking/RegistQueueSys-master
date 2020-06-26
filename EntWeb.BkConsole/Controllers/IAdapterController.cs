using DotNetty.Common.Utilities;
using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntWeb.BkConsole.Common;
using EntWeb.BkConsole.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Controllers
{
    public class IAdapterController : Controller
    {
        /**
         * 
         * 
         * 医生信息：index=0
         * 窗口屏信息
         *     叫号信息（calling）：index=1
         *     等候信息（waiting）: index=2
         *     排队信息（queuing）：index=3
         *     过号信息（noarrival）: index=4
         * 
         * 综合屏信息
         *     叫号信息（calling）：index=6
         *     等候信息（waiting）: index=7
         *     排队信息（queuing）：index=8
         *     处理信息（processing）: index=9
         *     
         **/
        private string getMaskName(string name,string maskFlag)
        {
            string realName = "";

            if (name.IndexOf('(') <0)
            {
                realName = name.Trim();
                if (maskFlag.Equals("1") && realName.Length > 1)
                {
                    realName = realName.Substring(0, 1) + "*" + realName.Substring(realName.Length - 1, 1);
                }

                return realName;
            }
            else
            {
                realName = name.Trim().Substring(0, name.IndexOf('('));
                string extension = name.Trim().Substring(name.IndexOf('('));
                if (maskFlag.Equals("1") && realName.Length > 1)
                {
                    realName = realName.Substring(0, 1) + "*" + realName.Substring(realName.Length - 1, 1);
                }

                return realName + extension;
            }
        }

        #region 基本信息
        public string getCurrentDatetime()
        {
            CodeData codeData = new CodeData();
            codeData.code = "200";
            codeData.msg = "success";
            codeData.data = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");

            return JsonConvert.SerializeObject(codeData);
        }
        public string getCurrentDatetime2()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString("yyyy年MM月dd日<br />"));
            sb.Append(CalendarHelper.GetCNDayOfWeek(DateTime.Now));
            sb.Append(DateTime.Now.ToString(" HH:mm:ss"));

            CodeData codeData = new CodeData();
            codeData.code = "200";
            codeData.msg = "success";
            codeData.data = sb.ToString();

            return JsonConvert.SerializeObject(codeData);
        }

        public string getHospitalInfo(string id)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";

            try
            {
                StringBuilder sb = new StringBuilder();

                ContentInfoBLL infoBoss = new ContentInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ContentInfo info = infoBoss.GetRecordByNo(id);

                if (info != null)
                {
                    sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                    //sb.Append("<tr>");
                    //sb.Append("<td colspan=\"2\"> ");
                    //sb.Append("<img src=\""+ info.sPostPicture+ "\" /> ");
                    //sb.Append("</td>");
                    //sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append(info.sTitle);
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td id=\"HInto\">");
                    sb.Append(info.sNContent);
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                }
                else
                {
                    sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                    //sb.Append("<tr>");
                    //sb.Append("<td colspan=\"2\"> ");
                    //sb.Append("<img src=\"nopic.jpg\" /> ");
                    //sb.Append("</td>");
                    //sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("医院简介");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td id=\"HInto\">");
                    sb.Append("无");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                }
                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getHospitalInfo2(string id)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";

            try
            {
                StringBuilder sb = new StringBuilder();

                ContentInfoBLL infoBoss = new ContentInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ContentInfo info = infoBoss.GetRecordByNo(id);

                if (info != null)
                { 
                    sb.Append(info.sNContent); 
                }
                else
                { 
                    sb.Append("&nbsp;"); 
                }
                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
         
        public string getHospitalImage(string id)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";

            try
            {
                StringBuilder sb = new StringBuilder();

                ContentInfoBLL infoBoss = new ContentInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ContentInfo info = infoBoss.GetRecordByNo(id);

                if (info != null)
                {
                    sb.Append("<img src=\""+ info.sPostPicture + "\" />"); 
                }
                else
                {
                    sb.Append("<img src=\"Hospital.jpg\" />");
                }
                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
         
        public string getBranchNameByBranchId(string branchNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";

            try
            {

                //DateTime dt = DateTime.Parse("2019-09-20");
                //int day = (dt - DateTime.Now).Days;

                //if (day > 0)
                //{
                //    BranchInfoBLL counterBoss = new BranchInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                //    codeData.data = counterBoss.GetRecordNameByNo(branchNo) + "(试用版，"+day+"天后到期！)";
                //}
                //else
                //{
                //    codeData.data =  "(试用已到期，请注册正式版本!)";
                //}

                BranchInfoBLL counterBoss = new BranchInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                codeData.data = counterBoss.GetRecordNameByNo(branchNo);


                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getCounterNameByCounterId(string branchNo, string counterNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";

            try
            {
                CounterInfoBLL counterBoss = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                codeData.data = counterBoss.GetRecordNameByNo(counterNo);

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        #endregion

        #region 医生信息

        public string getStaffInfoByCounterId(string branchNo, string counterNo)
        {
            return getStafferInfoByCounterId(branchNo, counterNo);
        }

        public string getStaffInfoByCounterId2(string branchNo, string counterNo)
        {
            return getStafferInfoByCounterId2(branchNo, counterNo);
        }

        public string getStaffInfoByCounterId3(string branchNo, string counterNo)
        {
            return getStafferInfoByCounterId3(branchNo, counterNo);
        }

        public string getStaffInfoByCounterId4(string branchNo, string counterNo)
        {
            return getStafferInfoByCounterId4(branchNo, counterNo);
        }

        public string getStafferInfoByCounterId(string branchNo, string counterNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                StringBuilder sb = new StringBuilder();

                //获取登录窗口的医生/员工编号
                string stafferNo = AdapterUtil.GetStafferNoByCounterNo(counterNo);

                ////判断是否有更新内容，
                //if (PublicHelper.GetStateValue(branchNo, stafferNo, 1).Equals("0"))
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}
                ////更新标记
                //PublicHelper.SetStateValue(branchNo, stafferNo, "0", 1);

                StafferInfoBLL staffBoss = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferInfo staff = staffBoss.GetRecordByNo(stafferNo);

                if (staff != null)
                {
                    sb.Append("<div class=\"title1\">医生姓名 : &nbsp;&nbsp;" + staff.sStafferName + "</div> <br />");
                    sb.Append("<div class=\"content1\">");
                    sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                    sb.Append("<tr>");
                    sb.Append("<td valign=\"top\" align=\"left\">");
                    sb.Append("<img src=\"" + staff.sHeadPhoto + "\" alt=\"" + staff.sStafferName + "\" /><br />");
                    sb.Append("<span>职称:" + staff.sRanks + "</span><br />");
                    sb.Append("<span>职务:" + staff.sComments + "</span>");
                    sb.Append("</td>");
                    sb.Append("<td valign=\"top\">");
                    sb.Append("<p align=\"left\">" + AdapterUtil.SubStringEx(staff.sSummary, 120) + "</p>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");
                }
                else
                {
                    sb.Append("<div class=\"title1\">医生姓名 : &nbsp;&nbsp;</div> <br />");
                    sb.Append("<div class=\"content1\">");
                    sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("<img src=\"nopic.jpg\" alt=\"无\" />");
                    sb.Append("<span>职称:</span>");
                    sb.Append("<span>职务:</span>");
                    sb.Append("</td>");
                    sb.Append("<td valign=\"top\">");
                    sb.Append("<p align=\"left\">暂无</p>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getStafferInfoByCounterId2(string branchNo, string counterNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                StringBuilder sb = new StringBuilder();

                //获取登录窗口的医生/员工编号
                string stafferNo = AdapterUtil.GetStafferNoByCounterNo(counterNo);

                ////判断是否有更新内容，
                //if (PublicHelper.GetStateValue(branchNo, stafferNo, 1).Equals("0"))
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}
                ////更新标记
                //PublicHelper.SetStateValue(branchNo, stafferNo, "0", 1);

                StafferInfoBLL staffBoss = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferInfo staff = staffBoss.GetRecordByNo(stafferNo);

                if (staff != null)
                {
                    sb.Append("<div class=\"title1\">医师 : &nbsp;&nbsp;" + staff.sStafferName + "</div> <br />");
                    sb.Append("<div class=\"content1\">");
                    sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                    sb.Append("<tr>");
                    sb.Append("<td valign=\"top\">");
                    sb.Append("<img src=\"" + staff.sHeadPhoto + "\" alt=\"" + staff.sStafferName + "\" />");
                    sb.Append("<span>" + staff.sRanks + "</span>");
                    sb.Append("</td>");
                    sb.Append("<td valign=\"top\">");
                    sb.Append("<marquee  behavior=\"scroll\" direction=\"up\" height=\"300\" scrollamount=\"2\">" + staff.sSummary + "</marquee>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");
                }
                else
                {
                    sb.Append("<div class=\"title1\">医师 : &nbsp;&nbsp;无</div> <br />");
                    sb.Append("<div class=\"content1\">");
                    sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("<img src=\"nopic.jpg\" alt=\"无\" />");
                    sb.Append("<span>&nbsp;</span>");
                    sb.Append("</td>");
                    sb.Append("<td valign=\"top\">");
                    sb.Append("<p align=\"left\">暂无</p>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getStafferInfoByCounterId3(string branchNo, string counterNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                StringBuilder sb = new StringBuilder();

                //获取登录窗口的医生/员工编号
                string stafferNo = AdapterUtil.GetStafferNoByCounterNo(counterNo);

                //判断是否有更新内容，
                //if (PublicHelper.GetStateValue(branchNo, stafferNo, 1).Equals("0"))
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}
                ////更新标记
                //PublicHelper.SetStateValue(branchNo, stafferNo, "0", 1);

                StafferInfoBLL staffBoss = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferInfo staff = staffBoss.GetRecordByNo(stafferNo);

                if (staff != null)
                {
                    sb.Append("<div class=\"title1\">护士 : &nbsp;&nbsp;" + staff.sStafferName + "</div> <br />");
                    sb.Append("<div class=\"content1\">");
                    sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                    sb.Append("<tr>");
                    sb.Append("<td valign=\"top\">");
                    sb.Append("<img src=\"" + staff.sHeadPhoto + "\" alt=\"" + staff.sStafferName + "\" />");
                    sb.Append("<span>" + staff.sRanks + "</span>");
                    sb.Append("</td>");
                    sb.Append("<td valign=\"top\">");
                    sb.Append("<marquee  behavior=\"scroll\" direction=\"up\" height=\"300\" scrollamount=\"2\">" + staff.sSummary + "</marquee>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");
                }
                else
                {
                    sb.Append("<div class=\"title1\">护士 : &nbsp;&nbsp;无</div> <br />");
                    sb.Append("<div class=\"content1\">");
                    sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("<img src=\"nopic.jpg\" alt=\"无\" />");
                    sb.Append("<span>&nbsp;</span>");
                    sb.Append("</td>");
                    sb.Append("<td valign=\"top\">");
                    sb.Append("<p align=\"left\">暂无</p>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getStafferInfoByCounterId4(string branchNo, string counterNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                StringBuilder sb = new StringBuilder();

                //获取登录窗口的医生/员工编号
                string staffNo = AdapterUtil.GetStafferNoByCounterNo(counterNo); 

                StafferInfoBLL staffBoss = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferInfo staff = staffBoss.GetRecordByNo(staffNo);

                if (staff != null)
                {
                    sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                    sb.Append("<tr>");
                    sb.Append("<td class=\"doctorImg\">");
                    sb.Append("<img src=\"" + staff.sHeadPhoto + "\" alt=\"staff photo\" />");
                    sb.Append("</td>");
                    sb.Append("<td class=\"doctorInfo\">");
                    sb.Append("<div class=\"title\">");
                    sb.Append("医生");
                    sb.Append("</div>");
                    sb.Append("<div class=\"doctorName\">" + staff.sStafferName + "</div>");
                    sb.Append("<div class=\"title\">");
                    sb.Append("职称");
                    sb.Append("</div>");
                    sb.Append("<div class=\"doctorTitle\">" + staff.sRanks + "</div>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                }
                else
                {
                    sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                    sb.Append("<tr>");
                    sb.Append("<td class=\"doctorImg\">");
                    sb.Append("<img src=\"nopic.jpg\" alt=\"staff photo\" />");
                    sb.Append("</td>");
                    sb.Append("<td class=\"doctorInfo\">");
                    sb.Append("<div class=\"title\">");
                    sb.Append("医生");
                    sb.Append("</div>");
                    sb.Append("<div class=\"doctorName\">&nbsp;</div>");
                    sb.Append("<div class=\"title\">");
                    sb.Append("职称");
                    sb.Append("</div>");
                    sb.Append("<div class=\"doctorTitle\">&nbsp;</div>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getStafferInfoByCounterId5(string branchNo, string counterNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                StringBuilder sb = new StringBuilder();

                //获取登录窗口的医生/员工编号
                string stafferNo = AdapterUtil.GetStafferNoByCounterNo(counterNo);

                //判断是否有更新内容，
                //if (PublicHelper.GetStateValue(branchNo, stafferNo, 1).Equals("0"))
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}
                ////更新标记
                //PublicHelper.SetStateValue(branchNo, stafferNo, "0", 1);

                StafferInfoBLL staffBoss = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferInfo staff = staffBoss.GetRecordByNo(stafferNo);

                if (staff != null)
                {
                    sb.Append("<div id=\"dInfo\">");
                    //sb.Append("d");
                    sb.Append("<img src=\"" + staff.sHeadPhoto + "\" >");
                    sb.Append("<div id=\"dName\">");
                    sb.Append(staff.sStafferName);
                    sb.Append("</div>");
                    sb.Append("<div id=\"dTitle\">");
                    sb.Append(staff.sRanks);
                    sb.Append("</div>");
                    sb.Append("</div>");

                    sb.Append("<div id=\"dIntro\">");
                    sb.Append(staff.sSummary);
                    sb.Append("</div>"); 
                }
                else
                {
                    sb.Append("<div id=\"dInfo\">");
                    sb.Append("<img src=\"nopic.jpg\" />");
                    sb.Append("<div id=\"dName\">");
                    sb.Append("&nbsp;");
                    sb.Append("</div>");
                    sb.Append("<div id=\"dTitle\">");
                    sb.Append("&nbsp;");
                    sb.Append("</div>");
                    sb.Append("</div>");

                    sb.Append("<div id=\"dIntro\">");
                    sb.Append("暂无医生就诊");
                    sb.Append("</div>");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getStafferInfoByCounterId6(string branchNo, string counterNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                StringBuilder sb = new StringBuilder();

                //获取登录窗口的医生/员工编号
                string stafferNo = AdapterUtil.GetStafferNoByCounterNo(counterNo); 

                StafferInfoBLL staffBoss = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferInfo staff = staffBoss.GetRecordByNo(stafferNo);

                if (staff != null)
                {
                    sb.Append("<div id=\"dTitle\">");
                    sb.Append(staff.sRanks);
                    sb.Append("</div>");

                    sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("<div id=\"dImg\">");
                    sb.Append("<img src=\"" + staff.sHeadPhoto + "\" />");
                    sb.Append("</div>");
                    sb.Append("</td>");
                    sb.Append("<td>");
                    sb.Append("<div id=\"dIntro\">");
                    sb.Append(staff.sSummary);
                    sb.Append("</div>");
                    sb.Append("<div id=\"docotor\">");
                    sb.Append("医生");
                    sb.Append("</div>");
                    sb.Append("<div id=\"dname\">");
                    sb.Append(staff.sStafferName);
                    sb.Append("</div>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>"); 
                }
                else
                {
                    sb.Append("<div id=\"dTitle\">");
                    sb.Append("主任医师");
                    sb.Append("</div>");

                    sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("<div id=\"dImg\">");
                    sb.Append("<img src=\"nopic.jpg\" />");
                    sb.Append("</div>");
                    sb.Append("</td>");
                    sb.Append("<td>");
                    sb.Append("<div id=\"dIntro\">");
                    sb.Append("无"); 
                    sb.Append("</div>");
                    sb.Append("<div id=\"docotor\">");
                    sb.Append("医生");
                    sb.Append("</div>");
                    sb.Append("<div id=\"dname\">");
                    sb.Append("&nbsp;");
                    sb.Append("</div>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }


        public string getStafferInfoByCounterId7(string branchNo, string counterNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                StringBuilder sb = new StringBuilder();

                //获取登录窗口的医生/员工编号
                string stafferNo = AdapterUtil.GetStafferNoByCounterNo(counterNo); 

                StafferInfoBLL staffBoss = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferInfo staff = staffBoss.GetRecordByNo(stafferNo);

                if (staff != null)
                {
                    sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                    sb.Append("<tr>");
                    sb.Append("<td width=\"45%\">");
                    sb.Append("<img src=\"" + staff.sHeadPhoto + "\" alt=\"staff photo\" />");
                    sb.Append("</td>"); 
                    sb.Append("<td>");
                    sb.Append("<span>" + staff.sStafferName + "</span><br />");
                    sb.Append("<small>" + staff.sRanks + "</small>"); 
                    sb.Append("<p>" + AdapterUtil.SubStringEx(staff.sSummary, 120) + "</p> ");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                }
                else
                {
                    sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                    sb.Append("<tr>");
                    sb.Append("<td width=\"45%\">");
                    sb.Append("<img src=\"nopic.jpg\" alt=\"staff photo\" />");
                    sb.Append("</td>"); 
                    sb.Append("<td>"); 
                    sb.Append("<p>&nbsp;</p> ");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        #endregion

        #region 表格列表
        //窗口屏-
        public string getCallingTableByCounterId(string branchNo, string counterNos, string countNum)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = int.Parse(countNum);
                string counterNo = counterNos.Split(',')[0];
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder(); 

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                ViewTicketFlowsCollections ticketColl = null;
                ViewTicketFlows ticket = null;

                //获取登录窗口的医生/员工编号
                string sStafferNo = AdapterUtil.GetStafferIdByCounterId(counterNo);

                string sWhere = " DataFlag=0 And  BranchNo = '" + branchNo + "' And StafferNo='" + sStafferNo + "' And  EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                //呼叫人员
                int j = 0;
                ticketColl = ticketBoss.GetRecordsByPaging(ref j, 1, 1, sWhere + " And ProcessState=" + PublicConsts.PROCSTATE_CALLING);
                if (ticketColl != null && ticketColl.Count > 0)
                {
                    ticket = ticketColl.GetFirstOne();
                    sb.Append("<tr style=\"background-color:darkorange;color:white;\">");
                    sb.Append("<td width=\"300\">接诊</td>");
                    sb.Append("<td>" + ticket.sTicketNo + " " + getMaskName(ticket.sCnName,maskFlag) + "</td>");
                    sb.Append("</tr>");
                }
                else
                {
                    sb.Append("<tr style=\"background-color:darkorange;color:white;\">");
                    sb.Append("<td width=\"300\">接诊</td>");
                    sb.Append("<td>&nbsp;</td>");
                    sb.Append("</tr>");
                }

                //等候人员
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = count;
                s_model.sFields = "*";
                s_model.sCondition = sWhere + "And ProcessState Between " + PublicConsts.PROCSTATE_WAITING + " And " + PublicConsts.PROCSTATE_WAITAREA9 + " ";
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                if (ticketColl != null && ticketColl.Count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (i < ticketColl.Count)
                        {
                            ticket = ticketColl[i];
                        }
                        else
                        {
                            ticket = null;
                        }

                        if (ticket != null)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td>候诊" + (i + 1) + "</td>");
                            //StringHelper.DesensitizeStr(getMaskName(ticket.sCnName,maskFlag),1,0,'*')
                            sb.Append("<td style='overflow:hidden;'>" + ticket.sTicketNo + " " + getMaskName(ticket.sCnName,maskFlag));

                            if (ticket.iPriorityType > PublicConsts.PRIORITY_TYPE0 && ticket.iPriorityType < PublicConsts.PRIORITY_TYPE6)
                            {
                                sb.Append("(" + PublicHelper.GetPriorityType(ticket.iPriorityType) + ")");
                            }
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }
                        else
                        {
                            sb.Append("<tr>");
                            sb.Append("<td>候诊" + (i + 1) + "</td>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("</tr>");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>候诊" + (i + 1) + "</td>");
                        sb.Append("<td>&nbsp;</td>");
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        //综合屏
        public string getWaitingTableByCounterIds(string branchNo, string counterNos, string pageIndex, string pageSize)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                //string workingMode = IUserContext.GetParamValue(branchNo, PublicConsts.DEF_WORKINGMODE, "Others");
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] counterArr = counterNos.Split(',');
                string stafferNos = "", stafferNo = "";
                string stemp = ""; 

                foreach (string counter in counterArr)
                {
                    stemp = AdapterUtil.GetStafferIdByCounterId(counter);
                    if (!string.IsNullOrEmpty(stemp))
                    {
                        stafferNos += stemp + ",";
                    }
                }

                stafferNos = stafferNos.Trim(',');

                stafferNos = stafferNos.Replace(",", "','");

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_WAITING + " And " + PublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 1000;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                int tableIndex = int.Parse(pageIndex);
                int tableSize = int.Parse(pageSize);
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;
                ViewTicketFlows tempFlows = null;

                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                sb.Append("<tr style=\"background-color:darkorange;color:white;\">");
                sb.Append("<td>序号</td>");
                foreach (string counter in counterArr)
                {
                    sb.Append("<td>" + AdapterUtil.GetCounterNameById(counter) + "</td>");
                }
                sb.Append("</tr>");
                if (ticketList != null && ticketList.Count > 0)
                {
                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string counter in counterArr)
                        {
                            stafferNo = AdapterUtil.GetStafferIdByCounterId(counter);

                            tempList = ticketList.FindAll(p => p.sStafferNo.Equals(stafferNo));
                            if (tempList != null && tempList.Count > i)
                            {
                                tempFlows = tempList[i];
                                if (tempFlows.iProcessState == PublicConsts.PROCSTATE_CALLING)
                                {
                                    sb.Append("<td style=\"color:red;\">" + tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag) + "</td>");
                                }
                                else
                                {
                                    //sb.Append("<td>" + getMaskName(tempFlows.sCnName,maskFlag) + "</td>");
                                    sb.Append("<td>" + tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag));

                                    if (tempFlows.iPriorityType > PublicConsts.PRIORITY_TYPE0 && tempFlows.iPriorityType < PublicConsts.PRIORITY_TYPE6)
                                    {
                                        sb.Append("(" + PublicHelper.GetPriorityType(tempFlows.iPriorityType) + ")");
                                    }
                                    sb.Append("</td>");
                                }
                            }
                            else
                            {
                                sb.Append("<td>&nbsp;</td>");
                            }
                        }
                        sb.Append("</tr>");
                    }
                }
                else
                {
                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string counter in counterArr)
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getWaitingTableByStafferIds(string branchNo, string stafferNos, string pageIndex, string pageSize)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                //string workingMode = IUserContext.GetParamValue(branchNo, PublicConsts.DEF_WORKINGMODE, "Others");
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] staffArr = stafferNos.Split(',');
                stafferNos = stafferNos.Replace(",", "','"); 

                string sWhere = " DataFlag=0 And  BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_WAITING + " And " + PublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 1000;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                int tableIndex = int.Parse(pageIndex);
                int tableSize = int.Parse(pageSize);
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;
                ViewTicketFlows tempFlows = null;

                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                sb.Append("<tr style=\"background-color:darkorange;color:white;\">");
                sb.Append("<td>序号</td>");
                foreach (string stafferNo in staffArr)
                {
                    sb.Append("<td>" + AdapterUtil.GetStafferNameById(stafferNo) + "</td>");
                }
                sb.Append("</tr>");
                if (ticketList != null && ticketList.Count > 0)
                {
                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string stafferNo in staffArr)
                        {
                            tempList = ticketList.FindAll(p => p.sStafferNo.Equals(stafferNo));
                            if (tempList != null && tempList.Count > i)
                            {
                                tempFlows = tempList[i];
                                if (tempFlows.iProcessState == PublicConsts.PROCSTATE_CALLING)
                                {
                                    sb.Append("<td style=\"color:red;\">" + tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag) + "</td>");
                                }
                                else
                                {
                                    //sb.Append("<td>"  + getMaskName(tempFlows.sCnName,maskFlag) + "</td>");
                                    sb.Append("<td>" + tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag));

                                    if (tempFlows.iPriorityType > PublicConsts.PRIORITY_TYPE0 && tempFlows.iPriorityType < PublicConsts.PRIORITY_TYPE6)
                                    {
                                        sb.Append("(" + PublicHelper.GetPriorityType(tempFlows.iPriorityType) + ")");
                                    }
                                    sb.Append("</td>");
                                }
                            }
                            else
                            {
                                sb.Append("<td>&nbsp;</td>");
                            }
                        }
                        sb.Append("</tr>");
                    }
                }
                else
                {
                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string stafferNo in staffArr)
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        sb.Append("</tr>");
                    }
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getWaitingTableByServiceIds(string branchNo, string serviceNos, string pageIndex, string pageSize)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                //string workingMode = IUserContext.GetParamValue(branchNo, PublicConsts.DEF_WORKINGMODE, "Others");
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] serviceArr = serviceNos.Split(',');
                serviceNos = serviceNos.Replace(",", "','");
                 

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And ServiceNo In ('" + serviceNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_WAITING + " And " + PublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 1000;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                int tableIndex = int.Parse(pageIndex);
                int tableSize = int.Parse(pageSize);
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;
                ViewTicketFlows tempFlows = null;

                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                sb.Append("<tr style=\"background-color:darkorange;color:white;\">");
                sb.Append("<td>序号</td>");
                foreach (string serviceNo in serviceArr)
                {
                    sb.Append("<td>" + AdapterUtil.GetServiceNameByNo(serviceNo) + "</td>");
                }
                sb.Append("</tr>");
                if (ticketList != null && ticketList.Count > 0)
                {
                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string serviceNo in serviceArr)
                        {
                            tempList = ticketList.FindAll(p => p.sServiceNo.Equals(serviceNo));
                            if (tempList != null && tempList.Count > i)
                            {
                                tempFlows = tempList[i];
                                if (tempFlows.iProcessState == PublicConsts.PROCSTATE_CALLING)
                                {
                                    sb.Append("<td style=\"color:red;\">" + tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag) + "</td>");
                                }
                                else
                                {
                                    //sb.Append("<td>" + getMaskName(tempFlows.sCnName,maskFlag) + "</td>");
                                    sb.Append("<td>" + tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag));

                                    if (tempFlows.iPriorityType > PublicConsts.PRIORITY_TYPE0 && tempFlows.iPriorityType < PublicConsts.PRIORITY_TYPE6)
                                    {
                                        sb.Append("(" + PublicHelper.GetPriorityType(tempFlows.iPriorityType) + ")");
                                    }
                                    sb.Append("</td>");
                                }
                            }
                            else
                            {
                                sb.Append("<td>&nbsp;</td>");
                            }
                        }
                        sb.Append("</tr>");
                    }
                }
                else
                {
                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string serviceNo in serviceArr)
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getQueuingTableByCounterIds(string branchNo, string counterNos, string pageIndex, string pageSize)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                //string workingMode = IUserContext.GetParamValue(branchNo, PublicConsts.DEF_WORKINGMODE, "Others");
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] counterArr = counterNos.Split(',');
                 

                string stafferNos = "", stafferNo = "";
                string stemp = "";
                foreach (string counter in counterArr)
                {
                    stemp = AdapterUtil.GetStafferIdByCounterId(counter);
                    if (!string.IsNullOrEmpty(stemp))
                    {
                        stafferNos += stemp + ",";
                    }
                }

                stafferNos = stafferNos.Trim(',');

                stafferNos = stafferNos.Replace(",", "','");

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_WAITING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 1000;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                int tableIndex = int.Parse(pageIndex);
                int tableSize = int.Parse(pageSize);
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;
                ViewTicketFlows tempFlows = null;

                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                sb.Append("<tr style=\"background-color:darkorange;color:white;\">");
                sb.Append("<td>序号</td>");
                foreach (string counter in counterArr)
                {
                    sb.Append("<td>" + AdapterUtil.GetCounterNameById(counter) + "</td>");
                }
                sb.Append("</tr>");
                if (ticketList != null && ticketList.Count > 0)
                {

                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string counter in counterArr)
                        {
                            stafferNo = AdapterUtil.GetStafferIdByCounterId(counter);

                            tempList = ticketList.FindAll(p => p.sStafferNo.Equals(stafferNo));
                            if (tempList != null && tempList.Count > i)
                            {
                                tempFlows = tempList[i];
                                //sb.Append("<td>" + getMaskName(tempFlows.sCnName,maskFlag) + "</td>");
                                sb.Append("<td>" + tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag));

                                if (tempFlows.iPriorityType > PublicConsts.PRIORITY_TYPE0 && tempFlows.iPriorityType < PublicConsts.PRIORITY_TYPE6)
                                {
                                    sb.Append("(" + PublicHelper.GetPriorityType(tempFlows.iPriorityType) + ")");
                                }
                                sb.Append("</td>");
                            }
                            else
                            {
                                sb.Append("<td>&nbsp;</td>");
                            }
                        }
                        sb.Append("</tr>");
                    }
                }
                else
                {
                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string counter in counterArr)
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getQueuingTableByStafferIds(string branchNo, string stafferNos, string pageIndex, string pageSize)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                //string workingMode = IUserContext.GetParamValue(branchNo, PublicConsts.DEF_WORKINGMODE, "Others");
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] staffArr = stafferNos.Split(',');
                stafferNos = stafferNos.Replace(",", "','");
                ////判断综合屏状态
                //int index = 0;
                //foreach (string stafferNo in staffArr)
                //{
                //    if (PublicHelper.GetStateValue(branchNo, stafferNo, 8).Equals("1"))
                //    {
                //        PublicHelper.SetStateValue(branchNo, stafferNo, "0", 8);
                //        index++;
                //    }
                //}

                //if (index == 0)
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_WAITING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 1000;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                int tableIndex = int.Parse(pageIndex);
                int tableSize = int.Parse(pageSize);
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;
                ViewTicketFlows tempFlows = null;

                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                sb.Append("<tr style=\"background-color:darkorange;color:white;\">");
                sb.Append("<td>序号</td>");
                foreach (string stafferNo in staffArr)
                {
                    sb.Append("<td>" + AdapterUtil.GetStafferNameById(stafferNo) + "</td>");
                }
                sb.Append("</tr>");
                if (ticketList != null && ticketList.Count > 0)
                {

                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string stafferNo in staffArr)
                        {
                            tempList = ticketList.FindAll(p => p.sStafferNo.Equals(stafferNo));
                            if (tempList != null && tempList.Count > i)
                            {
                                tempFlows = tempList[i];
                                //sb.Append("<td>" + getMaskName(tempFlows.sCnName,maskFlag) + "</td>");
                                sb.Append("<td>" + tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag));

                                if (tempFlows.iPriorityType > PublicConsts.PRIORITY_TYPE0 && tempFlows.iPriorityType < PublicConsts.PRIORITY_TYPE6)
                                {
                                    sb.Append("(" + PublicHelper.GetPriorityType(tempFlows.iPriorityType) + ")");
                                }
                                sb.Append("</td>");
                            }
                            else
                            {
                                sb.Append("<td>&nbsp;</td>");
                            }
                        }
                        sb.Append("</tr>");
                    }
                }
                else
                {
                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string stafferNo in staffArr)
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getQueuingTableByServiceIds(string branchNo, string serviceNos, string pageIndex, string pageSize)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                //string workingMode = IUserContext.GetParamValue(branchNo, PublicConsts.DEF_WORKINGMODE, "Others");
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] serviceArr = serviceNos.Split(',');
                serviceNos = serviceNos.Replace(",", "','");


                ////判断综合屏状态
                //int index = 0;
                //foreach (string serviceNo in serviceArr)
                //{
                //    if (PublicHelper.GetStateValue(branchNo, serviceNo, 8).Equals("1"))
                //    {
                //        PublicHelper.SetStateValue(branchNo, serviceNo, "0", 8);
                //        index++;
                //    }
                //}

                //if (index == 0)
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And ServiceNo In ('" + serviceNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_WAITING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 1000;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                int tableIndex = int.Parse(pageIndex);
                int tableSize = int.Parse(pageSize);
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;
                ViewTicketFlows tempFlows = null;

                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                sb.Append("<tr style=\"background-color:darkorange;color:white;\">");
                sb.Append("<td>序号</td>");
                foreach (string serviceNo in serviceArr)
                {
                    sb.Append("<td>" + AdapterUtil.GetServiceNameByNo(serviceNo) + "</td>");
                }
                sb.Append("</tr>");
                if (ticketList != null && ticketList.Count > 0)
                {

                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string serviceNo in serviceArr)
                        {
                            tempList = ticketList.FindAll(p => p.sServiceNo.Equals(serviceNo));
                            if (tempList != null && tempList.Count > i)
                            {
                                tempFlows = tempList[i];
                                //sb.Append("<td>"+ getMaskName(tempFlows.sCnName,maskFlag) + "</td>");
                                sb.Append("<td>" + tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag));

                                if (tempFlows.iPriorityType > PublicConsts.PRIORITY_TYPE0 && tempFlows.iPriorityType < PublicConsts.PRIORITY_TYPE6)
                                {
                                    sb.Append("(" + PublicHelper.GetPriorityType(tempFlows.iPriorityType) + ")");
                                }
                                sb.Append("</td>");
                            }
                            else
                            {
                                sb.Append("<td>&nbsp;</td>");
                            }
                        }
                        sb.Append("</tr>");
                    }
                }
                else
                {
                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string serviceNo in serviceArr)
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getProcessingTableByCounterIds(string branchNo, string counterNos, string pageIndex, string pageSize)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                //string workingMode = IUserContext.GetParamValue(branchNo, PublicConsts.DEF_WORKINGMODE, "Others");
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] counterArr = counterNos.Split(',');

                ////判断综合屏状态
                //int index = 0;
                //foreach (string counterNo in counterArr)
                //{
                //    if (PublicHelper.GetStateValue(branchNo, counterNo, 9).Equals("1"))
                //    {
                //        PublicHelper.SetStateValue(branchNo, counterNo, "0", 9);
                //        index++;
                //    }
                //}

                //if (index == 0)
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}

                string stafferNos = "", stafferNo = "";
                string stemp = "";
                foreach (string counter in counterArr)
                {
                    stemp = AdapterUtil.GetStafferIdByCounterId(counter);
                    if (!string.IsNullOrEmpty(stemp))
                    {
                        stafferNos += stemp + ",";
                    }
                }

                stafferNos = stafferNos.Trim(',');

                stafferNos = stafferNos.Replace(",", "','");

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 1000;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                int tableIndex = int.Parse(pageIndex);
                int tableSize = int.Parse(pageSize);
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;
                ViewTicketFlows tempFlows = null;

                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                sb.Append("<tr style=\"background-color:darkorange;color:white;\">");
                sb.Append("<td width=\"18%\" >序号</td>");
                foreach (string counter in counterArr)
                {
                    sb.Append("<td>" + AdapterUtil.GetCounterNameById(counter) + "</td>");
                }
                sb.Append("</tr>");
                if (ticketList != null && ticketList.Count > 0)
                {
                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string counter in counterArr)
                        {
                            stafferNo = AdapterUtil.GetStafferIdByCounterId(counter);

                            tempList = ticketList.FindAll(p => p.sStafferNo.Equals(stafferNo));
                            if (!string.IsNullOrEmpty(stafferNo) &&tempList != null && tempList.Count > i)
                            {
                                tempFlows = tempList[i];
                                if (tempFlows.iProcessState == PublicConsts.PROCSTATE_CALLING)
                                {
                                    sb.Append("<td style=\"color:red;\">" + tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag) + "</td>");
                                }
                                else
                                {
                                    //sb.Append("<td>"  + getMaskName(tempFlows.sCnName,maskFlag) + "</td>");
                                    sb.Append("<td>" + tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag));

                                    if (tempFlows.iPriorityType > PublicConsts.PRIORITY_TYPE0 && tempFlows.iPriorityType < PublicConsts.PRIORITY_TYPE6)
                                    {
                                        sb.Append("(" + PublicHelper.GetPriorityType(tempFlows.iPriorityType) + ")");
                                    }
                                    sb.Append("</td>");
                                }
                            }
                            else
                            {
                                sb.Append("<td>&nbsp;</td>");
                            }
                        }
                        sb.Append("</tr>");
                    }
                }
                else
                {
                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string counter in counterArr)
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        sb.Append("</tr>");
                    }
                }


                sb.Append("<tr>");
                sb.Append("<td>等候人数</td>");
                foreach (string counterNo in counterArr)
                {
                    sb.Append("<td>" + PublicHelper.getProcessingCountByCounterNo(branchNo, counterNo) + "</td>");
                }
                sb.Append("</tr>");

                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getProcessingTableByStafferIds(string branchNo, string stafferNos, string pageIndex, string pageSize)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                //string workingMode = IUserContext.GetParamValue(branchNo, PublicConsts.DEF_WORKINGMODE, "Others");
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] staffArr = stafferNos.Split(',');
                stafferNos = stafferNos.Replace(",", "','");
                ////判断综合屏状态
                //int index = 0;
                //foreach (string stafferNo in staffArr)
                //{
                //    if (PublicHelper.GetStateValue(branchNo, stafferNo, 9).Equals("1"))
                //    {
                //        PublicHelper.SetStateValue(branchNo, stafferNo, "0", 9);
                //        index++;
                //    }
                //}

                //if (index == 0)
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}

                string sWhere = " DataFlag=0 And  BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 1000;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                int tableIndex = int.Parse(pageIndex);
                int tableSize = int.Parse(pageSize);
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;
                ViewTicketFlows tempFlows = null;

                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                sb.Append("<tr style=\"background-color:darkorange;color:white;\">");
                sb.Append("<td >序号</td>");
                foreach (string stafferNo in staffArr)
                {
                    sb.Append("<td>" + AdapterUtil.GetStafferNameById(stafferNo) + "</td>");
                }
                sb.Append("</tr>");
                if (ticketList != null && ticketList.Count > 0)
                {
                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string stafferNo in staffArr)
                        {
                            tempList = ticketList.FindAll(p => p.sStafferNo.Equals(stafferNo));
                            if (tempList != null && tempList.Count > i)
                            {
                                tempFlows = tempList[i];
                                if (tempFlows.iProcessState == PublicConsts.PROCSTATE_CALLING)
                                {
                                    sb.Append("<td style=\"color:red;\">" + tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag) + "</td>");
                                }
                                else
                                {
                                    //sb.Append("<td>" + tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag) + "</td>");
                                    sb.Append("<td>" + tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag));

                                    if (tempFlows.iPriorityType > PublicConsts.PRIORITY_TYPE0 && tempFlows.iPriorityType < PublicConsts.PRIORITY_TYPE6)
                                    {
                                        sb.Append("(" + PublicHelper.GetPriorityType(tempFlows.iPriorityType) + ")");
                                    }
                                    sb.Append("</td>");
                                }
                            }
                            else
                            {
                                sb.Append("<td>&nbsp;</td>");
                            }
                        }
                        sb.Append("</tr>");
                    }
                }
                else
                {
                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string stafferNo in staffArr)
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        sb.Append("</tr>");
                    }
                }

                sb.Append("<tr>");
                sb.Append("<td>等候人数</td>");
                foreach (string stafferNo in staffArr)
                {
                    sb.Append("<td>" + PublicHelper.getQueuingCountByStafferNo(branchNo, stafferNo) + "</td>");
                }
                sb.Append("</tr>");

                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //返回所有的记录，按业务
        public string getProcessingTableByServiceIds(string branchNo, string serviceNos, string pageIndex, string pageSize)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                //string workingMode = IUserContext.GetParamValue(branchNo, PublicConsts.DEF_WORKINGMODE, "Others");
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] serviceArr = serviceNos.Split(',');
                serviceNos = serviceNos.Replace(",", "','");

                ////判断综合屏状态
                //int index = 0;
                //foreach (string serviceNo in serviceArr)
                //{
                //    if (PublicHelper.GetStateValue(branchNo, serviceNo, 9).Equals("1"))
                //    {
                //        PublicHelper.SetStateValue(branchNo, serviceNo, "0", 9);
                //        index++;
                //    }
                //}

                //if (index == 0)
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}

                string sWhere = " DataFlag=0 And  BranchNo = '" + branchNo + "' And ServiceNo In ('" + serviceNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 1000;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                int tableIndex = int.Parse(pageIndex);
                int tableSize = int.Parse(pageSize);
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;
                ViewTicketFlows tempFlows = null;

                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                sb.Append("<tr style=\"background-color:darkorange;color:white;\">");
                sb.Append("<td>序号</td>");
                foreach (string serviceNo in serviceArr)
                {
                    sb.Append("<td>" + AdapterUtil.GetServiceNameByNo(serviceNo) + "</td>");
                }
                sb.Append("</tr>");
                if (ticketList != null && ticketList.Count > 0)
                {
                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string serviceNo in serviceArr)
                        {
                            tempList = ticketList.FindAll(p => p.sServiceNo.Equals(serviceNo));
                            if (tempList != null && tempList.Count > i)
                            {
                                tempFlows = tempList[i];
                                if (tempFlows.iProcessState == PublicConsts.PROCSTATE_CALLING)
                                {
                                    sb.Append("<td style=\"color:red;\">" + tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag) + "</td>");
                                }
                                else
                                {
                                    //sb.Append("<td>" + getMaskName(tempFlows.sCnName,maskFlag) + "</td>");
                                    sb.Append("<td>" + tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag));

                                    if (tempFlows.iPriorityType > PublicConsts.PRIORITY_TYPE0 && tempFlows.iPriorityType < PublicConsts.PRIORITY_TYPE6)
                                    {
                                        sb.Append("(" + PublicHelper.GetPriorityType(tempFlows.iPriorityType) + ")");
                                    }
                                    sb.Append("</td>");
                                }
                            }
                            else
                            {
                                sb.Append("<td>&nbsp;</td>");
                            }
                        }
                        sb.Append("</tr>");
                    }
                }
                else
                {
                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string serviceNo in serviceArr)
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        sb.Append("</tr>");
                    }
                }

                sb.Append("<tr>");
                sb.Append("<td>等候人数</td>");
                foreach (string serviceNo in serviceArr)
                {
                    sb.Append("<td>" + PublicHelper.getQueuingCountByServiceNo(branchNo, serviceNo) + "</td>");
                }
                sb.Append("</tr>");

                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        //返回所有的记录，按业务
        public string getProcessingTableByServiceIds2(string branchNo, string serviceNos, string pageIndex, string pageSize)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                //string workingMode = IUserContext.GetParamValue(branchNo, PublicConsts.DEF_WORKINGMODE, "Others");
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] serviceArr = serviceNos.Split(',');
                serviceNos = serviceNos.Replace(",", "','");

                ////判断综合屏状态
                //int index = 0;
                //foreach (string serviceNo in serviceArr)
                //{
                //    if (PublicHelper.GetStateValue(branchNo, serviceNo, 9).Equals("1"))
                //    {
                //        PublicHelper.SetStateValue(branchNo, serviceNo, "0", 9);
                //        index++;
                //    }
                //}

                //if (index == 0)
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And ServiceNo In ('" + serviceNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 1000;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                int tableIndex = int.Parse(pageIndex);
                int tableSize = int.Parse(pageSize);
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;
                ViewTicketFlows tempFlows = null;

                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                sb.Append("<tr style=\"background-color:darkorange;color:white;\">");
                sb.Append("<td width=\"18%\">序号</td>");
                foreach (string serviceNo in serviceArr)
                {
                    sb.Append("<td>" + AdapterUtil.GetServiceNameByNo(serviceNo) + "</td>");
                }
                sb.Append("</tr>");
                if (ticketList != null && ticketList.Count > 0)
                {
                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string serviceNo in serviceArr)
                        {
                            tempList = ticketList.FindAll(p => p.sServiceNo.Equals(serviceNo));
                            if (tempList != null && tempList.Count > i)
                            {
                                tempFlows = tempList[i];
                                if (tempFlows.iProcessState == PublicConsts.PROCSTATE_CALLING)
                                {
                                    sb.Append("<td style=\"color:red;\">请" + tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag) + "到" + AdapterUtil.GetCounterNameById(tempFlows.sProcessedCounterNo) + "就诊</td>");
                                }
                                else
                                {
                                    //sb.Append("<td>" + getMaskName(tempFlows.sCnName,maskFlag) + "</td>");
                                    sb.Append("<td>" + tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag));

                                    if (tempFlows.iPriorityType > PublicConsts.PRIORITY_TYPE0 && tempFlows.iPriorityType < PublicConsts.PRIORITY_TYPE6)
                                    {
                                        sb.Append("(" + PublicHelper.GetPriorityType(tempFlows.iPriorityType) + ")");
                                    }
                                    sb.Append("</td>");
                                }
                            }
                            else
                            {
                                sb.Append("<td>&nbsp;</td>");
                            }
                        }
                        sb.Append("</tr>");
                    }
                }
                else
                {
                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string serviceNo in serviceArr)
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        sb.Append("</tr>");
                    }
                }
                sb.Append("<tr>");
                sb.Append("<td>等候人数</td>");
                foreach (string serviceNo in serviceArr)
                {
                    sb.Append("<td>" + PublicHelper.getProcessingCountByServiceNo(branchNo, serviceNo) + "</td>");
                }
                sb.Append("</tr>");

                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        
        public string getWaitingTableByServiceIds2(string branchNo, string serviceNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] serviceArr = serviceNos.Split(',');
                serviceNos = serviceNos.Replace(",", "','");

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And ServiceNo In ('" + serviceNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_WAITING + " And " + PublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 1000;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                int tableIndex = 1;
                int tableSize = int.Parse(num);
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;
                ViewTicketFlows tempFlows = null;

                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                if (ticketList != null && ticketList.Count > 0)
                {
                    for (int i = 0; i < tableSize; i++)
                    {
                        if (i % 2 == 0)
                        {
                            sb.Append("<tr>");
                        }
                        if (i < ticketList.Count)
                        {
                            sb.Append("<td width='50%'>" + ticketList[i].sTicketNo + " " + getMaskName(ticketList[i].sCnName, maskFlag) + "</td>");

                        }
                        else
                        {
                            sb.Append("<td width='50%'>&nbsp;</td>");
                        }
                        if (i % 2 == 1)
                        {
                            sb.Append("</tr>");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < tableSize / 2; i++)
                    {
                        sb.Append("<tr><td>&nbsp;</td><td>&nbsp;</td></tr>");
                    }
                }
                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getWaitingListByCounterIds2(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int tableSize = int.Parse(num);
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                string[] counterArr = counterNos.Split(',');
                string stafferNos = "";
                string stemp = "";
                foreach (string counter in counterArr)
                {
                    stemp = AdapterUtil.GetStafferIdByCounterId(counter);
                    if (!string.IsNullOrEmpty(stemp))
                    {
                        stafferNos += stemp + ",";
                    }
                }

                stafferNos = stafferNos.Trim(',');

                string[] staffArr = stafferNos.Split(',');
                stafferNos = stafferNos.Replace(",", "','");

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_WAITING + " And " + PublicConsts.PROCSTATE_WAITAREA9 + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = tableSize;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                ViewTicketFlows ticketFlow = null;

                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    for (int i = 0; i < tableSize; i++)
                    {
                        if (i % 2 == 0)
                        {
                            sb.Append("<tr>");
                        }
                        if (i < ticketColl.Count)
                        {
                            sb.Append("<td>" + ticketColl[i].sTicketNo + " " + getMaskName(ticketColl[i].sCnName, maskFlag) + "</td>");

                        }
                        else
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        if (i % 2 == 1)
                        {
                            sb.Append("</tr>");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < tableSize / 2; i++)
                    {
                        sb.Append("<tr><td>&nbsp;</td><td>&nbsp;</td></tr>");
                    }
                }
                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        #endregion

        #region 等待人数
        //按医生返回等候人数
        public string getWaitingCountByStafferIds(string branchNo, string stafferNos)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                string sWhere = "";
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();
                sb.Append("等候人数：");

                string[] staffArr = stafferNos.Split(',');
                foreach (string stafferNo in staffArr)
                {
                    count = PublicHelper.getProcessingCountByStafferNo(branchNo, stafferNo);
                    sb.Append("<span>" + AdapterUtil.GetStafferNameById(stafferNo) + ":" + count + "人</span>&nbsp; &nbsp; &nbsp; ");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        //按业务返回等候人数
        public string getWaitingCountByServiceIds(string branchNo, string serviceNos)
        {
            CodeData codeData = new CodeData();
            codeData.code = "200";
            codeData.msg = "success";
            codeData.data = "";
            try
            {
                int count = 0;
                string sWhere = "";
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();
                sb.Append("等候人数：");

                string[] serviceArr = serviceNos.Split(',');
                foreach (string serviceNo in serviceArr)
                {
                    count = PublicHelper.getProcessingCountByServiceNo(branchNo, serviceNo);
                    sb.Append("<span>" + AdapterUtil.GetServiceNameByNo(serviceNo) + "" + count + "人</span>&nbsp; &nbsp; &nbsp; ");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        #endregion

        #region 过号列表
        public string getNonarrivalsByStafferIds(string branchNo, string stafferNos)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                string sWhere = "";
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] staffArr = stafferNos.Split(',');
                stafferNos = stafferNos.Replace(",", "','");
                ////判断综合屏状态
                //int index = 0;
                //foreach (string stafferNo in staffArr)
                //{
                //    if (PublicHelper.GetStateValue(branchNo, stafferNo, 4).Equals("1"))
                //    {
                //        PublicHelper.SetStateValue(branchNo, stafferNo, "0", 4);
                //        index++;
                //    }
                //}

                //if (index == 0)
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}

                sWhere = " BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState = " + PublicConsts.PROCSTATE_NONARRIVAL + "  And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    foreach (ViewTicketFlows ticket in ticketColl)
                    {
                        sb.Append(ticket.sTicketNo + "&nbsp;" + getMaskName(ticket.sCnName,maskFlag) + "&nbsp;&nbsp;&nbsp;");
                    }
                }
                else
                {
                    sb.Append("&nbsp;");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getNonarrivalsByServiceIds(string branchNo, string serviceNos)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                string sWhere = "";
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] serviceArr = serviceNos.Split(',');
                serviceNos = serviceNos.Replace(",", "','"); 

                sWhere = " BranchNo = '" + branchNo + "' And ServiceNo In ('" + serviceNos + "') And ProcessState = " + PublicConsts.PROCSTATE_NONARRIVAL + "  And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    foreach (ViewTicketFlows ticket in ticketColl)
                    {
                        sb.Append(ticket.sTicketNo + "&nbsp;" + getMaskName(ticket.sCnName,maskFlag) + "&nbsp;&nbsp;");
                    }
                }
                else
                {
                    sb.Append("&nbsp;");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }


        public string getNonarrivalsByServiceIds2(string branchNo, string serviceNos)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                string sWhere = "";
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] serviceArr = serviceNos.Split(',');
                serviceNos = serviceNos.Replace(",", "','");

                sWhere = " BranchNo = '" + branchNo + "' And ServiceNo In ('" + serviceNos + "') And ProcessState = " + PublicConsts.PROCSTATE_NONARRIVAL + "  And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    foreach (ViewTicketFlows ticketFlow in ticketColl)
                    {
                        if (ticketFlow.iPriorityType == PublicConsts.PRIORITY_TYPE1)
                        {
                            sb.Append(getMaskName(ticketFlow.sCnName, maskFlag) + "  预-" + ticketFlow.sTicketNo + " &nbsp;&nbsp; ");
                        }
                        else
                        {
                            sb.Append(getMaskName(ticketFlow.sCnName, maskFlag) + " " + ticketFlow.sTicketNo + " &nbsp;&nbsp; ");
                        } 
                    }
                }
                else
                {
                    sb.Append("&nbsp;");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getNonarrivalsByCounterIds(string branchNo, string counterNos)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0; 
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] counterArr = counterNos.Split(',');
                counterNos = counterNos.Replace(",", "','");

                ////判断综合屏状态
                //int index = 0;
                //foreach (string counterNo in counterArr)
                //{
                //    if (PublicHelper.GetStateValue(branchNo, counterNo, 4).Equals("1"))
                //    {
                //        PublicHelper.SetStateValue(branchNo, counterNo, "0", 4);
                //        index++;
                //    }
                //}

                //if (index == 0)
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}

                string sWhere = " BranchNo = '" + branchNo + "' And ProcessedCounterNo In ('" + counterNos + "') And ProcessState = " + PublicConsts.PROCSTATE_NONARRIVAL + "  And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    foreach (ViewTicketFlows ticket in ticketColl)
                    {
                        sb.Append(ticket.sTicketNo + "&nbsp;" + getMaskName(ticket.sCnName,maskFlag) + "&nbsp;&nbsp;");
                    }
                }
                else
                {
                    sb.Append("&nbsp;");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getNonarrivalsByCounterIds2(string branchNo, string counterNos)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] counterArr = counterNos.Split(',');
                counterNos = counterNos.Replace(",", "','"); 

                string sWhere = " BranchNo = '" + branchNo + "' And ProcessedCounterNo In ('" + counterNos + "') And ProcessState = " + PublicConsts.PROCSTATE_NONARRIVAL + "  And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(ref count, 1, 4, sWhere);

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    int i = 1;
                    foreach (ViewTicketFlows ticket in ticketColl)
                    {
                        sb.Append(ticket.sTicketNo + "&nbsp;" + getMaskName(ticket.sCnName, maskFlag) + "&nbsp;&nbsp;");
                        if (i % 2 == 0)
                        {
                            sb.Append("<br />");
                        }
                        i++;
                    }
                }
                else
                {
                    sb.Append("&nbsp;");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        #endregion

        #region 叫号列表

        public string getCallingListByStaffId(string branchNo, string stafferNos)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                string sWhere = "";
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string stafferNo = stafferNos.Split(',')[0]; 

                sWhere = " DataFlag=0 And  BranchNo = '" + branchNo + "' And StafferNo ='" + stafferNo + "' And ProcessState = " + PublicConsts.PROCSTATE_CALLING + "  And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(ref count, 1, 1, sWhere);

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    sb.Append(ticketColl.GetFirstOne().sTicketNo + " " + getMaskName(ticketColl.GetFirstOne().sCnName, maskFlag));
                }
                else
                {
                    sb.Append("&nbsp;");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string getCallingListByServiceId(string branchNo, string serviceNos)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                string sWhere = "";
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string serviceNo = serviceNos.Split(',')[0];

                ////判断是否有更新内容，
                //if (PublicHelper.GetStateValue(branchNo, serviceNo, 1).Equals("0"))
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}
                ////更新标记
                //PublicHelper.SetStateValue(branchNo, serviceNo, "0", 1);

                sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And ServiceNo ='" + serviceNo + "' And ProcessState = " + PublicConsts.PROCSTATE_CALLING + "  And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    sb.Append(ticketColl.GetFirstOne().sTicketNo + " " + getMaskName(ticketColl.GetFirstOne().sCnName, maskFlag));
                }
                else
                {
                    sb.Append("&nbsp;");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }


        public string getCallingListByServiceId2(string branchNo, string serviceNos)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                string sWhere = "";
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewTicketFlows ticketFlow = null;

                string serviceNo = serviceNos.Split(',')[0]; 

                sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And ServiceNo ='" + serviceNo + "' And ProcessState = " + PublicConsts.PROCSTATE_CALLING + "  And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    ticketFlow = ticketColl.GetFirstOne();
                    if (ticketFlow.iPriorityType == PublicConsts.PRIORITY_TYPE1)
                    {
                        sb.Append(getMaskName(ticketFlow.sCnName, maskFlag) + "  预-" + ticketFlow.sTicketNo + " <br /> ");
                    }
                    else
                    {
                        sb.Append(getMaskName(ticketFlow.sCnName, maskFlag) + " " + ticketFlow.sTicketNo + " <br /> ");
                    }
                }
                else
                {
                    sb.Append("&nbsp;");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }


        public string getCallingListByCounterId(string branchNo, string counterNos)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                string sWhere = "";
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                string counterNo = counterNos.Split(',')[0]; 

                sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And ProcessedCounterNo ='" + counterNo + "' And ProcessState = " + PublicConsts.PROCSTATE_CALLING + "  And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    sb.Append(ticketColl.GetFirstOne().sTicketNo + " " + getMaskName(ticketColl.GetFirstOne().sCnName, maskFlag));
                }
                else
                {
                    sb.Append("&nbsp;");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        } 
        #endregion

        #region 等候列表
        public string getWaitingListByStafferIds(string branchNo, string stafferNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = int.Parse(num);
                string sWhere = "";
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] staffArr = stafferNos.Split(',');
                stafferNos = stafferNos.Replace(",", "','");
                ////判断综合屏状态
                //int index = 0;
                //foreach (string stafferNo in staffArr)
                //{
                //    if (PublicHelper.GetStateValue(branchNo, stafferNo, 2).Equals("1"))
                //    {
                //        PublicHelper.SetStateValue(branchNo, stafferNo, "0",2);
                //        index++;
                //    }
                //}

                //if (index == 0)
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}

                sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_WAITING + " And " + PublicConsts.PROCSTATE_WAITAREA9 + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = count;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                ViewTicketFlows ticketFlow = null;

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (i < ticketColl.Count)
                        {
                            ticketFlow = ticketColl[i];
                            if (ticketFlow.iPriorityType > PublicConsts.PRIORITY_TYPE0)
                            {
                                sb.Append(ticketFlow.sTicketNo + " " + getMaskName(ticketFlow.sCnName, maskFlag) + "(" + PublicHelper.GetPriorityType(ticketFlow.iPriorityType) + ") <br /> ");
                            }
                            else if (ticketFlow.iProcessState == PublicConsts.PROCSTATE_REDIAGNOSIS)
                            {
                                sb.Append(ticketFlow.sTicketNo + " " + getMaskName(ticketFlow.sCnName, maskFlag) + "(复诊) <br /> ");
                            }
                            else if (ticketFlow.iProcessState == PublicConsts.PROCSTATE_PASSTICKET)
                            {
                                sb.Append(ticketFlow.sTicketNo + " " + getMaskName(ticketFlow.sCnName, maskFlag) + "(过号) <br /> ");
                            } 
                            else
                            {
                                sb.Append(ticketFlow.sTicketNo + " " + getMaskName(ticketFlow.sCnName, maskFlag) + " <br /> ");
                            }
                        }
                        else
                        {
                            sb.Append("&nbsp;<br />");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        sb.Append("&nbsp;<br />");
                    }
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getWaitingListByServiceIds(string branchNo, string serviceNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = int.Parse(num);
                string sWhere = "";
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] serviceArr = serviceNos.Split(',');
                serviceNos = serviceNos.Replace(",", "','");
                 

                sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And ServiceNo In ('" + serviceNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_WAITING + " And " + PublicConsts.PROCSTATE_WAITAREA9 + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = count;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                ViewTicketFlows ticketFlow = null;

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (i < ticketColl.Count)
                        {
                            ticketFlow = ticketColl[i];
                            if (ticketFlow.iPriorityType > PublicConsts.PRIORITY_TYPE0)
                            {
                                sb.Append(ticketFlow.sTicketNo + " " + getMaskName(ticketFlow.sCnName, maskFlag) + "(" + PublicHelper.GetPriorityType(ticketFlow.iPriorityType) + ") <br /> ");
                            }
                            else if (ticketFlow.iProcessState == PublicConsts.PROCSTATE_REDIAGNOSIS)
                            {
                                sb.Append(ticketFlow.sTicketNo + " " + getMaskName(ticketFlow.sCnName, maskFlag) + "(复诊) <br /> ");
                            }
                            else if (ticketFlow.iProcessState == PublicConsts.PROCSTATE_PASSTICKET)
                            {
                                sb.Append(ticketFlow.sTicketNo + " " + getMaskName(ticketFlow.sCnName, maskFlag) + "(过号) <br /> ");
                            }
                            else if (ticketFlow.iPriorityType == PublicConsts.PRIORITY_TYPE1)
                            {
                                sb.Append(ticketFlow.sTicketNo + " " + getMaskName(ticketFlow.sCnName, maskFlag) + "(预) <br /> ");
                            }
                            else
                            {
                                sb.Append(ticketFlow.sTicketNo + " " + getMaskName(ticketFlow.sCnName, maskFlag) + " <br /> ");
                            }
                        }
                        else
                        {
                            sb.Append("&nbsp;<br />");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        sb.Append("&nbsp;<br />");
                    }
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getWaitingListByServiceIds2(string branchNo, string serviceNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = int.Parse(num);
                string sWhere = "";
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] serviceArr = serviceNos.Split(',');
                serviceNos = serviceNos.Replace(",", "','");


                sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And ServiceNo In ('" + serviceNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_WAITING + " And " + PublicConsts.PROCSTATE_WAITAREA9 + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = count;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                ViewTicketFlows ticketFlow = null;

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (i < ticketColl.Count)
                        {
                            ticketFlow = ticketColl[i];
                            if (ticketFlow.iPriorityType == PublicConsts.PRIORITY_TYPE1)
                            {
                                sb.Append(getMaskName(ticketFlow.sCnName, maskFlag) + "  预-" + ticketFlow.sTicketNo+ " <br /> ");
                            }
                            else
                            {
                                sb.Append(getMaskName(ticketFlow.sCnName, maskFlag) + " " + ticketFlow.sTicketNo + " <br /> ");
                            }
                        }
                        else
                        {
                            sb.Append("&nbsp;<br />");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        sb.Append("&nbsp;<br />");
                    }
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getWaitingListByCounterIds(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            string workingMode = PublicHelper.GetParamValue(branchNo, PublicConsts.DEF_WORKINGMODE, "Others");

            if (workingMode.Equals("SERVICE"))
            {
                return getWaitingListByCounterIdsInService(branchNo, counterNos, num);
            }
            else
            {
                return getWaitingListByCounterIdsInStaffer(branchNo, counterNos, num);
            }
        }

        private string getWaitingListByCounterIdsInService(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = int.Parse(num); 
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string temp = "";
                string[] counterArr = counterNos.Split(',');  

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And  ProcessState Between " + PublicConsts.PROCSTATE_WAITING + " And " + PublicConsts.PROCSTATE_WAITAREA9 + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                foreach (string counterNo in counterArr)
                {
                    temp += " CounterNos Like ('%" + counterNo + "%') Or ";
                }

                if (!string.IsNullOrEmpty(temp))
                {
                    temp = temp.Substring(0, temp.Length - 3);
                    sWhere += " And (" + temp + ")";
                }

                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = count;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                ViewTicketFlows ticketFlow = null;

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (i < ticketColl.Count)
                        {
                            ticketFlow = ticketColl[i];
                            if (ticketFlow.iPriorityType > PublicConsts.PRIORITY_TYPE0)
                            {
                                sb.Append(ticketFlow.sTicketNo + " " + getMaskName(ticketFlow.sCnName, maskFlag) + "(" + PublicHelper.GetPriorityType(ticketFlow.iPriorityType) + ") <br /> ");
                            } 
                            else
                            {
                                sb.Append(ticketFlow.sTicketNo + " " + getMaskName(ticketFlow.sCnName, maskFlag) + " <br /> ");
                            }
                        }
                        else
                        {
                            sb.Append("&nbsp;<br />");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        sb.Append("&nbsp;<br />");
                    }
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        private string getWaitingListByCounterIdsInStaffer(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = int.Parse(num); 
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                string[] counterArr = counterNos.Split(',');
                string stafferNos = "";
                string stemp = "";
                foreach (string counter in counterArr)
                {
                    stemp = AdapterUtil.GetStafferIdByCounterId(counter);
                    if (!string.IsNullOrEmpty(stemp))
                    {
                        stafferNos += stemp + ",";
                    }
                }

                stafferNos = stafferNos.Trim(',');
                 
                string[] staffArr = stafferNos.Split(',');
                stafferNos = stafferNos.Replace(",", "','"); 

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_WAITING + " And " + PublicConsts.PROCSTATE_WAITAREA9 + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = count;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                ViewTicketFlows ticketFlow = null;

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (i < ticketColl.Count)
                        {
                            ticketFlow = ticketColl[i];
                            if (ticketFlow.iPriorityType > PublicConsts.PRIORITY_TYPE0)
                            {
                                sb.Append(ticketFlow.sTicketNo + " " + getMaskName(ticketFlow.sCnName, maskFlag) + "(" + PublicHelper.GetPriorityType(ticketFlow.iPriorityType) + ") <br /> ");
                            } 
                            else
                            {
                                sb.Append(ticketFlow.sTicketNo + " " + getMaskName(ticketFlow.sCnName, maskFlag) + " <br /> ");
                            }
                        }
                        else
                        {
                            sb.Append("&nbsp;<br />");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        sb.Append("&nbsp;<br />");
                    }
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        #endregion

        #region 通用排队列表
        public string getQueuingListByStafferIds(string branchNo, string stafferNos)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                string sWhere = "";
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] staffArr = stafferNos.Split(',');
                stafferNos = stafferNos.Replace(",", "','");
                ////判断综合屏状态
                //int index = 0;
                //foreach (string stafferNo in staffArr)
                //{
                //    if (PublicHelper.GetStateValue(branchNo, stafferNo, 3).Equals("1"))
                //    {
                //        PublicHelper.SetStateValue(branchNo, stafferNo, "0", 3);
                //        index++;
                //    }
                //}

                //if (index == 0)
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}

                sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_WAITING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 100;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    foreach (ViewTicketFlows ticket in ticketColl)
                    {
                        sb.Append(ticket.sTicketNo + " " + getMaskName(ticket.sCnName,maskFlag) + " <br /> ");
                    }
                }
                 
                codeData.data  = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getQueuingListByServiceIds(string branchNo, string serviceNos)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                string sWhere = "";
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] serviceArr = serviceNos.Split(',');
                serviceNos = serviceNos.Replace(",", "','");

                ////判断综合屏状态
                //int index = 0;
                //foreach (string serviceNo in serviceArr)
                //{
                //    if (PublicHelper.GetStateValue(branchNo, serviceNo, 3).Equals("1"))
                //    {
                //        PublicHelper.SetStateValue(branchNo, serviceNo, "0", 3);
                //        index++;
                //    }
                //}

                //if (index == 0)
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}

                sWhere = " DataFlag=0 And  BranchNo = '" + branchNo + "' And ServiceNo In ('" + serviceNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_WAITING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 100;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    foreach (ViewTicketFlows ticket in ticketColl)
                    {
                        sb.Append(ticket.sTicketNo + " " + getMaskName(ticket.sCnName,maskFlag) + "<br /> ");
                    }
                }
                 
                codeData.data  = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getQueuingListByCounterIds(string branchNo, string counterNos)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            string workingMode = PublicHelper.GetParamValue(branchNo, PublicConsts.DEF_WORKINGMODE, "Others");

            if (workingMode.Equals("SERVICE"))
            {
                return getQueuingListByCounterIdsInService(branchNo, counterNos);
            }
            else
            {
                return getQueuingListByCounterIdsInStaff(branchNo, counterNos);
            }
        }
        private string getQueuingListByCounterIdsInService(string branchNo, string counterNos)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            { 
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string temp = "";
                string[] counterArr = counterNos.Split(',');

                ////判断综合屏状态
                //int index = 0;
                //foreach (string counterNo in counterArr)
                //{
                //    if (PublicHelper.GetStateValue(branchNo, counterNo, 3).Equals("1"))
                //    {
                //        PublicHelper.SetStateValue(branchNo, counterNo, "0", 3);
                //        index++;
                //    }
                //}

                //if (index == 0)
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}

                string sWhere = " DataFlag=0 And  BranchNo = '" + branchNo + "' And  ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_WAITING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                foreach (string counterNo in counterArr)
                {
                    temp += " CounterNos Like ('%" + counterNo + "%') Or ";
                }

                if (!string.IsNullOrEmpty(temp))
                {
                    temp = temp.Substring(0, temp.Length - 3);
                    sWhere += " And (" + temp + ")";
                }

                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 100;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    foreach (ViewTicketFlows ticket in ticketColl)
                    {
                        sb.Append(ticket.sTicketNo + " " + getMaskName(ticket.sCnName,maskFlag) + " <br /> ");
                    }
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        private string getQueuingListByCounterIdsInStaff(string branchNo, string counterNos)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                string sWhere = "";
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                string[] counterArr = counterNos.Split(',');
                string stafferNos = "";
                string stemp = "";
                foreach (string counter in counterArr)
                {
                    stemp = AdapterUtil.GetStafferIdByCounterId(counter);
                    if (!string.IsNullOrEmpty(stemp))
                    {
                        stafferNos += stemp + ",";
                    }
                }

                stafferNos = stafferNos.Trim(',');

                string[] staffArr = stafferNos.Split(',');
                stafferNos = stafferNos.Replace(",", "','");
                ////判断综合屏状态
                //int index = 0;
                //foreach (string stafferNo in staffArr)
                //{
                //    if (PublicHelper.GetStateValue(branchNo, stafferNo, 3).Equals("1"))
                //    {
                //        PublicHelper.SetStateValue(branchNo, stafferNo, "0",3);
                //        index++;
                //    }
                //}

                //if (index == 0)
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}

                sWhere = " DataFlag=0 And  BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_WAITING + "  And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 100;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    foreach (ViewTicketFlows ticket in ticketColl)
                    {
                        sb.Append(ticket.sTicketNo + " " + getMaskName(ticket.sCnName,maskFlag) + " <br /> ");
                    }
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        #endregion

        #region 取药队列
        public string getCallingRecipeByCounterId(string branchNo, string counterNo)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            StringBuilder sb = new StringBuilder();
            try
            {
                int count = 0;
                DateTime workDate = DateTime.Now;

                string strWhere = " BranchNo='" + branchNo + "' And CounterNo='" + counterNo + "' And RecipeState=3 And  ProcessState=2 And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 1;
                s_model.sFields = "*";
                s_model.sCondition = strWhere;
                s_model.sOrderField = " ModDate ";
                s_model.sOrderType = "Desc";
                s_model.sTableName = "ViewRecipeFlows";

                ViewRecipeFlowsBLL recipeBoss = new ViewRecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewRecipeFlowsCollections recipeColl = recipeBoss.GetRecordsByPaging(s_model);

                if (recipeColl != null && recipeColl.Count > 0)
                {
                    sb.Append("请" + recipeColl[0].sTicketNo + "  " + getMaskName(recipeColl[0].sCnName, maskFlag) + "取药");
                }
                else
                {
                    sb.Append("&nbsp;");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getCallingRecipeByCounterId2(string branchNo, string counterNo)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            StringBuilder sb = new StringBuilder();
            try
            {
                int count = 0;
                DateTime workDate = DateTime.Now;

                string strWhere = " BranchNo='" + branchNo + "' And CounterNo ='" + counterNo + "' And RecipeState=3 And  ProcessState=2 And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 1;
                s_model.sFields = "*";
                s_model.sCondition = strWhere;
                s_model.sOrderField = " ModDate ";
                s_model.sOrderType = "Desc";
                s_model.sTableName = "ViewRecipeFlows";

                ViewRecipeFlowsBLL recipeBoss = new ViewRecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewRecipeFlowsCollections recipeColl = recipeBoss.GetRecordsByPaging(s_model);

                if (recipeColl != null && recipeColl.Count > 0)
                {
                    sb.Append(recipeColl[0].sTicketNo + "  " + getMaskName(recipeColl[0].sCnName, maskFlag));
                }
                else
                {
                    sb.Append("&nbsp;");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getCallingRecipesByCounterIds(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            StringBuilder sb = new StringBuilder();
            sb.Append("&nbsp;");
            try
            {
                int count = 0;
                int size = int.Parse(num);
                DateTime workDate = DateTime.Now;
                counterNos = counterNos.Replace(",", "','");

                string strWhere = " BranchNo='" + branchNo + "' And CounterNo In ('" + counterNos + "') And RecipeState=3 And  ProcessState=2 And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                ViewRecipeFlowsBLL recipeBoss = new ViewRecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewRecipeFlowsCollections recipeColl = recipeBoss.GetRecordsByPaging(ref count, 1, 100, strWhere);

                if (recipeColl != null && recipeColl.Count > 0)
                {
                    count = 0;
                    sb.Clear();
                    foreach (ViewRecipeFlows recipe in recipeColl)
                    {
                        count++;
                        sb.Append(getMaskName(recipe.sCnName, maskFlag) + "&nbsp;&nbsp;&nbsp;&nbsp;");
                        if (count % size == 0)
                        {
                            sb.Append("<br />");
                        }
                    }
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getCallingRecipesTbByCounterIds(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                int size = int.Parse(num);
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();
                counterNos = counterNos.Replace(",", "','");

                string strWhere = " BranchNo='" + branchNo + "' And CounterNo In ('" + counterNos + "') And RecipeState=3 And  ProcessState=2 And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                ViewRecipeFlowsBLL recipeBoss = new ViewRecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewRecipeFlowsCollections recipeColl = recipeBoss.GetRecordsByPaging(ref count, 1, 100, strWhere);

                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                if (recipeColl != null && recipeColl.Count > 0)
                {
                    for (int i = 0; i < size; i++)
                    {
                        if (i % 2 == 0)
                        {
                            sb.Append("<tr>");
                        }
                        if (i < recipeColl.Count)
                        {
                            sb.Append("<td>" + recipeColl[i].sTicketNo + " " + getMaskName(recipeColl[i].sCnName, maskFlag) + "</td>");
                        }
                        else
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        if (i % 2 == 1)
                        {
                            sb.Append("</tr>");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < size / 2; i++)
                    {
                        sb.Append("<tr><td>&nbsp;</td><td>&nbsp;</td></tr>");
                    }
                }
                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getWaitingRecipesByCounterIds(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            StringBuilder sb = new StringBuilder();
            sb.Append("&nbsp;");

            try
            {
                int count = 0;
                int size = int.Parse(num);
                DateTime workDate = DateTime.Now;
                counterNos = counterNos.Replace(",", "','");

                string strWhere = " BranchNo='" + branchNo + "' And CounterNo In ('" + counterNos + "') And RecipeState=3 And  ProcessState=1 And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                ViewRecipeFlowsBLL recipeBoss = new ViewRecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewRecipeFlowsCollections recipeColl = recipeBoss.GetRecordsByPaging(ref count, 1, 100, strWhere);

                if (recipeColl != null && recipeColl.Count > 0)
                {
                    count = 0;
                    sb.Clear();
                    foreach (ViewRecipeFlows recipe in recipeColl)
                    {
                        count++;
                        sb.Append(getMaskName(recipe.sCnName, maskFlag) + "&nbsp;&nbsp;&nbsp;&nbsp;");
                        if (count % size == 0)
                        {
                            sb.Append("<br />");
                        }
                    }
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getWaitingRecipesTbByCounterIds(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            StringBuilder sb = new StringBuilder();

            try
            {
                int count = 0;
                int rows = int.Parse(num);
                int colums = 4;
                DateTime workDate = DateTime.Now;
                counterNos = counterNos.Replace(",", "','");

                string strWhere = " BranchNo='" + branchNo + "' And CounterNo In ('" + counterNos + "') And RecipeState=3 And  ProcessState=1 And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 100;
                s_model.sFields = "*";
                s_model.sCondition = strWhere;
                s_model.sOrderField = " EnqueueTime Asc ,ID ";
                s_model.sOrderType = "Desc";
                s_model.sTableName = "ViewRecipeFlows";

                ViewRecipeFlowsBLL recipeBoss = new ViewRecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewRecipeFlowsCollections recipeColl = recipeBoss.GetRecordsByPaging(s_model);
                ViewRecipeFlows recipe = null;
                int idx = 0;

                sb.Append("<table id=\"table\">");
                sb.Append("<tr id=\"DocTitleName\">");
                sb.Append("<td colspan=\"" + colums + "\">");
                sb.Append("等候取药");
                sb.Append("</td>");
                sb.Append("</tr>");


                if (recipeColl != null && recipeColl.Count > 0)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        sb.Append("<tr>");
                        for (int j = 1; j <= colums; j++)
                        {
                            idx = (i * colums) + j;
                            if (idx <= recipeColl.Count)
                            {
                                recipe = recipeColl[idx - 1];
                                sb.Append("<td>");
                                sb.Append(recipe.sTicketNo + "  " + getMaskName(recipe.sCnName, maskFlag));
                                sb.Append("</td>");
                            }
                            else
                            {
                                sb.Append("<td>&nbsp;</td>");
                            }
                        }
                        sb.Append("</tr>");
                    }
                }
                else
                {
                    for (int i = 0; i < rows; i++)
                    {
                        sb.Append("<tr>");
                        for (int j = 0; j < colums; j++)
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        sb.Append("</tr>");
                    }
                }

                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getWaitingRecipesTbByCounterIds2(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";

            try
            {
                int count = 0;
                int size = int.Parse(num);
                StringBuilder sb = new StringBuilder();
                DateTime workDate = DateTime.Now;
                counterNos = counterNos.Replace(",", "','");

                string strWhere = " BranchNo='" + branchNo + "' And CounterNo In ('" + counterNos + "') And RecipeState=3 And  ProcessState=1 And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 100;
                s_model.sFields = "*";
                s_model.sCondition = strWhere;
                s_model.sOrderField = " EnqueueTime Asc ,ID ";
                s_model.sOrderType = "Desc";
                s_model.sTableName = "ViewRecipeFlows";

                ViewRecipeFlowsBLL recipeBoss = new ViewRecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewRecipeFlowsCollections recipeColl = recipeBoss.GetRecordsByPaging(s_model);


                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                if (recipeColl != null && recipeColl.Count > 0)
                {
                    for (int i = 0; i < size; i++)
                    {
                        if (i % 2 == 0)
                        {
                            sb.Append("<tr>");
                        }
                        if (i < recipeColl.Count)
                        {
                            sb.Append("<td>" + recipeColl[i].sTicketNo + " " + getMaskName(recipeColl[i].sCnName, maskFlag) + "</td>");
                        }
                        else
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        if (i % 2 == 1)
                        {
                            sb.Append("</tr>");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < size / 2; i++)
                    {
                        sb.Append("<tr><td>&nbsp;</td><td>&nbsp;</td></tr>");
                    }
                }
                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getWaitingRecipesTbByCounterIds3(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            StringBuilder sb = new StringBuilder();
            try
            {
                int count = 0;
                int rows = int.Parse(num); 
                DateTime workDate = DateTime.Now;
                counterNos = counterNos.Replace(",", "','");

                string strWhere = " BranchNo='" + branchNo + "' And CounterNo In ('" + counterNos + "') And RecipeState=3 And  ProcessState=1 And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = rows;
                s_model.sFields = "*";
                s_model.sCondition = strWhere;
                s_model.sOrderField = " EnqueueTime Asc ,ID ";
                s_model.sOrderType = "Desc";
                s_model.sTableName = "ViewRecipeFlows";

                ViewRecipeFlowsBLL recipeBoss = new ViewRecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewRecipeFlowsCollections recipeColl = recipeBoss.GetRecordsByPaging(s_model);
                ViewRecipeFlows recipe = null;
                int idx = 0;

                sb.Append("<table id=\"table\">");
                sb.Append("<tr id=\"DocTitleName\">");
                sb.Append("<td>");
                sb.Append("等候取药");
                sb.Append("</td>");
                sb.Append("</tr>");


                if (recipeColl != null && recipeColl.Count > 0)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        if (i < recipeColl.Count)
                        {
                            recipe = recipeColl[i];
                            sb.Append("<tr>");
                            sb.Append("<td>");
                            sb.Append(recipe.sTicketNo + "  " + getMaskName(recipe.sCnName, maskFlag));
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }
                        else
                        {
                            sb.Append("<tr>");
                            sb.Append("<td>");
                            sb.Append("&nbsp;");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < rows; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>");
                        sb.Append("&nbsp;");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                    }
                }

                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getPassedRecipesByCounterIds(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            StringBuilder sb = new StringBuilder();
            sb.Append("&nbsp;");

            try
            {
                int count = 0;
                int size = int.Parse(num);
                DateTime workDate = DateTime.Now;
                counterNos = counterNos.Replace(",", "','");

                string strWhere = " BranchNo='" + branchNo + "' And CounterNo In ('" + counterNos + "') And RecipeState=3 And  ProcessState=3 And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                ViewRecipeFlowsBLL recipeBoss = new ViewRecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewRecipeFlowsCollections recipeColl = recipeBoss.GetRecordsByPaging(ref count, 1, size, strWhere);

                if (recipeColl != null && recipeColl.Count > 0)
                {
                    sb.Clear();
                    foreach (ViewRecipeFlows recipe in recipeColl)
                    {
                        sb.Append(getMaskName(recipe.sCnName, maskFlag) + "(" + recipe.sTicketNo + ")" + "&nbsp;&nbsp;&nbsp;&nbsp;");
                    }
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getPassedRecipesTbByCounterIds(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            StringBuilder sb = new StringBuilder();

            try
            {
                int count = 0;
                int rows = int.Parse(num);
                int colums = 4;
                DateTime workDate = DateTime.Now;
                counterNos = counterNos.Replace(",", "','");

                string strWhere = " BranchNo='" + branchNo + "' And CounterNo In ('" + counterNos + "') And RecipeState=3 And  ProcessState=3 And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 100;
                s_model.sFields = "*";
                s_model.sCondition = strWhere;
                s_model.sOrderField = " EnqueueTime Asc ,ID ";
                s_model.sOrderType = "Desc";
                s_model.sTableName = "ViewRecipeFlows";

                ViewRecipeFlowsBLL recipeBoss = new ViewRecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewRecipeFlowsCollections recipeColl = recipeBoss.GetRecordsByPaging(s_model);
                ViewRecipeFlows recipe = null;
                int idx = 0;

                sb.Append("<table id=\"table\">");
                sb.Append("<tr id=\"DocTitleName\">");
                sb.Append("<td colspan=\"" + colums + "\">");
                sb.Append("已过号未取药病人");
                sb.Append("</td>");
                sb.Append("</tr>");


                if (recipeColl != null && recipeColl.Count > 0)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        sb.Append("<tr>");
                        for (int j = 1; j <= colums; j++)
                        {
                            idx = (i * colums) + j;
                            if (idx <= recipeColl.Count)
                            {
                                recipe = recipeColl[idx - 1];
                                sb.Append("<td>");
                                sb.Append(recipe.sTicketNo + "  " + getMaskName(recipe.sCnName, maskFlag));
                                sb.Append("</td>");
                            }
                            else
                            {
                                sb.Append("<td>&nbsp;</td>");
                            }
                        }
                        sb.Append("</tr>");
                    }
                }
                else
                {
                    for (int i = 0; i < rows; i++)
                    {
                        sb.Append("<tr>");
                        for (int j = 0; j < colums; j++)
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        sb.Append("</tr>");
                    }
                }

                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        
        public string getMakingRecipesByCounterIds(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            StringBuilder sb = new StringBuilder();
            sb.Append("&nbsp;");

            try
            {
                int count = 0;
                int size = int.Parse(num);
                DateTime workDate = DateTime.Now;
                counterNos = counterNos.Replace(",", "','");

                string strWhere = " BranchNo='" + branchNo + "' And CounterNo In ('" + counterNos + "') And RecipeState=2 And  ProcessState=0 And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                ViewRecipeFlowsBLL recipeBoss = new ViewRecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewRecipeFlowsCollections recipeColl = recipeBoss.GetRecordsByPaging(ref count, 1, 100, strWhere);

                if (recipeColl != null && recipeColl.Count > 0)
                {
                    count = 0;
                    sb.Clear();
                    foreach (ViewRecipeFlows recipe in recipeColl)
                    {
                        count++;
                        sb.Append(getMaskName(recipe.sCnName, maskFlag) + "&nbsp;&nbsp;&nbsp;&nbsp;");
                        if (count % size == 0)
                        {
                            sb.Append("<br />");
                        }
                    }
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        #endregion

        #region 通用排队-视图+数据
        public string getProcessingViewByServiceIds(string branchNo, string serviceNos,string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = int.Parse(num);
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder(); 

                string[] serviceArr = serviceNos.Split(',');

                foreach (string serviceNo in serviceArr)
                {
                    sb.Append("<div class=\"wrapper\">");
                    sb.Append("<div class=\"title1\">" + AdapterUtil.GetServiceNameByNo(serviceNo) + "</div>");
                    sb.Append("<div class=\"content1\" id=\"" + serviceNo + "_calling\">");
                    sb.Append("&nbsp;");
                    sb.Append("</div>");
                    sb.Append("<div class=\"title2\" >以下患者可到科室门口等候</div>");
                    sb.Append("<div class=\"content2\" id=\"" + serviceNo + "_waiting\">");
                    for (int i = 0; i < count; i++)
                    {
                        sb.Append("&nbsp;<br />");
                    }
                    sb.Append("</div>");
                    sb.Append("<div class=\"title3\">以下患者请在等候区等候</div>");
                    sb.Append("<div class=\"content3\">");
                    sb.Append("<marquee style=\"width: 100%; height:190px;text-align:center; \"direction=\"up\" scrollAmount=\"3\"  id=\"" + serviceNo + "_queuing\" > ");
                    sb.Append("&nbsp;");
                    sb.Append("</marquee>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
                 
                sb.Append("<div class=\"clearit\"></div>");                 
                codeData.data  = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getProcessingViewByStafferIds(string branchNo, string stafferNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = int.Parse(num);
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                string[] staffArr = stafferNos.Split(','); 

                foreach (string stafferNo in staffArr)
                {
                    sb.Append("<div class=\"wrapper\">");
                    sb.Append("<div class=\"title1\">" + AdapterUtil.GetStafferNameById(stafferNo) + "</div>");
                    sb.Append("<div class=\"content1\" id=\"" + stafferNo + "_calling\">");
                    sb.Append("&nbsp;");
                    sb.Append("</div>");
                    sb.Append("<div class=\"title2\" >以下患者可到科室门口等候</div>");
                    sb.Append("<div class=\"content2\" id=\"" + stafferNo + "_waiting\">");
                    for (int i = 0; i < count; i++)
                    {
                        sb.Append("&nbsp;<br />");
                    }
                    sb.Append("</div>");
                    sb.Append("<div class=\"title3\">以下患者请在等候区等候</div>");
                    sb.Append("<div class=\"content3\">");
                    sb.Append("<marquee style=\"width: 100%; height:190px;text-align:center; \"direction=\"up\" scrollAmount=\"3\"  id=\"" + stafferNo + "_queuing\" > ");
                    sb.Append("&nbsp;");
                    sb.Append("</marquee>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }

                sb.Append("<div class=\"clearit\"></div>");
                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }  
        }
        public string getProcessingViewByCounterIds(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = int.Parse(num);
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                string[] counterArr = counterNos.Split(',');

                foreach (string counterNo in counterArr)
                {
                    sb.Append("<div class=\"wrapper\">");
                    sb.Append("<div class=\"title1\">" + AdapterUtil.GetCounterNameById(counterNo) + "</div>");
                    sb.Append("<div class=\"content1\" id=\"" + counterNo + "_calling\">");
                    sb.Append("&nbsp;");
                    sb.Append("</div>");
                    sb.Append("<div class=\"title2\" >以下患者可到科室门口等候</div>");
                    sb.Append("<div class=\"content2\" id=\"" + counterNo + "_waiting\">");
                    for (int i = 0; i < count; i++)
                    {
                        sb.Append("&nbsp;<br />");
                    }
                    sb.Append("</div>");
                    sb.Append("<div class=\"title3\">以下患者请在等候区等候</div>");
                    sb.Append("<div class=\"content3\">");
                    sb.Append("<marquee style=\"width: 100%;height:120px; text-align:center; \"direction=\"up\" scrollAmount=\"3\"  id=\"" + counterNo + "_queuing\" > ");
                    sb.Append("&nbsp;");
                    sb.Append("</marquee>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }

                sb.Append("<div class=\"clearit\"></div>");
                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            } 
        }

        public string getProcessingDataByServiceIds(string branchNo, string serviceNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";

            try
            {
                List<ProcessData> processList = new List<ProcessData>();

                ProcessData processData = null; 

                int count = int.Parse(num);
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string tempName = "";
                string[] serviceArr = serviceNos.Split(',');
                serviceNos = serviceNos.Replace(",", "','"); 

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And ServiceNo In ('" + serviceNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 100;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null; 
                string temp = "";

                if (ticketList != null && ticketList.Count > 0)
                {
                    foreach (string serviceNo in serviceArr)
                    {
                        processData = new ProcessData();
                        processData.processCode = serviceNo;

                        temp = "";
                        tempList = ticketList.FindAll(p => p.sServiceNo.Equals(serviceNo) && p.iProcessState == PublicConsts.PROCSTATE_CALLING);
                        if (tempList != null && tempList.Count > 0)
                        {
                            temp = "<span style=\"font-size:16px;\">正在检查</span>" + getMaskName(tempList[0].sCnName, maskFlag);
                        }
                        else
                        {
                            temp = "&nbsp;";
                        }
                        processData.callingData = temp;

                        temp = "";
                        tempList = ticketList.FindAll(p => p.sServiceNo.Equals(serviceNo) && p.iProcessState > PublicConsts.PROCSTATE_WAITING && p.iProcessState < PublicConsts.PROCSTATE_CALLING);
                        if (tempList != null && tempList.Count > 0)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                if (i < tempList.Count)
                                {
                                    tempName = "";
                                    if (tempList[i].iPriorityType > PublicConsts.PRIORITY_TYPE0)
                                    {
                                        tempName = "(" + PublicHelper.GetPriorityType(tempList[i].iPriorityType) + ")";
                                    }
                                    temp += (tempList[i].sTicketNo + " " + getMaskName(tempList[i].sCnName, maskFlag) + tempName + "<br />");                                   
                                } 
                            }
                        }
                        else
                        {
                            temp = ("&nbsp;");
                        }
                        processData.waitingData = temp;

                        temp = "";
                        tempList = ticketList.FindAll(p => p.sServiceNo.Equals(serviceNo) && p.iProcessState >= PublicConsts.PROCSTATE_DIAGNOSIS && p.iProcessState < PublicConsts.PROCSTATE_WAITING);
                        if (tempList != null && tempList.Count > 0)
                        {
                            foreach (ViewTicketFlows tempFlows in tempList)
                            {
                                tempName = "";
                                if (tempFlows.iPriorityType > PublicConsts.PRIORITY_TYPE0)
                                {
                                    tempName = "(" + PublicHelper.GetPriorityType(tempFlows.iPriorityType) + ")";
                                }
                                temp += (tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag) + tempName + "<br />");
                            }
                        }
                        else
                        {
                            temp = ("&nbsp;");
                        }
                        processData.queuingData = temp;

                        processList.Add(processData);
                    }
                } 

                codeData.data = JsonConvert.SerializeObject(processList);
                 
                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        
        public string getProcessingDataByStafferIds(string branchNo, string stafferNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";

            try
            {
                List<ProcessData> processList = new List<ProcessData>();

                ProcessData processData = null;

                string tempName = "";
                int count = int.Parse(num);
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                 
                string[] staffArr = stafferNos.Split(',');
                stafferNos = stafferNos.Replace(",", "','"); 

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 100;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;
                string temp = "";

                if (ticketList != null && ticketList.Count > 0)
                {
                    foreach (string stafferNo in staffArr)
                    {
                        processData = new ProcessData();
                        processData.processCode = stafferNo;

                        temp = "";
                        tempList = ticketList.FindAll(p => p.sStafferNo.Equals(stafferNo) && p.iProcessState == PublicConsts.PROCSTATE_CALLING);
                        if (tempList != null && tempList.Count > 0)
                        {
                            temp = "<span style=\"font-size:16px;\">正在检查</span>" + getMaskName(tempList[0].sCnName, maskFlag);
                        }
                        else
                        {
                            temp = "&nbsp;";
                        }
                        processData.callingData = temp;

                        temp = "";
                        tempList = ticketList.FindAll(p => p.sStafferNo.Equals(stafferNo) && p.iProcessState > PublicConsts.PROCSTATE_WAITING && p.iProcessState < PublicConsts.PROCSTATE_CALLING);
                        if (tempList != null && tempList.Count > 0)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                if (i < tempList.Count)
                                {
                                    tempName = "";
                                    if (tempList[i].iPriorityType > PublicConsts.PRIORITY_TYPE0)
                                    {
                                        tempName = "(" + PublicHelper.GetPriorityType(tempList[i].iPriorityType) + ")";
                                    }
                                    temp += (tempList[i].sTicketNo + " " + getMaskName(tempList[i].sCnName, maskFlag) + tempName + "<br />");
                                } 
                            }
                        }
                        else
                        {
                            temp = ("&nbsp;");
                        }
                        processData.waitingData = temp;

                        temp = "";
                        tempList = ticketList.FindAll(p => p.sStafferNo.Equals(stafferNo) && p.iProcessState >= PublicConsts.PROCSTATE_DIAGNOSIS && p.iProcessState < PublicConsts.PROCSTATE_WAITING);
                        if (tempList != null && tempList.Count > 0)
                        {
                            foreach (ViewTicketFlows tempFlows in tempList)
                            {
                                tempName = "";
                                if (tempFlows.iPriorityType > PublicConsts.PRIORITY_TYPE0)
                                {
                                    tempName = "(" + PublicHelper.GetPriorityType(tempFlows.iPriorityType) + ")";
                                }
                                temp += (tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName, maskFlag) + tempName + "<br />");
                            }
                        }
                        else
                        {
                            temp = ("&nbsp;");
                        }
                        processData.queuingData = temp;

                        processList.Add(processData);
                    }
                }

                codeData.data = JsonConvert.SerializeObject(processList);
                 
                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        
        public string getProcessingDataByCounterIds(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            string workingMode = PublicHelper.GetParamValue(branchNo, PublicConsts.DEF_WORKINGMODE, "Others");

            if (workingMode.Equals("SERVICE"))
            {
                return getProcessingDataByCounterIdsInService(branchNo,counterNos,num);
            }
            else
            { 
                return getProcessingDataByCounterIdsInStaff(branchNo, counterNos, num);
            }
        }

        private string getProcessingDataByCounterIdsInService(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";

            try
            { 
                List<ProcessData> processList = new List<ProcessData>();
                ProcessData processData = null;

                int count = int.Parse(num);
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string temp = "";
                string tempName = "";
                string[] counterArr = counterNos.Split(',');
                ////判断综合屏状态
                //int index = 0;
                //foreach (string counterNo in counterArr)
                //{
                //    if (PublicHelper.GetStateValue(branchNo, counterNo, 9).Equals("1"))
                //    {
                //        PublicHelper.SetStateValue(branchNo, counterNo, "0", 9);
                //        index++;
                //    }
                //}

                //if (index == 0)
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}

                string sWhere = " DataFlag=0 And  BranchNo = '" + branchNo + "' And  ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                foreach(string counterNo in counterArr)
                {
                    temp += " CounterNos Like ('%" + counterNo + "%') Or ";
                }

                if (!string.IsNullOrEmpty(temp))
                {
                    temp = temp.Substring(0, temp.Length - 3);
                    sWhere += " And (" + temp + ")";
                }

                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 100;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,PriorityType Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;

                if (ticketList != null && ticketList.Count > 0)
                {
                    foreach (string counterNo in counterArr)
                    {
                        processData = new ProcessData();
                        processData.processCode = counterNo;

                        temp = ""; 
                        tempList = ticketList.FindAll(p => p.sProcessedCounterNo.Equals(counterNo) && p.iProcessState == PublicConsts.PROCSTATE_CALLING);
                        if (tempList != null && tempList.Count > 0)
                        {
                            temp = "<span style=\"font-size:16px;\">正在检查</span>" + getMaskName(tempList[0].sCnName, maskFlag);
                        }
                        else
                        {
                            temp = "&nbsp;";
                        }
                        processData.callingData = temp;

                        temp = "";
                        tempList = ticketList.FindAll(p => p.sCounterNos.Contains(counterNo) && p.iProcessState > PublicConsts.PROCSTATE_WAITING && p.iProcessState < PublicConsts.PROCSTATE_CALLING);
                        if (tempList != null && tempList.Count > 0)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                if (i < tempList.Count)
                                {
                                    tempName = "";
                                    if (tempList[i].iPriorityType > PublicConsts.PRIORITY_TYPE0)
                                    {
                                        tempName = "(" + PublicHelper.GetPriorityType(tempList[i].iPriorityType) + ")";
                                    }
                                    temp += (tempList[i].sTicketNo + " " + getMaskName(tempList[i].sCnName, maskFlag) + tempName + "<br />");
                                }
                            }
                        }
                        else
                        {
                            temp = ("&nbsp;");
                        }
                        processData.waitingData = temp;

                        temp = "";
                        tempList = ticketList.FindAll(p => p.sCounterNos.Contains(counterNo) && p.iProcessState >= PublicConsts.PROCSTATE_DIAGNOSIS && p.iProcessState < PublicConsts.PROCSTATE_WAITING);
                        if (tempList != null && tempList.Count > 0)
                        {
                            foreach (ViewTicketFlows tempFlows in tempList)
                            {
                                tempName = "";
                                if (tempFlows.iPriorityType > PublicConsts.PRIORITY_TYPE0)
                                {
                                    tempName = "(" + PublicHelper.GetPriorityType(tempFlows.iPriorityType) + ")";
                                }
                                temp += (tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName, maskFlag) + tempName + "<br />");
                            }
                        }
                        else
                        {
                            temp = ("&nbsp;");
                        }
                        processData.queuingData = temp;

                        processList.Add(processData);
                    }
                }

                codeData.data = JsonConvert.SerializeObject(processList);

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        private string getProcessingDataByCounterIdsInStaff(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";

            try
            { 
                List<ProcessData> processList = new List<ProcessData>();
                ProcessData processData = null;

                int count = int.Parse(num);
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string temp = "";
                string tempName = "";
                string stafferNos = "";
                string stafferNo = "";
                string[] counterArr = counterNos.Split(',');
                foreach (string counterNo in counterArr)
                {
                    temp = AdapterUtil.GetStafferIdByCounterId(counterNo);
                    if (!string.IsNullOrEmpty(temp))
                    {
                        stafferNos += temp + ",";
                    }
                }

                stafferNos = stafferNos.Trim(',');

                string[] staffArr = stafferNos.Split(',');
                stafferNos = stafferNos.Replace(",", "','");
                ////判断综合屏状态
                //int index = 0;
                //foreach (string tempNo in staffArr)
                //{
                //    if (PublicHelper.GetStateValue(branchNo, tempNo, 9).Equals("1"))
                //    {
                //        PublicHelper.SetStateValue(branchNo, tempNo, "0", 9);
                //        index++;
                //    }
                //}

                //if (index == 0)
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 100;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;

                if (ticketList != null && ticketList.Count > 0)
                {
                    foreach (string counterNo in counterArr)
                    {
                        processData = new ProcessData();
                        processData.processCode = counterNo;

                        temp = "";
                        stafferNo = AdapterUtil.GetStafferIdByCounterId(counterNo);
                        tempList = ticketList.FindAll(p => p.sStafferNo.Equals(stafferNo) && p.iProcessState == PublicConsts.PROCSTATE_CALLING);
                        if (tempList != null && tempList.Count > 0)
                        {
                            temp = "<span style=\"font-size:16px;\">正在检查</span>" + getMaskName(tempList[0].sCnName, maskFlag);
                        }
                        else
                        {
                            temp = "&nbsp;";
                        }
                        processData.callingData = temp;

                        temp = "";
                        tempList = ticketList.FindAll(p => p.sStafferNo.Equals(stafferNo) && p.iProcessState > PublicConsts.PROCSTATE_WAITING && p.iProcessState < PublicConsts.PROCSTATE_CALLING);
                        if (tempList != null && tempList.Count > 0)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                if (i < tempList.Count)
                                {
                                    tempName = "";
                                    if (tempList[i].iPriorityType > PublicConsts.PRIORITY_TYPE0)
                                    {
                                        tempName = "(" + PublicHelper.GetPriorityType(tempList[i].iPriorityType) + ")";
                                    }
                                    temp += (tempList[i].sTicketNo + " " + getMaskName(tempList[i].sCnName, maskFlag) + tempName + "<br />");
                                }
                            }
                        }
                        else
                        {
                            temp = ("&nbsp;");
                        }
                        processData.waitingData = temp;

                        temp = "";
                        tempList = ticketList.FindAll(p => p.sStafferNo.Equals(stafferNo) && p.iProcessState >= PublicConsts.PROCSTATE_DIAGNOSIS && p.iProcessState < PublicConsts.PROCSTATE_WAITING);
                        if (tempList != null && tempList.Count > 0)
                        {
                            foreach (ViewTicketFlows tempFlows in tempList)
                            {
                                tempName = "";
                                if (tempFlows.iPriorityType > PublicConsts.PRIORITY_TYPE0)
                                {
                                    tempName = "(" + PublicHelper.GetPriorityType(tempFlows.iPriorityType) + ")";
                                }
                                temp += (tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName, maskFlag) + tempName + "<br />");
                            }
                        }
                        else
                        {
                            temp = ("&nbsp;");
                        }
                        processData.queuingData = temp;

                        processList.Add(processData);
                    }
                }

                codeData.data = JsonConvert.SerializeObject(processList);

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        #endregion

        #region 万州妇幼保健院
        public string getProcessingViewByCounterIdsWz(string branchNo, string counterNos)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            { 
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                string[] counterArr = counterNos.Split(','); 

                foreach (string counterNo in counterArr)
                {
                    sb.Append("<div class=\"wrapper\">");
                    sb.Append("<div class=\"title1\">" + AdapterUtil.GetCounterNameById(counterNo) + "</div>");
                    sb.Append("<div class=\"content1\" id=\"" + counterNo + "_calling\">");
                    sb.Append("&nbsp;");
                    sb.Append("</div>"); 
                    sb.Append("<div class=\"title3\">以下患者请在等候区等候</div>");
                    sb.Append("<div class=\"content3\">");
                    sb.Append("<marquee style=\"width: 100%;height:340px; text-align:center; \"direction=\"up\" scrollAmount=\"3\"  id=\"" + counterNo + "_queuing\" > ");
                    sb.Append("&nbsp;");
                    sb.Append("</marquee>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }

                sb.Append("<div class=\"clearit\"></div>");
                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        } 
         
        public string getProcessingDataByCounterIdsWz(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            string workingMode = PublicHelper.GetParamValue(branchNo, PublicConsts.DEF_WORKINGMODE, "Others");

            if (workingMode.Equals("SERVICE"))
            {
                return getProcessingDataByCounterIdsInServiceWz(branchNo, counterNos, num);
            }
            else
            {
                return getProcessingDataByCounterIdsInStaffWz(branchNo, counterNos, num);
            }
        }

        private string getProcessingDataByCounterIdsInServiceWz(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";

            try
            {
                List<ProcessData> processList = new List<ProcessData>();
                ProcessData processData = null;

                int count = int.Parse(num);
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string temp = "";
                string[] counterArr = counterNos.Split(',');
                ////判断综合屏状态
                //int index = 0;
                //foreach (string counterNo in counterArr)
                //{
                //    if (PublicHelper.GetStateValue(branchNo, counterNo, 9).Equals("1"))
                //    {
                //        PublicHelper.SetStateValue(branchNo, counterNo, "0", 9);
                //        index++;
                //    }
                //}

                //if (index == 0)
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}

                string sWhere = " DataFlag=0 And  BranchNo = '" + branchNo + "' And  ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                foreach (string counterNo in counterArr)
                {
                    temp += " CounterNos Like ('%" + counterNo + "%') Or ";
                }

                if (!string.IsNullOrEmpty(temp))
                {
                    temp = temp.Substring(0, temp.Length - 3);
                    sWhere += " And (" + temp + ")";
                }

                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 100;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;

                if (ticketList != null && ticketList.Count > 0)
                {
                    foreach (string counterNo in counterArr)
                    {
                        processData = new ProcessData();
                        processData.processCode = counterNo;

                        temp = "";
                        tempList = ticketList.FindAll(p => p.sProcessedCounterNo.Equals(counterNo) && p.iProcessState == PublicConsts.PROCSTATE_CALLING);
                        if (tempList != null && tempList.Count > 0)
                        {
                            temp = "<span style=\"font-size:16px;\">正在检查</span>" + getMaskName(tempList[0].sCnName, maskFlag);
                        }
                        else
                        {
                            temp = "&nbsp;";
                        }
                        processData.callingData = temp;
                         
                        processData.waitingData = "";

                        temp = "";
                        tempList = ticketList.FindAll(p => p.sCounterNos.Contains(counterNo) && p.iProcessState >= PublicConsts.PROCSTATE_DIAGNOSIS && p.iProcessState < PublicConsts.PROCSTATE_CALLING);
                        if (tempList != null && tempList.Count > 0)
                        {
                            foreach (ViewTicketFlows tempFlows in tempList)
                            {
                                temp += (tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag) + "<br />");
                            }
                        }
                        else
                        {
                            temp = ("&nbsp;");
                        }
                        processData.queuingData = temp;

                        processList.Add(processData);
                    }
                }

                codeData.data = JsonConvert.SerializeObject(processList);

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        private string getProcessingDataByCounterIdsInStaffWz(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";

            try
            {
                List<ProcessData> processList = new List<ProcessData>();
                ProcessData processData = null;

                int count = int.Parse(num);
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string temp = "";
                string stafferNos = "";
                string stafferNo = "";
                string[] counterArr = counterNos.Split(',');
                foreach (string counterNo in counterArr)
                {
                    temp = AdapterUtil.GetStafferIdByCounterId(counterNo);
                    if (!string.IsNullOrEmpty(temp))
                    {
                        stafferNos += temp + ",";
                    }
                }

                stafferNos = stafferNos.Trim(',');

                string[] staffArr = stafferNos.Split(',');
                stafferNos = stafferNos.Replace(",", "','");
                ////判断综合屏状态
                //int index = 0;
                //foreach (string tempNo in staffArr)
                //{
                //    if (PublicHelper.GetStateValue(branchNo, tempNo, 9).Equals("1"))
                //    {
                //        PublicHelper.SetStateValue(branchNo, tempNo, "0",9);
                //        index++;
                //    }
                //}

                //if (index == 0)
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 100;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;

                if (ticketList != null && ticketList.Count > 0)
                {
                    foreach (string counterNo in counterArr)
                    {
                        processData = new ProcessData();
                        processData.processCode = counterNo;

                        temp = "";
                        stafferNo = AdapterUtil.GetStafferIdByCounterId(counterNo);
                        tempList = ticketList.FindAll(p => p.sStafferNo.Equals(stafferNo) && p.iProcessState == PublicConsts.PROCSTATE_CALLING);
                        if (tempList != null && tempList.Count > 0)
                        {
                            temp = "<span style=\"font-size:16px;\">正在检查</span>" + getMaskName(tempList[0].sCnName, maskFlag);
                        }
                        else
                        {
                            temp = "&nbsp;";
                        }
                        processData.callingData = temp;

                        processData.waitingData = "";

                        temp = "";
                        tempList = ticketList.FindAll(p => p.sStafferNo.Equals(stafferNo) && p.iProcessState >= PublicConsts.PROCSTATE_DIAGNOSIS && p.iProcessState < PublicConsts.PROCSTATE_CALLING);
                        if (tempList != null && tempList.Count > 0)
                        {
                            foreach (ViewTicketFlows tempFlows in tempList)
                            {
                                temp += (tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName,maskFlag) + "<br />");
                            }
                        }
                        else
                        {
                            temp = ("&nbsp;");
                        }
                        processData.queuingData = temp;

                        processList.Add(processData);
                    }
                }

                codeData.data = JsonConvert.SerializeObject(processList);

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        #endregion

        #region 璧山妇幼保健院

        public string getProcessingDataByCounterIdsBs(string branchNo, string counterNos)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";

            try
            {
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string temp = "";
                string stafferNos = "";
                string stafferNo = "";
                string[] counterArr = counterNos.Split(','); 

                foreach (string counterNo in counterArr)
                {
                    temp = AdapterUtil.GetStafferIdByCounterId(counterNo);
                    if (!string.IsNullOrEmpty(temp))
                    {
                        if (!IsExpert(temp))
                        {
                            stafferNos += temp + ",";
                        }
                    }
                }

                stafferNos = stafferNos.Trim(',');
                stafferNos = stafferNos.Replace(",", "','");

                if (!string.IsNullOrEmpty(stafferNos))
                { 
                    string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                    SqlModel s_model = new SqlModel();
                    s_model.iPageNo = 1;
                    s_model.iPageSize = 100;
                    s_model.sFields = "*";
                    s_model.sCondition = sWhere;
                    s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                    s_model.sOrderType = "Asc";
                    s_model.sTableName = "ViewTicketFlows";

                    ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                    List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                    List<ViewTicketFlows> tempList = null;

                    sb.Clear();

                    sb.Append("<table id=\"table\">");
                    sb.Append("<tr id=\"DocTitleName\">");
                    sb.Append("<td width=\"150\">医生诊室</td><td width=\"100\">坐诊医生</td><td width=\"200\">当前就诊</td><td>等候队列</td><td width=\"100\">等候人数</td>");
                    sb.Append("</tr>");

                    if (ticketList != null && ticketList.Count > 0)
                    {
                        foreach (string counterNo in counterArr)
                        {
                            stafferNo = AdapterUtil.GetStafferIdByCounterId(counterNo);

                            sb.Append("<tr>");
                            sb.Append("<td>");
                            sb.Append(AdapterUtil.GetCounterNameById(counterNo));
                            sb.Append("</td>");
                            sb.Append("<td>");
                            sb.Append(AdapterUtil.GetStafferNameById(stafferNo));
                            sb.Append("</td>");

                            tempList = ticketList.FindAll(p => p.sStafferNo.Equals(stafferNo) && p.iProcessState == PublicConsts.PROCSTATE_CALLING);
                            if (tempList != null && tempList.Count > 0)
                            {
                                sb.Append("<td>");
                                sb.Append(tempList[0].sTicketNo + " " + getMaskName(tempList[0].sCnName, maskFlag));
                                if(tempList[0].iPriorityType==PublicConsts.PRIORITY_TYPE1)
                                {
                                    sb.Append("(预)");
                                }
                                sb.Append("</td>");
                            }
                            else
                            {
                                sb.Append("<td>");
                                sb.Append("&nbsp;");
                                sb.Append("</td>");
                            }

                            tempList = ticketList.FindAll(p => p.sStafferNo.Equals(stafferNo) && p.iProcessState >= PublicConsts.PROCSTATE_DIAGNOSIS && p.iProcessState < PublicConsts.PROCSTATE_CALLING);
                            if (tempList != null && tempList.Count > 0)
                            {
                                temp = "";
                                int i = 0;
                                foreach (ViewTicketFlows tempFlows in tempList)
                                {
                                    i++;
                                    temp += (tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName, maskFlag)) ;
                                    if (tempFlows.iPriorityType == PublicConsts.PRIORITY_TYPE1)
                                    {
                                        temp += "(预)";
                                    }
                                    temp+= "&nbsp;&nbsp;&nbsp;&nbsp;";
                                    if (i > 1)
                                    {
                                        break;
                                    }
                                }

                                sb.Append("<td style=\"overflow:scroll;\">");
                                sb.Append(temp);
                                sb.Append("</td>");
                            }
                            else
                            {
                                sb.Append("<td>");
                                sb.Append("&nbsp;");
                                sb.Append("</td>");
                            }

                            sb.Append("<td>");
                            sb.Append(PublicHelper.getProcessingCountByCounterNo(branchNo, counterNo));
                            sb.Append("</td>");

                            sb.Append("</tr>");

                        }
                    }
                    else
                    {
                        foreach (string counterNo in counterArr)
                        {
                            stafferNo = AdapterUtil.GetStafferIdByCounterId(counterNo);

                            sb.Append("<tr>");
                            sb.Append("<td>");
                            sb.Append(AdapterUtil.GetCounterNameById(counterNo));
                            sb.Append("</td>");
                            sb.Append("<td>");
                            sb.Append(AdapterUtil.GetStafferNameById(stafferNo));
                            sb.Append("</td>");
                            sb.Append("<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>");
                            sb.Append("</tr>");
                        }
                    }
                    sb.Append("</table>");
                }
                else
                {
                    sb.Append("<table id=\"table\">");
                    sb.Append("<tr id=\"DocTitleName\">");
                    sb.Append("<td width=\"150\">医生诊室</td><td width=\"100\">坐诊医生</td><td width=\"200\">当前就诊</td><td>等候队列</td><td width=\"100\">等候人数</td>");
                    sb.Append("</tr>");

                    foreach (string counterNo in counterArr)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>");
                        sb.Append(AdapterUtil.GetCounterNameById(counterNo));
                        sb.Append("</td>");
                        sb.Append("<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getProcessingDataByCounterIdsBs2(string branchNo, string counterNos)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";

            try
            {
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string temp = "";
                string stafferNos = "";
                string stafferNo = "";
                string[] counterArr = counterNos.Split(',');
                ////判断综合屏状态
                //int index = 0;
                //foreach (string counterNo in counterArr)
                //{
                //    if (PublicHelper.GetStateValue(branchNo, counterNo, 9).Equals("1"))
                //    {
                //        PublicHelper.SetStateValue(branchNo, counterNo, "0", 9);
                //        index++;
                //    }
                //}

                //if (index == 0)
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}

                foreach (string counterNo in counterArr)
                {
                    temp = AdapterUtil.GetStafferIdByCounterId(counterNo);
                    if (!string.IsNullOrEmpty(temp))
                    {
                        if (IsExpert(temp))
                        {
                            stafferNos += temp + ",";
                        }
                    }
                }

                stafferNos = stafferNos.Trim(',');
                stafferNos = stafferNos.Replace(",", "','");

                if (!string.IsNullOrEmpty(stafferNos))
                {
                    string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                    SqlModel s_model = new SqlModel();
                    s_model.iPageNo = 1;
                    s_model.iPageSize = 100;
                    s_model.sFields = "*";
                    s_model.sCondition = sWhere;
                    s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                    s_model.sOrderType = "Asc";
                    s_model.sTableName = "ViewTicketFlows";

                    ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                    List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                    List<ViewTicketFlows> tempList = null;

                    sb.Clear();

                    sb.Append("<table id=\"table\">");
                    sb.Append("<tr id=\"DocTitleName\">");
                    sb.Append("<td width=\"150\">专家诊室</td><td width=\"100\">坐诊专家</td><td width=\"200\">当前就诊</td><td>等候队列</td><td width=\"100\">等候人数</td>");
                    sb.Append("</tr>");

                    if (ticketList != null && ticketList.Count > 0)
                    {
                        foreach (string counterNo in counterArr)
                        {
                            stafferNo = AdapterUtil.GetStafferIdByCounterId(counterNo);

                            sb.Append("<tr>");
                            sb.Append("<td>");
                            sb.Append(AdapterUtil.GetCounterNameById(counterNo));
                            sb.Append("</td>");
                            sb.Append("<td>");
                            sb.Append(AdapterUtil.GetStafferNameById(stafferNo));
                            sb.Append("</td>");

                            tempList = ticketList.FindAll(p => p.sStafferNo.Equals(stafferNo) && p.iProcessState == PublicConsts.PROCSTATE_CALLING);
                            if (tempList != null && tempList.Count > 0)
                            {
                                sb.Append("<td>");
                                sb.Append(tempList[0].sTicketNo + " " + getMaskName(tempList[0].sCnName, maskFlag));
                                if (tempList[0].iPriorityType == PublicConsts.PRIORITY_TYPE1)
                                {
                                    sb.Append("(预)");
                                }
                                sb.Append("</td>");
                            }
                            else
                            {
                                sb.Append("<td>");
                                sb.Append("&nbsp;");
                                sb.Append("</td>");
                            }

                            tempList = ticketList.FindAll(p => p.sStafferNo.Equals(stafferNo) && p.iProcessState >= PublicConsts.PROCSTATE_DIAGNOSIS && p.iProcessState < PublicConsts.PROCSTATE_CALLING);
                            if (tempList != null && tempList.Count > 0)
                            {
                                temp = "";
                                int i = 0;
                                foreach (ViewTicketFlows tempFlows in tempList)
                                {
                                    i++; 
                                    temp += (tempFlows.sTicketNo + " " + getMaskName(tempFlows.sCnName, maskFlag));
                                    if (tempFlows.iPriorityType == PublicConsts.PRIORITY_TYPE1)
                                    {
                                        temp += "(预)";
                                    }
                                    temp += "&nbsp;&nbsp;&nbsp;&nbsp;";
                                    if (i > 1)
                                    {
                                        break;
                                    }
                                }

                                sb.Append("<td style=\"overflow:scroll;\">");
                                sb.Append(temp);
                                sb.Append("</td>");
                            }
                            else
                            {
                                sb.Append("<td>");
                                sb.Append("&nbsp;");
                                sb.Append("</td>");
                            }

                            sb.Append("<td>");
                            sb.Append(PublicHelper.getProcessingCountByCounterNo(branchNo, counterNo));
                            sb.Append("</td>");

                            sb.Append("</tr>");

                        }
                    }
                    else
                    {
                        foreach (string counterNo in counterArr)
                        {
                            stafferNo = AdapterUtil.GetStafferIdByCounterId(counterNo);

                            sb.Append("<tr>");
                            sb.Append("<td>");
                            sb.Append(AdapterUtil.GetCounterNameById(counterNo));
                            sb.Append("</td>");
                            sb.Append("<td>");
                            sb.Append(AdapterUtil.GetStafferNameById(stafferNo));
                            sb.Append("</td>");
                            sb.Append("<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>");
                            sb.Append("</tr>");
                        }
                    }
                    sb.Append("</table>");

                }
                else
                {
                    sb.Append("<table id=\"table\">");
                    sb.Append("<tr id=\"DocTitleName\">");
                    sb.Append("<td width=\"150\">专家诊室</td><td width=\"100\">坐诊专家</td><td width=\"200\">当前就诊</td><td>等候队列</td><td width=\"100\">等候人数</td>");
                    sb.Append("</tr>");

                    foreach (string counterNo in counterArr)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>");
                        sb.Append(AdapterUtil.GetCounterNameById(counterNo));
                        sb.Append("</td>");
                        sb.Append("<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getWaitingRecipesByCounterIdsBs(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            StringBuilder sb = new StringBuilder(); 

            try
            {
                int count = 0;
                int rows = int.Parse(num);
                int colums = 4;
                DateTime workDate = DateTime.Now;
                counterNos = counterNos.Replace(",", "','");

                string strWhere = " BranchNo='" + branchNo + "' And CounterNo In ('" + counterNos + "') And RecipeState=3 And  ProcessState=1 And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 100;
                s_model.sFields = "*";
                s_model.sCondition = strWhere;
                s_model.sOrderField = " EnqueueTime Asc ,ID ";
                s_model.sOrderType = "Desc";
                s_model.sTableName = "ViewRecipeFlows";

                ViewRecipeFlowsBLL recipeBoss = new ViewRecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewRecipeFlowsCollections recipeColl = recipeBoss.GetRecordsByPaging(s_model);
                ViewRecipeFlows recipe = null;
                int idx = 0;

                sb.Append("<table id=\"table\">");
                sb.Append("<tr id=\"DocTitleName\">");
                sb.Append("<td colspan=\""+ colums + "\">");
                sb.Append("等候取药");
                sb.Append("</td>");
                sb.Append("</tr>");


                if (recipeColl != null && recipeColl.Count > 0)
                {
                    for (int i = 0; i <rows; i++)
                    {  
                        sb.Append("<tr>");
                        for (int j = 1; j <= colums; j++)
                        {
                            idx = (i* colums) + j;
                            if (idx <= recipeColl.Count)
                            {
                                recipe = recipeColl[idx - 1];
                                sb.Append("<td>");
                                sb.Append(recipe.sTicketNo + "  " + getMaskName(recipe.sCnName,maskFlag));
                                sb.Append("</td>");
                            }
                            else
                            {
                                sb.Append("<td>&nbsp;</td>");
                            }
                        }
                        sb.Append("</tr>");
                    } 
                }
                else
                {
                    for (int i = 0; i < rows; i++)
                    {
                        sb.Append("<tr>");
                        for (int j = 0; j < colums; j++)
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        sb.Append("</tr>");
                    }
                } 

                sb.Append("</table>"); 

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        
        public bool IsExpert(string sNo)
        {
            StafferInfo info=PageHelper.getStafferInfoByNo(sNo);
            if (info != null)
            {
                return ("专家列表:龚琼华".IndexOf(info.sStafferName)>0); 
            }
            return false;
        }
          
        // 
        public string getServiceRotaData()
        {

            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewServiceRotaBLL rotaBoss = new ViewServiceRotaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewServiceRotaCollections rotaColl = rotaBoss.GetAllRecords();

                sb.Append("<table id=\"table\">");

                sb.Append("<tr id=\"DocTitleName\">");
                sb.Append("<th>科室</th>");
                sb.Append("<th>业务名称</th>");
                sb.Append("<th>排班类型</th>");
                sb.Append("<th>挂号费用</th>");
                sb.Append("<th>已挂人数</th>");
                sb.Append("</tr>");
                if (rotaColl != null && rotaColl.Count > 0)
                {

                    foreach (ViewServiceRota rota in rotaColl)
                    {
                        sb.Append("<tr>");
                        sb.Append("<th>" + rota.sBranchNo + "</th>");
                        sb.Append("<th>" + rota.sServiceName + "</th>");
                        sb.Append("<th>" + rota.iRotaType + "</th>");
                        sb.Append("<th>" + rota.dRegisteFees.ToString("0.0") + "</th>");
                        sb.Append("<th>0</th>");
                        sb.Append("</tr>");
                    }
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>&nbps;</td>");
                        sb.Append("<td>&nbps;</td>");
                        sb.Append("<td>&nbps;</td>");
                        sb.Append("<td>&nbps;</td>");
                        sb.Append("<td>&nbps;</td>");
                        sb.Append("</tr>");
                    }

                }
                sb.Append("</table>");


                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        // 
        public string getStafferRotaData()
        {

            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewStafferRotaBLL rotaBoss = new ViewStafferRotaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewStafferRotaCollections rotaColl = rotaBoss.GetAllRecords();

                sb.Append("<table id=\"table\">");

                sb.Append("<tr id=\"DocTitleName\">");
                sb.Append("<th>科室</th>");
                sb.Append("<th>医生</th>");
                sb.Append("<th>排班类型</th>");
                sb.Append("<th>挂号费用</th>");
                sb.Append("<th>已挂人数</th>");
                sb.Append("</tr>");
                if (rotaColl != null && rotaColl.Count > 0)
                {
                    foreach (ViewStafferRota rota in rotaColl)
                    {
                        sb.Append("<tr>");
                        sb.Append("<th>" + rota.sBranchNo + "</th>");
                        sb.Append("<th>" + rota.sStafferName + "</th>");
                        sb.Append("<th>" + rota.iRotaType + "</th>");
                        sb.Append("<th>" + rota.dRegisteFees.ToString("0.0") + "</th>");
                        sb.Append("<th>0</th>");
                        sb.Append("</tr>");
                    }
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>&nbps;</td>");
                        sb.Append("<td>&nbps;</td>");
                        sb.Append("<td>&nbps;</td>");
                        sb.Append("<td>&nbps;</td>");
                        sb.Append("<td>&nbps;</td>");
                        sb.Append("</tr>");
                    }

                }
                sb.Append("</table>");


                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        // 
        public string getCounterRotaData(string num = "10")
        {

            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                int pageSize = int.Parse(num);
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                CounterInfoBLL counterBoss = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewStafferRotaBLL rotaBoss = new ViewStafferRotaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                CounterInfoCollections counterColl = counterBoss.GetRecordsByPaging(ref count, 1, pageSize, " LogonState=1 OR IsAutoLogon=1 ");
                ViewStafferRota staffRota;
                CounterInfo counter;

                sb.Append("<table id=\"table\">");

                sb.Append("<tr id=\"DocTitleName\">");
                sb.Append("<th width=\"170\">医生诊室</th>");
                sb.Append("<th width=\"170\">医生姓名</th>");
                sb.Append("<th width=\"*\">职称</th>");
                sb.Append("<th width=\"170\">诊疗费</th>");
                sb.Append("<th width=\"170\">已挂人数</th>");
                sb.Append("</tr>");

                if (counterColl != null && counterColl.Count > 0)
                {
                    for (int i = 0; i < pageSize; i++)
                    {
                        if (i < counterColl.Count)
                        {
                            counter = counterColl[i];
                            staffRota = PageHelper.getStafferRotaByStafferNo(counter.sLogonStafferNo);
                            if (staffRota != null)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td>" + counter.sCounterName + "</td>");
                                sb.Append("<td>" + staffRota.sStafferName + "</td>");
                                sb.Append("<td>" + staffRota.sSummary + "</td>");
                                sb.Append("<td>" + staffRota.dRegisteFees.ToString("0.0") + "元</td>");
                                sb.Append("<td>" + PublicHelper.getAllCountByStafferNo(staffRota.sStafferNo) + "</td>");
                                sb.Append("</tr>");
                            }
                            else
                            {
                                sb.Append("<tr>");
                                sb.Append("<td>&nbsp;</td>");
                                sb.Append("<td>&nbsp;</td>");
                                sb.Append("<td>&nbsp;</td>");
                                sb.Append("<td>&nbsp;</td>");
                                sb.Append("<td>&nbsp;</td>");
                                sb.Append("</tr>");
                            }
                        }
                        else
                        {
                            sb.Append("<tr>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("</tr>");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < pageSize; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>&nbsp;</td>");
                        sb.Append("<td>&nbsp;</td>");
                        sb.Append("<td>&nbsp;</td>");
                        sb.Append("<td>&nbsp;</td>");
                        sb.Append("<td>&nbsp;</td>");
                        sb.Append("</tr>");
                    }

                }
                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        #endregion

        #region 青海第五人民医院

        // 
        public string getProcessingDataByServiceIds_Qhkq(string branchNo, string serviceNos, string pageIndex, string pageSize)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                //string workingMode = IUserContext.GetParamValue(branchNo, PublicConsts.DEF_WORKINGMODE, "Others");
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] serviceArr = serviceNos.Split(',');
                serviceNos = serviceNos.Replace(",", "','"); 

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And ServiceNo In ('" + serviceNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 1000;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                int tableIndex = int.Parse(pageIndex);
                int tableSize = int.Parse(pageSize);
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;
                ViewTicketFlows tempFlows = null;

                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                sb.Append("<tr >");
                sb.Append("<td width=\"10%\" style=\"font-size:30px;padding:43px 2px;\">序号</td>");
                foreach (string serviceNo in serviceArr)
                {
                    sb.Append("<td style=\"font-size:30px;\">" + AdapterUtil.GetServiceNameByNo(serviceNo) + "</td>");
                }
                sb.Append("</tr>");
                if (ticketList != null && ticketList.Count > 0)
                {
                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string serviceNo in serviceArr)
                        {
                            tempList = ticketList.FindAll(p => p.sServiceNo.Equals(serviceNo));
                            if (tempList != null && tempList.Count > i)
                            {
                                tempFlows = tempList[i];
                                if (tempFlows.iProcessState == PublicConsts.PROCSTATE_CALLING)
                                {
                                    sb.Append("<td style=\"color:red;\">" + getMaskName(tempFlows.sCnName,maskFlag) + "</td>");
                                }
                                else
                                {
                                    sb.Append("<td>" + getMaskName(tempFlows.sCnName,maskFlag) + "</td>"); 
                                }
                            }
                            else
                            {
                                sb.Append("<td>&nbsp;</td>");
                            }
                        }
                        sb.Append("</tr>");
                    }
                }
                else
                {
                    for (int i = 0; i < tableSize - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        foreach (string serviceNo in serviceArr)
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        sb.Append("</tr>");
                    }
                }

                sb.Append("<tr>");
                sb.Append("<td style=\"font-size:20px;\">等候人数</td>");
                foreach (string serviceNo in serviceArr)
                {
                    sb.Append("<td>" + PublicHelper.getQueuingCountByServiceNo(branchNo, serviceNo) + "</td>");
                }
                sb.Append("</tr>");

                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        // 
        public string getOperatingDataByServiceIds_Qhkq(string pageIndex, string pageSize)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");

            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                //string workingMode = IUserContext.GetParamValue(branchNo, PublicConsts.DEF_WORKINGMODE, "Others");
                ViewOperatFlowsBLL infoBoss = new ViewOperatFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                int count = 0;
                int pIndex = int.Parse(pageIndex);
                int pSize = int.Parse(pageSize);
                string sWhere = " OperatTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddMinutes(10).ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                ViewOperatFlowsCollections infoColl = infoBoss.GetRecordsByPaging(ref count,pIndex,pSize,sWhere);
                 

                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                sb.Append("<tr>");
                sb.Append("<th width=\"20%\">姓名</th>");
                sb.Append("<th width=\"10%\">性别</th>");
                sb.Append("<th width=\"30%\">科室</th>");
                sb.Append("<th width=\"15%\">手术间</th>");
                sb.Append("<th width=\"25%\">状态</th>");
                sb.Append("</tr>");

                for(int i = 0; i < pSize; i++)
                {
                    if (infoColl != null && i < infoColl.Count)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + getMaskName(infoColl[i].sCnName,maskFlag) + "</td>");
                        sb.Append("<td>" + (infoColl[i].iSex==0?"女":"男") + "</td>");
                        sb.Append("<td>" + infoColl[i].sDataFrom + "</td>");
                        sb.Append("<td>" + infoColl[i].sRoomNo + "</td>");
                        sb.Append("<td style=\"color: red;\">" + PublicHelper.GetOperateState(infoColl[i].iOperatState) + "</td>");
                        sb.Append("</tr>");
                    }
                    else
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>&nbsp;</td>");
                        sb.Append("<td>&nbsp;</td>");
                        sb.Append("<td>&nbsp;</td>");
                        sb.Append("<td>&nbsp;</td>");
                        sb.Append("<td>&nbsp;</td>");
                        sb.Append("</tr>");
                    }
                } 

                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        #endregion
          
        #region 绵阳三台县

        public string getCallingListByCounterId_stx(string branchNo, string counterNos)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                string sWhere = "";
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                string counterNo = counterNos.Split(',')[0]; 

                sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And ProcessedCounterNo ='" + counterNo + "' And ProcessState = " + PublicConsts.PROCSTATE_CALLING + "  And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    sb.Append("<span>"+getMaskName(ticketColl.GetFirstOne().sCnName, maskFlag)+"("+ ticketColl.GetFirstOne().sTicketNo + ")</span>");
                }
                else
                {
                    sb.Append("&nbsp;");
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        
        public string getWaitingListByCounterIds_stx(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = int.Parse(num);
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                string[] counterArr = counterNos.Split(',');
                string stafferNos = "";
                string stemp = "";
                foreach (string counter in counterArr)
                {
                    stemp = AdapterUtil.GetStafferIdByCounterId(counter);
                    if (!string.IsNullOrEmpty(stemp))
                    {
                        stafferNos += stemp + ",";
                    }
                }

                stafferNos = stafferNos.Trim(',');

                string[] staffArr = stafferNos.Split(',');
                stafferNos = stafferNos.Replace(",", "','");
                ////判断综合屏状态
                //int index = 0;
                //foreach (string stafferNo in staffArr)
                //{
                //    if (PublicHelper.GetStateValue(branchNo, stafferNo, 2).Equals("1"))
                //    {
                //        PublicHelper.SetStateValue(branchNo, stafferNo, "0", 2);
                //        index++;
                //    }
                //}

                //if (index == 0)
                //{
                //    return JsonConvert.SerializeObject(codeData);
                //}

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_WAITING + " And " + PublicConsts.PROCSTATE_WAITAREA9 + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = count;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                ViewTicketFlows ticketFlow = null;

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (i < ticketColl.Count)
                        {
                            ticketFlow = ticketColl[i];
                             
                                sb.Append(getMaskName(ticketFlow.sCnName, maskFlag)+"("+ ticketFlow.sTicketNo+")" + " &nbsp;&nbsp; ");
                             
                        }
                        else
                        {
                            sb.Append("&nbsp;");
                        }
                    }
                }
                else
                { 
                        sb.Append("&nbsp;");
                   
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        #endregion

        #region 爱尔眼科模板
         
        public string getWaitingListByCounterIds3(string branchNo, string counterNos, string num )
        {
            string workingMode = PublicHelper.GetParamValue(branchNo, PublicConsts.DEF_WORKINGMODE, "Others");

            if (workingMode.Equals("SERVICE"))
            {
                return getWaitingListByCounterIdsInService3(branchNo, counterNos, num);
            }
            else
            {
                return getWaitingListByCounterIdsInStaffer3(branchNo, counterNos, num);
            }
        }

        private string getWaitingListByCounterIdsInService3(string branchNo, string counterNos, string num )
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = int.Parse(num);
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string temp = "";
                string[] counterArr = counterNos.Split(',');   


                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And  ProcessState Between " + PublicConsts.PROCSTATE_WAITING + " And " + PublicConsts.PROCSTATE_WAITAREA9 + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                foreach (string counterNo in counterArr)
                {
                    temp += " CounterNos Like ('%" + counterNo + "%') Or ";
                }

                if (!string.IsNullOrEmpty(temp))
                {
                    temp = temp.Substring(0, temp.Length - 3);
                    sWhere += " And (" + temp + ")";
                }

                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = count;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                ViewTicketFlows ticketFlow = null;

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (i < ticketColl.Count)
                        {
                            ticketFlow = ticketColl[i];
                            if (ticketFlow.iPriorityType > PublicConsts.PRIORITY_TYPE0)
                            {
                                sb.Append(ticketFlow.sTicketNo + " " + getMaskName(ticketFlow.sCnName, maskFlag) + "(" + PublicHelper.GetPriorityType(ticketFlow.iPriorityType) + ")");
                            }
                            else
                            {
                                sb.Append(ticketFlow.sTicketNo + " " + getMaskName(ticketFlow.sCnName, maskFlag) + "");
                            }
                        }
                        else
                        {
                            sb.Append("&nbsp;");
                        }

                        if (i % 2 != 0)
                        {
                            sb.Append("<br />");
                        }
                        else
                        {
                            sb.Append("&nbsp;&nbsp;");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        sb.Append("&nbsp;");

                        if (i % 2 != 0)
                        {
                            sb.Append("<br />");
                        }
                    }
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }



        private string getWaitingListByCounterIdsInStaffer3(string branchNo, string counterNos, string num )
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = int.Parse(num);
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                string[] counterArr = counterNos.Split(',');
                string stafferNos = "";
                string stemp = "";
                foreach (string counter in counterArr)
                {
                    stemp = AdapterUtil.GetStafferIdByCounterId(counter);
                    if (!string.IsNullOrEmpty(stemp))
                    {
                        stafferNos += stemp + ",";
                    }
                }

                stafferNos = stafferNos.Trim(',');

                string[] staffArr = stafferNos.Split(',');
                stafferNos = stafferNos.Replace(",", "','"); 

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState Between " + PublicConsts.PROCSTATE_WAITING + " And " + PublicConsts.PROCSTATE_WAITAREA9 + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = count;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                ViewTicketFlows ticketFlow = null;

                if (ticketColl != null && ticketColl.Count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (i < ticketColl.Count)
                        {
                            ticketFlow = ticketColl[i];
                            if (ticketFlow.iPriorityType > PublicConsts.PRIORITY_TYPE0)
                            {
                                sb.Append(ticketFlow.sTicketNo + " " + getMaskName(ticketFlow.sCnName, maskFlag) + "(" + PublicHelper.GetPriorityType(ticketFlow.iPriorityType) + ")");
                            }
                            else
                            {
                                sb.Append(ticketFlow.sTicketNo + " " + getMaskName(ticketFlow.sCnName, maskFlag) + "");
                            }
                        }
                        else
                        {
                            sb.Append("&nbsp;");
                        }

                        if (i % 2 != 0)
                        {
                            sb.Append("<br />");
                        }
                        else
                        {
                            sb.Append("&nbsp;&nbsp;");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        sb.Append("&nbsp;");

                        if (i % 2 != 0)
                        {
                            sb.Append("<br />");
                        }
                    }
                }

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        #endregion

        #region 标准通用模板

        public string getAllCallingListByCounterIds(string branchNo, string counterNos, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int tableIndex = 1;
                int tableSize = int.Parse(num);
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();
                 
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string[] counterArr = counterNos.Split(',');
                string stafferNos = "", stafferNo = "";
                string stemp = ""; 

                foreach (string counter in counterArr)
                {
                    stemp = AdapterUtil.GetStafferIdByCounterId(counter);
                    if (!string.IsNullOrEmpty(stemp))
                    {
                        stafferNos += stemp + ",";
                    }
                }

                stafferNos = stafferNos.Trim(',');

                stafferNos = stafferNos.Replace(",", "','");

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And StafferNo In ('" + stafferNos + "') And ProcessState =" + PublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = tableIndex;
                s_model.iPageSize = tableSize;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";
                 
                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;
                ViewTicketFlows tempFlows = null;

                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                sb.Append("<tr>");
                sb.Append("<td style=\"background:#065c8f;color:#FFF;\">");
                sb.Append("科室");
                sb.Append("</td>");
                sb.Append("<td style=\"background:#fd7c14;color:#FFF;\">");
                sb.Append("当前叫号");
                sb.Append("</td>");
                sb.Append("<td style=\"background:#2689cd;color:#FFF;\">");
                sb.Append("医生");
                sb.Append("</td>");
                sb.Append("</tr>");

                if (ticketList != null && ticketList.Count > 0)
                {
                    for (int i = 0; i < tableSize; i++)
                    {
                         if(i< ticketList.Count)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td>");
                            sb.Append(AdapterUtil.GetCounterByStafferNo(ticketList[i].sStafferNo).sCounterName);
                            sb.Append("</td>");
                            sb.Append("<td>");
                            sb.Append(ticketList[i].sTicketNo+" "+ getMaskName(ticketList[i].sCnName, maskFlag));
                            sb.Append("</td>");
                            sb.Append("<td>");
                            sb.Append(PageHelper.getStafferInfoNameByNo(ticketList[i].sStafferNo));
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }
                        else
                        {
                            sb.Append("<tr>");
                            sb.Append("<td>");
                            sb.Append("&nbsp;");
                            sb.Append("</td>");
                            sb.Append("<td>");
                            sb.Append("&nbsp;");
                            sb.Append("</td>");
                            sb.Append("<td>");
                            sb.Append("&nbsp;");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < tableSize; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>");
                        sb.Append("&nbsp;");
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append("&nbsp;");
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append("&nbsp;");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getAllWaitingListByCounterIds(string branchNo, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int tableIndex = 1;
                int tableSize = int.Parse(num);
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                 

                string sWhere = "  DataFlag=0 And BranchNo = '" + branchNo + "' And  ProcessState Between " + PublicConsts.PROCSTATE_WAITING + " And " + PublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = tableIndex;
                s_model.iPageSize = tableSize;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;
                ViewTicketFlows tempFlows = null;

                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                sb.Append("<tr>");
                sb.Append("<td colspan=\"2\" id=\"WTitle\">");
                sb.Append("等候就诊");
                sb.Append("</td>");
                sb.Append("</tr>");

                if (ticketList != null && ticketList.Count > 0)
                {
                    for (int i = 0; i < tableSize; i++)
                    {
                        if (i % 2 == 0)
                        {
                            sb.Append("<tr>");
                        }
                        if (i < ticketList.Count)
                        {
                            sb.Append("<td>" + ticketList[i].sTicketNo + " " + getMaskName(ticketList[i].sCnName, maskFlag) + "</td>");

                        }
                        else
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        if (i % 2 == 1)
                        {
                            sb.Append("</tr>");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < tableSize / 2; i++)
                    {
                        sb.Append("<tr><td>&nbsp;</td><td>&nbsp;</td></tr>");
                    }
                }
                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getAllPassedListByCounterIds(string branchNo, string num)
        {
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int tableIndex = 1;
                int tableSize = int.Parse(num);
                DateTime workDate = DateTime.Now;
                StringBuilder sb = new StringBuilder();

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());


                string sWhere = " BranchNo = '" + branchNo + "' And  ProcessState = " + PublicConsts.PROCSTATE_NONARRIVAL + "  And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = tableIndex;
                s_model.iPageSize = tableSize;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " EnqueueTime ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsCollections ticketColl = ticketBoss.GetRecordsByPaging(s_model);
                List<ViewTicketFlows> ticketList = AdapterUtil.ToViewList<ViewTicketFlows>(ticketColl);
                List<ViewTicketFlows> tempList = null;
                ViewTicketFlows tempFlows = null;

                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                sb.Append("<tr>");
                sb.Append("<td colspan=\"2\" id=\"PTitle\">");
                sb.Append("过号患者");
                sb.Append("</td>");
                sb.Append("</tr>");

                if (ticketList != null && ticketList.Count > 0)
                {
                    for (int i = 0; i < tableSize; i++)
                    {
                        if (i % 2 == 0)
                        {
                            sb.Append("<tr>");
                        }
                        if (i < ticketList.Count)
                        {
                            sb.Append("<td>" + ticketList[i].sTicketNo + " " + getMaskName(ticketList[i].sCnName, maskFlag) + "</td>");
                        }
                        else
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        if (i % 2 == 1)
                        {
                            sb.Append("</tr>");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < tableSize / 2; i++)
                    {
                        sb.Append("<tr><td>&nbsp;</td><td>&nbsp;</td></tr>");
                    }
                }
                sb.Append("</table>");

                codeData.data = sb.ToString();

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        #endregion
    }
}