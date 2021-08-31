using System.Threading.Tasks;
using System.Collections.Generic;
using FTS.Precatorio.Domain.Tickets.Repository;
using FTS.Precatorio.Infrastructure.Database.SQLServer.Context;
using FTS.Precatorio.Domain.Tickets;
using Microsoft.EntityFrameworkCore;

namespace FTS.Precatorio.Infrastructure.Database.SQLServer.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private FTSPrecatorioContext _context;

        public TicketRepository(FTSPrecatorioContext context) : base()
        {
            _context = context;
        }

        public async Task<ICollection<Ticket>> GetAll()
        {
            var query = _context.Set<Ticket>();
            var list = await query.ToListAsync();

            return list;
        }
    }
}