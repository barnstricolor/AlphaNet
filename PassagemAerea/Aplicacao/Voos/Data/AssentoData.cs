using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.PassagemAerea.Aplicacao.Voos.Data
{
    public class AssentoData
    {
        public int numero { get; set; }
        public bool reservado { get; set; }
        public AssentoData(int numero,bool reservado) {
            this.numero = numero;
            this.reservado = reservado;
        }
    }
}
