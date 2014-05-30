using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IU.Models
{
    public class NovoUsuarioComando
    {
        [Required(ErrorMessage = "O campo Código é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Código")]
        [RegularExpression("^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public string usuarioId { get; set; }

        [Required(ErrorMessage = "O campo Login é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Login")]
        public string login {get; set;}

        [Required(ErrorMessage = "O campo Senha é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Senha")]
        public string senha { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome")]
        public string nome {get; set;}

        [Required(ErrorMessage = "O campo E-mail é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "E-mail")]
        [RegularExpression(@"[\w-]+@([\w-]+\.)+[\w-]+", ErrorMessage = "O e-mail informado não é valido")]
        public string email {get; set;}

        [Display(Name = "Papel")]
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
