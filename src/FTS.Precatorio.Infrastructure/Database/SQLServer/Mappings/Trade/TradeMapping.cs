using FTS.Precatorio.Domain.Trade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTS.Precatorio.Infrastructure.Database.SQLServer.Mappings
{
    public class TradeMapping : IEntityTypeConfiguration<Trade>
    {
        public void Configure(EntityTypeBuilder<Trade> builder)
        {
            builder.Property(x => x.Id).HasColumnName("Tra_TradeId");
            builder.Property(x => x.Code).HasColumnName("Tra_Code").HasMaxLength(30).IsRequired();
            builder.Property(x => x.GroupId).HasColumnName("Gru_GrupoId").IsRequired();

            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);

            builder.ToTable("Tra_Trade", "Administracao");
        }
    }
}