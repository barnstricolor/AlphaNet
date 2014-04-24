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
        private ClienteId clienteId;
        private ISet<Assento> assentos;
        public Reserva(ClienteId clienteId, ISet<Assento> assentos)
        {
            this.clienteId = clienteId;
            this.assentos = assentos;
        }
        
        internal ISet<Assento> todosAssentos()
        {
            return assentos;
        }

        internal bool paraCliente(Cliente cliente)
        {
            return cliente.clienteId().Equals(this.clienteId);
        }
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj)) return true;
            if (object.ReferenceEquals(null, obj)) return false;
            if (this.GetType() != obj.GetType()) return false;
            var vo = obj as Reserva;
            return vo.clienteId.Equals(clienteId) && 
                vo.assentos.Equals(assentos);
        }

        public override int GetHashCode()
        {
            var hash = 197;
            hash = hash * 39 + 
                (clienteId != null ? clienteId.GetHashCode() : 0) +
                (assentos != null ? assentos.GetHashCode() : 0);

            return hash;

        }

    }
}
