using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using AlphaNet.PassagemAerea.Domain.Model.Cidades;
using AlphaNet.PassagemAerea.Domain.Model.Voos;
using AlphaNet.PassagemAerea.Domain.Model.Clientes;
using AlphaNet.PassagemAerea.Aplicacao.Voos;
using AlphaNet.PassagemAerea.Aplicacao.Voos.Data;
using System.Collections.Generic;

namespace PassagemAereaTest.Domain.Model.VooTest
{
    [TestClass]
    public class VooTest
    {

        [TestMethod]
        public void novoVoo()
        {
            Voo voo = new Voo(
                new VooId("123"),
                aviaoParaTest(),
                cidadeParaTest("rao"),
                cidadeParaTest("sao"),
                new DateTime(),
                135.89);

            Assert.AreEqual(135.89, voo.preco());
        }
        [TestMethod]
        public void novaReservaClienteNormal()
        {            
            Aviao aviao = aviaoParaTest();
            Voo voo = vooParaTest(aviao,135.89);
            Cliente cliente = clienteNormalParaTest("barns");
            voo.novaReserva(
                cliente, 
                aviao.assento(1), 
                aviao.assento(2), 
                aviao.assento(3), 
                aviao.assento(4));
            Assert.IsTrue(voo.listaAssentosReservados().Count == 4);
            Assert.AreEqual(4 * 135.89, voo.obterReservaPeloCliente(cliente).total());
        }
        [TestMethod]
        public void novaReservaClienteEspecial()
        {
            Aviao aviao = aviaoParaTest();
            Voo voo = vooParaTest(aviao, 135.89);
            Cliente cliente = clienteEspecialParaTest("barns");
            voo.novaReserva(
                cliente,
                aviao.assento(1),
                aviao.assento(2),
                aviao.assento(3),
                aviao.assento(4));
            Assert.IsTrue(voo.listaAssentosReservados().Count == 4);
            Assert.AreEqual((4 * 135.89) * .9, voo.obterReservaPeloCliente(cliente).total());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void novaReservaParaMesmoCliente()
        {
            Aviao aviao = aviaoParaTest();
            Voo voo = vooParaTest(aviao);
            Cliente cliente = clienteParaTest();
            voo.novaReserva(
                cliente,
                aviao.assento(1));
            voo.novaReserva(
                cliente,
                aviao.assento(2));
        }
        
        [TestMethod]
        public void cancelarReserva()
        {
            Cliente ricardo = clienteParaTest("ricardo");
            Cliente celio = clienteParaTest("celio");
            Aviao aviao = aviaoParaTest();
            Voo voo = vooParaTest(aviao);
            voo.novaReserva(
                ricardo,
                aviao.assento(1),
                aviao.assento(2));
            voo.novaReserva(
                celio,
                aviao.assento(3));
            Assert.IsTrue(voo.listaAssentosReservados().Count == 3);

            voo.cancelarReserva(ricardo);

            Assert.IsTrue(voo.listaAssentosReservados().Count == 1);

            voo.cancelarReserva(celio);

            Assert.IsTrue(voo.listaAssentosReservados().Count == 0);            
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void cancelarReservaInexistente()
        {
            Cliente ricardo = clienteParaTest("ricardo");
            Cliente celio = clienteParaTest("celio");
            Aviao aviao = aviaoParaTest();
            Voo voo = vooParaTest(aviao);
            voo.novaReserva(
                ricardo,
                aviao.assento(1),
                aviao.assento(2));

            voo.cancelarReserva(celio);
        }

        [TestMethod]
        public void precoPromocional() {
            Aviao aviao = aviaoParaTest();
            Voo voo = vooParaTest(aviao);
            voo.precoPromocional(10);
            Assert.AreEqual(10, voo.preco());
            Assert.IsTrue(voo.promocional());
        }
        [TestMethod]
        public void alterarPreco()
        {
            Aviao aviao = aviaoParaTest();
            Voo voo = vooParaTest(aviao,50);
            voo.alterarPreco(100);
            Assert.AreEqual(100, voo.preco());
            Assert.IsFalse(voo.promocional());
        }
        [TestMethod]
        public void alterarPrecoPromocionalParaPrecoNormal()
        {
            Aviao aviao = aviaoParaTest();
            Voo voo = vooParaTest(aviao, 50);
            voo.precoPromocional(10);
            Assert.AreEqual(10, voo.preco());
            Assert.IsTrue(voo.promocional());
            voo.alterarPreco(100);
            Assert.AreEqual(100, voo.preco());
            Assert.IsFalse(voo.promocional());
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void assentoReservado()
        {
            Aviao aviao = aviaoParaTest();
            Voo voo = vooParaTest(aviao);
            voo.novaReserva(
                clienteParaTest("barns"),
                aviao.assento(1),
                aviao.assento(2));
            voo.novaReserva(
                clienteParaTest("celiao"),
                aviao.assento(1));
        }
        //[TestMethod]
        public void mapaAssentos()
        {
            VooService vooService = new VooService();
            foreach (AssentoData data in vooService.mapaAssentos("1"))
            {
                Console.WriteLine("Assento:" + data.numero + " - " + data.reservado);
            }

        }
        private Aviao aviaoParaTest()
        {
            return new Aviao(new AviaoId("1"), "Focker", 100);
        }
        private Cidade cidadeParaTest(string nome)
        {
            return new Cidade(new CidadeId(nome), nome, "14100");
        }
        private Cliente clienteParaTest(string nome)
        {
            return new Cliente(new ClienteId(nome), nome, "@");
        }
        private Cliente clienteParaTest()
        {
            return clienteParaTest("barns");
        }
        private Cliente clienteNormalParaTest(string nome)
        {
            Cliente cliente = clienteParaTest(nome);
            cliente.definirComoNormal();
            return cliente;
        }
        private Cliente clienteEspecialParaTest(string nome)
        {
            Cliente cliente = clienteParaTest(nome);
            cliente.definirComoEspecial();
            return cliente;
        }
        private Voo vooParaTest(Aviao aviao, double preco)
        {
            return new Voo(
                new VooId("123"),
                aviao,
                cidadeParaTest("rao"),
                cidadeParaTest("sao"),
                new DateTime(),
                preco);
        }
        private Voo vooParaTest(Aviao aviao)
        {
            return vooParaTest(aviao, 52);
        }
        
    }
}
