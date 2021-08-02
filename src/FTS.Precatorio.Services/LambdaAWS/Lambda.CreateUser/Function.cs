using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using FTS.Precatorio.Application.Services;
using FTS.Precatorio.Domain.Core.SQS;
using FTS.Precatorio.Domain.Trade;
using FTS.Precatorio.Domain.Trade.Repository;
using FTS.Precatorio.Infrastructure.IoC;
using Lambda.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Lambda.CreateUser
{
    public class Function
    {
        private static IServiceCollection _serviceCollection;
        static IServiceProvider _serviceProvider;

        public Function()
        {
            if (_serviceCollection == null)
            {
                _serviceCollection = new ServiceCollection();

                var builder = StartupUtil.Startup();
                var configuration = builder.Build();

                ConfigureServices(_serviceCollection, configuration);
                _serviceProvider = _serviceCollection.BuildServiceProvider();
            }
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
            context.Logger.LogLine($"Processed message {message.Body}");

            try
            {
                var trade = JsonSerializer.Deserialize<Trade>(message.Body);

                _serviceProvider.GetService<TradeService>().Add(trade);
            }
            catch (Exception ex)
            {

            }

            // TODO: Do interesting work based on the new message
            await Task.CompletedTask;
        }

        private static void ConfigureServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            IoCInfra.RegisterServices(serviceCollection, configuration);
        }
    }
}
