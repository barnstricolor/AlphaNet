using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using AlphaNet.PassagemAerea.Domain.Model.Cidades;
using AlphaNet.PassagemAerea.Domain.Model.Clientes;
using AlphaNet.PassagemAerea.Domain.Model.Voos;
using AlphaNet.PassagemAerea.Domain.Model.Publicos;
using AlphaNet.PassagemAerea.Aplicacao.Voos;
using AlphaNet.PassagemAerea.Aplicacao.Avioes;
using AlphaNet.PassagemAerea.Aplicacao.Cidades;
using AlphaNet.PassagemAerea.Aplicacao.Clientes;

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
        
        public static CidadeRepositorio cidadeRepositorio() 
        {            
            return container.Resolve<CidadeRepositorio>();
        }
        public static ClienteRepositorio clienteRepositorio()
        {
            return container.Resolve<ClienteRepositorio>();
        }
        public static VooRepositorio vooRepositorio()
        {
            return container.Resolve<VooRepositorio>();
        }
        public static PublicoService publicoService()
        {
            return container.Resolve<PublicoService>();
        }
        //SERVICOS
        public static VooService vooService()
        {
            return container.Resolve<VooService>();
        }
        public static AplicacaoAviaoService aplicacaoAviaoService()
        {
            return container.Resolve<AplicacaoAviaoService>();
        }
        public static CidadeService cidadeService()
        {
            return container.Resolve<CidadeService>();
        }
        public static ClienteService clienteService()
        {
            return container.Resolve<ClienteService>();
        }
    }
}
