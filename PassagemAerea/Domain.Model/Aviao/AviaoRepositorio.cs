using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.PassagemAerea.Domain.Model.Aviao
{
    public interface AviaoRepositorio
    {
        AviaoId proximaIdentidade();
        void salvar(Aviao aviao);
        Aviao obterPeloId(AviaoId aviaoId);
        List<Aviao> todosAvioes();
        void limpar();
        void excluir(AviaoId aviaoId);
    }
}
