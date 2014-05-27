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
        public string vooId {get;set;}
        public string aviaoId { get; set; }
        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        public string aviaoModelo { get; set; }
        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        public string cidadeOrigemId { get; set; }
        public string cidadeOrigemNome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        public string cidadeDestinoId { get; set; }
        public string cidadeDestinoNome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        [DataType(DataType.Date, ErrorMessage = "Data inválida.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime partida { get; set; }
        public int totalAssentos { get; set; }
        public int reservados { get; set; }
        [Required(ErrorMessage = "Campo obrigatório", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]{1,7}$", ErrorMessage = "Este campo deve conter apenas números")]
        public double preco { get; set; }
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
        public string clienteId { get; set; }
        public string clienteNome { get; set; }
        public double preco { get; set; }
        public string assentos { get; set; }
    }

    public class VooReservaData
    {
        public string vooId { get; set; }
        public string aviaoModelo { get; set; }
        public string cidadeOrigemNome { get; set; }
        public string cidadeDestinoNome { get; set; }
        public DateTime partida { get; set; }
        public string clienteId { get; set; }
        public string clienteNome { get; set; }
        public double precoReserva { get; set; }
        public string assentosReservados { get; set; }  
    }
}
