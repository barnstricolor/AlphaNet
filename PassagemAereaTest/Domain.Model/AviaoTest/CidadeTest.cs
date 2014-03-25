using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlphaNet.PassagemAerea.Domain.Model.Cidade;
using AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio;

namespace AlphaNet.PassagemAereaTest.Domain.Model.CidadeTest
{
    [TestClass]
    public class CidadeTest
    {
        CidadeRepositorio cidadeRepositorio; 
        [TestInitialize]
        public void setUpTest()
        {
            cidadeRepositorio = new EfCidadeRepositio();
        }

        [TestMethod]
        public void alterarNome()
        {
            Cidade cidade = new Cidade(cidadeRepositorio.proximaIdentidade(), "RIBEIRAO PRETO", "14100");
            Assert.AreEqual("RIBEIRAO PRETO", cidade.nome());
            
        }
    }
}
