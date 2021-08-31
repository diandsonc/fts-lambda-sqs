using System;

namespace FTS.Precatorio.Domain.Tickets
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public Guid Control_TenantId { get; set; }
        public string Code { get; set; }
        public decimal? Amount { get; set; }
    }
}