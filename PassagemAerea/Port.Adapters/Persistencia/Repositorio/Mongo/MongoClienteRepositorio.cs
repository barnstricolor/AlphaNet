using AlphaNet.PassagemAerea.Domain.Model.Clientes;
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
    public class MongoClienteRepositorio : AbstractMongoRepositorio, ClienteRepositorio 
    {
        public ClienteId proximaIdentidade()
        {
            return new ClienteId(Guid.NewGuid().ToString().ToUpper());
        }

        public void salvar(Cliente cliente)
        {
            ClienteEntidade entidade = entidadePeloId(cliente.clienteId());
            if (entidade == null) entidade = new ClienteEntidade();
            preencherEntidade(entidade, cliente);
            colecao().Save(entidade);
        }

        private MongoCollection<ClienteEntidade> colecao()
        {
            return database.GetCollection<ClienteEntidade>("clientes");
        }

        private ClienteEntidade entidadePeloId(ClienteId clienteId)
        {
            return colecao().FindOne(queryPeloId(clienteId));
        }
        public Cliente obterPeloId(ClienteId clienteId)
        {
            return modeloPelaEntidade(entidadePeloId(clienteId));
        }

        public List<Cliente> todosClientes()
        {
            List<Cliente> result = new List<Cliente>();
            MongoCursor<ClienteEntidade> cursor = colecao().FindAll();

            foreach (ClienteEntidade entidade in cursor) 
                result.Add(modeloPelaEntidade(entidade));

            return result;
        }

        public void limpar()
        {
            colecao().RemoveAll();
        }

        public void excluir(ClienteId clienteId)
        {
            colecao().Remove(queryPeloId(clienteId));
        }

        private Cliente modeloPelaEntidade(ClienteEntidade entidade)
        { 
            return new Cliente(
                new ClienteId(entidade.clienteId),
                entidade.nome,
                entidade.email);
        }

        private void preencherEntidade(ClienteEntidade entidade, Cliente cliente)
        {
            entidade.clienteId = cliente.clienteId().Id;
            entidade.nome = cliente.nome();
            entidade.email = cliente.email();
            entidade.rg = cliente.rg();
            entidade.cpf = cliente.cpf().ToString();

            entidade.ocupacao = cliente.ocupacao();
            entidade.renda = cliente.renda();
            entidade.sexo = cliente.sexo();
            entidade.desconto = cliente.desconto();
            entidade.promocao = cliente.promocao();
            entidade.especial = cliente.especial();

            entidade.telefone = cliente.telefone();
            entidade.celular = cliente.celular();

            entidade.endereco = cliente.endereco();
            entidade.numeroEndereco = cliente.numeroEndereco();
            entidade.bairro = cliente.bairro();
            entidade.cep = cliente.cep();
            entidade.dataCadastro = cliente.dataCadastro();

        }

        private IMongoQuery queryPeloId(ClienteId clienteId) {
            return Query.EQ("clienteId", clienteId.Id);
        }


        public Cliente clientePeloEmail(string email)
        {
            throw new NotImplementedException();
        }


        public List<Cliente> clientesParaPromocao()
        {
            throw new NotImplementedException();
        }


        public List<Cliente> clientesCidade(Domain.Model.Cidades.CidadeId cidadeId)
        {
            throw new NotImplementedException();
        }
    }
}
