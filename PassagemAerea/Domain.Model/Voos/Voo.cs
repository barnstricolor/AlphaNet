using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using AlphaNet.PassagemAerea.Domain.Model.Cidades;
using AlphaNet.PassagemAerea.Domain.Model.Clientes;
using AlphaNet.Common.Domain.Model;

namespace AlphaNet.PassagemAerea.Domain.Model.Voos
{
    public class Voo
    {
        private VooId _vooId;
        private AviaoId _aviaoId;
        private CidadeId _origemId;
        private CidadeId _destinoId;
        private DateTime _partida;
        private ISet<Reserva> _reservas;
        private double _preco;
        private bool _promocional;
        private DateTime _chegada;
        public int _id { get; set; }

        public Voo(VooId vooId, Aviao aviao, Cidade origem, Cidade destino, DateTime partida, double preco)
        {
            this._vooId = vooId;
            this._aviaoId = aviao.aviaoId();
            this._origemId = origem.cidadeId();
            this._destinoId = destino.cidadeId();
            this._partida = partida;
            this._chegada = partida.AddHours(1);
            this._preco = preco;
            this._promocional = false;
            this._reservas = new HashSet<Reserva>();
        }

        public HashSet<Assento> listaAssentosReservados()
        {
            return new HashSet<Assento>(this.assentosReservados());
        }
        public void novaReserva(Cliente cliente, params Assento[] assentos)
        {
            if (obterReservaPeloCliente(cliente)!=null)
                throw new InvalidOperationException("Já existe reserva para esse Cliente.");
            
            foreach (Assento assento in assentos)
	        {
                if (this.assentosReservados().Contains(assento))
                    throw new InvalidOperationException("Assento Reservado");
	        }

            _reservas.Add(
                new Reserva(
                    cliente, 
                    new HashSet<Assento>(assentos.ToList()),
                    this._preco));
        }
        public void adicionarReserva(Cliente cliente, params Assento[] assentos)
        {
            Reserva reserva = new Reserva(
                    cliente,
                    new HashSet<Assento>(assentos.ToList()),
                    this._preco);
            
            reserva._id = -1;

            _reservas.Add(reserva);
        }

        public ISet<Assento> assentosReservados()
        {   
            ISet<Assento> result = new HashSet<Assento>();
            foreach (Reserva reserva in _reservas)
                foreach (Assento assento in reserva.todosAssentos())
                    result.Add(assento);

            return result;

        }

        public Reserva obterReservaPeloCliente(Cliente cliente)
        {
            foreach (Reserva reserva in _reservas)
            {
                if (reserva.paraCliente(cliente))
                    return reserva;
            }
            return null;
        }
        public List<Reserva> obterReservas()
        {
            return _reservas.ToList();
        }
        public void cancelarReserva(Cliente cliente)
        {
            Reserva reserva = obterReservaPeloCliente(cliente);
            if (reserva == null)
                throw new InvalidOperationException("Não existe reserva para este cliente");

            _reservas.Remove(reserva);
        }
        public void cancelarTodasReservas()
        {
            this._reservas = new HashSet<Reserva>(); 
        }
        public bool assentoReservado(Assento assento)
        {
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

        public double preco()
        {
            return this._preco;
        }

        public void precoPromocional(double preco)
        {
            this._preco = preco;
            this._promocional = true;
            DomainEventPublisher.Instance.Publish(new PrecoPromocionalDefinido(this));
        }
        public bool promocional() {
            return this._promocional;
        }

        public void alterarPreco(double preco)
        {
            this._preco = preco;
            this._promocional = false;
        }
        public ISet<Reserva> reservas() {
            return this._reservas;
        }

        public bool temReservaParaCliente(ClienteId clienteId)
        {
            foreach (Reserva reserva in _reservas)
            {
                if (reserva.paraCliente(clienteId))
                    return true;
            }
            return false;
        }

        public DateTime chegada()
        {
            return this._chegada;
        }
    }

}
