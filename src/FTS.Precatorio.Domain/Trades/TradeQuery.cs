using System;
using Amazon.DynamoDBv2.DocumentModel;
using FTS.Precatorio.Domain.Converters;

namespace FTS.Precatorio.Domain.Trades
{
    public class TradeQuery
    {
        public Ulid BeforeTradeUlid { get; set; }
        public int Length { get; set; }
        public string Code { get; set; }
        public string PaginationToken { get; set; }

        public QueryOperationConfig ToDynamoDBQuery(Guid tenantId)
        {
            var key = new TradeKey(BeforeTradeUlid, tenantId);
            var filter = new QueryFilter();

            filter.AddCondition(DynamoDBAttributeNames.PartitionKey, QueryOperator.Equal, key.PartitionKey);
            filter.AddCondition(DynamoDBAttributeNames.SortKey, QueryOperator.Equal, key.SortKey);

            ScanByCode(filter);

            var queryConfig = new QueryOperationConfig
            {
                Filter = filter,
                Limit = Length,
                BackwardSearch = true
            };

            if (!string.IsNullOrEmpty(PaginationToken))
                queryConfig.PaginationToken = PaginationToken;

            return queryConfig;
        }

        private void ScanByCode(QueryFilter filter)
        {
            if (String.IsNullOrEmpty(Code)) return;

            filter.AddCondition(nameof(Trade.Code), QueryOperator.Equal, Code);
        }
    }
}