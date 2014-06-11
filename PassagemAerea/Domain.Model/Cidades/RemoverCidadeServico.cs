using AlphaNet.PassagemAerea.Domain.Model;
using AlphaNet.PassagemAerea.Domain.Model.Cidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PassagemAerea.Domain.Model.Cidades
{
    public class RemoverCidadeServico
    {
        public void remover(Cidade cidade)
        {
            if (DominioRegistro.vooRepositorio().voosCidade(cidade.cidadeId()).Count > 0 )
                throw new InvalidOperationException("Existe Voo para esta Cidade (Origem/Destino)");

            if (DominioRegistro.clienteRepositorio().clientesCidade(cidade.cidadeId()).Count > 0)
                throw new InvalidOperationException("Existe Cliente cadastrado para esta Cidade");

            DominioRegistro.cidadeRepositorio().excluir(cidade.cidadeId());
        }
    }
}
