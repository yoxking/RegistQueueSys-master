using EntFrm.Business.BLL;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Web;
using EntFrm.Framework.Web.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Areas.ShowData.Controllers
{
    public class SelectPlayersController : frmMainController
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
                DsPlayerInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, 50, Condition);
                int totalCount = infoBLL.GetCountByCondition(Condition);

                PagerHelper pager = new PagerHelper(PageIndex, 50, totalCount);

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

        private DsPlayerClassCollections getPlayerClasses()
        {
            DsPlayerClassBLL infoBLL = new DsPlayerClassBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

            return infoBLL.GetAllRecords();
        }
    }
}