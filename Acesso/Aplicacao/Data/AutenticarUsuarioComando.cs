using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphanet.Acesso.Aplicacao.Data
{
    public class AutenticarUsuarioComando
    {
        public string nome { get; set; }
        public string senha { get; set; }

        public AutenticarUsuarioComando(string nome, string senha) {
            this.nome = nome;
            this.senha = senha;
        }
    }
}
