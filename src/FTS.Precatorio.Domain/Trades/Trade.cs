using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;
using FTS.Precatorio.Domain.Converters;
using FTS.Precatorio.Domain.Tickets;
using FTS.Precatorio.Domain.Trades.Enum;

namespace FTS.Precatorio.Domain.Trades
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

        [DynamoDBProperty(typeof(DynamoDBGuidConverter))]
        public Guid ExternalId { get; set; }

        [DynamoDBProperty]
        public string Code { get; set; }

        [DynamoDBProperty(typeof(DynamoDBEnumConverter<TradeType>))]
        public TradeType Type { get; set; }

        [DynamoDBProperty]
        public List<Ticket> Tickets { get; set; }

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