using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
