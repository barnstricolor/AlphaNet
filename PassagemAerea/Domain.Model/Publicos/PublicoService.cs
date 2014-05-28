using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.PassagemAerea.Domain.Model.Publicos
{
    public interface PublicoService
    {
        Gestor gestorPela(string identidade);
        Outros obterPapel(string identidade);

    }
}
