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
        public List<AssentoData> mapaAssentos(string vooId)
        {            
            Voo voo = vooRepositorio().obterPeloId(new VooId(vooId));
            Aviao aviao = aviaoRepositorio().obterPeloId(voo.aviaoId());

            List<AssentoData> result = new List<AssentoData>();
            for (int i = 1; i <= aviao.assentos(); i++)
            {
                result.Add(new AssentoData(i,voo.assentoReservado(aviao.assento(i))));    
            }

            return result;
        }
        public VooData novoVoo(string aviaoId , string cidadeOrigemId, string cidadeDestinoId, DateTime partida, double preco)
        {
            Aviao aviao = aviaoRepositorio().obterPeloId(new AviaoId(aviaoId));
            Cidade origem = cidadeRepositorio().obterPeloId(new CidadeId(cidadeOrigemId));
            Cidade destino = cidadeRepositorio().obterPeloId(new CidadeId(cidadeDestinoId));

            Voo voo = new Voo(vooRepositorio().proximaIdentidade(), aviao, origem,destino,partida,preco);
            vooRepositorio().salvar(voo);
            
            return construir(voo);
        }
        public List<VooData> todosVoos()
        {
            List<VooData> result = new List<VooData>();
            foreach (Voo voo in vooRepositorio().todosVoos())
                result.Add(construir(voo));

            return result;
        }
        private ReservaData construirReserva(Reserva reserva) {
            
            ReservaData result = new ReservaData();

            result.clienteNome = cliente(reserva.clienteId().Id).nome();

            foreach (Assento assento in reserva.assentos())
                result.assentos += assento.assento()+" ";

            result.preco = reserva.total();
            result.clienteId = reserva.clienteId().Id;

            return result;

        }

        public void cancelarReserva(string vooId,string clienteId){
            voo(vooId).cancelarReserva(cliente(clienteId));
        }

        public VooData vooComReservas(string vooId)
        {
            Voo v = voo(vooId);
            VooData data = construir(v);

            foreach (Reserva reserva in v.reservas())                
                data.addReserva(construirReserva(reserva));

            return data;
        }

        private Voo voo(string vooId) {
            return vooRepositorio().obterPeloId(new VooId(vooId));
        }

        public VooData obterVoo(string vooId)
        {
            return construir(voo(vooId));
        }
        public void alterarPreco(string vooId, double novoPreco, bool promocional)
        {
            if (promocional)
                voo(vooId).precoPromocional(novoPreco);
            else
                voo(vooId).alterarPreco(novoPreco);
        }
        private Cliente cliente(string clienteId) {
            return clienteRepositorio().obterPeloId(new ClienteId(clienteId));
        }
        private Aviao aviao(AviaoId aviaoId) {
            return aviaoRepositorio().obterPeloId(aviaoId);
        }
        public void novaReserva(VooComando comando)
        {
            Aviao a = aviao(voo(comando.vooId).aviaoId());
            
            List<Assento> lista = new List<Assento>();

            foreach (int assento in comando.assentos)
                lista.Add(a.assento(assento));

            voo(comando.vooId).novaReserva(
                cliente(comando.clienteId),
                lista.ToArray());
        }

        public void excluir(string vooId) {

            vooRepositorio().excluir(new VooId(vooId));
        }
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
        private VooData construir(Voo voo)
        {

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
            data.preco = voo.preco();
            data.promocional = voo.promocional();
            return data;
        }

    }
}
