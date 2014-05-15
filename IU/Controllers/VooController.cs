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
using AlphaNet.PassagemAerea.Aplicacao.Publicos;

namespace IU.Controllers
{
    public class VooController : AbstractController
    {

        public ActionResult Index()
        {
            if (!this.usuarioLogadoGestor()) 
                return RedirectToAction("IndexPessoal", "Voo");

            VooService vooService = new VooService();
            return View(vooService.todosVoos());
        }
        public ActionResult IndexPessoal()
        {
            VooService vooService = new VooService();
            return View(vooService.todosVoos());
        }
        public ActionResult MostrarReservasCliente()
        {
            string clienteId = (string)TempData["clienteId"];

            VooService vooService = new VooService();
            
            return View("ReservasPessoal", vooService.reservasCliente(clienteId));
        }
        public ActionResult MostrarReservasClienteLogado()
        {
            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");
            
            ClienteService clienteService = new ClienteService();

            ClienteData cliente = clienteService.clientePorEmail((string)Session["email"]);

            VooService vooService = new VooService();

            return View("ReservasPessoal", vooService.reservasCliente(cliente.clienteId));
            
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
        public ActionResult CancelarReservaPessoal(string vooId, string clienteId)
        {
            VooService vooService = new VooService();

            vooService.cancelarReserva(vooId, clienteId);

            return View("ReservasPessoal", vooService.reservasCliente(clienteId));
        }

        public ActionResult Novo()
        {
            if(!this.usuarioLogadoGestor())
                return RedirectToAction("Index", "Home");
            
            AplicacaoAviaoService aviaoService = new AplicacaoAviaoService();
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
        public ActionResult NovaReservaPessoal(string vooId)
        {
            if (!this.usuarioEstaLogado())
            {
                ViewBag.vooId = vooId;
                return View("LoginNovaReserva");
            }
            else
                if (TempData["vooId"]!=null)
                    vooId = (string)TempData["vooId"];

            ClienteService clienteService = new ClienteService();
            VooService vooService = new VooService();
            VooData voo = vooService.obterVoo(vooId);

            ClienteData cliente = clienteService.clientePorEmail((string)Session["email"]);
            if (cliente == null)
            {
                ViewBag.vooId = vooId;
            
                ViewBag.email = (string)Session["email"];
                ViewBag.nome = (string)Session["nome"];
                return View("PreencherDadosCliente");
            }
            ViewBag.clienteId = cliente.clienteId;
            ViewBag.nome = cliente.nome;
            ViewBag.email = cliente.email;

            ViewBag.mapaAssentos = vooService.mapaAssentos(voo.vooId);
            ViewBag.voo = voo;

            return View("NovaReservaPessoal");
        }
        [HttpPost]
        public ActionResult NovaReservaPessoal(string vooId, string clienteId, int quantidadeAssentos)
        {
            VooService vooService = new VooService();
            List<int> assentosReservados = new List<int>();

            for (int i = 1; i <= quantidadeAssentos; i++)
            {
                var assento = Request.Params["chk" + i];
                if (assento != null && assento.StartsWith("true"))
                {
                    assentosReservados.Add(i);
                }
            }

            VooComando comando = new VooComando(vooId, clienteId, assentosReservados);

            vooService.novaReserva(comando);
            TempData["clienteId"] = clienteId;
            return RedirectToAction("MostrarReservasCliente", "Voo");
        }

        public ActionResult NovoCliente(string nome, string email, string vooId) {
            ClienteService clienteService = new ClienteService();
            ClienteData cliente = clienteService.novoCliente(nome, email);
            ViewBag.vooId = vooId;
            return this.NovaReservaPessoal(vooId);
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
