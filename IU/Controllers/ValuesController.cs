using Alphanet.Acesso.Aplicacao;
using Alphanet.Acesso.Domain.Model.Usuarios;
using AlphaNet.PassagemAerea.Aplicacao.Avioes;
using AlphaNet.PassagemAerea.Aplicacao.Cidades;
using AlphaNet.PassagemAerea.Aplicacao.Clientes;
using AlphaNet.PassagemAerea.Aplicacao.Clientes.Data;
using AlphaNet.PassagemAerea.Aplicacao.Voos;
using AlphaNet.PassagemAerea.Domain.Model;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace IU.Controllers
{
    public class ValuesController : Controller
    {
        AcessoAplicacaoService acessoAplicacaoService = new AcessoAplicacaoService();
        // GET api/values
        public ActionResult Index()
        {
            /*

                string aviaoId = DominioRegistro.aplicacaoAviaoService().novoAviao("BOEING 747", 342);
                string cidadeIdOrigem = DominioRegistro.cidadeService().novaCidade("RIBEIRÃO PRETO - SP", "14100");
                string cidadeIdDestino = DominioRegistro.cidadeService().novaCidade("SÃO PAULO - SP", "14000");

                clienteService.novoCliente("VIAÇÃO NASSER " + i, i.ToString() + "@" + i.ToString());
                vooService.novoVoo(aviaoId,cidadeId,cidadeId,new DateTime(),105*i);
            */
            novoUsuario("martin", "martin123", "Martin Fowler", "martin@venus.com", "Gestor");
            novoUsuario("kent", "kent123", "Kent Beck", "kent@frio.com", "Atendente");
            novoUsuario("pi", "pi", "pi", "pi@pi.com", "Gestor");

            string aviaoId = DominioRegistro.aplicacaoAviaoService().novoAviao("BOEING 747", 30);
            string cidadeIdOrigem = DominioRegistro.cidadeService().novaCidade("RIBEIRÃO PRETO - SP", "14100");
            string cidadeIdDestino = DominioRegistro.cidadeService().novaCidade("SÃO PAULO - SP", "14000");

            ClienteData cliente = DominioRegistro.clienteService().novoCliente("RICARDO","HDR_RICARDO@HOTMAIL.COM");
            cliente.especial = true;
            cliente.promocao = true;
            cliente.desconto = 10;
            DominioRegistro.clienteService().alterarDados(cliente);

            cliente = DominioRegistro.clienteService().novoCliente("ROLLAN", "rollan_paiva@hotmail.com");
            cliente.especial = true;
            cliente.promocao = true;
            cliente.desconto = 15;
            DominioRegistro.clienteService().alterarDados(cliente);

            cliente = DominioRegistro.clienteService().novoCliente("THIAGO", "thiago.marega@gmail.com");
            cliente.especial = true;
            cliente.promocao = true;
            cliente.desconto = 20;
            DominioRegistro.clienteService().alterarDados(cliente);

            cliente = DominioRegistro.clienteService().novoCliente("Francisco", "fcnfilho@gmail.com");
            cliente.especial = true;
            cliente.promocao = true;
            cliente.desconto = 25;
            DominioRegistro.clienteService().alterarDados(cliente);

            cliente = DominioRegistro.clienteService().novoCliente("Denise", "denisemcastro@hotmail.com");
            cliente.especial = true;
            cliente.promocao = true;
            cliente.desconto = 30;
            DominioRegistro.clienteService().alterarDados(cliente);

            DominioRegistro.vooService().novoVoo(aviaoId, cidadeIdOrigem, cidadeIdDestino, new DateTime(), 999.99);
            DominioRegistro.vooService().novoVoo(aviaoId, cidadeIdDestino, cidadeIdOrigem, new DateTime().AddDays(1), 999.99);


            return RedirectToAction("Index", "Home");
        }

        private void novoUsuario(string login, string senha,string nome, string email , string papel) {
            NovoUsuarioComando comando = new NovoUsuarioComando(login, senha, nome, email, papel);

            acessoAplicacaoService.novoUsuario(comando);
        }
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

    }
}