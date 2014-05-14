using AlphaNet.PassagemAerea.Domain.Model;
using AlphaNet.PassagemAerea.Domain.Model.Publicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.PassagemAerea.Port.Adapters.Servico
{
    public class TraduzirPublicoService : PublicoService
    {

        public Gestor gestorPela(string identidade)
        {
            return usuarioNoPapelAdapter().paraGestor(identidade);

        }

        private UsuarioNoPapelAdapter usuarioNoPapelAdapter() {
            return new HttpUsuarioNoPapelAdapter();
        }
    }
}
