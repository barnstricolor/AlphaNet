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

        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        [RegularExpression("^[a-zA-ZãÃáÁàÀêÊéÉèÈíÍìÌôÔõÕóÓòÒúÚùÙûÛçÇºª' '0123456789]{1,60}$", ErrorMessage = "Este campo deve conter apenas letras e números")]
        public string modelo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]{1,3}$", ErrorMessage = "Este campo deve conter apenas números")]
        public int assentos { get; set; }

    }
}
