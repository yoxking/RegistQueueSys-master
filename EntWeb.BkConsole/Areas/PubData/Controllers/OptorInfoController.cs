using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using EntFrm.Framework.Web.Controls;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Areas.PubData.Controllers
{
    public class OptorInfoController : frmMainController
    {
        //
        // GET: /System/Role/
        public override ActionResult Index()
        {
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                SUsersInfo info = infoBLL.GetRecordByNo(sSuNo);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            { }
            return View();
        }

        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string sNo = Request.Form["sSuNo"].ToString();
                string sTrueName = Request.Form["sTrueName"].ToString(); 
                string sDeptNo = Request.Form["sDeptNo"].ToString();
                int iSex = Request.Form["iSex"].ToInt();
                string sUIDNo = Request.Form["sUIDNo"].ToString();
                string sTelphone = Request.Form["sTelphone"].ToString();
                string sEMail = Request.Form["sEMail"].ToString();
                string sContactAddress = Request.Form["sContactAddress"].ToString();

                SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                SUsersInfo info = infoBLL.GetRecordByNo(sNo);

                //新增操作
                if (info != null)
                {
                    info.sTrueName = sTrueName;
                    info.sDeptNo = sDeptNo;
                    info.iSex = iSex;
                    info.sUIDNo = sUIDNo;
                    info.sTelphone = sTelphone;
                    info.sEMail = sEMail; 

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
                json.Message = "保存信息时发生内部错误！" + ex.Message;
            }
            return Json(json);
        }

        public ActionResult Psword()
        {
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                SUsersInfo info = infoBLL.GetRecordByNo(sSuNo);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            { }
            return View();
        }

        public ActionResult UpdatePsword()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {

                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string sNo = Request.Form["sSuNo"].ToString();
                string sPassword0 = Request.Form["sOldPsword"].ToString();
                string sPassword = Request.Form["sNewPsword"].ToString();
                string sPassword2 = Request.Form["sNewPsword2"].ToString();

                if (sPassword2.Equals(sPassword))
                {
                    SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                    SUsersInfo info = infoBLL.GetRecordByNo(sNo);

                    //新增操作
                    if (info != null)
                    {
                        if (info.sPassword.Equals(sPassword0))
                        {
                            info.sPassword = sPassword;

                            info.sModOptor = sSuNo;
                            info.dModDate = DateTime.Now;

                            if (infoBLL.UpdateRecord(info))
                            {
                                json.Message = "保存成功";
                                json.Status = "Success";
                            }
                        }
                        else
                        {
                            json.Message = "原始密码错误！";
                            json.Status = "Failure";
                        }
                    }
                }
                else
                {
                    json.Message = "两次输入的密码不相同！";
                    json.Status = "Failure";
                }
            }
            catch (Exception ex)
            {
                json.Message = "保存信息时发生内部错误！" + ex.Message;
            }
            return Json(json);
        }
    }
}