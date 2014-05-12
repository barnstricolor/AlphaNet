using Alphanet.Acesso.Aplicacao.Data;
using Alphanet.Acesso.Domain.Model.Usuarios;
using AlphaNet.Acesso.Domain.Model;
using AlphaNet.Acesso.Domain.Model.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphanet.Acesso.Aplicacao
{
    public class AcessoAplicacaoService
    {
        public UsuarioData usuarioNoPapel(string usuario, string papel) {
            return new UsuarioData(usuario,papel);
        }

        public string novoUsuario(NovoUsuarioComando comando) {

            Usuario usuario = new Usuario(
                usuarioRepositorio().proximaIdentidade(),
                comando.login,
                comando.senha,
                comando.nome,
                comando.email,
                new Papel(comando.papel));

            usuarioRepositorio().salvar(usuario);

            return usuario.usuarioId().Id;
        }

        public void alterarSenha(string usuarioId, string novaSenha) {
            Usuario usuario = usuarioPeloId(usuarioId);
        }

        private Usuario usuarioPeloId(string usuarioId) {
            return usuarioRepositorio().obterPeloId(new UsuarioId(usuarioId));
        }

        private UsuarioRepositorio usuarioRepositorio() {
            return DominioRegistro.usuarioRepositorio();
        }


    }
}
