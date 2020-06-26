using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using EntFrm.Framework.Web.Controls;

using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Areas.ShowData.Controllers
{
    public class PublishFlowsController : frmMainController
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

                DsPublishFlowsBLL infoBLL = new DsPublishFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPublishFlowsCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
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
                DsPublishFlows info = new DsPublishFlows();
                info.sPFlowNo = CommonHelper.Get_New12ByteGuid();
                info.sPlayerNos = " ";
                info.dStartDate = DateTime.Now;
                info.dEnditDate = DateTime.Now;
                info.sStartTime = DateTime.Now.ToString("HH:mm:ss");
                info.sEnditTime = DateTime.Now.ToString("HH:mm:ss");
                info.sPlayWeeks = "1,2,3,4,5,6,0";
                info.dPublishDate = DateTime.Now;

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
                DsPublishFlowsBLL infoBLL = new DsPublishFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPublishFlows info = infoBLL.GetRecordByNo(id);

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
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string sNo = Request.Form["PFlowNo"].ToString();
                string ProgmNo = Request.Form["ProgramId"].ToString();
                string ProgmType = Request.Form["ProgmType"].ToString();
                string PlayMode = Request.Form["PlayMode"].ToString();
                string PlayerNos = Request.Form["PlayerIds"].ToString();
                string StartDate = Request.Form["StartDate"].ToString();
                string EnditDate = Request.Form["EnditDate"].ToString();
                string PlayWeeks = Request.Form["PlayWeeks"].ToString(); 
                string StartTime = Request.Form["StartTime"].ToString();
                string EnditTime = Request.Form["EnditTime"].ToString();
                string DSchedule = Request.Form["DSchedule"].ToString();
                string Comments = Request.Form["Comments"].ToString(); 

                DsPublishFlowsBLL infoBLL = new DsPublishFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPublishFlows info = infoBLL.GetRecordByNo(sNo);

                //新增操作
                if (info == null)
                {
                    info = new DsPublishFlows();
                    info.sPFlowNo = sNo;
                    info.iDataFlag = 0;
                    info.sProgmNo = ProgmNo;
                    info.sProgmType = ProgmType;
                    info.sPlayerNos = PlayerNos;
                    info.sPlayMode = PlayMode;
                    info.sPlayWeeks = PlayWeeks;
                    info.dStartDate = DateTime.Parse(StartDate);
                    info.dEnditDate = DateTime.Parse(EnditDate);
                    info.sStartTime = StartTime;
                    info.sEnditTime = EnditTime;
                    info.iPublishState = 1;
                    info.sPublishOptor = sSuNo;
                    info.dPublishDate = DateTime.Parse(DSchedule);
                    info.iCheckState = 1;
                    info.sCheckOptor = sSuNo;
                    info.dCheckDate = DateTime.Now;
                    info.sBranchNo = PublicHelper.Get_BranchNo() ;

                    info.sAddOptor = sSuNo;
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = sSuNo;
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sAppCode = PublicHelper.Get_AppCode() + ";";

                    if (infoBLL.AddNewRecord(info))
                    {
                        addDownloadFlows(ProgmNo, PlayerNos, sNo, DSchedule);

                        json.Message = "保存成功";
                        json.Status = "Success";
                    }
                }
                //更新操作
                else
                {
                    info.sProgmNo = ProgmNo;
                    info.sProgmType = ProgmType;
                    info.sPlayerNos = PlayerNos;
                    info.sPlayMode = PlayMode;
                    info.sPlayWeeks = PlayWeeks;
                    info.dStartDate = DateTime.Parse(StartDate);
                    info.dEnditDate = DateTime.Parse(EnditDate);
                    info.sStartTime = StartTime;
                    info.sEnditTime = EnditTime;
                    info.sPublishOptor = sSuNo;
                    info.dPublishDate = DateTime.Parse(DSchedule);

                    info.sModOptor = sSuNo;
                    info.dModDate = DateTime.Now;

                    if (infoBLL.UpdateRecord(info))
                    {
                        addDownloadFlows(ProgmNo, PlayerNos, sNo, DSchedule);

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

        private void addDownloadFlows(string programNo,string playerNos,string publishNo,string dschedule)
        {
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;
                string[] playerArr = playerNos.Split(';');
                DsDwloadFlowsBLL infoBLL = new DsDwloadFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                infoBLL.HardDeleteByCondition(" PublishNo='" + publishNo + "' ");
                DsDwloadFlows dflow = null;

                foreach (string pno in playerArr)
                {
                    dflow = new DsDwloadFlows();

                    dflow.sDFlowNo = CommonHelper.Get_New12ByteGuid();
                    dflow.iDataFlag = 1;
                    dflow.sProgmNo = programNo;
                    dflow.sPlayerNo = pno;
                    dflow.sPublishNo = publishNo;
                    dflow.dDSchedule = DateTime.Parse(dschedule);
                    dflow.iIssueStatus = 0;
                    dflow.dIssueDate = DateTime.Now;
                    dflow.iIFailCount = 0;
                    dflow.iISucCount = 0;
                    dflow.sDloadProgress = "";
                    dflow.sDloadStatus = "";
                    dflow.sBranchNo = PublicHelper.Get_BranchNo();

                    dflow.sAddOptor = sSuNo;
                    dflow.dAddDate = DateTime.Now;
                    dflow.sModOptor = sSuNo;
                    dflow.dModDate = DateTime.Now;
                    dflow.iValidityState = 1;
                    dflow.sAppCode = PublicHelper.Get_AppCode() + ";";

                    infoBLL.AddNewRecord(dflow);
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
        }


        // GET: /System/Role/Delete/5
        //[(Message = "信息删除(Delete)")]
        public override ActionResult Delete(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                DsPublishFlowsBLL infoBLL = new DsPublishFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
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
                DsPublishFlowsBLL infoBLL = new DsPublishFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPublishFlows info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}