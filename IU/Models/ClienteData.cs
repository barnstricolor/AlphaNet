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
        [Required(ErrorMessage = "Este campo deve ser preenchido!")]
        [Display(Name = "Código")]
        [RegularExpression(@"^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public string clienteId {get; set; }

        [Required(ErrorMessage = "Este campo deve ser preenchido!")]
        [Display(Name = "Nome completo")]
        [RegularExpression(@"^[a-zA-Z' ']{1,60}$", ErrorMessage = "Este campo deve conter apenas letras, limite de 60 caracteres!")]
        public string nome {get; set; }

        [Required(ErrorMessage = "Este campo deve ser preenchido!", AllowEmptyStrings = false)]
        [Display(Name = "E-mail")]
        [RegularExpression(@"^[\w-]+@([\w-]+\.)+[\w-]+$", ErrorMessage = "O e-mail informado não é valido")]
        public string email {get; set; }

        [Display(Name = "RG")]
        [RegularExpression(@"^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números com até 11 dígitos!")]
        public string rg {get; set; }


        [Required(ErrorMessage = "Este campo deve ser preenchido!")]
        [Display(Name = "CPF")]
        [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}\-?\d{2}$", ErrorMessage = "Este campo deve conter apenas números, limite de 11 dígitos!")]
        public string cpf {get; set; }

        [Display(Name = "Ocupação")]
        [Required(ErrorMessage = "Este campo deve ser preenchido!")]
        public string ocupacao {get; set; }

        [Display(Name = "Renda")]
        [Required(ErrorMessage = "Este campo deve ser preenchido!")]
        [RegularExpression(@"^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números de 1 à 11 dígitos!")]
        public double renda {get; set; }

        [Display(Name = "Sexo")]
        public string sexo {get; set; }

        [Required(ErrorMessage = "Este campo deve ser preenchido!")]
        [Display(Name = "Desconto de cliente especial")]
        public double desconto {get; set; }

        [Display(Name = "Receber promoções?")]
        public bool promocao {get; set; }

        [Display(Name = "Cliente Especial")]
        public bool especial {get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Este campo deve ser preenchido!", AllowEmptyStrings = false)]
        [RegularExpression(@"^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números de 1 à 11 dígitos")]
        public string telefone {get; set; }

        [RegularExpression(@"^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números de 1 à 11 dígitos!")]
        [Display(Name = "Celular")]
        public string celular {get; set; }

        [Display(Name = "Endereço")]
        [RegularExpression(@"^[a-zA-Z0-9' ']{0,60}$", ErrorMessage = "Limite de 60 caracteres excedido!")]
        public string endereco {get; set; }

        [Display(Name = "Nº")]
        public string numeroEndereco {get; set; }

        [Display(Name = "Bairro")]
        public string bairro {get; set; }

        [Display(Name = "CEP")]
        [RegularExpression(@"^[0-9]{7,11}$", ErrorMessage = "Este campo deve conter apenas números, de 8 à 11 dígitos!")]
        public string cep {get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required(ErrorMessage = "Este campo deve ser preenchido!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy/hh:mm}")]
        [DataType(DataType.Date, ErrorMessage = "Data inválida.")]        
        public DateTime dataCadastro { get; set; }

        [Display(Name = "Cidade")]
        public string cidade {get; set; }

    }
}
