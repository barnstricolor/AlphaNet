using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaNet.PassagemAerea.Aplicacao.Cidades;
using AlphaNet.PassagemAerea.Aplicacao.Cidades.Data;
using Alphanet.Acesso.Aplicacao;
using IU.Models;


namespace IU.Controllers
{
    public class UsuarioController : AbstractController
    {
        //
        // GET: /Cidade/

        public ActionResult Index()
        {
            if ((string)Session["papel"] != "Gestor")
                return RedirectToAction("Index_adm", "Home");
            
            return View(acessoAplicacaoService.todosUsuarios());
        }

        public ActionResult Novo() {
            return View("Form", new UsuarioData());
        }

        [HttpPost]
        public ActionResult Salvar(UsuarioData usuario) {
            
            if (usuario.usuarioId == null)
                acessoAplicacaoService.novoUsuario(converterParaServico(usuario));
            else
            {
                acessoAplicacaoService.alterarDados(usuario.usuarioId ,converterParaServico(usuario));
            }              
                
            return RedirectToAction("Index", "Usuario");
        }

        public ActionResult Editar(string usuarioId = "")
        {
            UsuarioData usuario = converterParaIu(acessoAplicacaoService.UsuarioPeloId(usuarioId));
            return View("Form", usuario);
        }

        public ActionResult Excluir(string usuarioId = "")
        {
            acessoAplicacaoService.excluirUsuario(usuarioId);
            return RedirectToAction("Index", "Usuario");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }
        private UsuarioData converterParaIu(Alphanet.Acesso.Aplicacao.Data.UsuarioData data)
        {
            UsuarioData result = new UsuarioData(data.login, data.nome, data.email);
            result.senha = data.senha;
            result.papel = data.papel;
            return result;
        }
        /*/private Alphanet.Acesso.Aplicacao.Data.UsuarioData converterParaServico(UsuarioData data)
        {   
            Alphanet.Acesso.Aplicacao.Data.UsuarioData result = new Alphanet.Acesso.Aplicacao.Data.UsuarioData(data.login, data.nome, data.email);
            result.senha = data.senha;
            result.papel = data.papel;
            return result;
        }*/
        private Alphanet.Acesso.Aplicacao.NovoUsuarioComando converterParaServico(UsuarioData data)
        {
            return new Alphanet.Acesso.Aplicacao.NovoUsuarioComando(data.login, data.senha, data.nome, data.email, data.papel);
        }
    }
}
