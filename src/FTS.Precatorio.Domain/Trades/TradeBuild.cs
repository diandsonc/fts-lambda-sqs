using System;

namespace FTS.Precatorio.Domain.Trades
{
    public partial class Trade
    {
        public void WithKey(string partitionKey, string sortKey)
        {
            PartitionKey = partitionKey;
            SortKey = sortKey;
        }

        public void AddLogData(string username)
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = username;
        }

        public void UpdateLogData(string username)
        {
            LastAttemptAt = DateTime.UtcNow;
            LastAttemptUser = username;
        }
    }
}