﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstDemo.OA.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewData["name"] = LoginUser.UName;
            return View();
        }
        public ActionResult HomePage()
        {
            return View();
        }
    }
}