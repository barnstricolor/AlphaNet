using AlphaNet.PassagemAerea.Aplicacao.Voos.Data;
using AlphaNet.PassagemAerea.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IU.Controllers
{
    public class HomeController : AbstractController
    {
        public ActionResult Index()
        {
            ViewBag.msgAutenticacao = TempData["msgAutenticacao"];
            ViewBag.cidades = DominioRegistro.cidadeService().todasCidades();
            return View(new List<VooData>());
        }
        [HttpPost]
        public ActionResult Index(string radio, string origem, string destino, DateTime partida, DateTime retorno, int assentos)
        {
            ViewBag.msgAutenticacao = TempData["msgAutenticacao"];
            ViewBag.cidades = DominioRegistro.cidadeService().todasCidades();
            return View(vooService.todosVoos());
        }

        public ActionResult Index_adm()
        {
            if (!usuarioLogadoGestor())
                return RedirectToAction("Index", "Home");
            return View();
        }
        public ActionResult LoginNovaReserva()
        {
            return View();
        }
     
    }
}
