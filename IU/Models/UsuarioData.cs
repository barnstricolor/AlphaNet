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
        [Required(ErrorMessage = "O campo Código é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Código")]
        [RegularExpression(@"^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public string usuarioId { get; set; }

        [Required(ErrorMessage = "O campo Login é obrigatório")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,60}$", ErrorMessage = "O login deve ter até 60 caracteres.")]
        [Display(Name = "Login")]
        public string login { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "O campo e-mail é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "E-mail")]
        [RegularExpression(@"[\w-]+@([\w-]+\.)+[\w-]+", ErrorMessage = "O e-mail informado não é valido")]
        public string email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Senha")]
        public string senha { get; set; }

        [Display(Name = "Papel")]
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
