using Amazon.DynamoDBv2;
using FTS.Precatorio.Domain.Core.Interfaces;

namespace FTS.Precatorio.Infrastructure.Database.DynamoDB.Context
{
    public class FTSPrecatorioContext : CoreContext
    {
        public FTSPrecatorioContext(IAmazonDynamoDB client) : base(client) { }

        public FTSPrecatorioContext(IUserToken token, IAmazonDynamoDB client) : base(client) { }
    }
}