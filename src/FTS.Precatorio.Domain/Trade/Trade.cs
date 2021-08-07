using System;
using Amazon.DynamoDBv2.DataModel;

namespace FTS.Precatorio.Domain.Trade
{
    [DynamoDBTable("test_trade")]
    public partial class Trade
    {
        [DynamoDBHashKey("PK")]
        public string PartitionKey { get; set; }

        [DynamoDBRangeKey("SK")]
        public string SortKey { get; set; }

        public Ulid Ulid { get; set; }

        public string Code { get; set; }

        public DateTime DataInc { get; set; }

        public string UsuInc { get; set; }

        public Trade() { }

        public void CreateKey(Guid tenantId)
        {
            Ulid = Ulid.NewUlid();
            PartitionKey = tenantId.ToString();
            SortKey = $"Trade#{Ulid}";
        }
    }
}