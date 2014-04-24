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
                aviaoParaTest(),
                cidadeParaTest("rao"),
                cidadeParaTest("sao"),
                new DateTime());
        }
        [TestMethod]
        public void novaReserva()
        {            
            Aviao aviao = aviaoParaTest();
            Voo voo = vooParaTest(aviao);
            voo.novaReserva(
                clienteParaTest("barns"), 
                aviao.assento(1), 
                aviao.assento(2), 
                aviao.assento(3), 
                aviao.assento(4));
            Assert.IsTrue(voo.listaAssentosReservados().Count == 4);

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
        [TestMethod]
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
            return new Cliente(new ClienteId(nome), nome, "@", "rg", "cpf", cidadeParaTest("rao"));
        }
        private Voo vooParaTest(Aviao aviao)
        {
            return new Voo(
                aviao,
                cidadeParaTest("rao"),
                cidadeParaTest("sao"),
                new DateTime());
        }
        
    }
}
