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

        public Publico(string identidade, string nome, string email) {
            this._identidade = identidade;
            this._nome = nome;
            this._email = email;
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

    }
}
