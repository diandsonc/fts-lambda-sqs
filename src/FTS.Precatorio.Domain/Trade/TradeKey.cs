using System;

namespace FTS.Precatorio.Domain.Trade
{
    public class TradeKey
    {
        public string PartitionKey { get; }
        public string SortKey { get; }

        public TradeKey(Trade trade, Guid tenantId) : this(trade.Ulid, tenantId) { }

        public TradeKey(Ulid ulid, Guid tenantId)
        {
            PartitionKey = $"Tenant#{tenantId.ToString()}#Trade#{ulid.Time.ToString("yyyy-MM-dd")}";
            SortKey = $"Trade#{ulid}";
        }
    }
}