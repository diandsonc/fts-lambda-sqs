using System;
using Amazon.DynamoDBv2.DataModel;
using FTS.Precatorio.Domain.Converters;

namespace FTS.Precatorio.Domain.Trade
{
    [DynamoDBTable(DynamoDBAttributeNames.Table)]
    public partial class Trade
    {
        [DynamoDBHashKey(DynamoDBAttributeNames.PartitionKey)]
        public string PartitionKey { get; set; }

        [DynamoDBRangeKey(DynamoDBAttributeNames.SortKey)]
        public string SortKey { get; set; }

        [DynamoDBProperty(typeof(DynamoDBUlidConverter))]
        public Ulid Ulid { get; set; }

        [DynamoDBProperty]
        public string Code { get; set; }

        [DynamoDBProperty]
        public DateTime CreatedAt { get; set; }

        [DynamoDBProperty]
        public DateTime? LastAttemptAt { get; set; }

        [DynamoDBProperty]
        public string CreatedBy { get; set; }

        [DynamoDBProperty]
        public string LastAttemptUser { get; set; }
    }
}