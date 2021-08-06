using FTS.Precatorio.Domain.Trade;
using FTS.Precatorio.Domain.Trade.Repository;
using FTS.Precatorio.Infrastructure.AWS;
using FTS.Precatorio.Infrastructure.Database.DynamoDB.Context;
using Microsoft.Extensions.Configuration;

namespace FTS.Precatorio.Infrastructure.Database.DynamoDB.Repository
{
    public class TradeRepository : Repository<Trade>, ITradeRepository
    {
        private FTSPrecatorioContext _context;
        private readonly IConfiguration _configuration;
        private readonly IAWSService _awsService;

        public TradeRepository(IAWSService awsService, IConfiguration configuration, FTSPrecatorioContext context) : base(context)
        {
            _awsService = awsService;
            _configuration = configuration;
            _context = context;
        }

        public void SendMessage(string message)
        {
            var queueName = _awsService.GetKeyValue("AWS:SQS:Queues:QueueTrade");

            _awsService.SendMessageToSQS(queueName, message);
        }
    }
}