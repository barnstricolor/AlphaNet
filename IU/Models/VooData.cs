using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IU.Models
{

    public class VooData
    {
        [Required(ErrorMessage = "O campo Código é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Código")]
        [RegularExpression("^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public string vooId {get;set;}

        [Required(ErrorMessage = "O campo Código do Avião é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Código do Avião")]
        //[RegularExpression("^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public string aviaoId { get; set; }

        [Required(ErrorMessage = "O campo Modelo do Avião é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Modelo do Avião")]
        public string aviaoModelo { get; set; }

        [Required(ErrorMessage = "Campo Código da Cidade de origem", AllowEmptyStrings = false)]
        [Display(Name = "Código da Cidade de origem")]
        //[RegularExpression("^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public string cidadeOrigemId { get; set; }

        [Required(ErrorMessage = "Campo Cidade de origem", AllowEmptyStrings = false)]
        [Display(Name = "Cidade de origem")]
        //[RegularExpression("^[a-zA-ZãÃáÁàÀêÊéÉèÈíÍìÌôÔõÕóÓòÒúÚùÙûÛçÇºª' ']{1,60}$", ErrorMessage = "Este campo deve conter apenas letras")]
        public string cidadeOrigemNome { get; set; }

        [Required(ErrorMessage = "Campo Código da Cidade de destino", AllowEmptyStrings = false)]
        [Display(Name = "Código da Cidade de destino")]
        //[RegularExpression("^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public string cidadeDestinoId { get; set; }

        [Required(ErrorMessage = "Campo Cidade de destino", AllowEmptyStrings = false)]
        [Display(Name = "Cidade de destino")]
        //[RegularExpression("^[a-zA-ZãÃáÁàÀêÊéÉèÈíÍìÌôÔõÕóÓòÒúÚùÙûÛçÇºª' ']{1,60}$", ErrorMessage = "Este campo deve conter apenas letras")]
        public string cidadeDestinoNome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        [DataType(DataType.Date, ErrorMessage = "Data inválida.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/dd/MM/ hh:mm}",  ApplyFormatInEditMode = true)]
        public DateTime partida { get; set; }

        [Required ( ErrorMessage  =  "Campo obrigatório Formato HH:. MM (24 horas time)" )] 
        [DisplayFormat ( ApplyFormatInEditMode  =  true ,  DataFormatString  =  "{0:HH:mm}" )] 
        public  TimeSpan  horario {  get ;  set ;  }


        [Required(ErrorMessage = "Campo Cidade de origem", AllowEmptyStrings = false)]
        [Display(Name = "Cidade de origem")]
        public int totalAssentos { get; set; }
        
        [Display(Name = "Reservados")]
        public int reservados { get; set; }

        [Required(ErrorMessage = "Campo Preço é obrigatório", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]{1,7}$", ErrorMessage = "Este campo deve conter apenas números")]
        public double preco { get; set; }

        [Display(Name = "Promocional")]
        public bool promocional { get; set; }
        public List<ReservaData> _reservas = new List<ReservaData>();

        public void addReserva(ReservaData reserva) {
            this._reservas.Add(reserva);
        }
        public List<ReservaData> reservas() {
            return this._reservas;
        }
    }

    public class ReservaData
    {
        [Required(ErrorMessage = "O campo Código é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Código")]
        [RegularExpression("^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public string clienteId { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome")]
        [RegularExpression("^[a-zA-ZãÃáÁàÀêÊéÉèÈíÍìÌôÔõÕóÓòÒúÚùÙûÛçÇºª' ']{1,60}$", ErrorMessage = "Este campo deve conter apenas letras")]
        public string clienteNome { get; set; }

        [Required(ErrorMessage = "O campo Preço é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Preço")]
        [RegularExpression("^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public double preco { get; set; }

        [Display(Name = "Assentos")]
        public string assentos { get; set; }
    }

    public class VooReservaData
    {
        [Required(ErrorMessage = "O campo Código é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Código")]
        [RegularExpression("^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public string vooId { get; set; }

        [Required(ErrorMessage = "O campo Modelo do Avião é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Modelo do Avião")]
        public string aviaoModelo { get; set; }

        [Required(ErrorMessage = "Campo Cidade de origem", AllowEmptyStrings = false)]
        [Display(Name = "Cidade de origem")]
        [RegularExpression("^[a-zA-ZãÃáÁàÀêÊéÉèÈíÍìÌôÔõÕóÓòÒúÚùÙûÛçÇºª' ']{1,60}$", ErrorMessage = "Este campo deve conter apenas letras")]
        public string cidadeOrigemNome { get; set; }

        [Required(ErrorMessage = "Campo Cidade de destino", AllowEmptyStrings = false)]
        [Display(Name = "Cidade de destino")]
        [RegularExpression("^[a-zA-ZãÃáÁàÀêÊéÉèÈíÍìÌôÔõÕóÓòÒúÚùÙûÛçÇºª' ']{1,60}$", ErrorMessage = "Este campo deve conter apenas letras")]
        public string cidadeDestinoNome { get; set; }

        [Required(ErrorMessage = "Campo de Data é obrigatório", AllowEmptyStrings = false)]
        [DataType(DataType.Date, ErrorMessage = "Data inválida.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/dd/MM/ hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime partida { get; set; }

        [Required(ErrorMessage = "O campo Código do Cliente é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Código do Cliente")]
        [RegularExpression("^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public string clienteId { get; set; }

        [Required(ErrorMessage = "O campo Nome do Cliente é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome completo")]
        [RegularExpression("^[a-zA-ZãÃáÁàÀêÊéÉèÈíÍìÌôÔõÕóÓòÒúÚùÙûÛçÇºª' ']{1,60}$", ErrorMessage = "Este campo deve conter apenas letras")]
        public string clienteNome { get; set; }

        [Required(ErrorMessage = "O campo Preço é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Preço")]
        [RegularExpression("^[0-9]{1,11}$", ErrorMessage = "Este campo deve conter apenas números")]
        public double precoReserva { get; set; }

        [Display(Name = "Assentos Reservados")]
        public string assentosReservados { get; set; }  
    }
}
