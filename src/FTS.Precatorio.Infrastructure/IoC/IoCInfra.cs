using Amazon.SimpleNotificationService;
using Amazon.StepFunctions;
using Amazon.DynamoDBv2;
using Amazon.SQS;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FTS.Precatorio.Infrastructure.AWS;
using FTS.Precatorio.Infrastructure.Database.DynamoDB.Repository;
using FTS.Precatorio.Infrastructure.Database.DynamoDB.Context;
using FTS.Precatorio.Infrastructure.User;
using FTS.Precatorio.Domain.Trades.Services;
using FTS.Precatorio.Domain.Notifications;
using FTS.Precatorio.Domain.Trades.Repository;

namespace FTS.Precatorio.Infrastructure.IoC
{
    public partial class IoCInfra
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IUserToken, UserResolverService>();
            services.AddSingleton<IConfiguration>(configuration);

            services.AddScoped<IDomainNotification, DomainNotification>();

            services.AddTransient<TradeService>();

            RegisterAmazonServices(services, configuration);
            RegisterRespositories(services);
        }

        private static void RegisterRespositories(IServiceCollection services)
        {
            services.AddScoped<ITradeRepository, TradeRepository>();
        }

        private static void RegisterAmazonServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<FTSPrecatorioContext>();

            services.AddAWSService<IAmazonDynamoDB>();
            services.AddAWSService<IAmazonSimpleNotificationService>();
            services.AddAWSService<IAmazonSQS>();
            services.AddAWSService<IAmazonStepFunctions>();

            services.AddTransient<IAWSService, AWSService>();
        }
    }
}