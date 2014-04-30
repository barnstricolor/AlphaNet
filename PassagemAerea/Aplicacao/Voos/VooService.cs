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
        private ClienteRepositorio clienteRepositorio()
        {
            return DominioRegistro.clienteRepositorio();
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
        public VooData novoVoo(string aviaoId , string cidadeOrigemId, string cidadeDestinoId, DateTime partida)
        {
            Aviao aviao = aviaoRepositorio().obterPeloId(new AviaoId(aviaoId));
            Cidade origem = cidadeRepositorio().obterPeloId(new CidadeId(cidadeOrigemId));
            Cidade destino = cidadeRepositorio().obterPeloId(new CidadeId(cidadeDestinoId));

            Voo voo = new Voo(vooRepositorio().proximaIdentidade(), aviao, origem,destino,partida);
            vooRepositorio().salvar(voo);
            
            VooData data = new VooData();
            data.vooId = voo.vooId().Id;
            data.aviaoId = voo.aviaoId().Id;
            data.aviaoModelo = aviao.modelo();
            data.partida = voo.partida();
            data.cidadeOrigemId = voo.origemId().Id;
            data.cidadeOrigemNome = origem.nome();
            data.cidadeDestinoId = voo.destinoId().Id;
            data.cidadeDestinoNome = destino.nome();
            return data;
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
                data.totalAssentos = aviao.assentos();
                data.reservados = voo.assentosReservados().Count;
                result.Add(data);
            }
            return result;
        }

        public VooData obterVoo(string vooId)
        {
            Voo voo = vooRepositorio().obterPeloId(new VooId(vooId));
            
            Aviao aviao = aviaoRepositorio().obterPeloId(voo.aviaoId());
            Cidade origem = cidadeRepositorio().obterPeloId(voo.origemId());
            Cidade destino = cidadeRepositorio().obterPeloId(voo.destinoId());

            VooData data = new VooData();
            data.vooId = voo.vooId().Id;
            data.aviaoId = voo.aviaoId().Id;
            data.aviaoModelo = aviao.modelo();
            data.partida = voo.partida();
            data.cidadeOrigemId = voo.origemId().Id;
            data.cidadeOrigemNome = origem.nome();
            data.cidadeDestinoId = voo.destinoId().Id;
            data.cidadeDestinoNome = destino.nome();
            data.totalAssentos = aviao.assentos();
            data.reservados = voo.assentosReservados().Count;
            return data;
        }

        public void novaReserva(string vooId, string clienteId, List<int> assentos)
        {
            Voo voo = vooRepositorio().obterPeloId(new VooId(vooId));
            Aviao aviao = aviaoRepositorio().obterPeloId(voo.aviaoId());
            Cliente cliente = clienteRepositorio().obterPeloId(new ClienteId(clienteId));
            
            List<Assento> lista = new List<Assento>();
            foreach (int assento in assentos)
                lista.Add(aviao.assento(assento));

            voo.novaReserva(
                cliente,
                lista.ToArray());
        }
    }
}
