using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Clientes;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;

namespace AlphaNet.PassagemAerea.Domain.Model.Voos
{
    public class Reserva
    {
        private static double DESCONTO=10;
        
        private ClienteId _clienteId;
        private ISet<Assento> _assentos;
        private double _preco;
        public ClienteId clienteId() {
            return this._clienteId;
        }
        public ISet<Assento> assentos(){
            return this._assentos;
        }
        public Reserva(Cliente cliente, ISet<Assento> assentos, double preco)
        {
            this._clienteId = cliente.clienteId();
            this._assentos = assentos;
            if (cliente.estaComoEspecial())
                this._preco = preco * (1 - DESCONTO / 100);
            else
                this._preco = preco;
            
        }
        
        internal ISet<Assento> todosAssentos()
        {
            return _assentos;
        }

        internal bool paraCliente(Cliente cliente)
        {
            return cliente.clienteId().Equals(this._clienteId);
        }
        internal bool paraCliente(ClienteId clienteId)
        {
            return clienteId.Equals(this._clienteId);
        }
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj)) return true;
            if (object.ReferenceEquals(null, obj)) return false;
            if (this.GetType() != obj.GetType()) return false;
            var vo = obj as Reserva;
            return vo._clienteId.Equals(_clienteId) && 
                vo._assentos.Equals(_assentos);
        }

        public override int GetHashCode()
        {
            var hash = 197;
            hash = hash * 39 + 
                (_clienteId != null ? _clienteId.GetHashCode() : 0) +
                (_assentos != null ? _assentos.GetHashCode() : 0);

            return hash;

        }

        public double total()
        {
            return this._preco * _assentos.Count;
        }
    }
}
