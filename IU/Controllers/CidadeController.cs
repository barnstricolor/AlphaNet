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

    }
}
