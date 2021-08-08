using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using FTS.Precatorio.Infrastructure.User;

namespace FTS.Precatorio.Infrastructure.Database.DynamoDB.Context
{
    public class FTSPrecatorioContext : DynamoDBContext
    {
        private Guid _control_TenantId { get; set; }
        private string _control_UserLogin { get; set; }

        public FTSPrecatorioContext(IAmazonDynamoDB client) : base(client) { }

        public FTSPrecatorioContext(IUserToken token, IAmazonDynamoDB client) : base(client)
        {
            _control_TenantId = token.GetTenantId();
            _control_UserLogin = token.GetUserLogin();
        }

        public Guid GetTenantId() => _control_TenantId;

        public string GetUserLogin() => _control_UserLogin;
    }
}