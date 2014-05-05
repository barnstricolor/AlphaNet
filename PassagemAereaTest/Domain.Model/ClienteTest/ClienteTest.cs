using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlphaNet.PassagemAerea.Domain.Model.Clientes;

namespace PassagemAereaTest.Domain.Model.ClienteTest
{
    [TestClass]
    public class ClienteTest
    {


        [TestMethod]
        public void clienteNormal()
        {
            Cliente cliente = new Cliente(new ClienteId("1"), "Cliente Normal", "email");
            Assert.IsFalse(cliente.estaComoEspecial());
        }
        [TestMethod]
        public void clienteEspecial()
        {
            Cliente cliente = new Cliente(new ClienteId("1"), "Cliente Normal", "email");
            cliente.definirComoEspecial();
            Assert.IsTrue(cliente.estaComoEspecial());
        }
        [TestMethod]
        public void clienteEspecialParaNormal()
        {
            Cliente cliente = new Cliente(new ClienteId("1"), "Cliente Normal", "email");
            cliente.definirComoEspecial();
            Assert.IsTrue(cliente.estaComoEspecial());
            cliente.definirComoNormal();
            Assert.IsFalse(cliente.estaComoEspecial());
        }

    }
}
