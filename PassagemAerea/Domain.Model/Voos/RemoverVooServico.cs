using AlphaNet.PassagemAerea.Domain.Model;
using AlphaNet.PassagemAerea.Domain.Model.Voos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PassagemAerea.Domain.Model.Voos
{
    public class RemoverVooServico
    {
        public void remover(Voo voo)
        {
            if (DominioRegistro.vooRepositorio().obterPeloId(voo.vooId()).assentosReservados().Count > 0)
                throw new InvalidOperationException("Voo com Reerva");

            DominioRegistro.vooRepositorio().excluir(voo.vooId());
        }
    }
}
