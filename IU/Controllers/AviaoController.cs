using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IU.Models;
using AlphaNet.PassagemAerea.Aplicacao;

namespace IU.Controllers
{
    public class AviaoController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NovoAviao()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NovoAviao(Aviao aviao)
        {
            AviaoService aviaoService = new AviaoService();

            aviaoService.novoAviao(aviao.modelo, aviao.assentos);

            return View();
        }




    }
}
