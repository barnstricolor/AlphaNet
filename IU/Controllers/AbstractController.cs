using AlphaNet.PassagemAerea.Aplicacao.Publicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IU.Controllers
{
    public class AbstractController : Controller
    {               
        protected bool usuarioEstaLogado() {
            return usuarioLogin() != null;
        }
        protected string usuarioLogin()
        {
            return (string)Session["login"];
        }
        protected string usuarioEmail()
        {
            return (string)Session["email"];
        }
        protected bool usuarioLogadoGestor() {
            PublicoAplicacaoService publicoAplicacaoService = new PublicoAplicacaoService();
            return publicoAplicacaoService.gestor(usuarioEmail());
        }
    }
}
