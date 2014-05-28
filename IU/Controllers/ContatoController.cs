using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IU.Controllers
{
    public class ContatoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string nome, string email, string telefone, string cidade, string uf, string assunto,
            string observacoes)
        {
            return View();
        }
    
     
    }
}
