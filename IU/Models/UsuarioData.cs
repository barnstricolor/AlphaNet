using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IU.Models
{
    public class UsuarioData
    {  
        public string usuarioId { get; set; }

        [Required(ErrorMessage = "O campo login é obrigatório")]
        public string login { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string nome { get; set; }

        public string email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório")]
        public string senha { get; set; }
        public string papel { get; set; }
//        public string cep { get; set; }

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
