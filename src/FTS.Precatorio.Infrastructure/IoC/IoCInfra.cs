using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon.SQS;
using FTS.Precatorio.Domain.Core.Interfaces;
using FTS.Precatorio.Domain.Core.SQS;
using FTS.Precatorio.Domain.Trade.Repository;
using FTS.Precatorio.Infrastructure.Amazon;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DynamoDbRepository = FTS.Precatorio.Infrastructure.Database.DynamoDB.Repository;
using DynamoDbContext = FTS.Precatorio.Infrastructure.Database.DynamoDB.Context;
using FTS.Precatorio.Infrastructure.User;

namespace FTS.Precatorio.Infrastructure.IoC
{
    public partial class IoCInfra
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IUserToken, UserResolverService>();

            RegisterAmazonServices(services, configuration);
            RegisterServicesDynamoDb(services);
        }

        private static void RegisterServicesDynamoDb(IServiceCollection services)
        {
            services.AddScoped<ITradeRepository, DynamoDbRepository.TradeRepository>();
        }

        private static void RegisterAmazonServices(IServiceCollection services, IConfiguration configuration)
        {
            var amazon = new AmazonConfiguration();
            configuration.GetSection("AmazonWS").Bind(amazon);

            services.AddScoped<DynamoDbContext.FTSPrecatorioContext>(sp =>
            {
                var region = amazon.DynamoDB?.Region ?? amazon.Region;
                var bucketRegion = RegionEndpoint.GetBySystemName(region);
                var autenticacao = new BasicAWSCredentials(amazon.AcessKey, amazon.SecretKey);
                var sqsClient = new AmazonDynamoDBClient(autenticacao, bucketRegion);

                return new DynamoDbContext.FTSPrecatorioContext(sqsClient);
            });

            services.AddScoped<ISQS, SQS.SQS>(sp =>
            {
                var region = amazon.SQS?.Region ?? amazon.Region;
                var bucketRegion = RegionEndpoint.GetBySystemName(region);
                var autenticacao = new BasicAWSCredentials(amazon.AcessKey, amazon.SecretKey);
                var sqsClient = new AmazonSQSClient(autenticacao, bucketRegion);

                return new SQS.SQS(sqsClient);
            });
        }
    }
}