using System.Collections.Generic;
using FTS.Precatorio.Domain.Tickets;

namespace test.Unity.Mocks
{
    public static class TicketMock
    {
        public static List<Ticket> CreateBuff()
        {
            var buff = new List<Ticket>();

            return buff;
        }

        public static List<Ticket> AddTicket(this List<Ticket> buff, string code)
        {
            buff.Add(new Ticket
            {
                Code = code
            });

            return buff;
        }
    }
}