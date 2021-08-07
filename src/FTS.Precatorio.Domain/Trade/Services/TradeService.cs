using System;
using System.Text.Json;
using System.Threading.Tasks;
using FTS.Precatorio.Domain.Notifications;
using FTS.Precatorio.Domain.Trade.Repository;

namespace FTS.Precatorio.Domain.Trade.Services
{
    public class TradeService
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly IDomainNotification _notifications;

        public TradeService(IDomainNotification notifications, ITradeRepository tradeRepository)
        {
            _notifications = notifications;
            _tradeRepository = tradeRepository;
        }

        public void AddToQueue(Trade trade)
        {
            var message = JsonSerializer.Serialize(new { Controller = "trade", Action = "insert", trade.Ulid });

            _tradeRepository.SendMessage(message);
        }

        public async Task Add(Trade trade)
        {
            var validate = new CreateTradeValidator().Validate(trade);

            if (!validate.IsValid)
            {
                _notifications.NotifyError(validate);
                return;
            }

            await _tradeRepository.SaveAsync(trade);
        }

        public async Task<Trade> GetTradeById(Ulid tradeId)
        {
            if (tradeId != Ulid.Empty)
            {
                var data = await _tradeRepository.GetTradeAsync(tradeId);

                return data;
            }

            return null;
        }
    }
}