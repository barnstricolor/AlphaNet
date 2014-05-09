using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphanet.Acesso.Aplicacao.Data
{
    public class AcessoAplicacaoService
    {
        public UsuarioData usuarioNoPapel(string usuario, string papel) {
            return new UsuarioData(usuario, "email@"+papel);
        }

    }
}
