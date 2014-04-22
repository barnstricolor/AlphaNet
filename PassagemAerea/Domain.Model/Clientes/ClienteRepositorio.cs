using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.PassagemAerea.Domain.Model.Clientes
{
    public interface ClienteRepositorio
    {
        ClienteId proximaIdentidade();
        void salvar(Cliente cliente);
        Cliente obterPeloId(ClienteId clienteId);
        List<Cliente> todosClientes();
        void limpar();
        void excluir(ClienteId clienteId);

    }
}
