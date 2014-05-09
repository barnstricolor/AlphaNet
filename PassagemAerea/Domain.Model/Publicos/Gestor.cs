using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.PassagemAerea.Domain.Model.Publicos
{
    public class Gestor: Publico
    {
        public Gestor(string identidade, string nome, string email) : 
            base(identidade, nome, email) {}

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj)) return true;
            if (object.ReferenceEquals(null, obj)) return false;
            if (this.GetType() != obj.GetType()) return false;
            var vo = obj as Gestor;
            return vo._identidade.Equals(_identidade) &&
                vo._nome.Equals(_nome) &&
                vo._email.Equals(_email);
        }

        public override int GetHashCode()
        {
            var hash = 337;
            hash = hash * 49 +
                (_identidade != null ? _identidade.GetHashCode() : 0) +
                (_nome != null ? _nome.GetHashCode() : 0)+
                (_email != null ? _email.GetHashCode() : 0);

            return hash;

        }
    }
}
