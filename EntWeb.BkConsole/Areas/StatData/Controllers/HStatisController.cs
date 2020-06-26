using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using EntFrm.Framework.Web.Controls;
using EntWeb.BkConsole.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Areas.StatData.Controllers
{
    public class HStatisController : frmMainController
    {

        // GET: StatData/TStatis
        public override ActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: StatData/TFlows
        public override ActionResult List()
        {
            try
            {

            }
            catch (Exception ex)
            { }
            return View();
        } 
    }
}