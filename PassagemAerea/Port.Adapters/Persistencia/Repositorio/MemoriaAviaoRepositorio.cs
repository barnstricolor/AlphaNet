using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;

namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio
{
    public class MemoriaAviaoRepositorio : AviaoRepositorio
    {
        private Dictionary<string, Aviao> store;

        public MemoriaAviaoRepositorio() {
            this.store = new Dictionary<string, Aviao>();
        }
        public AviaoId proximaIdentidade()
        {
            return new AviaoId(Guid.NewGuid().ToString().ToUpper());
        }

        public void salvar(Aviao aviao)
        {
            //store.Add(aviao.aviaoId().Id, aviao);
            store[aviao.aviaoId().Id] = aviao;
        }

        public Aviao obterPeloId(AviaoId aviaoId)
        {
            return store[aviaoId.Id];
        }
        
        public void limpar()
        {
            store.Clear();
        }
        public List<Aviao> todosAvioes() {

            return store.Values.ToList();
        }

        public void excluir(AviaoId aviaoId) {
            store.Remove(aviaoId.Id);
        }
    }
}
