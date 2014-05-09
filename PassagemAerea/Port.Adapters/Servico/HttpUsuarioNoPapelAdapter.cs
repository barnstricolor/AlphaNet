using AlphaNet.PassagemAerea.Domain.Model.Publicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.PassagemAerea.Port.Adapters.Servico
{
    public class HttpUsuarioNoPapelAdapter: UsuarioNoPapelAdapter
    {
        public Gestor paraGestor(string identidade)
        {
            throw new NotImplementedException();
        }
    }
}
