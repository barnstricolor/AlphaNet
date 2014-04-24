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
        private Aviao aviao;
        private Cidade origem;
        private Cidade destino;
        private DateTime partida;
        private ISet<Reserva> reservas;
        public Voo(Aviao aviao, Cidade origem, Cidade destino, DateTime partida)
        {
            this.aviao = aviao;
            this.origem = origem;
            this.destino = destino;
            this.partida = partida;
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
                    cliente.clienteId(), 
                    new HashSet<Assento>(assentos.ToList())));
        }

        private ISet<Assento> assentosReservados()
        {   
            ISet<Assento> result = new HashSet<Assento>();
            foreach (Reserva reserva in reservas)
                foreach (Assento assento in reserva.todosAssentos())
                    result.Add(assento);

            return result;

        }

        private Reserva obterReservaPeloCliente(Cliente cliente)
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
    }

}
