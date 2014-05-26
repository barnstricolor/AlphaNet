using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Cidades;
using Common.Domain.Model;

namespace AlphaNet.PassagemAerea.Domain.Model.Clientes
{
    public class Cliente
    {
        private ClienteId _clienteId;
        private string _nome;
        private string _email;
        private string _rg;
        private CPF _cpf;
        private bool _especial;        
        private string _ocupacao;
        private double _renda;
        private string _sexo;
        private double _desconto;
        private bool _promocao;       

        private string _telefone;
        private string _celular;

        private string _endereco;
        private string _numeroEndereco;
        private string _bairro;
        private string _cep;
        private CidadeId _cidade;
        private DateTime _dataCadastro;
        
        public int _id { get; set; }

        public Cliente(ClienteId clienteId,string nome, string email)
        {
            this._clienteId = clienteId;
            setNome(nome);
            setEmail(email);            
            this._cidade = null;
            this._cpf = null;
            this._especial = false;
        }

        public void alterarNome(string nome)
        {
            setNome(nome);
        }

        public void alterarEmail(string email)
        {
            setEmail(email);
        }

        public ClienteId clienteId()
        {
            return this._clienteId;
        }
        public string nome()
        {
            return this._nome;
        }
        public string email()
        {
            return this._email;
        }
        public string ocupacao()
        {
            return this._ocupacao;
        }

        public double renda()
        {
            return this._renda;
        }
        public string sexo()
        {
            return this._sexo;
        }

        public double desconto()
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
        }
        public CidadeId cidade()
        {
            return this._cidade;
        }

        public bool estaComoEspecial()
        {
            return this._especial;
        }

        public void definirComoEspecial()
        {
            setEspecial(true);
        }

        public void definirComoNormal()
        {
            setEspecial(false);
        }

        public string rg()
        {
            return this._rg;
        }
        public CPF cpf()
        {
            return this._cpf;
        }
        public DateTime dataCadastro()
        {
            return this._dataCadastro;
        }

        private void setNome(string nome)
        {
            if (nome == null || nome == "")
                throw new InvalidOperationException("Nome não pode ser vazio ou nulo.");
            this._nome = nome;
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
        private void setCpf(CPF cpf)
        {
            if (cpf == null)
                throw new InvalidOperationException("C.P.F. não pode ser nulo.");
            this._cpf = cpf;
        }
        private void setEspecial(bool especial)
        {
            this._especial = true;
        }
        private void setOcupacao(string ocupacao)
        {
            if (ocupacao == null || ocupacao == "")
                throw new InvalidOperationException("Ocupação não pode ser vazia ou nulo.");
            this._ocupacao = ocupacao;
        }
        private void setRenda(double renda)
        {
            if (renda.Equals(0))
                throw new InvalidOperationException("Renda não pode ser vazia ou zero.");
            this._renda = renda;
        }
        private void setSexo(string sexo)
        {
            if (sexo == null || sexo == "")
                throw new InvalidOperationException("Sexo não pode ser vazio ou nulo.");
            this._sexo = sexo;
        }
        private void setDesconto(double desconto)
        {
            if (desconto.Equals(0))
                throw new InvalidOperationException("Desconto não pode ser vazio ou zero.");
            this._desconto = desconto;
        }
        private void setPromocao(bool promocao)
        {
            this._promocao = promocao;
        }
        private void setTelefone(string telefone)
        {
            if (telefone == null || telefone=="")
                throw new InvalidOperationException("Telefone não pode ser vazio ou nulo.");
            this._telefone = telefone;
        }
        private void setCelular(string celular)
        {
            if (celular == null || celular == "")
                throw new InvalidOperationException("Celular não pode ser vazio ou nulo.");
            this._celular = celular;
        }
        private void setEndereco(string endereco)
        {
            if (endereco == null || endereco == "")
                throw new InvalidOperationException("Endereco não pode ser vazio ou nulo.");
            this._endereco = endereco;
        }
        private void setNumeroEndereco(string numeroEndereco)
        {
            if (numeroEndereco == null || numeroEndereco == "")
                throw new InvalidOperationException("Número endereço não pode ser vazio ou nulo.");
            this._numeroEndereco = numeroEndereco;
        }
        private void setBairro(string bairro)
        {
            if (bairro == null || bairro == "")
                throw new InvalidOperationException("Bairro não pode ser vazio ou nulo.");

            this._bairro = bairro;
        }
        private void setCep(string cep)
        {
            if (cep == null || cep == "")
                throw new InvalidOperationException("C.E.P.não pode ser vazio ou nulo.");

            this._cep = cep;
        }
        private void setCidade(CidadeId cidadeId)
        {
            if (cidadeId == null || cidadeId.Id == "")
                throw new InvalidOperationException("Cidade não pode ser vazio ou nulo.");
            this._cidade = cidadeId;
        }
        private void setDataCadastro(DateTime dataCadastro)
        {
            if (dataCadastro == null)
                throw new InvalidOperationException("Data de Cadastro deve ser informada.");
            this._dataCadastro = dataCadastro;
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj)) return true;
            if (object.ReferenceEquals(null, obj)) return false;
            if (this.GetType() != obj.GetType()) return false;
            var vo = obj as Cliente;
            return vo.clienteId().Equals(clienteId());
        }

        public override int GetHashCode()
        {
            var hash = 973;
            hash = hash * 3397 +
                (clienteId() != null ? clienteId().GetHashCode() : 0);

            return hash;

        }
        public void alterarRg(string rg)
        {
            setRg(rg);
        }
        public void alterarCpf(CPF cpf)
        {
            setCpf(cpf);
        }
        public void alterarOcupacao(string ocupacao)
        {
            setOcupacao(ocupacao);
        }
        public void alterarRenda(double renda)
        {
            setRenda(renda);
        }
        public void alterarSexo(string sexo)
        {
            setSexo(sexo);
        }
        public void alterarDesconto(double desconto)
        {
            setDesconto(desconto);
        }
        public void alterarTelefone(string telefone)
        {
            setTelefone(telefone);
        }
        public void alterarCelular(string celular)
        {
            setCelular(celular);
        }

        public void alterarEndereco(string endereco)
        {
            setEndereco(endereco);
        }
        public void alterarNumeroEndereco(string numeroEndereco)
        {
            setNumeroEndereco(numeroEndereco);
        }
        public void alterarBairro(string bairro)
        {
            setBairro(bairro);
        }
        public void alterarCep(string cep)
        {
            setCep(cep);
        }
        public void alterarCidade(CidadeId cidadeId)
        {
            setCidade(cidadeId);
        }
        public void alterarDataCadastro(DateTime dataCadastro)
        {
            setDataCadastro(dataCadastro);
        }
        public void alterarPromocao(bool promocao)
        {
            setPromocao(promocao);
        }

    }
}
