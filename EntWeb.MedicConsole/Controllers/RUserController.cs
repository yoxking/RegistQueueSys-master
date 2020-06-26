using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Framework.Web;
using EntWeb.MedicConsole.Common;
using System.Web.Mvc;

namespace EntWeb.MedicConsole.Controllers
{
    public class RUserController : frmMainController
    {
        // GET: RUser
        override
        public ActionResult Index()
        {
            return View();
        }

        // GET: Profile
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