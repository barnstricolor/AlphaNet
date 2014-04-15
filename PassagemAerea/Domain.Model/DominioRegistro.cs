using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using AlphaNet.PassagemAerea.Domain.Model.Aviao;

namespace AlphaNet.PassagemAerea.Domain.Model
{
    public class DominioRegistro
    {
        private static UnityContainer container = new UnityContainer();

        public static AviaoRepositorio aviaoRepositorio()
        {
            return container.Resolve<AviaoRepositorio>();
        }

        public static UnityContainer obterContainer()
        {
            return container;
        }
        

    }
}
