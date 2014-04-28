using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.EF.Mapping;

namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.EF
{
    public partial class Context : DbContext
    {
        static Context()
        {
            Database.SetInitializer<Context>(null);
        }

        public Context()
            : base("Name=Context")
        {
        }

        public DbSet<Aviao> AVIAO { get; set; }
        /*public DbSet<CIDADE> CIDADEs { get; set; }
        public DbSet<CLIENTE> CLIENTEs { get; set; }
        public DbSet<RESERVA> RESERVAs { get; set; }
        public DbSet<USUARIO> USUARIOs { get; set; }
        public DbSet<VOO> VOOs { get; set; }*/

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AviaoMap());
            /*modelBuilder.Configurations.Add(new CIDADEMap());
            modelBuilder.Configurations.Add(new CLIENTEMap());
            modelBuilder.Configurations.Add(new RESERVAMap());
            modelBuilder.Configurations.Add(new USUARIOMap());
            modelBuilder.Configurations.Add(new VOOMap());*/
        }
    }
}
