using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Aviao;


namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio
{
    public class EfAviaoRepositio:AviaoRepositorio
    {
        public AviaoId proximaIdentidade()
        {
            return new AviaoId( Guid.NewGuid().ToString().ToUpper());
        }
        public void salvar(Aviao aviao)
        {

        }
        public Aviao obterPeloId(AviaoId aviaoId)
        {
            return new Aviao(proximaIdentidade(),"747",747);
        }

    }
}
