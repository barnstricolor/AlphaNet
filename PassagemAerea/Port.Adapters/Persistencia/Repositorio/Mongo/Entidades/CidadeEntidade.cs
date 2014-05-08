using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Mongo.Entidades
{
    public class CidadeEntidade:Entidade
    {
        public string cidadeId { get; set; }
        public string nome { get; set; }
        public string cep { get; set; }
    }
}
