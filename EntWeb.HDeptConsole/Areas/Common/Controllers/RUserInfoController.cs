using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Framework.Web;
using System;
using System.Web.Mvc;

namespace EntWeb.HDeptConsole.Areas.Common.Controllers
{
    public class RUserInfoController : frmMainController
    {
        // GET: Common/RUserInfo
        override
        public ActionResult Index()
        {
            return View();
        }

        // GET: Common/RUserInfo
        override
        public ActionResult Detail(string id)
        {
            RUsersInfoBLL infoBLL = new RUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            RUsersInfo info = infoBLL.GetRecordByNo(id);

            ViewBag.RUserInfo = info;
            return View();
        }
    }
}