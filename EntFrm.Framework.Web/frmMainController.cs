using System.Web.Mvc;

namespace EntFrm.Framework.Web
{
    public class frmMainController : frmBaseController
    {
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageCount = 1;
        public int PageSize = 10;
        public int PageIndex = 1;
        public string Condition = "";
        public ViewExpress viewExpress;

        public frmMainController():base()
        {
            viewExpress = new ViewExpress();
            viewExpress.HomeUrl = "/";
            viewExpress.ViewTag = "MainFrame";
            viewExpress.ViewTitle = "分诊台";
            viewExpress.VSubtitle = "分诊管理系统v1.0";
            viewExpress.ViewDesc = "分诊管理系统v1.0";
            viewExpress.UserName = "admin";
            viewExpress.UserEmail = "admin@163.com";
            viewExpress.WaitingNum = 0;
            viewExpress.RegisteNum = 0;

        }
         
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult List()
        {
            return View();
        }

        public virtual ActionResult Search()
        {
            return View();
        }

        //
        // GET: /Account/Role/Create

        public virtual ActionResult Add()
        {
            return View();
        }

        public virtual ActionResult Edit(string id)
        { 
            return View();
        } 

        [HttpPost]
        public virtual ActionResult Save()
        {
            return View();
        }

        public virtual ActionResult Delete(string ids)
        {
            return View();
        }

        public virtual ActionResult Detail(string id)
        {
            return View();
        }
    }
}
