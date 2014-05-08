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
    public class MongoAviaoRepositorio : AbstractMongoRepositorio, AviaoRepositorio 
    {
     
        public AviaoId proximaIdentidade()
        {
            return new AviaoId(Guid.NewGuid().ToString().ToUpper());
        }

        public void salvar(Aviao aviao)
        {
            AviaoEntidade entidade = entidadePeloId(aviao.aviaoId());
            if (entidade == null) entidade = new AviaoEntidade();
            preencherEntidade(entidade,aviao);
            colecao().Save(entidade);
        }

        private MongoCollection<AviaoEntidade> colecao()
        {
            return database.GetCollection<AviaoEntidade>("avioes");
        }

        private AviaoEntidade entidadePeloId(AviaoId aviaoId)
        {
            return colecao().FindOne(queryPeloId(aviaoId));
        }
        public Aviao obterPeloId(AviaoId aviaoId)
        {
            return modeloPelaEntidade(entidadePeloId(aviaoId));
        }

        public List<Aviao> todosAvioes()
        {
            List<Aviao> result = new List<Aviao>();
            MongoCursor<AviaoEntidade> cursor = colecao().FindAll(); 
            
            foreach (AviaoEntidade entidade in cursor) 
                result.Add(modeloPelaEntidade(entidade));

            return result;
        }

        public void limpar()
        {
            throw new NotImplementedException();
        }

        public void excluir(AviaoId aviaoId)
        {
            colecao().Remove(queryPeloId(aviaoId));
        }

        private Aviao modeloPelaEntidade(AviaoEntidade entidade) { 
            return new Aviao(
                new AviaoId(entidade.aviaoId),
                entidade.modelo,
                entidade.assentos);
        }

        private void preencherEntidade(AviaoEntidade entidade, Aviao aviao) {
            entidade.aviaoId = aviao.aviaoId().Id;
            entidade.modelo = aviao.modelo();
            entidade.assentos = aviao.assentos();
        }

        private IMongoQuery queryPeloId(AviaoId aviaoId) {
            return Query.EQ("aviaoId", aviaoId.Id);
        }
    }
}
