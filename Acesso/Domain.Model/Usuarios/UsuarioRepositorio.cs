using AlphaNet.Acesso.Domain.Model.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphanet.Acesso.Domain.Model.Usuarios
{
    public interface UsuarioRepositorio
    {
        UsuarioId proximaIdentidade();
        void salvar(Usuario usuario);
        Usuario obterPeloId(UsuarioId usuarioId);

        void limpar();

        void remover(UsuarioId usuarioId);

        Usuario usuarioPelaCredencialAutenticacao(string usuario, string senha);
    }
}
