using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IU.Models
{
    public class AviaoData
    {
        public string aviaoId { get; set; }

        [Required(ErrorMessage = "O campo Modelo deve ser preenchido.")]
        [Display(Name = "Modelo")]
        [RegularExpression(@"[a-zA-Z0-9]{3,40}", ErrorMessage = "O campo MODELO deve conter de 3 à 40 caracteres.")]
        public string modelo { get; set; }

        [Required(ErrorMessage = "O campo Assento deve se preenchido")]
        [Display(Name = "Assentos")]
        public int assentos { get; set; }

    }
}
