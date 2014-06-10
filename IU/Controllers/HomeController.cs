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
        public ActionResult Index(string radio, string origem, string destino, DateTime partida, DateTime retorno, int assentos=1)
        {
            ViewBag.cidades = DominioRegistro.cidadeService().todasCidades();

            List<VooData> voos = new List<VooData>();

            foreach(VooData voo in  vooService.todosVoos()){
                if (voo.cidadeOrigemId.Equals(origem) &
                    voo.cidadeDestinoId.Equals(destino) &
                    voo.partida.Date.CompareTo(partida.Date) == 0)
                    voos.Add(voo);

                if (radio == "idavolta")
                {
                    if (voo.cidadeOrigemId.Equals(destino) &
                        voo.cidadeDestinoId.Equals(origem) &
                        voo.partida.Date.CompareTo(retorno.Date) == 0)
                        voos.Add(voo);
                }
            }


            return View(voos);
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
