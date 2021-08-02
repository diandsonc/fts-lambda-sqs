using FTS.Precatorio.Domain.Core.Interfaces;
using FTS.Precatorio.Domain.Trade;
using FTS.Precatorio.Infrastructure.Database.SQLServer.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FTS.Precatorio.Infrastructure.Database.SQLServer.Context
{
    public class FTSPrecatorioContext : CoreContext
    {
        public FTSPrecatorioContext() : base() { }

        public FTSPrecatorioContext(IUserToken token) : base(token) { }

        public DbSet<Trade> Trade { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TradeMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}