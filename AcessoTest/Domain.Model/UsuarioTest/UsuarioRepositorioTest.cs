using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alphanet.Acesso.Domain.Model.Usuarios;
using AlphaNet.Acesso.Domain.Model.Usuarios;
using AlphaNet.Acesso.Domain.Model;
using Alphanet.Acesso.Port.Adapters.Persistencia.Repositorio.Memoria;
using Microsoft.Practices.Unity;

namespace AlphaNet.AcessoTest.Domain.Model.UsuarioTest
{
    [TestClass]
    public class UsuarioRepositorioTest
    {
        UsuarioRepositorio usuarioRepositorio;

        [TestInitialize]
        public void setUpTest()
        {
            DominioRegistro.obterContainer().RegisterInstance<UsuarioRepositorio>(new MemoriaUsuarioRepositorio());
            
            usuarioRepositorio = DominioRegistro.usuarioRepositorio();
            usuarioRepositorio.limpar();
        }
        
        [TestMethod]
        public void salvarUsuario()
        {
            Usuario usuario = new Usuario(
                usuarioRepositorio.proximaIdentidade(),
                "Login", "Senha", "Nome","Email",new Papel("Operador"));

            usuarioRepositorio.salvar(usuario);

            usuario = usuarioRepositorio.obterPeloId(usuario.usuarioId());

            Assert.IsNotNull(usuario);
            
        }
        [TestMethod]
        public void removerUsuario()
        {
            Usuario usuario = new Usuario(
                usuarioRepositorio.proximaIdentidade(),
                "Login", "Senha", "Nome", "Email", new Papel("Operador"));

            usuarioRepositorio.salvar(usuario);

            usuarioRepositorio.remover(usuario.usuarioId());

            usuario = usuarioRepositorio.obterPeloId(usuario.usuarioId());

            Assert.IsNull(usuario);

        }
    }
}
