using Alphanet.Acesso.Domain.Model.Usuarios;
using AlphaNet.Acesso.Domain.Model;
using AlphaNet.Acesso.Domain.Model.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphanet.Acesso.Domain.Model
{
    public class AutenticacaoService
    {

        public Usuario autenticar(string usuario, string senha) {
            UsuarioRepositorio usuarioRepositorio = DominioRegistro.usuarioRepositorio();
            return usuarioRepositorio.usuarioPelaCredencialAutenticacao(usuario, senha);
        }

    }
}
