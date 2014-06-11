using AlphaNet.PassagemAerea.Domain.Model;
using AlphaNet.PassagemAerea.Domain.Model.Voos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaNet.PassagemAerea.Domain.Model.Clientes
{
    public class RemoverClienteServico
    {
        public void remover(Cliente cliente)
        {
            if (DominioRegistro.vooRepositorio().voosCliente(cliente.clienteId()).Count > 0)
                throw new InvalidOperationException("Cliente com Reerva");

            DominioRegistro.clienteRepositorio().excluir(cliente.clienteId());
        }
    }
}
