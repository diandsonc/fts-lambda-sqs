using System;
using System.ComponentModel.DataAnnotations;
using TradeEntity = FTS.Precatorio.Domain.Trade.Trade;

namespace FTS.Precatorio.Dto.Trade
{
    public class TradeViewModel
    {
        public Guid? Id { get; set; }

        [Required]
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

        public TradeEntity Map()
        {
            var data = new TradeEntity.Factory().Create(Code, Guid.Empty);

            return data;
        }
    }
}