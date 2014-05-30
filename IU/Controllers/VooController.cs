using AlphaNet.PassagemAerea.Aplicacao.Clientes;
using AlphaNet.PassagemAerea.Aplicacao.Voos.Comando;
using AlphaNet.PassagemAerea.Domain.Model;
using IU.Models;
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


            ClienteData cliente = converterClienteParaIu(clienteService.clientePorEmail((string)Session["email"]));

            return View("ReservasPessoal", 
                cliente==null?new List<VooReservaData>():
                converterReservaParaIu(vooService.reservasCliente(cliente.clienteId)));
            
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
            if (voo.vooId == null)
                vooService.novoVoo(voo.aviaoId, voo.cidadeOrigemId, voo.cidadeDestinoId, voo.partida, voo.preco);
            
            return RedirectToAction("Index", "Voo");
        }

        public ActionResult NovaReserva(string vooId)
        {
            VooData voo = converterVooParaIu(vooService.obterVoo(vooId));

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

            VooData voo = converterVooParaIu(vooService.obterVoo(vooId));

            ClienteData cliente = converterClienteParaIu(clienteService.clientePorEmail((string)Session["email"]));
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

            novo = converterParaIu(DominioRegistro.clienteService().novoCliente(cliente.nome, cliente.email));
            cliente.clienteId = novo.clienteId;
            DominioRegistro.clienteService().alterarDados(converterParaServico(cliente));
            Session["papel"] = "Cliente";
            ViewBag.vooId = vooId;
            
            return this.NovaReservaPessoal(vooId);
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
            if (data.cidade != null)
            {
                AlphaNet.PassagemAerea.Aplicacao.Cidades.Data.CidadeData cidade = new AlphaNet.PassagemAerea.Aplicacao.Cidades.Data.CidadeData();
                cidade.cidadeId = data.cidade;
                result.cidade = cidade;
            }
            return result;
        }

        private ClienteData converterParaIu(AlphaNet.PassagemAerea.Aplicacao.Clientes.Data.ClienteData data)
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
            if (data.cidade != null)
            {
                result.cidade = data.cidade.cidadeId;
            }

            return result;
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
        
        private ClienteData converterClienteParaIu(AlphaNet.PassagemAerea.Aplicacao.Clientes.Data.ClienteData data)
        {
            if (data== null)
                return null;

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
            if (data.cidade != null)
            {
                result.cidade = data.cidade.cidadeId;
            }

            return result;
        }
        private AlphaNet.PassagemAerea.Aplicacao.Clientes.Data.ClienteData converterClienteParaServico(ClienteData data)
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
            if (data.cidade != null)
            {
                AlphaNet.PassagemAerea.Aplicacao.Cidades.Data.CidadeData cidade = new AlphaNet.PassagemAerea.Aplicacao.Cidades.Data.CidadeData();
                cidade.cidadeId = data.cidade;
                result.cidade = cidade;
            }
            return result;
        }
        private List<VooData> converterVoosParaIu(List<AlphaNet.PassagemAerea.Aplicacao.Voos.Data.VooData> lista)
        {
            List<VooData> result = new List<VooData>();

            foreach (AlphaNet.PassagemAerea.Aplicacao.Voos.Data.VooData data in lista)
            {
                VooData voo = new VooData();

                voo.aviaoId = data.aviaoId;
                voo.vooId = data.vooId;
                voo.aviaoId = data.aviaoId;
                voo.aviaoModelo = data.aviaoModelo;
                voo.cidadeOrigemId = data.cidadeOrigemId;
                voo.cidadeOrigemNome = data.cidadeOrigemNome;
                voo.cidadeDestinoId = data.cidadeDestinoId;
                voo.cidadeDestinoNome = data.cidadeDestinoNome;
                voo.partida = data.partida;
                voo.totalAssentos = data.totalAssentos;
                voo.reservados = data.reservados;
                voo.preco = data.preco;
                voo.promocional = data.promocional;
                //public List<ReservaData> _reservas = new List<ReservaData>();
                result.Add(voo);
            }
            return result;
        }
        private VooReservaData converterReservaParaIu(AlphaNet.PassagemAerea.Aplicacao.Voos.Data.VooReservaData data)
        {
            VooReservaData result = new VooReservaData();

            result.vooId = data.vooId;
            result.aviaoModelo = data.aviaoModelo;
            result.cidadeOrigemNome = data.cidadeOrigemNome;
            result.cidadeDestinoNome = data.cidadeDestinoNome;
            result.partida = data.partida;
            result.clienteId = data.clienteId;
            result.clienteNome = data.clienteNome;
            result.precoReserva = data.precoReserva;
            result.assentosReservados = data.assentosReservados;


            return result;
        }
    }
}
