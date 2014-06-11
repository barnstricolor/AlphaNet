using AlphaNet.PassagemAerea.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PassagemAerea.Domain.Model.Avioes
{
    public class RemoverAviaoServico
    {
        public void remover(AlphaNet.PassagemAerea.Domain.Model.Avioes.Aviao aviao)
        {
            if (DominioRegistro.vooRepositorio().voosAviao(aviao.aviaoId()).Count > 0 )
                throw new InvalidOperationException("Existe Voo para este avião");

            DominioRegistro.aviaoRepositorio().excluir(aviao.aviaoId());
        }
    }
}
