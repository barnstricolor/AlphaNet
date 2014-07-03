using AlphaNet.PassagemAerea.Aplicacao.Clientes;
using AlphaNet.PassagemAerea.Aplicacao.Clientes.Data;
using AlphaNet.PassagemAerea.Aplicacao.Voos.Comando;
using AlphaNet.PassagemAerea.Aplicacao.Voos.Data;
using AlphaNet.PassagemAerea.Domain.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace IU.Controllers
{
    public class VooController : AbstractController
    {

        public ActionResult Index()
        {
            if ((string)Session["papel"] != "Gestor" & (string)Session["papel"] != "Atendente") 
                return RedirectToAction("IndexPessoal", "Voo");

            
            return View(vooService.todosVoos());
        }
        public ActionResult IndexPessoal()
        {
            return View(vooService.todosVoos());
        }
        public ActionResult MostrarReservasCliente()
        {
            string clienteId = (string)TempData["clienteId"];

            return View("ReservasPessoal", vooService.reservasCliente(clienteId));
        }
        public ActionResult MostrarReservasClienteLogado()
        {
            if (Session["email"] == null)
                return RedirectToAction("Index", "Home");

            if ((string)Session["papel"] != "Cliente")
                return RedirectToAction("Index", "Home");            


            ClienteData cliente = clienteService.clientePorEmail((string)Session["email"]);

            return View("ReservasPessoal", 
                cliente==null?new List<VooReservaData>():
                vooService.reservasCliente(cliente.clienteId));
            
        }
        public ActionResult AlterarPreco(string vooId)
        {
            return View(vooService.obterVoo(vooId));
        }
        
        public ActionResult Reservas(string vooId)
        {
            return View(vooService.vooComReservas(vooId));
        }

        [HttpPost]
        public ActionResult AlterarPreco(string vooId,double preco, bool promocional)
        {
            vooService.alterarPreco(vooId, preco, promocional);

            return RedirectToAction("Index", "Voo");
        }

        public ActionResult Cancelar(string vooId, string clienteId)
        {
            vooService.cancelarReserva(vooId, clienteId);

            return RedirectToAction("Reservas", "Voo", new { vooId = vooId});
        }
        public ActionResult CancelarReservaPessoal(string vooId, string clienteId)
        {
            vooService.cancelarReserva(vooId, clienteId);

            return View("ReservasPessoal", vooService.reservasCliente(clienteId));
        }

        public ActionResult Novo()
        {
            if(!this.usuarioLogadoGestor())
                return RedirectToAction("Index", "Home");            

            //ViewBag.vooData = new VooData();
            ViewBag.avioes = aviaoService.todosAvioes();
            ViewBag.cidades = cidadeService.todasCidades();

            return View("Form", new VooData());
        }

        [HttpPost]
        public ActionResult Salvar(VooData voo)
        {
            DateTime dataHoraPartida = new DateTime(
                voo.partida.Year,
                voo.partida.Month,
                voo.partida.Day,
                voo.horaPartida,
                voo.horaPartida,
                0);

            vooService.novoVoo(voo.aviaoId, voo.cidadeOrigemId, voo.cidadeDestinoId, dataHoraPartida, (double)voo.preco);
            
            return RedirectToAction("Index", "Voo");
        }

        public ActionResult NovaReserva(string vooId)
        {
            VooData voo = vooService.obterVoo(vooId);

            ViewBag.clientes = clienteService.todosClientes();
            ViewBag.mapaAssentos = vooService.mapaAssentos(voo.vooId);
            ViewBag.voo = voo;

            return View("NovaReserva");
        }
        [HttpPost]
        public ActionResult NovaReserva(string vooId,string clienteId,int quantidadeAssentos)
        {   
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

            VooData voo = vooService.obterVoo(vooId);

            ClienteData cliente = clienteService.clientePorEmail((string)Session["email"]);
            if (cliente == null)
            {
                ViewBag.vooId = vooId;
            
                ViewBag.email = (string)Session["email"];
                ViewBag.nome = (string)Session["nome"];
                ViewBag.cidades = DominioRegistro.cidadeService().todasCidades();

                ClienteData clienteVazio = new ClienteData();
                clienteVazio.nome = (string)Session["nome"];
                clienteVazio.email = (string)Session["email"];
                return View("PreencherDadosCliente",clienteVazio);
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

        public ActionResult NovoCliente(string vooId,ClienteData cliente)
        {
            ClienteData novo = new ClienteData();

            novo = DominioRegistro.clienteService().novoCliente(cliente.nome, cliente.email);
            
            cliente.clienteId = novo.clienteId;
            
            DominioRegistro.clienteService().alterarDados(cliente);
            
            ViewBag.vooId = vooId;
            if (!((string)Session["papel"] == "Gestor") & !((string)Session["papel"] == "Atendente"))
            {
                acessoAplicacaoService.alterarPapel((string)Session["email"], "Cliente");
                Session["papel"] = "Cliente";
            }
            
            return this.NovaReservaPessoal(vooId);
        }
        
        public ActionResult Excluir(string vooId)
        {
            vooService.excluir(vooId);
            return RedirectToAction("Index", "Voo");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }
    }
}
