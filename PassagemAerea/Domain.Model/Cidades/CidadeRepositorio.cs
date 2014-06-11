using AlphaNet.PassagemAerea.Domain.Model.Voos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.PassagemAerea.Domain.Model.Cidades
{
    public interface CidadeRepositorio
    {
        CidadeId proximaIdentidade();
        void salvar(Cidade cidade);
        Cidade obterPeloId(CidadeId CidadeId);
        List<Cidade> todasCidades();
        void limpar();
        void excluir(CidadeId cidadeId);
    }
}
