using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphanet.Acesso.Aplicacao
{
    public class RegistrarNovoUsuarioComando
    {
        public string login {get; set;}
        public string senha { get; set; }
        public string nome {get; set;}
        public string email {get; set;}
        public RegistrarNovoUsuarioComando(){}
        public RegistrarNovoUsuarioComando(string login, string senha, string nome, string email) {
            this.login = login;
            this.senha = senha;
            this.nome = nome;
            this.email = email;
        }

    }
}
