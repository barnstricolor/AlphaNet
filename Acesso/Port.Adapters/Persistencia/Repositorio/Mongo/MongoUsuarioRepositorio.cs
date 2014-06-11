using Alphanet.Acesso.Domain.Model.Usuarios;
using AlphaNet.Acesso.Domain.Model.Usuarios;
using AlphaNet.Acesso.Port.Adapters.Persistencia.Repositorio.Mongo.Entidades;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.Acesso.Port.Adapters.Persistencia.Repositorio.Mongo
{
    public class MongoUsuarioRepositorio : AbstractMongoRepositorio, UsuarioRepositorio 
    {
     
        public UsuarioId proximaIdentidade()
        {
            return new UsuarioId(Guid.NewGuid().ToString().ToUpper());
        }

        public void salvar(Usuario usuario)
        {
            UsuarioEntidade entidade = entidadePeloId(usuario.usuarioId());
            if (entidade == null) entidade = new UsuarioEntidade();
            preencherEntidade(entidade,usuario);
            colecao().Save(entidade);
        }

        private MongoCollection<UsuarioEntidade> colecao()
        {
            return database.GetCollection<UsuarioEntidade>("usuarios");
        }

        private UsuarioEntidade entidadePeloId(UsuarioId UsuarioId)
        {
            return colecao().FindOne(queryPeloId(UsuarioId));
        }
        public Usuario obterPeloId(UsuarioId UsuarioId)
        {
            return modeloPelaEntidade(entidadePeloId(UsuarioId));
        }

        public void limpar()
        {
            throw new NotImplementedException();
        }

        public void excluir(UsuarioId UsuarioId)
        {
            colecao().Remove(queryPeloId(UsuarioId));
        }

        private Usuario modeloPelaEntidade(UsuarioEntidade entidade) { 
            return new Usuario(
                new UsuarioId(entidade._usuarioId.Id),
                entidade._login,
                entidade._senha,
                entidade._nome,
                entidade._email,
                entidade._papel);
        }

        private void preencherEntidade(UsuarioEntidade entidade, Usuario Usuario) {
            entidade._usuarioId = Usuario.usuarioId();
            entidade._login = Usuario.login();
            entidade._senha = Usuario.senha();
            entidade._nome = Usuario.nome();
            entidade._email = Usuario.email();
            entidade._papel = Usuario.papel();
        }

        private IMongoQuery queryPeloId(UsuarioId UsuarioId) {
            return Query.EQ("UsuarioId", UsuarioId.Id);
        }

        public Usuario obterPeloEmail(string email)
        {
            throw new NotImplementedException();
        }

        public void remover(UsuarioId usuarioId)
        {
            throw new NotImplementedException();
        }

        public Usuario usuarioPelaCredencialAutenticacao(string usuario, string senha)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> obterTodos()
        {
            List<Usuario> result = new List<Usuario>();
            MongoCursor<UsuarioEntidade> cursor = colecao().FindAll();

            foreach (UsuarioEntidade entidade in cursor)
                result.Add(modeloPelaEntidade(entidade));

            return result;
        }
    }
}
