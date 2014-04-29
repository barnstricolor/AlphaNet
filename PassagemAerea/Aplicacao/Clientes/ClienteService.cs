using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model;
using AlphaNet.PassagemAerea.Domain.Model.Clientes;
using AlphaNet.PassagemAerea.Aplicacao.Clientes.Data;


namespace AlphaNet.PassagemAerea.Aplicacao.Clientes
{
    public class ClienteService
    {
        private ClienteRepositorio clienteRepositorio() {
            return DominioRegistro.clienteRepositorio();            
        }

        public string novoCliente(string nome,string email) {
            Cliente cliente = new Cliente(clienteRepositorio().proximaIdentidade(), nome, email);
            clienteRepositorio().salvar(cliente);
            return cliente.clienteId().Id;
        }

        public void alterarNome(string clienteId, string nome) {
            Cliente cliente = clienteRepositorio().obterPeloId(new ClienteId(clienteId));
            cliente.alterarNome(nome);
            clienteRepositorio().salvar(cliente);
        }

        public void alterarDados(string clienteId, string nome, string email){
            Cliente cliente = clienteRepositorio().obterPeloId(new ClienteId(clienteId));
            cliente.alterarNome(nome);
            cliente.alterarEmail(email);
            clienteRepositorio().salvar(cliente);
        }

        public void excluirCliente(string clienteId) {
            clienteRepositorio().excluir(new ClienteId(clienteId));
        }

        public List<ClienteData> todosClientes() {
            List<ClienteData> result = new List<ClienteData>();

            foreach (Cliente data in clienteRepositorio().todosClientes()) 
            {
                ClienteData cliente = new ClienteData();
                cliente.clienteId = data.clienteId().Id;
                cliente.nome = data.nome();
                cliente.email = data.email();
                result.Add(cliente);
            }

            return result;
        }

        public ClienteData obterCliente(string clienteId) {
            ClienteData result = new ClienteData();
            
            Cliente cliente = clienteRepositorio().obterPeloId(new ClienteId(clienteId));

            result.clienteId = cliente.clienteId().Id;
            result.nome = cliente.nome();
            result.email = cliente.email();

            return result;
        }
    }
}
