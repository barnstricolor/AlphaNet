using AlphaNet.PassagemAerea.Aplicacao.Voos.Data;
using AlphaNet.PassagemAerea.Domain.Model;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using AlphaNet.PassagemAerea.Domain.Model.Cidades;
using AlphaNet.PassagemAerea.Domain.Model.Clientes;
using AlphaNet.PassagemAerea.Domain.Model.Voos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.PassagemAerea.Aplicacao.Voos
{
    public class VooService
    {
        private AviaoRepositorio aviaoRepositorio()
        {
            return DominioRegistro.aviaoRepositorio();
        }
        public List<AssentoData> mapaAssentos(string vooId)
        {
            List<AssentoData> result = new List<AssentoData>();
            
            Aviao aviao = aviaoParaTest();
            Voo voo = vooParaTest(aviao);
            voo.novaReserva(clienteParaTest("Ricardo"), aviao.assento(2), aviao.assento(3));
            for (int i = 0; i < aviao.assentos(); i++)
            {
                result.Add(new AssentoData(i,voo.assentoReservado(aviao.assento(i))));    
            }

            return result;
        }
        private Aviao aviaoParaTest()
        {
            return new Aviao(new AviaoId("1"), "Focker", 4);
        }
        private Cidade cidadeParaTest(string nome)
        {
            return new Cidade(new CidadeId(nome), nome, "14100");
        }
        private Cliente clienteParaTest(string nome)
        {
            return new Cliente(new ClienteId(nome), nome, "@");
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
