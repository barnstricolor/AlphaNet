using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Aviao;
using AlphaNet.PassagemAerea.Domain.Model;
using AlphaNet.PassagemAerea.Aplicacao.Avioes.Data;


namespace AlphaNet.PassagemAerea.Aplicacao.Avioes
{
    public class AviaoService
    {
        private AviaoRepositorio aviaoRepositorio() {
            return DominioRegistro.aviaoRepositorio();
        }

        public string novoAviao(string modelo, int assentos) {
            Aviao aviao = new Aviao(aviaoRepositorio().proximaIdentidade(), modelo, assentos);
            aviaoRepositorio().salvar(aviao);
            return aviao.aviaoId().Id;
        }
        public void alterarModelo(String aviaoId,string modelo) {
            Aviao aviao = aviaoRepositorio().obterPeloId(new AviaoId(aviaoId));
            aviao.alterarModelo(modelo);
            aviaoRepositorio().salvar(aviao);
        }
        public List<AviaoData> todosAvioes() {
            List<AviaoData> result = new List<AviaoData>();

            foreach (Aviao data in aviaoRepositorio().todosAvioes())
            {
                AviaoData aviao = new AviaoData();
                aviao.modelo = data.modelo();
                aviao.assentos = data.assentos();
                result.Add(aviao);
            }

            return result;
        }
    }
}
