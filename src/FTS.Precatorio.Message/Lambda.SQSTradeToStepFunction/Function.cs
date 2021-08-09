using System;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using FTS.Precatorio.Infrastructure.AWS;
using FTS.Precatorio.Infrastructure.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Amazon.Lambda.SQSEvents.SQSEvent;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Lambda.SQSTradeToStepFunction
{
    public class Function
    {
        private IAWSService _awsService { get; }

        public Function()
        {
            var serviceCollection = new ServiceCollection();
            var configuration = LambdaConfiguration.Configuration;

            ConfigureServices(serviceCollection, configuration);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _awsService = serviceProvider.GetService<IAWSService>();
        }

        public void FunctionHandler(SQSEvent evnt, ILambdaContext context)
        {
            foreach (var message in evnt.Records)
            {
                if (message != null)
                    ProcessMessage(message, context);
            }
        }

        private void ProcessMessage(SQSMessage message, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine($"Processed message {message.Body}");

                _awsService.SendMessageToStepFunction("CreateTrade", message.Body);

                context.Logger.LogLine($"Message sent to step function");
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Error: {ex.Message}");

                // TODO send to sns
            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            IoCInfra.RegisterServices(serviceCollection, configuration);
        }
    }
}
