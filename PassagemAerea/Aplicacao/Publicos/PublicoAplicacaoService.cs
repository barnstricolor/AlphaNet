using AlphaNet.PassagemAerea.Domain.Model;
using AlphaNet.PassagemAerea.Domain.Model.Publicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.PassagemAerea.Aplicacao.Publicos
{
    public class PublicoAplicacaoService
    {
        public bool gestor(string identidade){

            return DominioRegistro.publicoService().gestorPela(identidade) != null;

        }
    }
}
