using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model;
using AlphaNet.PassagemAerea.Domain.Model.Clientes;
using AlphaNet.PassagemAerea.Aplicacao.Clientes.Data;
using Common.Domain.Model;
using AlphaNet.PassagemAerea.Domain.Model.Cidades;


namespace AlphaNet.PassagemAerea.Aplicacao.Clientes
{
    public class ClienteService
    {

        public ClienteData novoCliente(string nome,string email) {
            Cliente cliente = new Cliente(clienteRepositorio().proximaIdentidade(), nome, email);
            clienteRepositorio().salvar(cliente);
            return construir(cliente);
        }

        public void alterarNome(string clienteId, string nome) {
            Cliente cliente = clienteRepositorio().obterPeloId(new ClienteId(clienteId));
            cliente.alterarNome(nome);
            clienteRepositorio().salvar(cliente);
        }

        public void alterarDados(ClienteData comando){
            Cliente cliente = clienteRepositorio().obterPeloId(new ClienteId(comando.clienteId));
            if (comando.nome!=cliente.nome())
                cliente.alterarNome(comando.nome);
            if (comando.email != cliente.email())
                cliente.alterarEmail(comando.email);
            cliente.alterarPromocao(comando.promocao);
            if (comando.endereco != cliente.endereco()) 
                cliente.alterarEndereco(comando.endereco);
            if (comando.cpf != null && comando.cpf != cliente.cpf().ToString())
                cliente.alterarCpf(new CPF(comando.cpf));
            if (comando.cidade != null && comando.cidade.cidadeId != cliente.cidade().Id)
                cliente.alterarCidade(new CidadeId(comando.cidade.cidadeId));
            if (comando.telefone != cliente.telefone())
                cliente.alterarTelefone(comando.telefone);
            if (comando.celular != cliente.celular())
                cliente.alterarCelular(comando.celular);
            if (comando.renda != cliente.renda())
                cliente.alterarRenda(comando.renda);
            if (comando.ocupacao != cliente.ocupacao())
                cliente.alterarOcupacao(comando.ocupacao);
            if (comando.especial)
                cliente.definirComoEspecial();
            else
                cliente.definirComoNormal();
            if (comando.rg!= cliente.rg())
                cliente.alterarRg(comando.rg);
            if (comando.sexo != cliente.sexo())
                cliente.alterarSexo(comando.sexo);
            if (comando.numeroEndereco != cliente.numeroEndereco())
                cliente.alterarNumeroEndereco(comando.numeroEndereco);
            if (comando.bairro != cliente.bairro())
                cliente.alterarBairro(comando.bairro);
            if (comando.cep != cliente.cep())
                cliente.alterarCep(comando.cep);
            if (comando.desconto!= cliente.desconto())
                cliente.alterarDesconto(comando.desconto);

            clienteRepositorio().salvar(cliente);
        }

        public void excluirCliente(string clienteId) {
            clienteRepositorio().excluir(new ClienteId(clienteId));
        }

        public List<ClienteData> todosClientes() {
            
            List<ClienteData> result = new List<ClienteData>();

            foreach (Cliente cliente in clienteRepositorio().todosClientes()) 
                result.Add(construir(cliente));

            return result;
        }
        public List<ClienteData> todosClientesPromocao()
        {
            List<ClienteData> result = new List<ClienteData>();

            foreach (Cliente cliente in clienteRepositorio().clientesParaPromocao())
                result.Add(construir(cliente));

            return result;
        }

        public ClienteData obterCliente(string clienteId) {
            return construir(cliente(clienteId));
        }

        public ClienteData clientePorEmail(string email) {
            return construir(clienteRepositorio().clientePeloEmail(email));
        }

        private Cliente cliente(string clienteId)
        {
            return clienteRepositorio().obterPeloId(new ClienteId(clienteId));
        }

        private ClienteData construir(Cliente cliente) {

            if (cliente == null)
                return null;

            ClienteData data = new ClienteData();

            data.clienteId = cliente.clienteId().Id;
            data.nome = cliente.nome();
            data.email = cliente.email();

            data.rg = cliente.rg();
            if (cliente.cpf()!=null)
                data.cpf = cliente.cpf().ToString();
            
            if (cliente.ocupacao() != "")
                data.ocupacao = cliente.ocupacao();
            if (!cliente.renda().Equals(0))
                data.renda = cliente.renda();
            if (cliente.sexo() != "")
                data.sexo = cliente.sexo();
            if (!cliente.desconto().Equals(0))
                data.desconto = cliente.desconto();
            data.promocao = cliente.promocao();
            data.especial = cliente.especial();
            if (cliente.telefone() != "")
                data.telefone = cliente.telefone();
            if (cliente.celular() != "")
                data.celular = cliente.celular();
            if (cliente.endereco() != "")
                data.endereco = cliente.endereco();
                data.numeroEndereco = cliente.numeroEndereco();
                data.bairro = cliente.bairro();
                data.cep = cliente.cep();
                data.dataCadastro = cliente.dataCadastro();

            return data;
        }
        private ClienteRepositorio clienteRepositorio()
        {
            return DominioRegistro.clienteRepositorio();
        }
    }
}
