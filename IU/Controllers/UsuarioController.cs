using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaNet.PassagemAerea.Aplicacao.Cidades;
using AlphaNet.PassagemAerea.Aplicacao.Cidades.Data;
using Alphanet.Acesso.Aplicacao;
using Alphanet.Acesso.Aplicacao.Data;


namespace IU.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Cidade/

        public ActionResult Index()
        {
            AcessoAplicacaoService acessoAplicacaoService = new AcessoAplicacaoService();
            return View(acessoAplicacaoService.todosUsuarios());
        }

        public ActionResult Novo() {
            return View("Form", new UsuarioData());
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
