using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using AlphaNet.PassagemAerea.Domain.Model.Aviao;
using AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio;

namespace AlphaNet.PassagemAereaTest.Domain.Model.AviaoTest
{
    [TestClass]
    public class AviaoTest
    {
        AviaoRepositorio aviaoRepositorio;

        [TestInitialize]
        public void setUpTest()
        {
            //aviaoRepositorio = new EfAviaoRepositio();
            aviaoRepositorio = new MemoriaAviaoRepositorio();
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
    }
}
