﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IU.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.msgAutenticacao = TempData["msgAutenticacao"];
            return View();
        }
        public ActionResult Index_adm()
        {
            return View();
        }
        public ActionResult LoginNovaReserva()
        {
            return View();
        }
     
    }
}
