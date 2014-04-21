using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using AlphaNet.PassagemAerea.Domain.Model.Aviao;
using AlphaNet.PassagemAerea.Domain.Model.Cidade;

namespace AlphaNet.PassagemAerea.Domain.Model
{
    public class DominioRegistro
    {
        private static UnityContainer container = new UnityContainer();

        public static UnityContainer obterContainer()
        {
            return container;
        }

        public static AviaoRepositorio aviaoRepositorio()
        {
            return container.Resolve<AviaoRepositorio>();
        }
        
        public static CidadeRepositorio cidadeRepositorio() {
            
            return container.Resolve<CidadeRepositorio>();

        }
        

    }
}
