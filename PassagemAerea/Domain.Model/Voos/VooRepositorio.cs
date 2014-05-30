using AlphaNet.PassagemAerea.Domain.Model.Clientes;
using AlphaNet.PassagemAerea.Domain.Model.Voos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AlphaNet.PassagemAerea.Domain.Model.Voos
{
    public interface VooRepositorio
    {
        VooId proximaIdentidade();
        void salvar(Voo voo);
        Voo obterPeloId(VooId vooId);
        List<Voo> todosVoos();
        void limpar();
        void excluir(VooId vooId);


        List<Voo> voosCliente(ClienteId clienteId);
        void salvarReservas(Voo voo);
    }
}
