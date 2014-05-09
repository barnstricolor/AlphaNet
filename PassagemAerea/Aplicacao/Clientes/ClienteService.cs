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

        public ClienteData novoCliente(string nome,string email) {
            Cliente cliente = new Cliente(clienteRepositorio().proximaIdentidade(), nome, email);
            clienteRepositorio().salvar(cliente);
            return contruir(cliente);
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

            foreach (Cliente cliente in clienteRepositorio().todosClientes()) 
                result.Add(contruir(cliente));

            return result;
        }

        public ClienteData obterCliente(string clienteId) {
            return contruir(cliente(clienteId));
        }

        private Cliente cliente(string clienteId)
        {
            return clienteRepositorio().obterPeloId(new ClienteId(clienteId));
        }

        private ClienteData contruir(Cliente cliente) {

            ClienteData data = new ClienteData();

            data.clienteId = cliente.clienteId().Id;
            data.nome = cliente.nome();
            data.email = cliente.email();

            data.rg = cliente.rg();
            data.cpf = cliente.cpf();

            data.ocupacao = cliente.ocupacao();
            data.renda = cliente.renda();
            data.sexo = cliente.sexo();
            data.desconto = cliente.desconto();
            data.promocao = cliente.promocao();
            data.especial = cliente.especial();

            data.telefone = cliente.telefone();
            data.celular = cliente.celular();

            data.endereco = cliente.endereco();
            data.numeroEndereco = cliente.numeroEndereco();
            data.bairro = cliente.bairro();
            data.cep = cliente.cep();
            data.cadastro = cliente.cadastro();

            return data;
        }
        private ClienteRepositorio clienteRepositorio()
        {
            return DominioRegistro.clienteRepositorio();
        }
    }
}
