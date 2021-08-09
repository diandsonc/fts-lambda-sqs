using System.Text.Json;
using Amazon.Lambda.Core;
using FTS.Precatorio.Domain.Notifications;
using FTS.Precatorio.Domain.Trade.Services;
using FTS.Precatorio.Dto.Trade;
using FTS.Precatorio.Infrastructure.IoC;
using Lambda.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Lambda.CreateTrade
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

            var trade = JsonSerializer.Deserialize<TradeViewModel>(input.ToString());

            _tradeService.Add(trade.Map()).Wait();

            if (_notifications.HasNotifications())
            {
                var error = $"Error on create trade {trade.Id}: {JsonSerializer.Serialize(_notifications.GetNotifications())}";

                context.Logger.LogLine(error);

                return error;
            }

            context.Logger.LogLine($"Trade created");

            return JsonSerializer.Serialize(trade);
        }

        private static void ConfigureServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            IoCInfra.RegisterServices(serviceCollection, configuration);
        }
    }
}
