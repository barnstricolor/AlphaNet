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
using AlphaNet.PassagemAerea.Aplicacao.Clientes;
using AlphaNet.PassagemAerea.Aplicacao.Clientes.Data;
using AlphaNet.PassagemAerea.Domain.Model.Clientes;

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

        public ActionResult NovaReserva(string vooId)
        {
            ClienteService clienteService = new ClienteService();
            VooService vooService = new VooService();
            VooData voo = vooService.obterVoo(vooId);

            ViewBag.clientes = clienteService.todosClientes();
            ViewBag.mapaAssentos = vooService.mapaAssentos(voo.vooId);
            ViewBag.voo = voo;

            return View("NovaReserva");
        }
        [HttpPost]
        public ActionResult NovaReserva(VooComando comando,bool[] chk)
        {   
            VooService vooService = new VooService();

            for (int i = 1; i < chk.Length; i++)
            {
                if ((bool)chk.GetValue(i))
                    comando.assentos.Add(i);
            }

            vooService.novaReserva(comando);

            return RedirectToAction("Index", "Voo");
        }

        public ActionResult Excluir(string vooId)
        {
            VooService vooService = new VooService();
            vooService.excluir(vooId);
            return RedirectToAction("Index", "Voo");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

    }
}
