using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphanet.Acesso.Aplicacao
{
    public class AutenticarUsuarioComando
    {
        public string login { get; set; }
        public string senha { get; set; }

        public AutenticarUsuarioComando(string login, string senha) {
            this.login = login;
            this.senha = senha;
        }
        public AutenticarUsuarioComando() { }
    }
}
