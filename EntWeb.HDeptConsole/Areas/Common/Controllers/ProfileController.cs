using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Framework.Web;
using System;
using System.Web.Mvc;

namespace EntWeb.HDeptConsole.Areas.Common.Controllers
{
    public class ProfileController : frmMainController
    {
        public ProfileController()
        {
            ViewBag.ViewExpress = PublicHelper.Get_ViewExpress();
        }

        // GET: Common/Profile
        override
        public ActionResult Index()
        {
            LoginerInfo loginInfo=(LoginerInfo)Session["loginUser"];

            SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            SUsersInfo info = infoBLL.GetRecordByNo(loginInfo.UserNo);

            ViewBag.Info = info;
            return View();
        }

        
        public void SaveProfile(string sUserNo,string sTrueName,string sTelphone,string sEMail,string sComments)
        {
            SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            SUsersInfo info = infoBLL.GetRecordByNo(sUserNo);

            if (info != null )
            {
                info.sTrueName = sTrueName;
                info.sTelphone = sTelphone;
                info.sEMail = sEMail;
                info.sComments = sComments;
                info.dModDate = DateTime.Now;

                if (infoBLL.UpdateRecord(info))
                {
                    Response.Write("SUCCESS");
                }
                else
                {
                    Response.Write("ERROR");
                }
            }
            else
            {
                Response.Write("ERROR");
            }
        }

        public ActionResult Psword()
        {
            LoginerInfo loginInfo = (LoginerInfo)Session["loginUser"];

            ViewBag.Info = loginInfo;
            return View();
        }

        public void UpdatePswd(string sUserNo,string sOldPsword,string sNewPsword)
        { 
            SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            SUsersInfo info = infoBLL.GetRecordByNo(sUserNo);

            if (info != null&&info.sPassword.Equals(sOldPsword))
            {
                info.sPassword = sNewPsword;
                info.dModDate = DateTime.Now;

                if (infoBLL.UpdateRecord(info))
                {
                    Response.Write("SUCCESS");
                }
                else
                {
                    Response.Write("ERROR");
                }
            }
            else
            {
                Response.Write("ERROR");
            }
        }
    }
}