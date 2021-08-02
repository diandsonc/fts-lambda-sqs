using System;
using TradeEntity = FTS.Precatorio.Domain.Trade.Trade;
using System.Collections.Generic;

namespace FTS.Precatorio.Application.ViewModels.Trade
{
    public class TradeViewModel
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }

        public static List<TradeViewModel> Map(IEnumerable<TradeEntity> trades)
        {
            var data = new List<TradeViewModel>();

            foreach (var trade in trades)
            {
                data.Add(new TradeViewModel
                {
                    Id = trade.Id,
                    Code = trade.Code
                });
            }

            return data;
        }
    }
}