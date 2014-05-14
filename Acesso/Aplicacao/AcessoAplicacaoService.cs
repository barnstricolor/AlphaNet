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
        public UsuarioData usuarioNoPapel(string email, string papel) {

            Usuario usuario = usuarioRepositorio().obterPeloEmail(email);

            if (usuario == null)
                return null;

            return new UsuarioData(usuario.login(), usuario.nome(), usuario.email());
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
        
        public List<UsuarioData> todosUsuarios()
        {
            List<UsuarioData> result = new List<UsuarioData>();

            foreach (Usuario data in usuarioRepositorio().obterTodos())
            {
                UsuarioData usuario = new UsuarioData();
                usuario.usuarioId = data.usuarioId().Id;
                usuario.login = data.login();
                usuario.nome = data.nome();
                usuario.email = data.email();
                result.Add(usuario);
            }

            return result;
        }
    }
}
