using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.PassagemAerea.Aplicacao.Voos.Comando
{
    public class VooComando
    {
        public string vooId{ get; set;}
        public string clienteId{ get; set;}
        public List<int> assentos { get; set; }

        public VooComando(string vooId, string clienteId, List<int> assentos)
        {
            this.vooId = vooId;
            this.clienteId = clienteId;
            this.assentos = assentos;
        }
    }
}
