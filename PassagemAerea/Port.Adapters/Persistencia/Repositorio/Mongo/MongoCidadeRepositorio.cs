using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using AlphaNet.PassagemAerea.Domain.Model.Cidades;
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
    public class MongoCidadeRepositorio : AbstractMongoRepositorio, CidadeRepositorio 
    {
        public CidadeId proximaIdentidade()
        {
            return new CidadeId(Guid.NewGuid().ToString().ToUpper());
        }

        public void salvar(Cidade cidade)
        {
            CidadeEntidade entidade = entidadePeloId(cidade.cidadeId());
            if (entidade == null) entidade = new CidadeEntidade();
            preencherEntidade(entidade, cidade);
            colecao().Save(entidade);
        }

        private MongoCollection<CidadeEntidade> colecao()
        {
            return database.GetCollection<CidadeEntidade>("cidades");
        }

        private CidadeEntidade entidadePeloId(CidadeId cidadeId)
        {
            return colecao().FindOne(queryPeloId(cidadeId));
        }
        public Cidade obterPeloId(CidadeId cidadeId)
        {
            return modeloPelaEntidade(entidadePeloId(cidadeId));
        }

        public List<Cidade> todasCidades()
        {
            List<Cidade> result = new List<Cidade>();
            MongoCursor<CidadeEntidade> cursor = colecao().FindAll();

            foreach (CidadeEntidade entidade in cursor) 
                result.Add(modeloPelaEntidade(entidade));

            return result;
        }

        public void limpar()
        {
            throw new NotImplementedException();
        }

        public void excluir(CidadeId cidadeId)
        {
            colecao().Remove(queryPeloId(cidadeId));
        }

        private Cidade modeloPelaEntidade(CidadeEntidade entidade)
        { 
            return new Cidade(
                new CidadeId(entidade.cidadeId),
                entidade.nome,
                entidade.cep);
        }

        private void preencherEntidade(CidadeEntidade entidade, Cidade cidade)
        {
            entidade.cidadeId = cidade.cidadeId().Id;
            entidade.nome = cidade.nome();
            entidade.cep = cidade.cep();
        }

        private IMongoQuery queryPeloId(CidadeId cidadeId) {
            return Query.EQ("cidadeId", cidadeId.Id);
        }
    }
}
