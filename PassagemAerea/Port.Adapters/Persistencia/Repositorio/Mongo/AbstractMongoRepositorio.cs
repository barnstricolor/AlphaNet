using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Mongo.Entidades;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Mongo
{
    public class AbstractMongoRepositorio
    {
        protected static string connectionString = "mongodb://localhost";

        protected static MongoClient client = new MongoClient(connectionString);

        protected static MongoServer server = client.GetServer();

        protected static MongoDatabase database = server.GetDatabase("alphanet"); 
        
    }
}
