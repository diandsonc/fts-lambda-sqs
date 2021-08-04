using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using FTS.Precatorio.Domain.Trade.Services;
using FTS.Precatorio.Dto.Trade;
using FTS.Precatorio.Infrastructure.IoC;
using Lambda.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Lambda.CreateTrade
{
    public class Function
    {
        private TradeService TradeService { get; }

        public Function()
        {
            var serviceCollection = new ServiceCollection();
            var configuration = LambdaConfiguration.Configuration;

            ConfigureServices(serviceCollection, configuration);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            TradeService = serviceProvider.GetService<TradeService>();
        }

        public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
        {
            if (evnt.Records.Count > 1) throw new InvalidOperationException("Only one message by time");

            var message = evnt.Records.FirstOrDefault();

            if (message == null) return;

            await ProcessMessageAsync(message, context);
        }

        private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine($"Processed message {message.Body}");

                var trade = JsonSerializer.Deserialize<TradeViewModel>(message.Body);

                await TradeService.Add(trade.Map());

                context.Logger.LogLine($"Trade created {trade.Id}");

                //send to next queue
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Error: {ex.Message}");
            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            IoCInfra.RegisterServices(serviceCollection, configuration);
        }
    }
}
