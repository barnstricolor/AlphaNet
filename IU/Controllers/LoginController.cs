using Alphanet.Acesso.Aplicacao;
using Alphanet.Acesso.Aplicacao.Data;
using IU.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlphaNet.PassagemAerea.IU.Controllers
{
    public class LoginController : AbstractController
    {
        [HttpPost]
        public ActionResult Login(AutenticarUsuarioComando comando)
        {
            AutenticacaoAplicacaoService autenticacaoAplicacaoService = new AutenticacaoAplicacaoService();

            UsuarioData data = autenticacaoAplicacaoService.autenticar(comando);

            if (data != null)
            {
                Session["login"] = data.login;
                Session["nome"] = data.nome;
                Session["email"] = data.email;
                Session["gestor"] = this.usuarioLogadoGestor();
                if (this.usuarioLogadoGestor())
                    return RedirectToAction("Index_adm", "Home");
                else
                    return RedirectToAction("Index", "Home");
            } else{
                TempData["msgAutenticacao"] = "Falha no login";
                return RedirectToAction("Index", "Home");
            }
            
        }
        [HttpPost]
        public ActionResult Logoff()
        {
            Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}