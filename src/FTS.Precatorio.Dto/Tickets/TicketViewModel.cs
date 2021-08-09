using System;
using FTS.Precatorio.Domain.Tickets;

namespace FTS.Precatorio.Dto.Tickets
{
    public class TicketViewModel
    {
        public Guid Id { get; set; }
        public decimal? Amount { get; set; }

        public static TicketViewModel Map(Ticket ticket)
        {
            var data = new TicketViewModel
            {
                Id = ticket.Id,
                Amount = ticket.Amount
            };

            return data;
        }

        public Ticket Map()
        {
            var data = new Ticket
            {
                Id = Id,
                Amount = Amount
            };

            return data;
        }
    }
}