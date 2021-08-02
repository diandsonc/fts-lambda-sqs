using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FTS.Precatorio.Domain.Trade;
using FTS.Precatorio.Domain.Trade.Repository;
using FTS.Precatorio.Infrastructure.Database.SQLServer.Context;

namespace FTS.Precatorio.Infrastructure.Database.SQLServer.Repository
{
    public class TradeRepository : Repository<Trade>, ITradeRepository
    {
        private FTSPrecatorioContext _context;

        public TradeRepository(FTSPrecatorioContext context) : base(context)
        {
            _context = context;
        }

        public void AddTrade(Trade trade)
        {
            _context.Add(trade);
        }

        public IEnumerable<Trade> FindTrade(Expression<Func<Trade, bool>> predicate)
        {
            var trades = Find<Trade>(predicate);

            return trades;
        }

        public void UpdateTrade(Trade trade)
        {
            _context.Update(trade);
        }
    }
}