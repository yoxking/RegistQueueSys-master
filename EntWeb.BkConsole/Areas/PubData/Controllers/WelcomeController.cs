﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Areas.PubData.Controllers
{
    public class WelcomeController : Controller
    {
        //
        // GET: /PubData/Welcome/

        public ActionResult Index()
        {
            return View();
        }

    }
}
