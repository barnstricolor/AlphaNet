using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using AlphaNet.PassagemAerea.Domain.Model.Cidades;
using AlphaNet.PassagemAerea.Domain.Model.Clientes;

namespace AlphaNet.PassagemAerea.Domain.Model.Voos
{
    public class Voo
    {
        private VooId _vooId;
        private AviaoId _aviaoId;
        private CidadeId _origemId;
        private CidadeId _destinoId;
        private DateTime _partida;
        private ISet<Reserva> reservas;
        private VooId vooId1;
        private Aviao aviao;
        private Cidade cidade1;
        private Cidade cidade2;
        private DateTime dateTime;
        private double _preco;

        public Voo(VooId vooId, Aviao aviao, Cidade origem, Cidade destino, DateTime partida, double preco)
        {
            this._vooId = vooId;
            this._aviaoId = aviao.aviaoId();
            this._origemId = origem.cidadeId();
            this._destinoId = destino.cidadeId();
            this._partida = partida;
            this._preco = preco;
            this.reservas = new HashSet<Reserva>();
        }

        public HashSet<Assento> listaAssentosReservados()
        {
            return new HashSet<Assento>(this.assentosReservados());
        }

        public void novaReserva(Cliente cliente, params Assento[] assentos)
        {
            foreach (Assento assento in assentos)
	        {
                if (this.assentosReservados().Contains(assento))
                    throw new InvalidOperationException("Assento Reservado");
	        }

            reservas.Add(
                new Reserva(
                    cliente, 
                    new HashSet<Assento>(assentos.ToList()),
                    this._preco));
        }

        public ISet<Assento> assentosReservados()
        {   
            ISet<Assento> result = new HashSet<Assento>();
            foreach (Reserva reserva in reservas)
                foreach (Assento assento in reserva.todosAssentos())
                    result.Add(assento);

            return result;

        }

        public Reserva obterReservaPeloCliente(Cliente cliente)
        {
            foreach (Reserva reserva in reservas)
            {
                if (reserva.paraCliente(cliente))
                    return reserva;
            }
            return null;
        }
        public void cancelarReserva(Cliente cliente)
        {
            Reserva reserva = obterReservaPeloCliente(cliente);
            if (reserva == null)
                throw new InvalidOperationException("Não existe reserva para este cliente");

            reservas.Remove(reserva);
        }
        public bool assentoReservado(Assento assento) {
            return assentosReservados().Contains(assento);
        }
        public VooId vooId() {
            return this._vooId;
        }
        public AviaoId aviaoId() {
            return this._aviaoId;
        }
        public DateTime partida() {
            return this._partida;    
        }
        public CidadeId origemId()
        {
            return this._origemId;
        }
        public CidadeId destinoId()
        {
            return this._destinoId;
        }

        public object preco()
        {
            return _preco;
        }
    }

}
