using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaNet.PassagemAerea.Aplicacao.Avioes;
using AlphaNet.PassagemAerea.Aplicacao.Avioes.Data;
using AlphaNet.PassagemAerea.Aplicacao.Voos;
using AlphaNet.PassagemAerea.Aplicacao.Cidades;
using AlphaNet.PassagemAerea.Aplicacao.Clientes;
using AlphaNet.PassagemAerea.Domain.Model.Clientes;
using AlphaNet.PassagemAerea.Aplicacao.Publicos;
using IU.Models;
using AlphaNet.PassagemAerea.Aplicacao.Voos.Comando;
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

            ClienteData cliente = converterClienteParaIu(clienteService.clientePorEmail((string)Session["email"]));

            VooService vooService = new VooService();

            return View("ReservasPessoal", 
                cliente==null?new List<VooReservaData>():
                converterReservaParaIu(vooService.reservasCliente(cliente.clienteId)));
            
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
            VooData voo = converterVooParaIu(vooService.obterVoo(vooId));

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
            VooData voo = converterVooParaIu(vooService.obterVoo(vooId));

            ClienteData cliente = converterClienteParaIu(clienteService.clientePorEmail((string)Session["email"]));
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
            ClienteData cliente = converterClienteParaIu(clienteService.novoCliente(nome, email));
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
        private ClienteData converterClienteParaIu(AlphaNet.PassagemAerea.Aplicacao.Clientes.Data.ClienteData data)
        {
            ClienteData result = new ClienteData();

            result.clienteId = data.clienteId;
            result.nome = data.nome;
            result.email = data.email;
            result.rg = data.rg;
            result.cpf = data.cpf;
            result.ocupacao = data.ocupacao;
            result.renda = data.renda;
            result.sexo = data.sexo;
            result.desconto = data.desconto;
            result.promocao = data.promocao;
            result.especial = data.especial;
            result.telefone = data.telefone;
            result.celular = data.celular;
            result.endereco = data.endereco;
            result.numeroEndereco = data.numeroEndereco;
            result.bairro = data.bairro;
            result.cep = data.cep;
            result.dataCadastro = data.dataCadastro;
            /*if (data.cidade != null)
            {
                result.cidade.cep = data.cidade.cep;
                result.cidade.cidadeId = data.cidade.cidadeId;
                result.cidade.nome = data.cidade.nome;
            }*/

            return result;
        }
        private AlphaNet.PassagemAerea.Aplicacao.Clientes.Data.ClienteData converterParaServico(ClienteData data)
        {
            AlphaNet.PassagemAerea.Aplicacao.Clientes.Data.ClienteData result = new AlphaNet.PassagemAerea.Aplicacao.Clientes.Data.ClienteData();

            result.clienteId = data.clienteId;
            result.nome = data.nome;
            result.email = data.email;
            result.rg = data.rg;
            result.cpf = data.cpf;
            result.ocupacao = data.ocupacao;
            result.renda = data.renda;
            result.sexo = data.sexo;
            result.desconto = data.desconto;
            result.promocao = data.promocao;
            result.especial = data.especial;
            result.telefone = data.telefone;
            result.celular = data.celular;
            result.endereco = data.endereco;
            result.numeroEndereco = data.numeroEndereco;
            result.bairro = data.bairro;
            result.cep = data.cep;
            result.dataCadastro = data.dataCadastro;
            /*if (data.cidade != null)
            {
                result.cidade.cep = data.cidade.cep;
                result.cidade.cidadeId = data.cidade.cidadeId;
                result.cidade.nome = data.cidade.nome;
            }*/
            return result;
        }
        private VooData converterVooParaIu(AlphaNet.PassagemAerea.Aplicacao.Voos.Data.VooData data)
        {
            VooData result = new VooData();

            result.aviaoId = data.aviaoId;
            result.vooId = data.vooId;
            result.aviaoId = data.aviaoId;
            result.aviaoModelo = data.aviaoModelo;
            result.cidadeOrigemId = data.cidadeOrigemId;
            result.cidadeOrigemNome = data.cidadeOrigemNome;
            result.cidadeDestinoId = data.cidadeDestinoId;
            result.cidadeDestinoNome = data.cidadeDestinoNome;
            result.partida = data.partida;
            result.totalAssentos = data.totalAssentos;
            result.reservados = data.reservados;
            result.preco = data.preco;
            result.promocional = data.promocional;
            //public List<ReservaData> _reservas = new List<ReservaData>();

            return result;
        }

        private List<VooReservaData> converterReservaParaIu(List<AlphaNet.PassagemAerea.Aplicacao.Voos.Data.VooReservaData> lista)
        {
            List<VooReservaData> result = new List<VooReservaData>();
            foreach (AlphaNet.PassagemAerea.Aplicacao.Voos.Data.VooReservaData data in lista)
            {
                VooReservaData reserva = new VooReservaData();
                reserva.vooId = data.vooId;
                reserva.aviaoModelo = data.aviaoModelo;
                reserva.cidadeOrigemNome = data.cidadeOrigemNome;
                reserva.cidadeDestinoNome = data.cidadeDestinoNome;
                reserva.partida = data.partida;
                reserva.clienteId = data.clienteId;
                reserva.clienteNome = data.clienteNome;
                reserva.precoReserva = data.precoReserva;
                reserva.assentosReservados = data.assentosReservados;
                result.Add(reserva);
            }
            return result;
        }
    }
}
