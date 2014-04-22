using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Cidades;

namespace AlphaNet.PassagemAerea.Domain.Model.Clientes
{
    public class Cliente
    {
        private ClienteId _clienteId;
        private string _nome;
        private string _email;
        private string _rg;
        private string _cpf;
        
        /*private string _ocupacao;
        private string _renda;
        private string _sexo;
        private string _desconto;
        private bool _promocao;
        private bool _especial;        

        private string _telefone;
        private string _celular;

        private string _endereco;
        private string _numeroEndereco;
        private string _bairro;
        private string _cep;*/
        private Cidade _cidade;

        public Cliente(ClienteId clienteId,string nome, string email, string rg, string cpf, Cidade cidade)
        {
            this._clienteId = clienteId;
            setNome(nome);
            setEmail(email);
            setRg(rg);
            setCpf(cpf);
            this._cidade = cidade;
        }

        private void setNome(string nome) {
            if (nome == null || nome == "")
                throw new InvalidOperationException("Nome não pode ser vazio ou nulo.");
            this._nome = nome;
        }
        
        public void alterarNome(string nome)
        {
            setNome(nome);
        }
        
        private void setEmail(string email)
        {
            if (email == null || email == "")
                throw new InvalidOperationException("Email não pode ser vazio ou nulo.");
            this._email = email;
        }
        
        private void setRg(string rg)
        {
            if (rg == null || rg == "")
                throw new InvalidOperationException("R.G. não pode ser vazio ou nulo.");
            this._rg = rg;
        }

        private void setCpf(string cpf)
        {
            if (cpf == null || cpf == "")
                throw new InvalidOperationException("C.P.F. não pode ser vazio ou nulo.");
            this._cpf = cpf;
        }

        public ClienteId clienteId()
        {
            return this._clienteId;
        }
        //refazer com propriedades publicas ou objetos valor
        /*public string ocupacao()
        {
            return this._ocupacao;
        }

        public string renda()
        {
            return this._renda;
        }
        public string sexo()
        {
            return this._sexo;
        }

        public string desconto()
        {
            return this._desconto;
        }
        public bool promocao()
        {
            return this._promocao;
        }
        public bool especial()
        {
            return this._especial;
        }

        public string telefone()
        {
            return this._telefone;
        }
        public string celular()
        {
            return this._celular;
        }

        public string endereco()
        {
            return this._endereco;
        }
        public string numeroEndereco()
        {
            return this._numeroEndereco;
        }
        public string bairro()
        {
            return this._bairro;
        }

        public string cep()
        {
            return this._cep;
        }*/
        public Cidade cidade()
        {
            return this._cidade;
        }

    }
}
