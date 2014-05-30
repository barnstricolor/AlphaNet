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
        [Required(ErrorMessage = "Este campo deve ser preenchido!")]
        [Display(Name = "Código")]
        [RegularExpression(@"^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public string usuarioId { get; set; }

        [Required(ErrorMessage = "Este campo deve ser preenchido!", AllowEmptyStrings = false)]
        [RegularExpression(@"{1,50}", ErrorMessage = "O login deve possuir até 50 caracteres.")]
        [Display(Name = "Login")]
        public string login {get; set;}

        [Required(ErrorMessage = "Este campo deve ser preenchido!", AllowEmptyStrings = false)]
        [RegularExpression(@"{1,50}", ErrorMessage = "A senha deve possuir até 50 caracteres.")]
        [Display(Name = "Senha")]
        public string senha { get; set; }

        [Required(ErrorMessage = "Este campo deve ser preenchido!")]
        [Display(Name = "Nome")]
        public string nome {get; set;}

        [Required(ErrorMessage = "Este campo deve ser preenchido!")]
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
