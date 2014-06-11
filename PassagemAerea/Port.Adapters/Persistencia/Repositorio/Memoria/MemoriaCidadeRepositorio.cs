using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Cidades;
using AlphaNet.PassagemAerea.Domain.Model.Voos;

namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Memoria
{
    public class MemoriaCidadeRepositorio : CidadeRepositorio
    {
        private Dictionary<string, Cidade> store;

        public MemoriaCidadeRepositorio() {
            this.store = new Dictionary<string, Cidade>();
        }        
        
        public CidadeId proximaIdentidade()
        {
            return new CidadeId(Guid.NewGuid().ToString().ToUpper());
        }

        public void salvar(Cidade cidade)
        {
            store[cidade.cidadeId().Id] = cidade;
        }

        public Cidade obterPeloId(CidadeId CidadeId)
        {
            return store.ContainsKey(CidadeId.Id)? store[CidadeId.Id]:null;
        }

        public List<Cidade> todasCidades()
        {
            return store.Values.ToList();
        }

        public void limpar()
        {
            store.Clear();
        }

        public void excluir(CidadeId cidadeId)
        {
            store.Remove(cidadeId.Id);
        }


    }
}
