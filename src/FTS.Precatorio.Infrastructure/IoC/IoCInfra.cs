using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon.SQS;
using FTS.Precatorio.Domain.Trade.Repository;
using FTS.Precatorio.Infrastructure.Amazon;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FTS.Precatorio.Infrastructure.Database.DynamoDB.Repository;
using FTS.Precatorio.Infrastructure.Database.DynamoDB.Context;
using FTS.Precatorio.Infrastructure.User;
using FTS.Precatorio.Domain.Interfaces;
using FTS.Precatorio.Domain.SQS;
using FTS.Precatorio.Domain.Trade.Services;
using FTS.Precatorio.Domain.Notifications;
using FTS.Precatorio.Domain.SNS;
using Amazon.SimpleNotificationService;

namespace FTS.Precatorio.Infrastructure.IoC
{
    public partial class IoCInfra
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IUserToken, UserResolverService>();
            services.AddSingleton<IConfiguration>(configuration);

            services.AddTransient<TradeService>();

            services.AddScoped<IDomainNotification, DomainNotification>();

            RegisterAmazonServices(services, configuration);
            RegisterServicesDynamoDb(services);
        }

        private static void RegisterServicesDynamoDb(IServiceCollection services)
        {
            services.AddScoped<ITradeRepository, TradeRepository>();
        }

        private static void RegisterAmazonServices(IServiceCollection services, IConfiguration configuration)
        {
            var amazon = new AmazonConfiguration();
            configuration.GetSection("AmazonWS").Bind(amazon);

            services.AddScoped<FTSPrecatorioContext>(sp =>
            {
                var region = amazon.DynamoDB?.Region ?? amazon.Region;
                var bucketRegion = RegionEndpoint.GetBySystemName(region);
                var autenticacao = new BasicAWSCredentials(amazon.AcessKey, amazon.SecretKey);
                var sqsClient = new AmazonDynamoDBClient(autenticacao, bucketRegion);

                return new FTSPrecatorioContext(sqsClient);
            });

            services.AddScoped<ISQS, SQS>(sp =>
            {
                var region = amazon.SQS?.Region ?? amazon.Region;
                var bucketRegion = RegionEndpoint.GetBySystemName(region);
                var autenticacao = new BasicAWSCredentials(amazon.AcessKey, amazon.SecretKey);
                var sqsClient = new AmazonSQSClient(autenticacao, bucketRegion);

                return new SQS(sqsClient);
            });

            services.AddScoped<ISNS, SNS>(sp =>
            {
                var region = amazon.SNS?.Region ?? amazon.Region;
                var bucketRegion = RegionEndpoint.GetBySystemName(region);
                var autenticacao = new BasicAWSCredentials(amazon.AcessKey, amazon.SecretKey);
                var snsClient = new AmazonSimpleNotificationServiceClient(autenticacao, bucketRegion);

                return new SNS(snsClient);
            });
        }
    }
}