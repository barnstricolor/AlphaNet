using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaNet.Application;

namespace AlphaNet.IU.Controllers
{
    [HandleError]
    public class CidadeController : Controller
    {
        //
        // GET: /Cidade/
        private AviaoService aviaoService = new AviaoService();

    
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Nova()
        {
            return View("Form");
        }

        
    }
}
