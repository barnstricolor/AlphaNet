using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using System.Data.Entity;


namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.EF
{
    public class EfAviaoRepositio:AviaoRepositorio
    {
        Context context;

        public EfAviaoRepositio(Context context) 
        {
            this.context = context;
        }

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
        public void limpar()
        {
            throw new NotImplementedException();
        }
        public List<Aviao> todosAvioes()
        {
            return this.context.AVIAO.ToList();

        }
        public void excluir(AviaoId aviaoId)
        {
            throw new NotImplementedException();
        }

    }
}
