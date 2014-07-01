using Alphanet.Acesso.Domain.Model.Usuarios;
using Alphanet.Acesso.Port.Adapters.Persistencia.Repositorio.Memoria;
using Alphanet.Acesso.Port.Adapters.Persistencia.Repositorio.Oracle;
using AlphaNet.PassagemAerea.Aplicacao.Avioes;
using AlphaNet.PassagemAerea.Aplicacao.Cidades;
using AlphaNet.PassagemAerea.Aplicacao.Clientes;
using AlphaNet.PassagemAerea.Aplicacao.Voos;
using AlphaNet.PassagemAerea.Domain.Model;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using AlphaNet.PassagemAerea.Domain.Model.Cidades;
using AlphaNet.PassagemAerea.Domain.Model.Clientes;
using AlphaNet.PassagemAerea.Domain.Model.Publicos;
using AlphaNet.PassagemAerea.Domain.Model.Voos;
using AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.EF;
using AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Memoria;
using AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Mongo;
using AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Oracle;
using AlphaNet.PassagemAerea.Port.Adapters.Servico;
using Microsoft.Practices.Unity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
namespace IU
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            this.bancoEmMemoria();
            //this.bancoEF();
            //this.bancoOracle();
            //this.bancoMongo();

            DominioRegistro.obterContainer().RegisterInstance<PublicoService>(new TraduzirPublicoService());
            //SERVICOS
            DominioRegistro.obterContainer().RegisterInstance<VooService>(new VooService());
            DominioRegistro.obterContainer().RegisterInstance<AplicacaoAviaoService>(new AplicacaoAviaoService());
            DominioRegistro.obterContainer().RegisterInstance<CidadeService>(new CidadeService());
            
            DominioRegistro.obterContainer().RegisterInstance<ClienteService>(new ClienteService());      
   

        }
        private void bancoEmMemoria() {
            AlphaNet.Acesso.Domain.Model.DominioRegistro.obterContainer().RegisterInstance<UsuarioRepositorio>(new MemoriaUsuarioRepositorio());
            DominioRegistro.obterContainer().RegisterInstance<AviaoRepositorio>(new MemoriaAviaoRepositorio());
            DominioRegistro.obterContainer().RegisterInstance<CidadeRepositorio>(new MemoriaCidadeRepositorio());
            DominioRegistro.obterContainer().RegisterInstance<ClienteRepositorio>(new MemoriaClienteRepositorio());
            DominioRegistro.obterContainer().RegisterInstance<VooRepositorio>(new MemoriaVooRepositorio());
        }
        private void bancoEF()
        {
            DominioRegistro.obterContainer().RegisterInstance<System.Data.Entity.DbContext>(new Context());
            DominioRegistro.obterContainer().RegisterInstance<AviaoRepositorio>(new EfAviaoRepositio(new Context()));
        }
        private void bancoOracle()
        {
            AlphaNet.Acesso.Domain.Model.DominioRegistro.obterContainer().RegisterInstance<UsuarioRepositorio>(new OracleUsuarioRepositorio());
            DominioRegistro.obterContainer().RegisterInstance<AviaoRepositorio>(new OracleAviaoRepositorio());
            DominioRegistro.obterContainer().RegisterInstance<CidadeRepositorio>(new OracleCidadeRepositorio());
            DominioRegistro.obterContainer().RegisterInstance<ClienteRepositorio>(new OracleClienteRepositorio());
            DominioRegistro.obterContainer().RegisterInstance<VooRepositorio>(new OracleVooRepositorio());

        }
        private void bancoMongo() {
            DominioRegistro.obterContainer().RegisterInstance<AviaoRepositorio>(new MongoAviaoRepositorio());
            DominioRegistro.obterContainer().RegisterInstance<CidadeRepositorio>(new MongoCidadeRepositorio());
            DominioRegistro.obterContainer().RegisterInstance<ClienteRepositorio>(new MongoClienteRepositorio());
        }
    }
}