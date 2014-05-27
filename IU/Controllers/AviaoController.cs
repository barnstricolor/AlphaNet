using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaNet.PassagemAerea.Aplicacao.Avioes;
using IU.Models;
namespace IU.Controllers
{
    public class AviaoController : Controller
    {

        public ActionResult Index()
        {
            AplicacaoAviaoService aviaoService = new AplicacaoAviaoService();
            return View(aviaoService.todosAvioes());
        }

        public ActionResult Novo()
        {
            return View("Form",new AviaoData());
        }

        [HttpPost]
        public ActionResult Salvar(AviaoData aviao)
        {
            AplicacaoAviaoService aviaoService = new AplicacaoAviaoService();

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
            AplicacaoAviaoService aviaoService = new AplicacaoAviaoService();
            AviaoData aviaoData = converterParaIu(aviaoService.obterAviao(aviaoId));

            return View("Form", aviaoData);
        }

        public ActionResult Excluir(string aviaoId = "")
        {
            AplicacaoAviaoService aviaoService = new AplicacaoAviaoService();
            aviaoService.excluirAviao(aviaoId);
            return RedirectToAction("Index", "Aviao");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }
        private AviaoData converterParaIu(AlphaNet.PassagemAerea.Aplicacao.Avioes.Data.AviaoData data) {
            AviaoData result = new AviaoData();

            result.assentos = data.assentos;
            result.aviaoId = data.aviaoId;
            result.modelo = data.modelo;

            return result;
        }
    }
}
