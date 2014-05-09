using Alphanet.Acesso.Aplicacao.Data;
using Alphanet.Acesso.Domain.Model;
using AlphaNet.Acesso.Domain.Model.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphanet.Acesso.Aplicacao
{
    public class AutenticacaoAplicacaoService
    {

        public UsuarioData autenticar(AutenticarUsuarioComando comando) {

            Usuario usuario = autenticacaoService().autenticar(comando.nome, comando.senha);

            return new UsuarioData(usuario.nome(), usuario.email());

        }

        private AutenticacaoService autenticacaoService() {
            return new AutenticacaoService();
        }


    }
}
