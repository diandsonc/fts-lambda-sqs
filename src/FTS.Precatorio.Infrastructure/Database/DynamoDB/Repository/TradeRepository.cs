using FTS.Precatorio.Domain.SQS;
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
        private readonly ISQS _sqs;

        public TradeRepository(ISQS sqs, IConfiguration configuration, FTSPrecatorioContext context) : base(context)
        {
            _sqs = sqs;
            _configuration = configuration;
            _context = context;
        }

        public void SendMessage(string message)
        {
            var queueName = _configuration.GetValue<string>("AmazonWS:SQS:Queues:QueueTrade");

            _sqs.SendMessage(queueName, message);
        }
    }
}