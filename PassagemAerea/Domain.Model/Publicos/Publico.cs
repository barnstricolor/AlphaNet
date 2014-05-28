using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.PassagemAerea.Domain.Model.Publicos
{
    public abstract class Publico
    {
        protected string _nome;
        protected string _identidade;
        protected string _email;
        protected string _papel;

        public Publico(string identidade, string nome, string email, string papel) {
            this._identidade = identidade;
            this._nome = nome;
            this._email = email;
            this._papel = papel;
        }

        public string identidade() {
            return this._identidade;
        }

        public string nome() {
            return this._nome;
        }

        public string email() {
            return this._email;
        }
        public string papel()
        {
            return this._papel;
        }
    }
}
