using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace AlphaNet.PassagemAerea.Aplicacao.Voos.Data
{

    public class VooData
    {
        public string vooId {get;set;}
        public string aviaoId { get; set; }
        public string aviaoModelo { get; set; }
        public string cidadeOrigemId { get; set; }
        public string cidadeOrigemNome { get; set; }
        public string cidadeDestinoId { get; set; }
        public string cidadeDestinoNome { get; set; }
        public DateTime partida { get; set; }
        public int totalAssentos { get; set; }
        public int reservados { get; set; }
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

        public string cartao { get; set; }
        public string validade { get; set; }
        public string nome { get; set; }
        public string administradora { get; set; }

    }
}
