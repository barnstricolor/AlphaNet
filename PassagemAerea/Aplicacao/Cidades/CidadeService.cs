using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model;
using AlphaNet.PassagemAerea.Domain.Model.Cidade;
using AlphaNet.PassagemAerea.Aplicacao.Cidades.Data;


namespace AlphaNet.PassagemAerea.Aplicacao.Cidades
{
    public class CidadeService
    {
        private CidadeRepositorio cidadeRepositorio() {
            return DominioRegistro.cidadeRepositorio();            
        }

        public string novaCidade(string nome,string cep) {
            Cidade cidade = new Cidade(cidadeRepositorio().proximaIdentidade(), nome, cep);
            cidadeRepositorio().salvar(cidade);
            return cidade.cidadeId().Id;
        }

        public void alterarNome(string cidadeId, string nome) {
            Cidade cidade = cidadeRepositorio().obterPeloId(new CidadeId(cidadeId));
            cidade.alterarNome(nome);
            cidadeRepositorio().salvar(cidade);
        }

        public void alterarDados(string cidadeId, string nome, string cep){
            Cidade cidade = cidadeRepositorio().obterPeloId(new CidadeId(cidadeId));
            cidade.alterarNome(nome);
            cidade.alterarCep(cep);
            cidadeRepositorio().salvar(cidade);
        }

        public void excluirCidade(string cidadeId) {
            cidadeRepositorio().excluir(new CidadeId(cidadeId));
        }

        public List<CidadeData> todasCidades() {
            List<CidadeData> result = new List<CidadeData>();

            foreach (Cidade data in cidadeRepositorio().todasCidades()) 
            {
                CidadeData cidade = new CidadeData();
                cidade.cidadeId = data.cidadeId().Id;
                cidade.nome = data.nome();
                cidade.cep = data.cep();
                result.Add(cidade);
            }

            return result;
        }

        public CidadeData obterCidade(string cidadeId) {
            CidadeData result = new CidadeData();
            
            Cidade cidade = cidadeRepositorio().obterPeloId(new CidadeId(cidadeId));

            result.cidadeId = cidade.cidadeId().Id;
            result.nome = cidade.nome();
            result.cep = cidade.cep();

            return result;
        }
    }
}
