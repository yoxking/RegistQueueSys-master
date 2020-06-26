using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Web;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EntWeb.HDeptConsole.Areas.Common.Controllers
{
    public class MessageController : frmMainController
    {
        public MessageController()
        {
            viewExpress = PublicHelper.Get_ViewExpress();
            viewExpress.ViewTag = "MessageView";
            ViewBag.ViewExpress = viewExpress;
        }

        // GET: Common/Message
        override
        public ActionResult Index()
        {
            ViewBag.ItemList = PageService.GetStaffList(true);
            return View();
        } 

        public ActionResult getDataList_Message(int pageIndex = 1, int pageSize = 20, string condition = "")
        {
            try
            {
                TableData tdata = new TableData();
                string strWhere = "  BranchNo='" + PublicHelper.Get_BranchNo() + "' And SendDate  Between '"+DateTime.Now.ToString("yyyy-MM-dd 00:00:00")+"' And '"+ DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                if (!string.IsNullOrEmpty(condition) && !("99999999").Equals(condition))
                {
                    strWhere += " And MSender='" + condition + "' ";
                }

                MessageInfoBLL infoBLL = new MessageInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                MessageInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, pageIndex, pageSize, strWhere);
                int count = infoBLL.GetCountByCondition(Condition);

                tdata.code = 0;
                tdata.msg = "";
                tdata.count = count;
                tdata.data = PublicHelper.ConvertMessageList(infoColl);

                return Json(tdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        override
        public ActionResult Detail(string id)
        {

            MessageInfoBLL infoBLL = new MessageInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            MessageInfo info = infoBLL.GetRecordByNo(id);

            if (info != null&&info.iReadState==0)
            {
                info.iReadState = 1;
                info.dReceiveDate = DateTime.Now;
                info.dModDate = DateTime.Now;

                infoBLL.UpdateRecord(info);
            }

            ViewBag.Message = info;
            return View();
        }
    }
}