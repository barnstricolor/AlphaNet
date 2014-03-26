using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using AlphaNet.Application;

namespace AlphaNet.IU.Controllers
{
    public class CidadeController : ApiController
    {
        //
        // GET: /Cidade/
        static readonly AviaoService aviaoService = new AviaoService();
        
        [System.Web.Http.HttpGet]
        public string teste() {
            return aviaoService.teste("xxxxx");
        }
    }
}
