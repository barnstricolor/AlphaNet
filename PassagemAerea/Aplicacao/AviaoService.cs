using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Aviao;
using AlphaNet.PassagemAerea.Domain.Model;

namespace AlphaNet.PassagemAerea.Aplicacao
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
    }
}
