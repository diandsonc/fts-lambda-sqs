using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FTS.Precatorio.Domain.Trades;
using FTS.Precatorio.Domain.Trades.Repository;
using FTS.Precatorio.Infrastructure.AWS;
using FTS.Precatorio.Infrastructure.Database.DynamoDB.Context;
using Microsoft.Extensions.Configuration;

namespace FTS.Precatorio.Infrastructure.Database.DynamoDB.Repository
{
    public class TradeRepository : ITradeRepository
    {
        private FTSPrecatorioContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IAWSService _awsService;

        public TradeRepository(IAWSService awsService, IConfiguration configuration, FTSPrecatorioContext dbContext)
        {
            _awsService = awsService;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public void SendMessage(string message)
        {
            var queueName = _awsService.GetKeyValue("AWS:SQS:QueueTrade");

            _awsService.SendMessageToSQS(queueName, message);
        }

        public async Task<Trade> GetTradeAsync(Ulid ulid)
        {
            var tradeKey = new TradeKey(ulid, _dbContext.GetTenantId());

            return await _dbContext.LoadAsync<Trade>(tradeKey.PartitionKey, tradeKey.SortKey);
        }

        public async Task<ICollection<Trade>> ListTradeAsync(TradeQuery query)
        {
            var search = _dbContext.FromQueryAsync<Trade>(query.ToDynamoDBQuery(_dbContext.GetTenantId()));

            return await search.GetNextSetAsync(); ;
        }

        public async Task SaveAsync(Trade trade)
        {
            var key = new TradeKey(trade, _dbContext.GetTenantId());

            trade.WithKey(key.PartitionKey, key.SortKey);
            trade.AddLogData(_dbContext.GetUserLogin());


            await _dbContext.SaveAsync<Trade>(trade);
        }
    }
}