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
        // GET api/values
        public ActionResult Index()
        {
            VooService vooService = new VooService();
            AviaoService aviaoService = new AviaoService();
            CidadeService cidadeService = new CidadeService();
            ClienteService clienteService = new ClienteService();
            
            for (int i = 1; i <= 10; i++)
            {
                string aviaoId = aviaoService.novoAviao("BOEING ROLLAN 74" + i, i);
                string cidadeId = cidadeService.novaCidade("CIDADE - DENISE " + i, i.ToString());
                clienteService.novoCliente("VIAÇÃO NASSER " + i, i.ToString() + "@" + i.ToString());
                vooService.novoVoo(aviaoId,cidadeId,cidadeId,new DateTime(),105*i);
            }
            
            //return new string[] { "value1", "value2" };
            return RedirectToAction("Index", "Home");
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