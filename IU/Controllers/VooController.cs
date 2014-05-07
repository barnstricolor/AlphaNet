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

        public ActionResult AlterarPreco(string vooId)
        {
            VooService vooService = new VooService();
            return View(vooService.obterVoo(vooId));
        }
        
        public ActionResult Reservas(string vooId)
        {
            VooService vooService = new VooService();
            return View(vooService.vooComReservas(vooId));
        }

        [HttpPost]
        public ActionResult AlterarPreco(string vooId,double preco, bool promocional)
        {
            VooService vooService = new VooService();

            vooService.alterarPreco(vooId, preco, promocional);

            return RedirectToAction("Index", "Voo");
        }

        public ActionResult Cancelar(string vooId, string clienteId)
        {
            VooService vooService = new VooService();

            vooService.cancelarReserva(vooId, clienteId);

            return RedirectToAction("Reservas", "Voo", new { vooId = vooId});
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
                vooService.novoVoo(voo.aviaoId, voo.cidadeOrigemId, voo.cidadeDestinoId, voo.partida, voo.preco);
            
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
        public ActionResult NovaReserva(string vooId,string clienteId,int quantidadeAssentos)
        {   
            VooService vooService = new VooService();
            List<int> assentosReservados = new List<int>();

            for (int i = 1; i <= quantidadeAssentos; i++)
            { 
                var assento = Request.Params["chk"+i];
                if (assento!=null && assento.StartsWith("true")){
                    assentosReservados.Add(i);
                }
            }

            VooComando comando = new VooComando(vooId, clienteId, assentosReservados);

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
