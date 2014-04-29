using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.PassagemAerea.Aplicacao.Voos.Data
{
    public class VooCommand
    {
        public string vooId{ get; set;}
        public string clienteId{ get; set;}
        public int[] assentos { get; set; }
    }
}
