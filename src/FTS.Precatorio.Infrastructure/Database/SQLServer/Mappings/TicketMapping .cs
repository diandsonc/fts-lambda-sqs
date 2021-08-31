using FTS.Precatorio.Domain.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTS.Precatorio.Infrastructure.Database.SQLServer.Mappings
{
    public class TicketMapping : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(x => x.Id).HasColumnName("Tck_TicketId");
            builder.Property(x => x.Code).HasColumnName("Tck_Code").HasMaxLength(30).IsRequired();
            builder.Property(x => x.Control_TenantId).HasColumnName("Ctr_TenantId").IsRequired();
            builder.Property(x => x.Amount).HasColumnName("Tck_Amount").HasColumnType("decimal(5,2)").IsRequired();

            builder.ToTable("Tck_Ticket", "Administracao");
        }
    }
}