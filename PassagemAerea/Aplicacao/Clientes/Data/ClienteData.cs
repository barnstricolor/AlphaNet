using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Aplicacao.Cidades.Data;

namespace AlphaNet.PassagemAerea.Aplicacao.Clientes.Data
{
    public class ClienteData
    {
        public string clienteId {get; set; }
        public string nome {get; set; }
        public string email {get; set; }
        public string rg {get; set; }
        public string cpf {get; set; }

        public string ocupacao {get; set; }
        public string renda {get; set; }
        public string sexo {get; set; }
        public string desconto {get; set; }
        public bool promocao {get; set; }
        public bool especial {get; set; }

        public string telefone {get; set; }
        public string celular {get; set; }

        public string endereco {get; set; }
        public string numeroEndereco {get; set; }
        public string bairro {get; set; }
        public string cep {get; set; }
        public CidadeData _cidade {get; set; }

    }
}
