using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AlphaNet.PassagemAerea.Aplicacao.Avioes.Data
{
    public class AviaoData
    {
        public string aviaoId { get; set; }
        public string modelo { get; set; }
        public int assentos { get; set; }

    }
}
