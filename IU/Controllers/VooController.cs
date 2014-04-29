using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaNet.PassagemAerea.Aplicacao.Avioes;
using AlphaNet.PassagemAerea.Aplicacao.Avioes.Data;
using AlphaNet.PassagemAerea.Aplicacao.Voos;
using AlphaNet.PassagemAerea.Aplicacao.Voos.Data;
using AlphaNet.PassagemAerea.Aplicacao.Cidades;

namespace IU.Controllers
{
    public class VooController : Controller
    {

        public ActionResult Index()
        {
            VooService vooService = new VooService();
            return View(vooService.todosVoos());
        }

        public ActionResult Novo()
        {
            AviaoService aviaoService = new AviaoService();
            CidadeService cidadeService = new CidadeService();

            ViewBag.vooData = new VooData();
            ViewBag.avioes = aviaoService.todosAvioes();
            ViewBag.cidades = cidadeService.todasCidades();

            return View("Form",ViewBag);
        }

        [HttpPost]
        public ActionResult Salvar(VooData voo)
        {
            VooService vooService = new VooService();

            if (voo.vooId == null)
                vooService.novoVoo(voo.aviaoId, voo.cidadeOrigemId, voo.cidadeDestinoId, voo.partida);
            
            return RedirectToAction("Index", "Voo");
        }

        public ActionResult Excluir(string aviaoId = "")
        {
            AviaoService aviaoService = new AviaoService();
            aviaoService.excluirAviao(aviaoId);
            return RedirectToAction("Index", "Voo");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

    }
}
