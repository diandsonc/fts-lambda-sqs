using FTS.Precatorio.Domain.Amazon;
using FTS.Precatorio.Domain.Trade;
using FTS.Precatorio.Domain.Trade.Repository;
using FTS.Precatorio.Infrastructure.Database.DynamoDB.Context;
using Microsoft.Extensions.Configuration;

namespace FTS.Precatorio.Infrastructure.Database.DynamoDB.Repository
{
    public class TradeRepository : Repository<Trade>, ITradeRepository
    {
        private FTSPrecatorioContext _context;
        private readonly IConfiguration _configuration;
        private readonly AmazonService _amazonService;

        public TradeRepository(AmazonService amazonService, IConfiguration configuration, FTSPrecatorioContext context) : base(context)
        {
            _amazonService = amazonService;
            _configuration = configuration;
            _context = context;
        }

        public void SendMessage(string message)
        {
            var queueName = _amazonService.GetKeyValue("AWS:SQS:Queues:QueueTrade");

            _amazonService.SendMessageToSQS(queueName, message);
        }
    }
}