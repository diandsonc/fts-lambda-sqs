using System;
using System.Text.Json;
using System.Threading.Tasks;
using FTS.Precatorio.Domain.Trade.Repository;
using FTS.Precatorio.Domain.Trade.Services.Interfaces;

namespace FTS.Precatorio.Domain.Trade.Services
{
    public class TradeService : IAppServiceBase
    {
        private readonly ITradeRepository _tradeRepository;

        public TradeService(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        public void AddToQueue(Trade trade)
        {
            var message = JsonSerializer.Serialize(new { Controller = "trades", Action = "insert", trade.Id });

            _tradeRepository.SendMessage(message);
        }

        public async Task Add(Trade trade)
        {
            await _tradeRepository.Add(trade);
        }

        public async Task<Trade> GetTradeById(Guid tradeId)
        {
            if (tradeId != Guid.Empty)
            {
                var data = await _tradeRepository.GetById(tradeId);

                return data;
            }

            return null;
        }

        public void SetGroupId(Guid controlGroupId)
        {
            _tradeRepository.SetGroupId(controlGroupId);
        }

        public void IgnoreGroup(bool ignore)
        {
            _tradeRepository.IgnoreGroup = ignore;
        }

        public void Dispose()
        {
            _tradeRepository.Dispose();
        }
    }
}