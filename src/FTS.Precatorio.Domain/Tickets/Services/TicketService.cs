using System.Collections.Generic;
using System.Threading.Tasks;
using FTS.Precatorio.Domain.Tickets.Repository;

namespace FTS.Precatorio.Domain.Tickets.Services
{
    public class TicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<ICollection<Ticket>> GetAll()
        {
            var data = await _ticketRepository.GetAll();

            return data;
        }
    }
}