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
        public ActionResult Salvar(AviaoData aviao)
        {
            AviaoService aviaoService = new AviaoService();

            if (aviao.aviaoId == null)
            {
                aviaoService.novoAviao(aviao.modelo, aviao.assentos);
            }
            else 
            {
                aviaoService.alterarDados(aviao.aviaoId, aviao.modelo, aviao.assentos);
            }              
            
            return RedirectToAction("Index", "Aviao");
        }

        public ActionResult Editar(string aviaoId="")
        {
            AviaoService aviaoService = new AviaoService();
            AviaoData aviaoData =  aviaoService.obterAviao(aviaoId);

            return View("Form", aviaoData);
        }

        public ActionResult Excluir(string aviaoId = "")
        {
            AviaoService aviaoService = new AviaoService();
            aviaoService.excluirAviao(aviaoId);
            return RedirectToAction("Index", "Aviao");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

    }
}
