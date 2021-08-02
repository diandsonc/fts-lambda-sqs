using System;
using TradeEntity = FTS.Precatorio.Domain.Trade.Trade;

namespace FTS.Precatorio.Application.ViewModels.Trade
{
    public class TradeViewModel
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }

        public static TradeViewModel Map(TradeEntity trade)
        {
            var data = new TradeViewModel
            {
                Id = trade.Id,
                Code = trade.Code
            };

            return data;
        }
    }
}