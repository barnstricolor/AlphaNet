using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Mongo.Entidades
{
    public abstract class Entidade
    {
        public ObjectId Id { get; set; }
    }
}
