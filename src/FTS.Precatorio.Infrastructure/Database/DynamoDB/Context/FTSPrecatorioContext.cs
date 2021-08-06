using Amazon.DynamoDBv2;
using FTS.Precatorio.Infrastructure.User;

namespace FTS.Precatorio.Infrastructure.Database.DynamoDB.Context
{
    public class FTSPrecatorioContext : CoreContext
    {
        public FTSPrecatorioContext(IAmazonDynamoDB client) : base(client) { }

        public FTSPrecatorioContext(IUserToken token, IAmazonDynamoDB client) : base(client) { }
    }
}