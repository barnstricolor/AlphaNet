using AlphaNet.PassagemAerea.Domain.Model.Publicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.PassagemAerea.Port.Adapters.Servico
{
    public interface UsuarioNoPapelAdapter
    {
        Gestor paraGestor(string identidade);
    }
}
