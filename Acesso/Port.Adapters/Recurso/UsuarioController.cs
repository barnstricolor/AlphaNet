using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

using Alphanet.Acesso.Aplicacao;
using Alphanet.Acesso.Aplicacao.Data;



namespace Alphanet.Acesso.Port.Adapters.Recurso
{
    public class UsuarioController : ApiController
    {
        public UsuarioRecurso Get(string nome, string papel)
        {
            AcessoAplicacaoService acessoAplicacaoService = new AcessoAplicacaoService();

            UsuarioData data = acessoAplicacaoService.usuarioNoPapel(nome, papel);
            if (data == null)
                return null;

            UsuarioRecurso usuario = new UsuarioRecurso();
            usuario.nome = data.nome;
            usuario.email = data.email;
            return usuario;
        }

    }
}
