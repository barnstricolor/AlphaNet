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
        [Display(Name = "Nome", Prompt = "Nome")]
        public string nome { get; set; }
        [Required(ErrorMessage = "O campo E-MAIL é obrigatório!", AllowEmptyStrings = false)]
        public string email { get; set; }
        [Required(ErrorMessage = "O campo TELEFONE é obrigatório!", AllowEmptyStrings = false)]
        public string telefone { get; set; }
        [Required(ErrorMessage = "O campo CIDADE é obrigatório!", AllowEmptyStrings = false)]
        public string cidade { get; set; }
        [Required(ErrorMessage = "O campo UF é obrigatório!", AllowEmptyStrings = false)]
        public string uf { get; set; }
        [Required(ErrorMessage = "O campo ASSUNTO é obrigatório!", AllowEmptyStrings = false)]
        public string assunto { get; set; }
        public string observacao { get; set; }
    }
}