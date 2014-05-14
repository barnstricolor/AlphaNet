using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Clientes;

namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Memoria
{
    public class MemoriaClienteRepositorio : ClienteRepositorio
    {
        private Dictionary<string, Cliente> store;

        public MemoriaClienteRepositorio()
        {
            this.store = new Dictionary<string, Cliente>();
        }

        public ClienteId proximaIdentidade()
        {
            return new ClienteId(Guid.NewGuid().ToString().ToUpper());
        }

        public void salvar(Cliente cliente)
        {
            store[cliente.clienteId().Id] = cliente;
        }

        public Cliente obterPeloId(ClienteId clienteId)
        {
            return store[clienteId.Id];
        }

        public List<Cliente> todosClientes()
        {
            return store.Values.ToList();
        }

        public void limpar()
        {
            store.Clear();
        }

        public void excluir(ClienteId clienteId)
        {
            store.Remove(clienteId.Id);
        }


        public Cliente clientePeloEmail(string email)
        {
            foreach (Cliente cliente in store.Values.ToList())
                if (email.Equals(cliente.email()))
                    return cliente;
            return null;
        }
    }
}
