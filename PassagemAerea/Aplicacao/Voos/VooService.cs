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
        private VooRepositorio vooRepositorio()
        {
            return DominioRegistro.vooRepositorio();
        }
        private CidadeRepositorio cidadeRepositorio()
        {
            return DominioRegistro.cidadeRepositorio();
        }
        public List<AssentoData> mapaAssentos(string vooId)
        {            
            Voo voo = vooRepositorio().obterPeloId(new VooId(vooId));
            Aviao aviao = aviaoRepositorio().obterPeloId(voo.aviaoId());

            List<AssentoData> result = new List<AssentoData>();
            for (int i = 0; i < aviao.assentos(); i++)
            {
                result.Add(new AssentoData(i,voo.assentoReservado(aviao.assento(i))));    
            }

            return result;
        }

        public List<VooData> todosVoos()
        {
            List<VooData> result = new List<VooData>();
            foreach (Voo voo in vooRepositorio().todosVoos())
            {
                Aviao aviao = aviaoRepositorio().obterPeloId(voo.aviaoId());
                Cidade origem = cidadeRepositorio().obterPeloId(voo.origemId());
                Cidade destino = cidadeRepositorio().obterPeloId(voo.destinoId());

                VooData data=new VooData();
                data.vooId = voo.vooId().Id;
                data.aviaoId = voo.aviaoId().Id;
                data.aviaoModelo = aviao.modelo();
                data.partida = voo.partida();
                data.cidadeOrigemId= voo.origemId().Id;
                data.cidadeOrigemNome = origem.nome();
                data.cidadeDestinoId= voo.destinoId().Id;
                data.cidadeDestinoNome = destino.nome();
                result.Add(data);
            }
            return result;
        }
    }
}
