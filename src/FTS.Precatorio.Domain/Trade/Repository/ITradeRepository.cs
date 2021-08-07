using System;
using System.Threading.Tasks;

namespace FTS.Precatorio.Domain.Trade.Repository
{
    public interface ITradeRepository
    {
        void SendMessage(string message);
        Task Add(Trade obj);
        Task<Trade> GetById(Ulid id);
    }
}