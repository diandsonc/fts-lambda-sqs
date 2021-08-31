using System;
using FTS.Precatorio.Domain.Tickets;
using FTS.Precatorio.Infrastructure.Database.SQLServer.Mappings;
using FTS.Precatorio.Infrastructure.User;
using Microsoft.EntityFrameworkCore;

namespace FTS.Precatorio.Infrastructure.Database.SQLServer.Context
{
    public class FTSPrecatorioContext : DbContext
    {
        private Guid _control_TenantId { get; set; }
        private string _control_UserLogin { get; set; }
        public bool IgnoreGroup { get; set; }

        public FTSPrecatorioContext() { }

        public FTSPrecatorioContext(IUserToken token) : this()
        {
            _control_TenantId = token.GetTenantId();
            _control_UserLogin = token.GetUserLogin();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureTenantFilter(modelBuilder);

            modelBuilder.ApplyConfiguration(new TicketMapping());

            base.OnModelCreating(modelBuilder);
        }

        public void ConfigureTenantFilter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>().HasQueryFilter(e => IgnoreGroup || e.Control_TenantId == GetTenantId());
        }

        public Guid GetTenantId() => _control_TenantId;

        public string GetUserLogin() => _control_UserLogin;
    }
}