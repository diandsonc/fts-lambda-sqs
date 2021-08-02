using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FTS.Precatorio.Domain.Core.Interfaces;

namespace FTS.Precatorio.Domain.Trade.Repository
{
    public interface ITradeRepository : IRepository<Trade>
    {
        IEnumerable<Trade> FindTrade(Expression<Func<Trade, bool>> predicate);
        void AddTrade(Trade trade);
        void UpdateTrade(Trade trade);
    }
}