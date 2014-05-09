using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphanet.Acesso.Aplicacao.Data
{
    public class UsuarioData
    {
        public string nome { get; set; }
        public string email { get; set; }

        public UsuarioData(string nome, string email) {
            this.nome = nome;
            this.email = email;
        }
    }
}
