using FTS.Precatorio.Domain.Interfaces;

namespace FTS.Precatorio.Domain.Trade.Repository
{
    public interface ITradeRepository : IRepository<Trade>
    {
        void SendMessage(string message);
    }
}