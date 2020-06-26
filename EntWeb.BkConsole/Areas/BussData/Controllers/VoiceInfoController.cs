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
    public class VoiceInfoController : frmMainController
    {
        private string sWhere
        {
            set { TempData["Where_" + RouteData.Values["controller"].ToString()] = value; }
            get
            {
                var temp = TempData.Peek("Where_" + RouteData.Values["controller"].ToString());
                if (temp == null)
                {
                    return " 1=1 ";
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


        //[(Message = "语音信息表(List)")]
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

                VoiceInfoBLL infoBLL = new VoiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                VoiceInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
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
        //[(Message = "语音信息添加(Add)")]
        public override ActionResult Add()
        {
            try
            {
                VoiceInfo info = new VoiceInfo();
                info.sTtsNo = CommonHelper.Get_New12ByteGuid();
                info.iVolume = 100;
                info.iRate = 0;
                info.sFormatCalling = "请[姓名]到[科室]就诊";
                info.sFormatWaiting = "请[姓名]等候就诊";

                ViewBag.StackHolder = info;

            }
            catch (Exception ex)
            {
            }
            return View("Edit");
        }

        ////
        //// GET: /System/Role/Edit/5
        //[(Message = "语音信息编辑(Edit)")]
        public override ActionResult Edit(string id)
        {
            try
            {
                VoiceInfoBLL infoBLL = new VoiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                VoiceInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        [HttpPost]
        //[(Message = "语音信息保存(Save)")]
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string TtsNo = Request.Form["TtsNo"].ToString();
                string TtsName = Request.Form["TtsName"].ToString(); 
                string Voice = "";
                int Rate = int.Parse(Request.Form["Rate"].ToString());
                string FormatCalling = Request.Form["FormatCalling"].ToString();
                string FormatWaiting = Request.Form["FormatWaiting"].ToString();
                int Volume = int.Parse(Request.Form["Volume"].ToString());
                string Comments = Request.Form["Comments"].ToString();

                VoiceInfoBLL infoBLL = new VoiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                VoiceInfo info = infoBLL.GetRecordByNo(TtsNo);

                //新增操作
                if (info == null)
                {
                    info = new VoiceInfo();

                    info.sTtsNo = TtsNo;
                    info.sTtsName = TtsName;
                    info.sVoice = Voice;
                    info.iRate = Rate;
                    info.iVolume = Volume;
                    info.sPreMusic = "";
                    info.sFormatCalling = FormatCalling;
                    info.sFormatWaiting = FormatWaiting;
                    info.sPostMusic = "";
                    info.sBranchNo = "";
                    info.sComments = Comments;

                    info.sAddOptor = sSuNo;
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = sSuNo;
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sBranchNo = PublicHelper.Get_BranchNo();
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
                    info.sTtsName = TtsName;
                    info.sVoice = Voice;
                    info.iRate = Rate;
                    info.iVolume = Volume; 
                    info.sFormatCalling = FormatCalling;
                    info.sFormatWaiting = FormatWaiting;
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


        // GET: /System/Role/Delete/5
        //[(Message = "员工信息删除(Delete)")]
        public override ActionResult Delete(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                VoiceInfoBLL infoBLL = new VoiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
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
        //[(Message = "语音组信息详细(Detail)")]
        public override ActionResult Detail(string id)
        {
            try
            {
                VoiceInfoBLL infoBLL = new VoiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                VoiceInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}