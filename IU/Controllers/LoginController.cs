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
            } else
                TempData["msgAutenticacao"] = "Falha no login";

            return RedirectToAction("Index", "Home");            
            
        }
        [HttpPost]
        public ActionResult LoginNovaReserva(string login, string senha, string vooId)
        {
            AutenticacaoAplicacaoService autenticacaoAplicacaoService = new AutenticacaoAplicacaoService();
            AutenticarUsuarioComando comando = new AutenticarUsuarioComando(login,senha);

            UsuarioData data = autenticacaoAplicacaoService.autenticar(comando);

            if (data != null)
            {
                TempData["vooId"] = vooId;

                Session["login"] = data.login;
                Session["nome"] = data.nome;
                Session["email"] = data.email;
                Session["gestor"] = this.usuarioLogadoGestor();
            }
            else
                TempData["msgAutenticacao"] = "Falha no login";

            return RedirectToAction("NovaReservaPessoal", "Voo");

        }
        [HttpPost]
        public ActionResult Logoff()
        {
            Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult registrarNovoUsuario() {
            if (this.usuarioEstaLogado())
                return RedirectToAction("Index", "Home");

            return View("RegistrarNovoUsuario");
        }
        [HttpPost]
        public ActionResult registrarNovoUsuario(RegistrarNovoUsuarioComando comando)
        {
            AcessoAplicacaoService acessoAplicacaoService = new AcessoAplicacaoService();

            acessoAplicacaoService.registrarNovoUsuario(comando);

            this.colocarNaSessao(comando.login, comando.nome, comando.email, false);
            return RedirectToAction("Index", "Home");
        }
        private void colocarNaSessao(string login, string nome, string email, bool gestor)
        {            
            Session["login"] = login;
            Session["nome"] = nome;
            Session["email"] = email;
            Session["gestor"] = gestor;            
        }
    }
}