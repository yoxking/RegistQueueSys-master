using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using EntFrm.Framework.Web.Controls;

using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Areas.BussData.Controllers
{
    public class MessageInfoController : frmMainController
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
         
        public override ActionResult List()
        {
            try
            {
                PageIndex = int.Parse(Request.Form["pageIndex"] == null ? "1" : Request.Form["pageIndex"].ToString());
                Condition = " BranchNo='" + PublicHelper.Get_BranchNo() + "'";
                if (!string.IsNullOrEmpty(sWhere))
                {
                    Condition += " And " + sWhere;
                }

                MessageInfoBLL infoBLL = new MessageInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                MessageInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
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
         
        public override ActionResult Search()
        {
            sWhere = "1=1 ";

            //if (!string.IsNullOrEmpty(sTrueName))
            //{
            //    sWhere += " And (TrueName like '%" + sTrueName + "%'  OR LoginId like '%" + sTrueName + "%' )";
            //}

            return RedirectToAction("List");
        }

        public override ActionResult Add()
        {
            try
            {
                MessageInfo info = new MessageInfo();
                info.sMessNo = CommonHelper.Get_New12ByteGuid();
                info.dSendDate = DateTime.Now;
                info.dReceiveDate = DateTime.Now;

                ViewBag.StackHolder = info;

            }
            catch (Exception ex)
            {
            }
            return View("Edit");
        }
         
        public override ActionResult Edit(string id)
        {
            try
            {
                MessageInfoBLL infoBLL = new MessageInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                MessageInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        [HttpPost] 
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string MessNo = Request.Form["MessNo"].ToString();
                string MSender = Request.Form["MSender"].ToString();
                string MReceiver = Request.Form["MReceiver"].ToString();
                int MType = 1;
                string MTitle = Request.Form["MTitle"].ToString();
                string MContent = Request.Form["MContent"].ToString();
                string AttachFile = Request.Form["AttachFile"].ToString();
                DateTime SendDate = DateTime.Parse(Request.Form["SendDate"].ToString());
                DateTime ReceiveDate = DateTime.Parse(Request.Form["ReceiveDate"].ToString());
                int ReadState = Request.Form["ReadState"].ToInt();
                string Comments = Request.Form["Comments"].ToString();


                MessageInfoBLL infoBLL = new MessageInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                MessageInfo info = infoBLL.GetRecordByNo(MessNo);

                //新增操作
                if (info == null)
                {
                    info = new MessageInfo();
                    info.sMessNo = MessNo;
                    info.sMSender = MSender;
                    info.sMReceiver = MReceiver;
                    info.iMType = MType;
                    info.sMTitle = MTitle;
                    info.sMContent = MContent;
                    info.sAttachFile = AttachFile;
                    info.dSendDate = SendDate;
                    info.dReceiveDate = ReceiveDate;
                    info.iReadState = ReadState;
                    info.sBranchNo = PublicHelper.Get_BranchNo();

                    info.sAddOptor = sSuNo;
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = sSuNo;
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sAppCode = PublicHelper.Get_AppCode() + ";";
                    info.sComments = Comments;

                    if (infoBLL.AddNewRecord(info))
                    {
                        json.Message = "保存成功";
                        json.Status = "Success";
                    }
                }
                //更新操作
                else
                {
                    info.sMSender = MSender;
                    info.sMReceiver = MReceiver;
                    info.iMType = MType;
                    info.sMTitle = MTitle;
                    info.sMContent = MContent;
                    info.sAttachFile = AttachFile;
                    info.dSendDate = SendDate;
                    info.dReceiveDate = ReceiveDate;
                    info.iReadState = ReadState;
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
                json.Message = "保存人员信息发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }
         
        public override ActionResult Delete(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                MessageInfoBLL infoBLL = new MessageInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                infoBLL.SoftDeleteRecord(sNos);
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        } 

        public override ActionResult Detail(string id)
        {
            try
            {
                MessageInfoBLL infoBLL = new MessageInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                MessageInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}