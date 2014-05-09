using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphanet.Acesso.Domain.Model.Usuarios
{
    public class Papel
    {
        private string _nome;

        public Papel(string nome) {
            this._nome = nome;
        }
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj)) return true;
            if (object.ReferenceEquals(null, obj)) return false;
            if (this.GetType() != obj.GetType()) return false;
            var vo = obj as Papel;
            return vo._nome.Equals(_nome);
        }

        public override int GetHashCode()
        {
            var hash = 3197;
            hash = hash * 179 +
                (_nome != null ? _nome.GetHashCode() : 0);

            return hash;

        }

    }
}
