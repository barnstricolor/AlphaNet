using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaNet.PassagemAerea.Domain.Model.Avioes
{
    public class Assento
    {
        private int _numero;

        public Assento(int numero)
        {
            this._numero = numero;
        }
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj)) return true;
            if (object.ReferenceEquals(null, obj)) return false;
            if (this.GetType() != obj.GetType()) return false;
            var vo = obj as Assento;
            return vo._numero.Equals(_numero);
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 3 + _numero.GetHashCode();
            
            return hash;

        }
        public int assento() {
            return this._numero;
        }
    }
}
