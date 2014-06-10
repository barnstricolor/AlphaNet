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
using IU.Models;
namespace IU.Controllers
{
    public class ValuesController : AbstractController
    {
        // GET api/values
        public ActionResult Index()
        {            
            novoUsuario("martin", "martin123", "Martin Fowler", "martin@venus.com", "Gestor");
            novoUsuario("kent", "kent123", "Kent Beck", "kent@frio.com", "Atendente");
            novoUsuario("pi", "pi", "pi", "pi@pi.com", "Gestor");
            aviaoService.novoAviao("A219", 98);
            aviaoService.novoAviao("A230", 208);
            string aviaoId = aviaoService.novoAviao("A220", 148);

            string cidadeIdOrigem = cidadeService.novaCidade("RIBEIRÃO PRETO - Leite Lopes", "14100");
            string cidadeIdDestino = cidadeService.novaCidade("SÃO PAULO - Congonhas", "11000");
            cidadeService.novaCidade("Campinas - Viracopos", "08990");
            cidadeService.novaCidade("Porto Alegre - Salgado Filho", "19000");
            cidadeService.novaCidade("SÃO PAULO - Guarulhos", "65444");

            AlphaNet.PassagemAerea.Aplicacao.Clientes.Data.ClienteData cliente = clienteService.novoCliente("RICARDO","HDR_RICARDO@HOTMAIL.COM");
            cliente.especial = true;
            cliente.promocao = true;
            cliente.desconto = 10;
            clienteService.alterarDados(cliente);

            cliente = clienteService.novoCliente("ROLLAN", "rollan_paiva@hotmail.com");
            cliente.especial = true;
            cliente.promocao = true;
            cliente.desconto = 15;
            clienteService.alterarDados(cliente);

            cliente = clienteService.novoCliente("THIAGO", "thiago.marega@gmail.com");
            cliente.especial = true;
            cliente.promocao = true;
            cliente.desconto = 20;
            clienteService.alterarDados(cliente);

            cliente = clienteService.novoCliente("Francisco", "fcnfilho@gmail.com");
            cliente.especial = true;
            cliente.promocao = true;
            cliente.desconto = 25;
            clienteService.alterarDados(cliente);

            cliente = clienteService.novoCliente("Denise", "denisemcastro@hotmail.com");
            cliente.especial = true;
            cliente.promocao = true;
            cliente.desconto = 30;
            clienteService.alterarDados(cliente);

            for (int i = 1; i < 10; i++){

                DominioRegistro.vooService().novoVoo(aviaoId, cidadeIdOrigem, cidadeIdDestino, DateTime.Today.AddDays(i - 1), 1.267 * i);
                DominioRegistro.vooService().novoVoo(aviaoId, cidadeIdDestino, cidadeIdOrigem, DateTime.Today.AddDays(i+1), 970*i);
            }
            
            return RedirectToAction("Index", "Home");
        }

        private void novoUsuario(string login, string senha,string nome, string email , string papel) {
            NovoUsuarioComando comando = new NovoUsuarioComando(login, senha, nome, email, papel);

            acessoAplicacaoService.novoUsuario(converterParaServico(comando));
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

        private NovoUsuarioComando converterParaIu(Alphanet.Acesso.Aplicacao.NovoUsuarioComando data)
        {
            return new NovoUsuarioComando(data.login, data.senha, data.nome, data.email, data.papel);
        }
        private Alphanet.Acesso.Aplicacao.NovoUsuarioComando converterParaServico(NovoUsuarioComando data)
        {
            return new Alphanet.Acesso.Aplicacao.NovoUsuarioComando(data.login, data.senha, data.nome, data.email, data.papel);
        }
        /*private UsuarioData converterParaIu(Alphanet.Acesso.Aplicacao.Data.UsuarioData data)
        {
            UsuarioData result = new UsuarioData();

            result.usuarioId = data.usuarioId;
            result.login = data.login;
            result.nome = data.nome;
            result.email = data.email;
            result.senha = data.senha;
            result.papel = data.papel;

            return result;
        }
        private Alphanet.Acesso.Aplicacao.Data.UsuarioData converterParaServico(UsuarioData data)
        {
            Alphanet.Acesso.Aplicacao.Data.UsuarioData result = new Alphanet.Acesso.Aplicacao.Data.UsuarioData();

            result.usuarioId = data.usuarioId;
            result.login = data.login;
            result.nome = data.nome;
            result.email = data.email;
            result.senha = data.senha;
            result.papel = data.papel;

            return result;
        }
        */
    }
}