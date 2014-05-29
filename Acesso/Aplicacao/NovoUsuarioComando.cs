using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphanet.Acesso.Aplicacao
{
    public class NovoUsuarioComando
    {
        public string usuarioId { get; set; }
        public string login {get; set;}
        public string senha { get; set; }
        public string nome {get; set;}
        public string email {get; set;}
        public string papel {get; set;}

        public NovoUsuarioComando(string login, string senha, string nome, string email, string papel) {
            this.login = login;
            this.senha = senha;
            this.nome = nome;
            this.email = email;
            this.papel = papel;
        }

    }
}
