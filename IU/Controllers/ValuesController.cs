using AlphaNet.PassagemAerea.Aplicacao.Avioes;
using AlphaNet.PassagemAerea.Aplicacao.Cidades;
using AlphaNet.PassagemAerea.Aplicacao.Clientes;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IU.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            AviaoService aviaoService = new AviaoService();
            CidadeService cidadeService = new CidadeService();
            ClienteService clienteService = new ClienteService();
            for (int i = 1; i <= 100; i++)
            {
                aviaoService.novoAviao("BOEING 74" + i, 1);
                cidadeService.novaCidade("Cidade " + i, i.ToString());
                clienteService.novoCliente("Cliente " + i, i.ToString() + "@" + i.ToString());
            }
            
            return new string[] { "value1", "value2" };
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