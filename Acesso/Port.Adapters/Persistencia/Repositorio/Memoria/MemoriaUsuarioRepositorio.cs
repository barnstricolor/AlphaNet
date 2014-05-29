using Alphanet.Acesso.Domain.Model.Usuarios;
using AlphaNet.Acesso.Domain.Model.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphanet.Acesso.Port.Adapters.Persistencia.Repositorio.Memoria
{
    public class MemoriaUsuarioRepositorio : UsuarioRepositorio
    {
        private Dictionary<String, Usuario> store;

        public MemoriaUsuarioRepositorio() {
            this.store = new Dictionary<string, Usuario>();
        }

        public UsuarioId proximaIdentidade()
        {
            return new UsuarioId(Guid.NewGuid().ToString().ToUpper());
        }

        public void salvar(Usuario usuario)
        {
            store[usuario.usuarioId().Id] = usuario;
        }

        public Usuario obterPeloId(UsuarioId usuarioId)
        {
            if (this.store.Keys.Contains(usuarioId.Id))
                return this.store[usuarioId.Id];

            return null;
        }


        public void limpar()
        {
            this.store.Clear();
        }


        public void remover(UsuarioId usuarioId)
        {
            this.store.Remove(usuarioId.Id);
        }


        public Usuario usuarioPelaCredencialAutenticacao(string usuario, string senha)
        {
            foreach (Usuario u in store.Values.ToList()) {
                if (u.login().Equals(usuario) && u.senha().Equals(senha))
                    return u;
            }

            return null;
        }


        public Usuario obterPeloEmail(string email)
        {
            foreach (Usuario u in store.Values.ToList())
            {
                if (u.email().Equals(email))
                    return u;
            }

            return null;
        }


        public List<Usuario> obterTodos()
        {
            return store.Values.ToList();
        }
    }
}
