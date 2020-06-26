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
using System.Web.Mvc;

namespace EntWeb.BkConsole.Areas.ShowData.Controllers
{
    public class VersionInfoController : frmMainController
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
                Condition = sWhere;

                DsVersionInfoBLL infoBLL = new DsVersionInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsVersionInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
                int totalCount = infoBLL.GetCountByCondition(Condition);

                PagerHelper pager = new PagerHelper(PageIndex, PageSize, totalCount);

                Dictionary<string, object> stackHolder = new Dictionary<string, object>();
                stackHolder.Add("infoList", infoColl);
                stackHolder.Add("pager", pager);
                ViewBag.StackHolder = stackHolder;
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
            sWhere = "1=1 ";

            //if (!string.IsNullOrEmpty(sTrueName))
            //{
            //    sWhere += " And (TrueName like '%" + sTrueName + "%'  OR LoginId like '%" + sTrueName + "%' )";
            //}

            return RedirectToAction("List");
        }

        //
        // GET: /System/Role/Add
        //[(Message = "信息添加(Add)")]
        public override ActionResult Add()
        {
            try
            {
                DsVersionInfo info = new DsVersionInfo();
                info.sVerNo = CommonHelper.Get_New12ByteGuid();

                ViewBag.StackHolder = info; 
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
                DsVersionInfoBLL infoBLL = new DsVersionInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsVersionInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info; 
            }
            catch (Exception ex)
            {
            }
            return View();
        } 

        [HttpPost]
        //[(Message = "信息保存(Save)")]
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string sAppUrl = PublicHelper.GetConfigValue("AppUrl");
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string sNo = Request.Form["VerNo"].ToString();
                string sVerName = Request.Form["VerName"].ToString();
                string sVerType = Request.Form["VerType"].ToString();
                string sUpMode = Request.Form["UpMode"].ToString();
                string sVerCode = Request.Form["VerCode"].ToString();
                string sPlatform = Request.Form["Platform"].ToString();
                string sFilePath = Request.Form["FilePath"].ToString();
                string sPlayerNos = Request.Form["PlayerIds"].ToString();
                string sVerDesc = Request.Form["VerDesc"].ToString();
                string Comments = Request.Form["Comments"].ToString();

                DsVersionInfoBLL infoBLL = new DsVersionInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsVersionInfo info = infoBLL.GetRecordByNo(sNo);

                //新增操作
                if (info == null)
                {
                    info = new DsVersionInfo();
                    info.sVerNo = sNo;
                    info.sVerName = sVerName;
                    info.sVerType = sVerType;
                    info.sAppStart = "";
                    info.sUpMode = sUpMode;
                    info.sVerCode = sVerCode;
                    info.sPlatform = sPlatform;
                    info.sFileUrl = sAppUrl+sFilePath;
                    info.sPlayerNos = sPlayerNos;
                    info.iDLoadHits = 0;
                    info.iCheckState = 0;
                    info.sVerDesc = sVerDesc;
                    info.sComments = Comments;


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
                    info.sVerName = sVerName;
                    info.sVerType = sVerType;
                    info.sAppStart = "";
                    info.sUpMode = sUpMode;
                    info.sVerCode = sVerCode;
                    info.sPlatform = sPlatform;
                    info.sFileUrl = sFilePath;
                    info.sPlayerNos = sPlayerNos;
                    info.sVerDesc = sVerDesc;
                    info.sComments = Comments;

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
                DsVersionInfoBLL infoBLL = new DsVersionInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                infoBLL.SoftDeleteRecord(sNos);
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
                DsVersionInfoBLL infoBLL = new DsVersionInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsVersionInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        public ActionResult Update(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                DsVersionInfoBLL infoBLL = new DsVersionInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsVersionInfo info = null;

                foreach (string No in sNos)
                {
                    info = infoBLL.GetRecordByNo(No);
                    CmmdData command = new CmmdData();
                    command.cmmdName = "doUpdateApk"; 
                    command.cmmdType = "MAdapter";
                    command.cmmdArgs = new string[] { info.sFileUrl,info.sVerCode };

                    string s = JsonConvert.SerializeObject(command);

                    string[] playerNos = info.sPlayerNos.Split(';');
                    foreach (string playerNo in playerNos)
                    {

                        //使用Lamdba表达式
                        new Thread(
                            () =>
                            {
                                DsPlayerInfo p = PageHelper.getPlayerInfoByNo(playerNo);
                                RmtCmdService.CreateInstance().doRemoteCommand(p.sPlayerCode, s);
                            }).Start();
                    }
                }
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }
    }
}