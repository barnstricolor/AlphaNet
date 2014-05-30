using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace IU.Models
{
    public class CidadeData
    {
        public string cidadeId { get; set; }

        [Required(ErrorMessage = "O campo Nome da Cidade deve ser preenchido.")]
        [Display(Name = "Cidade")]
        [RegularExpression(@"{1,50}", ErrorMessage = "O campo NOME deve conter até 50 caracteres.")]
        public string nome { get; set; }

        [Required(ErrorMessage = "O campo CEP deve ser preenchido.")]
        [Display(Name = "CEP")]
        [RegularExpression("@[0-9]{8,8}$", ErrorMessage = "Preencha o CEP de forma correta.")]
        public string cep { get; set; }

    }
}
