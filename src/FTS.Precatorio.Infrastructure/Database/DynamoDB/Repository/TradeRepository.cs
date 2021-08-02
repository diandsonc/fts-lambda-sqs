using FTS.Precatorio.Domain.Trade;
using FTS.Precatorio.Domain.Trade.Repository;
using FTS.Precatorio.Infrastructure.Database.DynamoDB.Context;

namespace FTS.Precatorio.Infrastructure.Database.DynamoDB.Repository
{
    public class TradeRepository : Repository<Trade>, ITradeRepository
    {
        private FTSPrecatorioContext _context;

        public TradeRepository(FTSPrecatorioContext context) : base(context)
        {
            _context = context;
        }
    }
}