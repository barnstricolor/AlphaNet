using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Voos;
using AlphaNet.PassagemAerea.Domain.Model.Clientes;

namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Memoria
{
    public class MemoriaVooRepositorio : VooRepositorio
    {
        private Dictionary<string, Voo> store;

        public MemoriaVooRepositorio()
        {
            this.store = new Dictionary<string, Voo>();
        }
        public VooId proximaIdentidade()
        {
            return new VooId(Guid.NewGuid().ToString().ToUpper());
        }

        public void salvar(Voo voo)
        {
            //store.Add(aviao.aviaoId().Id, aviao);
            store[voo.vooId().Id] = voo;
        }

        public Voo obterPeloId(VooId aviaoId)
        {
            return store[aviaoId.Id];
        }
        
        public void limpar()
        {
            store.Clear();
        }
        public List<Voo> todosVoos() {

            return store.Values.ToList();
        }

        public void excluir(VooId vooId) {
            store.Remove(vooId.Id);
        }
        
        public List<Voo> voosCliente(ClienteId clienteId)
        {
            List<Voo> result = new List<Voo>();

            foreach (Voo voo in store.Values.ToList()) {
                if (voo.temReservaParaCliente(clienteId))
                    result.Add(voo);
            }

            return result;
        }


        public void salvarReservas(Voo voo)
        {
            throw new NotImplementedException();
        }


        public void cancelarReservaCliente(Voo voo, Cliente cliente)
        {
            throw new NotImplementedException();
        }
    }
}
