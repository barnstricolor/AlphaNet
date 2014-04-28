using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaNet.PassagemAerea.Domain.Model.Avioes;
using System.ComponentModel.DataAnnotations.Schema;
namespace AlphaNet.PassagemAerea.Port.Adapters.Persistencia.Repositorio.EF.Mapping
{
    public class AviaoMap : EntityTypeConfiguration<Aviao>
    {
        public AviaoMap()
        {
            this.HasKey(t => t.aviaoId());
            this.Property(t => t.aviaoId().Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(t => t.modelo()).IsRequired().HasMaxLength(100);
            this.ToTable("AVIAO", "PI");
            this.Property(t => t.aviaoId().Id).HasColumnName("ID_AVIAO");
            this.Property(t => t.modelo()).HasColumnName("NOM_MODELO");
            this.Property(t => t.assentos()).HasColumnName("QTD_ASSENTO");
            
        }
    }
}
