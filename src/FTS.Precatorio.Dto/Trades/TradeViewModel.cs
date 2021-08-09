using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FTS.Precatorio.Domain.Trades;
using FTS.Precatorio.Domain.Trades.Enum;
using FTS.Precatorio.Dto.Tickets;

namespace FTS.Precatorio.Dto.Trades
{
    public class TradeViewModel
    {
        public Ulid? Id { get; set; }

        public Guid ExternalId { get; set; }

        [Required]
        public string Code { get; set; }

        public TradeType Type { get; set; }

        public List<TicketViewModel> Tickets { get; set; }

        public static TradeViewModel Map(Trade trade)
        {
            var data = new TradeViewModel
            {
                Id = trade.Ulid,
                ExternalId = trade.ExternalId,
                Code = trade.Code,
                Type = trade.Type,
                Tickets = trade.Tickets.Select(x => TicketViewModel.Map(x)).ToList()
            };

            return data;
        }

        public Trade Map()
        {
            var data = new Trade
            {
                Ulid = Id ?? Ulid.NewUlid(),
                ExternalId = ExternalId,
                Code = Code,
                Type = Type,
                Tickets = Tickets.Select(x => x.Map()).ToList()
            };

            return data;
        }
    }
}