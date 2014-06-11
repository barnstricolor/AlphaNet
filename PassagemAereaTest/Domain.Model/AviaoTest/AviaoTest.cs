using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.EF;
using AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Memoria;
using Microsoft.Practices.Unity;
using AlphaNet.PassagemAerea.Domain.Model;
using PassagemAereaTest.Domain.Model.Avioes;
using AlphaNet.PassagemAerea.Domain.Model.Clientes;
using AlphaNet.PassagemAerea.Domain.Model.Cidades;
using AlphaNet.PassagemAerea.Domain.Model.Voos;

namespace AlphaNet.PassagemAereaTest.Domain.Model.AviaoTest
{
    [TestClass]
    public class AviaoTest
    {
        AviaoRepositorio aviaoRepositorio;
        VooRepositorio vooRepositorio;

        [TestInitialize]
        public void setUpTest()
        {
            DominioRegistro.obterContainer().RegisterInstance<AviaoRepositorio>(new MemoriaAviaoRepositorio());
            aviaoRepositorio = DominioRegistro.aviaoRepositorio();
            aviaoRepositorio.limpar();

            DominioRegistro.obterContainer().RegisterInstance<VooRepositorio>(new MemoriaVooRepositorio());
            vooRepositorio = DominioRegistro.vooRepositorio();
            vooRepositorio.limpar();
        }
        
        [TestMethod]
        public void alterarModelo()
        {
            Aviao aviao = new Aviao(aviaoRepositorio.proximaIdentidade(),"747",999);

            Assert.AreEqual("747",aviao.modelo());

            aviaoRepositorio.salvar(aviao);
            aviao = aviaoRepositorio.obterPeloId(aviao.aviaoId());
            
            aviao.alterarModelo("Boeing 747");
            Assert.AreEqual("Boeing 747", aviao.modelo());
            
        }
        [TestMethod]
        public void removerAviaoSemVoo() {

            Aviao aviao = new Aviao(aviaoRepositorio.proximaIdentidade(), "747", 999);

            aviaoRepositorio.salvar(aviao);
            RemoverAviaoServico removerAviaoServico = new RemoverAviaoServico();
            removerAviaoServico.remover(aviao);

            aviao = aviaoRepositorio.obterPeloId(aviao.aviaoId());

            Assert.IsNull(aviao);

        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void naoRemoverAviaoComVoo()
        {
            Aviao aviao = aviaoParaTest();
            aviaoRepositorio.salvar(aviao);

            vooParaTest(aviao);
                        
            RemoverAviaoServico removerAviaoServico = new RemoverAviaoServico();
            removerAviaoServico.remover(aviao);

            aviao = aviaoRepositorio.obterPeloId(aviao.aviaoId());
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
            Voo voo = new Voo(
                new VooId("123"),
                aviao,
                cidadeParaTest("rao"),
                cidadeParaTest("sao"),
                new DateTime(),
                preco);

            vooRepositorio.salvar(voo);

            return voo;
        }
        private Voo vooParaTest(Aviao aviao)
        {
            return vooParaTest(aviao, 52);
        }
        

    }
}
