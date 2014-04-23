using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaNet.PassagemAerea.Aplicacao.Cidades;
using AlphaNet.PassagemAerea.Aplicacao.Cidades.Data;


namespace IU.Controllers
{
    public class CidadeController : Controller
    {
        //
        // GET: /Cidade/

        public ActionResult Index()
        {
            CidadeService cidadeService = new CidadeService();
            return View(cidadeService.todasCidades());
        }

        public ActionResult Nova() {
            return View("Form", new CidadeData());
        }

        [HttpPost]
        public ActionResult Salvar(CidadeData cidade) {
            CidadeService cidadeService = new CidadeService();
            if (cidade.cidadeId == null)
            {
                cidadeService.novaCidade(cidade.nome, cidade.cep);
            }
            else {
                cidadeService.alterarDados(cidade.cidadeId, cidade.nome, cidade.cep);
            }
            return RedirectToAction("Index", "Cidade");
        }

        public ActionResult Editar(string cidadeId = "")
        {
            CidadeService cidadeService = new CidadeService();
            CidadeData cidadeData = cidadeService.obterCidade(cidadeId);
            return View("Form", cidadeData);
        }

        public ActionResult Excluir(string cidadeId = "")
        {
            CidadeService cidadeService = new CidadeService();
            cidadeService.excluirCidade(cidadeId);
            return RedirectToAction("Index", "Cidade");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }
    }
}
