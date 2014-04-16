using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IU.Models;
using AlphaNet.PassagemAerea.Aplicacao.Avioes;
using AlphaNet.PassagemAerea.Aplicacao.Avioes.Data;

namespace IU.Controllers
{
    public class AviaoController : Controller
    {

        public ActionResult Index()
        {
            AviaoService aviaoService = new AviaoService();
            List<Aviao> avioes = new List<Aviao>();

            foreach(AviaoData data in aviaoService.todosAvioes()){
                Aviao aviao = new Aviao();
                aviao.modelo = data.modelo;
                aviao.assentos = data.assentos;
                avioes.Add(aviao);
            }

            return View(avioes);
        }
        public ActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Novo(Aviao aviao)
        {
            AviaoService aviaoService = new AviaoService();

            aviaoService.novoAviao(aviao.modelo, aviao.assentos);

            return RedirectToAction("Index", "Aviao");
        }

    }
}
