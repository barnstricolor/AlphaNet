using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Alphanet.Acesso.Domain.Model.Usuarios;

namespace AlphaNet.Acesso.Domain.Model
{
    public class DominioRegistro
    {
        private static UnityContainer container = new UnityContainer();

        public static UnityContainer obterContainer()
        {
            return container;
        }

        public static UsuarioRepositorio usuarioRepositorio()
        {
            return container.Resolve<UsuarioRepositorio>();
        }
    }
}
