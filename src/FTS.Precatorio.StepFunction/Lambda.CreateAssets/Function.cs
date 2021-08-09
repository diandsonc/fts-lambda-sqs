using Amazon.Lambda.Core;
using FTS.Precatorio.Domain.Notifications;
using FTS.Precatorio.Domain.Trades.Services;
using FTS.Precatorio.Infrastructure.IoC;
using Lambda.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Lambda.CreateAssets
{
    public class Function
    {
        private TradeService _tradeService { get; }
        private IDomainNotification _notifications { get; }

        public Function()
        {
            var serviceCollection = new ServiceCollection();
            var configuration = LambdaConfiguration.Configuration;

            ConfigureServices(serviceCollection, configuration);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _tradeService = serviceProvider.GetService<TradeService>();
            _notifications = serviceProvider.GetService<IDomainNotification>();
        }

        public string FunctionHandler(object input, ILambdaContext context)
        {
            context.Logger.LogLine($"Processed message {input}");

            context.Logger.LogLine($"Assets created");

            return input.ToString();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            IoCInfra.RegisterServices(serviceCollection, configuration);
        }
    }
}
