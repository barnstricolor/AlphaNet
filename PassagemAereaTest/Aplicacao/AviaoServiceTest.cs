using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlphaNet.PassagemAerea.Aplicacao.Avioes;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using AlphaNet.PassagemAerea.Domain.Model;
using AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio;
using Microsoft.Practices.Unity;

namespace PassagemAereaTest.Aplicacao
{
    [TestClass]
    public class AviaoServiceTest
    {
        AviaoService aviaoService = new AviaoService();
        AviaoRepositorio aviaoRepositorio;

        [TestInitialize]
        public void setup()
        {
            DominioRegistro.obterContainer().RegisterInstance<AviaoRepositorio>(new MemoriaAviaoRepositorio());
            aviaoRepositorio = DominioRegistro.aviaoRepositorio();
            aviaoRepositorio.limpar();
        }

        [TestMethod]
        public void novoAviao()
        {
            string idNovoAviao = aviaoService.novoAviao("Boeing 747", 400);

            Aviao aviao = aviaoRepositorio.obterPeloId(new AviaoId(idNovoAviao));

            Assert.IsNotNull(aviao);
            Assert.AreEqual("Boeing 747", aviao.modelo());
            Assert.AreEqual(400, aviao.assentos());
        }
        [TestMethod]
        public void alterarModelo()
        {
            string idNovoAviao = aviaoService.novoAviao("Boeing 747", 400);
            
            aviaoService.alterarModelo(idNovoAviao, "Boeing 777");

            Aviao aviao = aviaoRepositorio.obterPeloId(new AviaoId(idNovoAviao));
            Assert.IsNotNull(aviao);
            Assert.AreEqual("Boeing 777", aviao.modelo());
            Assert.AreEqual(400, aviao.assentos());

        }

    }
}
