using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaNet.PassagemAerea.Domain.Model.Avioes
{
    public class Assento
    {
        private int numero;

        public Assento(int numero)
        {
            this.numero = numero;
        }
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj)) return true;
            if (object.ReferenceEquals(null, obj)) return false;
            if (this.GetType() != obj.GetType()) return false;
            var vo = obj as Assento;
            return vo.numero.Equals(numero);
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 3 + numero.GetHashCode();
            
            return hash;

        }

    }
}
