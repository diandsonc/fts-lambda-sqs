using System.Collections.Generic;
using System.Threading.Tasks;

namespace FTS.Precatorio.Domain.Tickets.Repository
{
    public interface ITicketRepository
    {
        Task<ICollection<Ticket>> GetAll();
    }
}