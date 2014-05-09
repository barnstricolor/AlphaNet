using Alphanet.Acesso.Aplicacao.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;


namespace Alphanet.Acesso.Port.Adapters.Recurso
{
    public class UsuarioController : ApiController
    {


        
        // GET api/values/5
        public UsuarioRecurso Get(string nome, string papel)
        {
            AcessoAplicacaoService acessoAplicacaoService = new AcessoAplicacaoService();

            UsuarioData data = acessoAplicacaoService.usuarioNoPapel(nome, papel);

            UsuarioRecurso usuario = new UsuarioRecurso();
            usuario.nome = data.nome;
            usuario.email = data.email;
            return usuario;
        }
    }
}
