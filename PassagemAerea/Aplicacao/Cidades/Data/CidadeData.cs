using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace AlphaNet.PassagemAerea.Aplicacao.Cidades.Data
{
    public class CidadeData
    {
        public string cidadeId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        //[RegularExpression("^[a-zA-ZãÃáÁàÀêÊéÉèÈíÍìÌôÔõÕóÓòÒúÚùÙûÛçÇºª' ', 0-9]{1,40}$", ErrorMessage = "Este campo não deve conter caracteres especiais")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]{1,7}$", ErrorMessage = "Este campo deve conter apenas números")]
        public string cep { get; set; }

    }
}
