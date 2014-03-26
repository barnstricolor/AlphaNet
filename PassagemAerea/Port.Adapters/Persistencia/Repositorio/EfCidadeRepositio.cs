using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Cidade;


namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio
{
    public class EfCidadeRepositio:CidadeRepositorio
    {
        public CidadeId proximaIdentidade()
        {
            return new CidadeId( Guid.NewGuid().ToString().ToUpper());
        }
        public void salvar(Cidade cidade){
        
        }
        public Cidade obterPeloId(CidadeId CidadeId) {
            return new Cidade(proximaIdentidade(),"RIBEIRAO PRETO","14100");
        }
    }
}
