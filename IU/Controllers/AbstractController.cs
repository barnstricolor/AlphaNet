using Alphanet.Acesso.Aplicacao;
using AlphaNet.PassagemAerea.Aplicacao.Avioes;
using AlphaNet.PassagemAerea.Aplicacao.Cidades;
using AlphaNet.PassagemAerea.Aplicacao.Clientes;
using AlphaNet.PassagemAerea.Aplicacao.Publicos;
using AlphaNet.PassagemAerea.Aplicacao.Voos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IU.Controllers
{
    public class AbstractController : Controller
    {
        protected PublicoAplicacaoService publicoAplicacaoService = new PublicoAplicacaoService();
        protected AcessoAplicacaoService acessoAplicacaoService = new AcessoAplicacaoService();
        protected VooService vooService = new VooService();
        protected AplicacaoAviaoService aviaoService = new AplicacaoAviaoService();
        protected CidadeService cidadeService = new CidadeService();
        protected ClienteService clienteService = new ClienteService();

        protected bool usuarioEstaLogado()
        {
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
            return publicoAplicacaoService.gestor(usuarioEmail());
        }
        protected string usuarioPapel()
        {
            return publicoAplicacaoService.papel(usuarioEmail());
        }

    }
}
