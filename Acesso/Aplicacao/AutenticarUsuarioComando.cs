using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Alphanet.Acesso.Aplicacao
{
    public class AutenticarUsuarioComando
    {
        [Required(ErrorMessage = "O campo Login é obrigatório")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,60}$", ErrorMessage = "Dados inválidos, utilize até 60 caracteres.")]
        [Display(Name = "Login")]
        public string login { get; set; }
        [Required(ErrorMessage = "O campo Senha é obrigatório", AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z0-9]{1,20}$", ErrorMessage = "Dados inválidos, utilize até 20 caracteres.")]
        [Display(Name = "Senha")]
        public string senha { get; set; }

        public AutenticarUsuarioComando(string login, string senha) {
            this.login = login;
            this.senha = senha;
        }
        public AutenticarUsuarioComando() { }
    }
}
