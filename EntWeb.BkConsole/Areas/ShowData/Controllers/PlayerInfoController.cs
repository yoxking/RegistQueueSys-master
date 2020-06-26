using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using EntFrm.Framework.Web.Controls;
using EntWeb.BkConsole.Entities; 
using EntWeb.BkConsole.Service;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Areas.ShowData.Controllers
{
    public class PlayerInfoController : frmMainController
    {
        private string sWhere
        {
            set { TempData["Where_" + RouteData.Values["controller"].ToString()] = value; }
            get
            {
                var temp = TempData.Peek("Where_" + RouteData.Values["controller"].ToString());
                if (temp == null)
                {
                    return "";
                }
                return temp.ToString();
            }
        }

        //
        // GET: /System/Role/
        public override ActionResult Index()
        {
            return RedirectToAction("List");
        }


        //[(Message = "信息列表(List)")]
        public override ActionResult List()
        {
            try
            {
                PageIndex = int.Parse(Request.Form["pageIndex"] == null ? "1" : Request.Form["pageIndex"].ToString());
                if (string.IsNullOrEmpty(sWhere))
                {
                    sWhere = " CheckState=1 ";
                }
                Condition = sWhere;

                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPlayerInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, PageSize, Condition);
                int totalCount = infoBLL.GetCountByCondition(Condition);

                PagerHelper pager = new PagerHelper(PageIndex, PageSize, totalCount);

                Dictionary<string, object> stackHolder = new Dictionary<string, object>();
                stackHolder.Add("infoList", infoColl);
                stackHolder.Add("pager", pager);
                ViewBag.StackHolder = stackHolder;
                ViewBag.ClassList = getPlayerClasses();
            }
            catch (Exception ex)
            { }
            return View();
        } 

        //
        // GET: /System/Role/Search
        //[(Message = "信息查询(Search)")]
        public override ActionResult Search()
        {
            sWhere = " CheckState=1 ";

            string pclassNo = Request.Form["PClassNo"].ToString();
            string keyword = Request.Form["Keyword"].ToString();

            if (!pclassNo.Equals("00000000"))
            {
                sWhere += " And PClassNo='" + pclassNo + "' ";
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                sWhere += " And ( PlayerName like '%" + keyword + "%'  OR IpAddress like '%" + keyword + "%' )";
            }

            return RedirectToAction("List");
        }

        //
        // GET: /System/Role/Add
        //[(Message = "信息添加(Add)")]
        public override ActionResult Add()
        {
            try
            {
                DsPlayerInfo info = new DsPlayerInfo();
                info.sPlayerNo = CommonHelper.Get_New12ByteGuid();

                ViewBag.StackHolder = info;
                ViewBag.ClassList = getPlayerClasses();
            }
            catch (Exception ex)
            {
            }
            return View("Edit");
        }

        ////
        //// GET: /System/Role/Edit/5
        //[(Message = "信息编辑(Edit)")]
        public override ActionResult Edit(string id)
        {
            try
            {
                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPlayerInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
                ViewBag.ClassList = getPlayerClasses();

            }
            catch (Exception ex)
            {
            }
            return View();
        }

        private DsPlayerClassCollections getPlayerClasses()
        {
            DsPlayerClassBLL infoBLL = new DsPlayerClassBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

            return infoBLL.GetAllRecords();
        }

        [HttpPost]
        //[(Message = "信息保存(Save)")]
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string sNo = Request.Form["PlayerNo"].ToString();
                string sPlayerName = Request.Form["PlayerName"].ToString();
                string sPlayerCode = Request.Form["PlayerCode"].ToString();
                string sPClassNo = Request.Form["PClassNo"].ToString();
                string sIpAddress = Request.Form["IpAddress"].ToString(); 
                string iLocalPort = Request.Form["LocalPort"].ToString();
                string sOsVersion = Request.Form["OSVersion"].ToString();
                string sApVersion = Request.Form["ApVersion"].ToString();
                string sComments = Request.Form["Comments"].ToString();

                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPlayerInfo info = infoBLL.GetRecordByNo(sNo);

                //新增操作
                if (info == null)
                {
                    info = new DsPlayerInfo();
                    info.sPlayerNo = sNo;
                    info.sPlayerName = sPlayerName;
                    info.sPlayerCode = sPlayerCode;
                    info.sPClassNo = sPClassNo;
                    info.sIpAddress = sIpAddress;
                    info.sMacAddress = "";
                    info.iLocalPort = int.Parse(iLocalPort);
                    info.sResolution = "1920*1080";
                    info.iOnlineState = 0;
                    info.dOnDuration = 0;
                    info.sOSVersion = sOsVersion;
                    info.sApVersion = sApVersion;
                    info.sStartupTime = "1970-01-01 06:00:00";
                    info.sShutdownTime = "1970-01-01 22:00:00";
                    info.sMachineCode = "";
                    info.iIsAuthorize = 0;
                    info.iCheckState = 0;
                    info.sBranchNo = "";
                    info.sComments = sComments; 


                    info.sAddOptor = sSuNo;
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = sSuNo;
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sAppCode = PublicHelper.Get_AppCode() + ";";

                    if (infoBLL.AddNewRecord(info))
                    {
                        json.Message = "保存成功";
                        json.Status = "Success";
                    }
                }
                //更新操作
                else
                {
                    info.sPlayerName = sPlayerName;
                    info.sPlayerCode = sPlayerCode;
                    info.sPClassNo = sPClassNo;
                    info.sIpAddress = sIpAddress; 
                    info.iLocalPort = int.Parse(iLocalPort);
                    info.sOSVersion = sOsVersion;
                    info.sApVersion = sApVersion;

                    info.iIsAuthorize = 1;
                    info.iCheckState = 1;   //认证通过

                    info.sModOptor = sSuNo;
                    info.dModDate = DateTime.Now;

                    if (infoBLL.UpdateRecord(info))
                    {
                        json.Message = "保存成功";
                        json.Status = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                json.Message = "信息发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }


        // GET: /System/Role/Delete/5
        //[(Message = "信息删除(Delete)")]
        public override ActionResult Delete(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                infoBLL.SoftDeleteRecord(sNos);
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }

        #region 设置控制

        // GET: /System/Role/Delete/5
        //[(Message = "重启设备(Reboot)")]
        public ActionResult Reboot(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPlayerInfo info = null;

                foreach(string No in sNos)
                {
                    info = infoBLL.GetRecordByNo(No); 
                    CmmdData command = new CmmdData();
                    command.cmmdName = "doReboot";
                    command.cmmdType = "MAdapter";
                    command.cmmdArgs = new string[] { "" };

                    string s = JsonConvert.SerializeObject(command); 

                    //使用Lamdba表达式
                    new Thread(
                        () => {
                            RmtCmdService.CreateInstance().doRemoteCommand(info.sPlayerCode,s);
                        }).Start();
                }
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }


        // GET: /System/Role/Delete/5
        //[(Message = "关机设备(Shutdown)")]
        public ActionResult Shutdown(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPlayerInfo info = null;

                foreach (string No in sNos)
                {
                    info = infoBLL.GetRecordByNo(No);
                    CmmdData command = new CmmdData();
                    command.cmmdName = "doShutdown";
                    command.cmmdType = "MAdapter";
                    command.cmmdArgs = new string[] { "" };

                    string s = JsonConvert.SerializeObject(command);

                    //使用Lamdba表达式
                    new Thread(
                        () =>
                        {
                            RmtCmdService.CreateInstance().doRemoteCommand(info.sPlayerCode, s);
                        }).Start();
                }
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }


        // GET: /System/Role/Delete/5
        //[(Message = "关机设备(Shutdown)")]
        public ActionResult StopPlay(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPlayerInfo info = null;

                foreach (string No in sNos)
                {
                    info = infoBLL.GetRecordByNo(No);
                    CmmdData command = new CmmdData();
                    command.cmmdName = "doStopPlay";
                    command.cmmdType = "MAdapter";
                    command.cmmdArgs = new string[] { "" };

                    string s = JsonConvert.SerializeObject(command);

                    //使用Lamdba表达式
                    new Thread(
                        () =>
                        {
                            RmtCmdService.CreateInstance().doRemoteCommand(info.sPlayerCode, s);
                        }).Start();
                }
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }


        //// GET: /System/Role/Delete/5
        ////[(Message = "关机设备(Shutdown)")]
        //public ActionResult StopPlay(string ids)
        //{
        //    JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
        //    try
        //    {
        //        string[] sNos = ids.Split(';');
        //        DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
        //        DsPlayerInfo info = null;

        //        foreach (string No in sNos)
        //        {
        //            string[] powerTime = new string[] {"13,50,00","13,40,00" };

        //            info = infoBLL.GetRecordByNo(No);
        //            CmmdData command = new CmmdData();
        //            command.cmmdName = "doPowerOnOff";
        //            command.cmmdType = "MAdapter";
        //            command.cmmdArgs = new string[] { DateTime.Now.ToString("yyyy,MM,dd,") + powerTime[0], DateTime.Now.ToString("yyyy,MM,dd,") + powerTime[1] };


        //            string s = JsonConvert.SerializeObject(command);

        //            //使用Lamdba表达式
        //            new Thread(
        //                () => {
        //                    RmtCmdService.CreateInstance().doRemoteCommand(info.sPlayerCode, s);
        //                }).Start();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        json.Message = "操作时发生内部错误！" + ex.Message;
        //        json.Status = "Failure";
        //    }
        //    return Json(json);
        //}

        public ActionResult RestartPlay(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPlayerInfo info = null;

                foreach (string No in sNos)
                {
                    info = infoBLL.GetRecordByNo(No);
                    CmmdData command = new CmmdData();
                    command.cmmdName = "doRestarPlay";
                    command.cmmdType = "MAdapter";
                    command.cmmdArgs = new string[] { "" };

                    string s = JsonConvert.SerializeObject(command);

                    //使用Lamdba表达式
                    new Thread(
                        () => {
                            RmtCmdService.CreateInstance().doRemoteCommand(info.sPlayerCode, s);
                        }).Start();
                }
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }
        public ActionResult ClearPlay(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPlayerInfo info = null;

                foreach (string No in sNos)
                {
                    info = infoBLL.GetRecordByNo(No);
                    CmmdData command = new CmmdData();
                    command.cmmdName = "doClearPlay";
                    command.cmmdType = "MAdapter";
                    command.cmmdArgs = new string[] { "" };

                    string s = JsonConvert.SerializeObject(command);

                    //使用Lamdba表达式
                    new Thread(
                        () => {
                            RmtCmdService.CreateInstance().doRemoteCommand(info.sPlayerCode, s);
                        }).Start();
                }
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }
        public ActionResult VolumeMin(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPlayerInfo info = null;

                foreach (string No in sNos)
                {
                    info = infoBLL.GetRecordByNo(No);
                    CmmdData command = new CmmdData();
                    command.cmmdName = "doSetVolume";
                    command.cmmdType = "MAdapter";
                    command.cmmdArgs = new string[] { "0" };

                    string s = JsonConvert.SerializeObject(command);

                    //使用Lamdba表达式
                    new Thread(
                        () => {
                            RmtCmdService.CreateInstance().doRemoteCommand(info.sPlayerCode, s);
                        }).Start();
                }
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }
        public ActionResult VolumeMax(string ids,string volume)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPlayerInfo info = null;

                foreach (string No in sNos)
                {
                    info = infoBLL.GetRecordByNo(No);
                    CmmdData command = new CmmdData();
                    command.cmmdName = "doSetVolume";
                    command.cmmdType = "MAdapter";
                    command.cmmdArgs = new string[] { volume };

                    string s = JsonConvert.SerializeObject(command);

                    //使用Lamdba表达式
                    new Thread(
                        () => {
                            RmtCmdService.CreateInstance().doRemoteCommand(info.sPlayerCode, s);
                        }).Start();
                }
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }

        // GET: /System/Role/Delete/5
        //[(Message = "设备截屏(Snapshot)")]
        public ActionResult Snapshot(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPlayerInfo info = null;

                foreach (string No in sNos)
                {
                    info = infoBLL.GetRecordByNo(No);
                    CmmdData command = new CmmdData();
                    command.cmmdName = "doSnapshot";
                    command.cmmdType = "MAdapter";
                    command.cmmdArgs = new string[] { "" };

                    string s = JsonConvert.SerializeObject(command);

                    //使用Lamdba表达式
                    new Thread(
                        () => {
                            RmtCmdService.CreateInstance().doRemoteCommand(info.sPlayerCode, s);
                        }).Start();
                }
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }

        // GET: /System/Role/Delete/5
        //[(Message = "设备截屏(Snapshot)")]
        public ActionResult SetTimer(string ids,string onTime,string offTime)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPlayerInfo info = null;

                foreach (string No in sNos)
                {
                    info = infoBLL.GetRecordByNo(No);
                    CmmdData command = new CmmdData();
                    command.cmmdName = "doPowerOnOff";
                    command.cmmdType = "MAdapter";
                    command.cmmdArgs = new string[] { onTime,offTime };

                    string s = JsonConvert.SerializeObject(command);

                    //使用Lamdba表达式
                    new Thread(
                        () => {
                            RmtCmdService.CreateInstance().doRemoteCommand(info.sPlayerCode, s);
                        }).Start();
                }
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }
        #endregion

        // GET: /System/Role/Delete/5
        //[(Message = "信息审核/认证(Valid)")]
        public ActionResult Valid(string ids)
        {
            string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPlayerInfo info = null;

                foreach (string m in sNos)
                {
                    info = infoBLL.GetRecordByNo(m);
                    info.iCheckState = 1;
                    info.iIsAuthorize = 1;
                    info.dModDate = DateTime.Now;

                    infoBLL.UpdateRecord(info);
                }
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }

        // GET: /PubsData/Content/Detail/5
        //[(Message = "信息详细(Detail)")]
        public override ActionResult Detail(string id)
        {
            try
            {
                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPlayerInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        public ActionResult Setting(string id)
        {
            try
            {
                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPlayerInfo info = infoBLL.GetRecordByNo(id);

                if (info != null)
                {
                    ViewBag.PlayerId = info.sPlayerNo;
                    ViewBag.PlayerName = info.sPlayerName;
                    if ((!string.IsNullOrEmpty(info.sParamsFmt)))
                    {
                        ParamsData paramsData = JsonConvert.DeserializeObject<ParamsData>(info.sParamsFmt);
                        ViewBag.ServiceNames = PageHelper.getServiceNamesByNos(paramsData.ServiceList);
                        ViewBag.CounterNames = PageHelper.getCounterNamesByNos(paramsData.CounterList);
                        ViewBag.ServiceIds = paramsData.ServiceList;
                        ViewBag.CounterIds = paramsData.CounterList;
                    }
                    else
                    {
                        ViewBag.ServiceNames = "";
                        ViewBag.CounterNames = "";
                        ViewBag.ServiceIds = "";
                        ViewBag.CounterIds = "";
                    }
                }
                else
                {
                    ViewBag.PlayerId = "";
                    ViewBag.PlayerName = "";
                    ViewBag.ServiceNames = "";
                    ViewBag.CounterNames = "";
                    ViewBag.ServiceIds = "";
                    ViewBag.CounterIds = "";
                }
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        public ActionResult SaveSetting()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string playerNo = Request.Form["playerId"].ToString();
                string counterList = Request.Form["counterIds"].ToString();
                string serviceList = Request.Form["serviceIds"].ToString();

                ParamsData paramsData = new ParamsData()
                {
                    CounterList = counterList,
                    ServiceList = serviceList
                };

                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPlayerInfo info = infoBLL.GetRecordByNo(playerNo);

                if (info != null)
                {
                    info.sParamsFmt = JsonConvert.SerializeObject(paramsData);
                    if (infoBLL.UpdateRecord(info))
                    { 
                        json.Message = "保存成功";
                        json.Status = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                json.Message = "信息发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }
    }
}