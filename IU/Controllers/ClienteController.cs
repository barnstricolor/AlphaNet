using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaNet.PassagemAerea.Aplicacao.Clientes;
using IU.Models;
using AlphaNet.PassagemAerea.Domain.Model;



namespace IU.Controllers
{
    public class ClienteController : Controller
    {
        //
        // GET: /Cidade/

        public ActionResult Index()
        {
            return View(DominioRegistro.clienteService().todosClientes());
        }

        public ActionResult Novo() {
            ViewBag.cidades = DominioRegistro.cidadeService().todasCidades();
            return View("Form", new ClienteData());
        }

        [HttpPost]
        public ActionResult Salvar(ClienteData cliente) {
            ClienteData novo = new ClienteData();

            if (cliente.clienteId == null)
            {
                novo = converterParaIu(DominioRegistro.clienteService().novoCliente(cliente.nome, cliente.email));
                cliente.clienteId = novo.clienteId;
            }
            DominioRegistro.clienteService().alterarDados(converterParaServico(cliente));
            
            return RedirectToAction("Index", "Cliente");
        }

        public ActionResult Editar(string clienteId = "")
        {
            ClienteData clienteData = converterParaIu(DominioRegistro.clienteService().obterCliente(clienteId));
            ViewBag.cidades = DominioRegistro.cidadeService().todasCidades();
            return View("Form", clienteData);
        }

        public ActionResult Excluir(string clienteId = "")
        {
            DominioRegistro.clienteService().excluirCliente(clienteId);
            return RedirectToAction("Index", "Cliente");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
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
    }
}
