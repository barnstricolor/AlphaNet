using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Aplicacao.Cidades.Data;
using System.ComponentModel.DataAnnotations;

namespace AlphaNet.PassagemAerea.Aplicacao.Clientes.Data
{
    public class ClienteData
    {
        public string clienteId {get; set; }

        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        [RegularExpression("^[a-zA-ZãÃáÁàÀêÊéÉèÈíÍìÌôÔõÕóÓòÒúÚùÙûÛçÇºª' ']{1,60}$", ErrorMessage = "Este campo deve conter apenas letras")]
        [Display(Name = "Nome completo")]
        public string nome {get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [RegularExpression(@"[\w-]+@([\w-]+\.)+[\w-]+", ErrorMessage = "O e-mail informado não é valido")]
        [Display(Name = "E-mail")]
        public string email {get; set; }

        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        [Display(Name  = "RG")]
        public string rg {get; set; }
        

        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        [Display(Name = "CPF")]
        public string cpf {get; set; }

        [Required(ErrorMessage = "O
        public string ocupacao {get; set; }
        public string renda {get; set; }
        public string sexo {get; set; }
        public string desconto {get; set; }
        public bool promocao {get; set; }
        public bool especial {get; set; }

        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]{1,9}$", ErrorMessage = "Este campo deve conter apenas números")]
        public string telefone {get; set; }
        public string celular {get; set; }

        public string endereco {get; set; }
        public string numeroEndereco {get; set; }
        public string bairro {get; set; }
        public string cep {get; set; }

        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        [DataType(DataType.Date, ErrorMessage = "Data inválida.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime cadastro { get; set; }
        public CidadeData _cidade {get; set; }

    }
}
