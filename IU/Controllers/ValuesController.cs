using Alphanet.Acesso.Aplicacao;
using Alphanet.Acesso.Domain.Model.Usuarios;
using AlphaNet.PassagemAerea.Aplicacao.Avioes;
using AlphaNet.PassagemAerea.Aplicacao.Cidades;
using AlphaNet.PassagemAerea.Aplicacao.Clientes;
using AlphaNet.PassagemAerea.Aplicacao.Voos;
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
            VooService vooService = new VooService();
            AplicacaoAviaoService aviaoService = new AplicacaoAviaoService();
            CidadeService cidadeService = new CidadeService();
            ClienteService clienteService = new ClienteService();
            

            for (int i = 1; i <= 10; i++)
            {
                string aviaoId = aviaoService.novoAviao("BOEING ROLLAN 74" + i, i);
                string cidadeId = cidadeService.novaCidade("CIDADE - DENISE " + i, i.ToString());
                clienteService.novoCliente("VIAÇÃO NASSER " + i, i.ToString() + "@" + i.ToString());
                vooService.novoVoo(aviaoId,cidadeId,cidadeId,new DateTime(),105*i);
            }

            novoUsuario("martin", "martin123", "Martin Fowler", "martin@venus.com", "Gestor");
            novoUsuario("kent", "kent123", "Kent Beck", "kent@frio.com", "Atendente");
            novoUsuario("pi", "pi", "pi", "pi@pi.com", "Gestor");

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