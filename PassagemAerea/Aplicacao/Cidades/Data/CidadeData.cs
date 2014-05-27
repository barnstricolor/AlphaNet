using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace AlphaNet.PassagemAerea.Aplicacao.Cidades.Data
{
    public class CidadeData
    {
        public string cidadeId { get; set; }
        public string nome { get; set; }
        public string cep { get; set; }

    }
}
