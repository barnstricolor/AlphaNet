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

            if(!usuario.desempenhaPapel(new Papel(papel)))
                return null;

            return new UsuarioData(usuario.login(), usuario.nome(), usuario.email());
        }

        public UsuarioData papel(string email)
        {
            Usuario usuario = usuarioRepositorio().obterPeloEmail(email);

            if (usuario == null)
                return null;

            UsuarioData result = new UsuarioData(usuario.login(), usuario.nome(), usuario.email());
            result.papel = usuario.papel().ToString();
            return result;
        }
        public UsuarioData UsuarioPeloId(string usuarioId)
        {
            Usuario usuario = usuarioRepositorio().obterPeloId(new UsuarioId(usuarioId));

            if (usuario == null)
                return null;

            UsuarioData result = new UsuarioData(usuario.login(), usuario.nome(), usuario.email());
            result.usuarioId = usuarioId;
            result.senha = usuario.senha();
            result.papel = usuario.papel().ToString();
            return result;
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

        public string registrarNovoUsuario(RegistrarNovoUsuarioComando comando) {

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
            usuario.alterarSenha(novaSenha);
            DominioRegistro.usuarioRepositorio().salvar(usuario);
        }

        public void alterarDados(string usuarioId, NovoUsuarioComando comando) {
            Usuario usuario = usuarioPeloId(usuarioId);
            usuario.alterarLogin(comando.login);
            usuario.alterarNome(comando.nome);          
            usuario.alterarSenha(comando.senha);
            usuario.alterarEmail(comando.email);
            usuario.alterarPapel(new Papel(comando.papel));
            DominioRegistro.usuarioRepositorio().salvar(usuario);
           
        }

        private Usuario usuarioPeloId(string usuarioId) {
            return usuarioRepositorio().obterPeloId(new UsuarioId(usuarioId));
        }

        private UsuarioRepositorio usuarioRepositorio() {
            return DominioRegistro.usuarioRepositorio();
        }
        public void excluirUsuario(string usuarioId)
        {
            DominioRegistro.usuarioRepositorio().remover(new UsuarioId(usuarioId));
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
