using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon.SQS;
using FTS.Precatorio.Domain.Core.Interfaces;
using FTS.Precatorio.Domain.Core.SQS;
using FTS.Precatorio.Domain.Trade.Repository;
using FTS.Precatorio.Infrastructure.Amazon;
using FTS.Precatorio.Infrastructure.Wow;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SQLServerContext = FTS.Precatorio.Infrastructure.Database.SQLServer.Context;
using SQLServerRepository = FTS.Precatorio.Infrastructure.Database.SQLServer.Repository;
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

            // Infra - Data
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            RegisterAmazonServices(services, configuration);
            RegisterServicesDynamoDb(services);
            RegisterServicesSQLServer(services);
        }

        private static void RegisterServicesDynamoDb(IServiceCollection services)
        {
        }

        private static void RegisterServicesSQLServer(IServiceCollection services)
        {
            services.AddScoped<SQLServerContext.FTSPrecatorioContext>();
            services.AddScoped<ITradeRepository, SQLServerRepository.TradeRepository>();
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