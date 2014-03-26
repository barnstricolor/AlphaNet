using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Aviao;

namespace AlphaNet.Application
{
    public class AviaoService
    {
        AviaoRepositorio aviaoRepositorio;
        public AviaoId novoAviao(string modelo, int assentos) {
            Aviao aviao = new Aviao(aviaoRepositorio.proximaIdentidade(), modelo, assentos);
            aviaoRepositorio.salvar(aviao);
            return aviao.aviaoId();
        }
        public void alterarModelo(AviaoId aviaoId,string modelo) {
            Aviao aviao = aviaoRepositorio.obterPeloId(aviaoId);
            aviao.alterarModelo(modelo);
            aviaoRepositorio.salvar(aviao);
        }
        public string teste(string str) {
            return str + " OK";
        }
    }
}
