using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AlphaNet.PassagemAerea.Domain.Model;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using AlphaNet.PassagemAerea.Domain.Model.Cidades;
using AlphaNet.PassagemAerea.Domain.Model.Clientes;
using AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Memoria;
using AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.EF;
using AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Oracle;
using AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.Mongo;
using Microsoft.Practices.Unity;
using AlphaNet.PassagemAerea.Domain.Model.Voos;
using AlphaNet.PassagemAerea.Port.Adapters.Servico;
using AlphaNet.PassagemAerea.Domain.Model.Publicos;
using Alphanet.Acesso.Domain.Model.Usuarios;
using Alphanet.Acesso.Port.Adapters.Persistencia.Repositorio.Memoria;
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
            AlphaNet.Acesso.Domain.Model.DominioRegistro.obterContainer().RegisterInstance<UsuarioRepositorio>(new MemoriaUsuarioRepositorio());
        }
        private void bancoEmMemoria() {
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
            //DominioRegistro.obterContainer().RegisterInstance<Oracle.ManagedDataAccess.Client.OracleConnection>(Bd());
            DominioRegistro.obterContainer().RegisterInstance<AviaoRepositorio>(new OracleAviaoRepositorio());
        }
        private void bancoMongo() {
            DominioRegistro.obterContainer().RegisterInstance<AviaoRepositorio>(new MongoAviaoRepositorio());
            DominioRegistro.obterContainer().RegisterInstance<CidadeRepositorio>(new MongoCidadeRepositorio());
            DominioRegistro.obterContainer().RegisterInstance<ClienteRepositorio>(new MongoClienteRepositorio());
        }
    }
}