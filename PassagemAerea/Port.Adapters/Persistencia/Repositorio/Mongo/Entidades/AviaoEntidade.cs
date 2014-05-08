using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Mongo.Entidades
{
    public class AviaoEntidade:Entidade
    {
        public string aviaoId { get; set; }
        public string modelo { get; set; }
        public int assentos { get; set; }
    }
}
