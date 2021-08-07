using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FTS.Precatorio.Domain.Trade.Repository
{
    public interface ITradeRepository
    {
        void SendMessage(string message);
        Task SaveAsync(Trade trade);
        Task<Trade> GetTradeAsync(Ulid id);
        Task<ICollection<Trade>> ListTradeAsync(TradeQuery query);
    }
}