using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaNet.PassagemAerea.Aplicacao.Avioes;
using AlphaNet.PassagemAerea.Aplicacao.Avioes.Data;

namespace IU.Controllers
{
    public class AviaoController : Controller
    {

        public ActionResult Index()
        {
            AplicacaoAviaoService aviaoService = new AplicacaoAviaoService();
            /*List<AviaoData> avioes = new List<AviaoData>();

            foreach(AviaoData data in aviaoService.todosAvioes()){
                AviaoData aviao = new AviaoData();
                aviao.modelo = data.modelo;
                aviao.assentos = data.assentos;
                avioes.Add(aviao);
            }
            */
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
            AviaoData aviaoData =  aviaoService.obterAviao(aviaoId);

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

    }
}
