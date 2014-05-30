using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Aplicacao.Cidades.Data;
using System.ComponentModel.DataAnnotations;

namespace IU.Models
{
    public class ClienteData
    {
        [Required(ErrorMessage = "O campo Código é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Código")]
        [RegularExpression("^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public string clienteId {get; set; }

        [Required(ErrorMessage = "O campo Nome completo é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome completo")]
        [RegularExpression("^[a-zA-ZãÃáÁàÀêÊéÉèÈíÍìÌôÔõÕóÓòÒúÚùÙûÛçÇºª' ']{1,60}$", ErrorMessage = "Este campo deve conter apenas letras")]
        public string nome {get; set; }

        [Required(ErrorMessage = "O campo e-mail é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "E-mail")]
        [RegularExpression(@"[\w-]+@([\w-]+\.)+[\w-]+", ErrorMessage = "O e-mail informado não é valido")]
        public string email {get; set; }

        [Display(Name = "RG")]
        [RegularExpression("^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public string rg {get; set; }
        

        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "CPF")]
        [RegularExpression("^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public string cpf {get; set; }

        [Display(Name = "Ocupação")]
        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        public string ocupacao {get; set; }

        [Display(Name = "Renda")]
        [Required(ErrorMessage= "Campo obrigatório")]
        [RegularExpression("^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public double renda {get; set; }

        [Display(Name = "Sexo")]
        public string sexo {get; set; }

        [Display(Name = "Desconto de cliente especial")]
        public double desconto {get; set; }

        [Display(Name = "Receber promoções?")]
        public bool promocao {get; set; }

        [Display(Name = "Cliente Especial")]
        public bool especial {get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public string telefone {get; set; }

        [RegularExpression("^[0-9]{1,9}$", ErrorMessage = "Este campo deve conter apenas números")]
        [Display(Name = "Celular")]
        public string celular {get; set; }

        [Display(Name = "Endereço")]
        public string endereco {get; set; }

        [Display(Name = "Nº")]
        public string numeroEndereco {get; set; }

        [Display(Name = "Bairro")]
        public string bairro {get; set; }

        [Display(Name = "CEP")]
        [RegularExpression("^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public string cep {get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data inválida.")]        
        public DateTime dataCadastro { get; set; }

        [Display(Name = "Cidade")]
        public string cidade {get; set; }

    }
}
