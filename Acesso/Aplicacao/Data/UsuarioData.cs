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

        public string usuarioId { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string papel { get; set; }
        public string cep { get; set; }

        public UsuarioData(string login, string nome, string email)
        {
            this.login = login;
            this.nome = nome;
            this.email = email;

        }
        public UsuarioData()
        { 
        }
    }
}
