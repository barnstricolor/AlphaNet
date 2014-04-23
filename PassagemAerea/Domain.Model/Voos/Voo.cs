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
        private HashSet<Assento> assentosReservados;

        public Voo(Aviao aviao, Cidade origem, Cidade destino, DateTime partida)
        {
            this.aviao = aviao;
            this.origem = origem;
            this.destino = destino;
            this.partida = partida;
            this.assentosReservados = new HashSet<Assento>();
        }

        public HashSet<Assento> listaAssentosReservados()
        {
            return new HashSet<Assento>(this.assentosReservados);
        }

        public void novaReserva(Cliente cliente, params Assento[] assentos)
        {
            if (assentosReservados.Intersect(assentos).Any())
                throw new InvalidOperationException("Assento reservado");

            foreach (Assento assento in assentos)
	        {
                if (!this.assentosReservados.Contains(assento))
                    this.assentosReservados.Add(assento);
                else
                    throw new InvalidOperationException("Assento reservado");
		 
	        }

        }
    }
}
