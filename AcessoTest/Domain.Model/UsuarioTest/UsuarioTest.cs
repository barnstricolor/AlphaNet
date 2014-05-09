using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlphaNet.Acesso.Domain.Model.Usuarios;
using Alphanet.Acesso.Domain.Model.Usuarios;

namespace AlphaNet.AcessoTest.Domain.Model.UsuarioTest
{
    [TestClass]
    public class UsuarioTest
    {
        
        [TestMethod]
        public void novoUsuario()
        {
            Usuario usuario = new Usuario(
                new UsuarioId("1"),
                "Login", 
                "Senha", 
                "Nome",
                "usuario@me.com",
                new Papel("Operador"));

            Assert.AreEqual("Login", usuario.login());
            Assert.AreEqual("Senha", usuario.senha());
            Assert.AreEqual("Nome", usuario.nome());

        }
        [TestMethod]
        public void alterarSenha()
        {
            Usuario usuario = usuarioParaTest();
            usuario.alterarSenha("NovaSenha");

            Assert.AreEqual("NovaSenha", usuario.senha());

        }
        [TestMethod]
        public void alterarNome()
        {
            Usuario usuario = usuarioParaTest();
            usuario.alterarNome("NovoNome");

            Assert.AreEqual("NovoNome", usuario.nome());

        }
        [TestMethod]
        public void atribuirPapel()
        {
            Usuario usuario = usuarioParaTest();
            usuario.atribuirPapel(new Papel("Administrador"));

            Assert.IsTrue(usuario.desempenhaPapel(new Papel("Administrador")));

        }        
        private Usuario usuarioParaTest() {            
            return new Usuario(
                new UsuarioId("1"),
                "Login", 
                "Senha", 
                "Nome",
                "usuario@me.com",
                new Papel("Operador"));
        }
    }
}
