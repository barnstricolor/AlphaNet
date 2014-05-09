using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using AlphaNet.PassagemAerea.Domain.Model;
using AlphaNet.PassagemAerea.Aplicacao.Avioes.Data;


namespace AlphaNet.PassagemAerea.Aplicacao.Avioes
{
    public class AplicacaoAviaoService
    {
        private AviaoRepositorio aviaoRepositorio() {
            return DominioRegistro.aviaoRepositorio();
        }

        public string novoAviao(string modelo, int assentos) {
            Aviao aviao = new Aviao(aviaoRepositorio().proximaIdentidade(), modelo, assentos);
            aviaoRepositorio().salvar(aviao);
            return aviao.aviaoId().Id;
        }
        public void alterarModelo(string aviaoId,string modelo) {
            Aviao aviao = aviaoRepositorio().obterPeloId(new AviaoId(aviaoId));
            aviao.alterarModelo(modelo);
            aviaoRepositorio().salvar(aviao);
        }
        public void alterarDados(string aviaoId, string modelo , int assentos)
        {
            Aviao aviao = aviaoRepositorio().obterPeloId(new AviaoId(aviaoId));
            aviao.alterarModelo(modelo);
            aviao.alterarAssentos(assentos);
            aviaoRepositorio().salvar(aviao);
        }

        public void excluirAviao(string aviaoId) {
            aviaoRepositorio().excluir(new AviaoId(aviaoId));
        }

        public List<AviaoData> todosAvioes()
        {
            List<AviaoData> result = new List<AviaoData>();

            foreach (Aviao data in aviaoRepositorio().todosAvioes())
            {
                AviaoData aviao = new AviaoData();
                aviao.aviaoId = data.aviaoId().Id;
                aviao.modelo = data.modelo();
                aviao.assentos = data.assentos();
                result.Add(aviao);
            }

            return result;
        }

        public AviaoData obterAviao(string aviaoId){
            AviaoData result = new AviaoData();

            Aviao aviao = aviaoRepositorio().obterPeloId(new AviaoId(aviaoId));

            result.aviaoId = aviao.aviaoId().Id;
            result.modelo = aviao.modelo();
            result.assentos = aviao.assentos();

            return result;
        }
    }
}
