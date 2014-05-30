using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AlphaNet.PassagemAerea.IU.Models
{
    public class Contato
    {
        [Required(ErrorMessage = "O campo NOME é obrigatório!", AllowEmptyStrings = false )]
        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "O campo e-mail é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "E-mail")]
        [RegularExpression(@"[\w-]+@([\w-]+\.)+[\w-]+", ErrorMessage = "O e-mail informado não é valido")]
        public string email { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public string telefone { get; set; }

        [Required(ErrorMessage = "O campo CIDADE é obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Cidade")]
        public string cidade { get; set; }

        [Required(ErrorMessage = "O campo UF é obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "UF")]
        public string uf { get; set; }

        [Required(ErrorMessage = "O campo ASSUNTO é obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Assunto")]
        public string assunto { get; set; }

        [Display(Name = "Observação")]
        public string observacao { get; set; }


    }
}