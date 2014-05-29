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

        [Required(ErrorMessage = "O campo MODELO deve ser preenchido.")]
        [RegularExpression(@"[a-zA-Z0-9]{3,40}", ErrorMessage = "O campo MODELO deve conter de 3 à 40 caracteres.")]
        public string modelo { get; set; }

        [Required(ErrorMessage = "O campo ASSENTOS deve se preenchido corretamente, use apenas números.")]
        [RegularExpression(@"[0-9]{1,5}", ErrorMessage = "O campo ASSENTOS deve conter apenas números e de 1 à 5 caracteres")]
        public int assentos { get; set; }

    }
}
