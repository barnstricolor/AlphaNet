using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaNet.PassagemAerea.Aplicacao.Cidades;
using AlphaNet.PassagemAerea.Domain.Model;
using AlphaNet.PassagemAerea.Aplicacao.Cidades.Data;


namespace IU.Controllers
{
    public class CidadeController : Controller
    {
        //
        // GET: /Cidade/

        public ActionResult Index()
        {
            return View(DominioRegistro.cidadeService().todasCidades());
        }

        public ActionResult Nova() {
            return View("Form", new CidadeData());
        }

        [HttpPost]
        public ActionResult Salvar(CidadeData cidade) {
            if (cidade.cidadeId == null)
            {
                DominioRegistro.cidadeService().novaCidade(cidade.nome, cidade.cep);
            }
            else {
                DominioRegistro.cidadeService().alterarDados(cidade.cidadeId, cidade.nome, cidade.cep);
            }
            return RedirectToAction("Index", "Cidade");
        }

        public ActionResult Editar(string cidadeId = "")
        {
            CidadeData cidadeData = converterParaIu(DominioRegistro.cidadeService().obterCidade(cidadeId));
            return View("Form", cidadeData);
        }

        public ActionResult Excluir(string cidadeId = "")
        {
            DominioRegistro.cidadeService().excluirCidade(cidadeId);
            return RedirectToAction("Index", "Cidade");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }
        private CidadeData converterParaIu(AlphaNet.PassagemAerea.Aplicacao.Cidades.Data.CidadeData data)
        {
            CidadeData result = new CidadeData();

            result.cep = data.cep;
            result.cidadeId = data.cidadeId;
            result.nome = data.nome;

            return result;
        }
    }
}
