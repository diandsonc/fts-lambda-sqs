using System;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using FTS.Precatorio.Infrastructure.AWS;
using FTS.Precatorio.Messaging.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Amazon.Lambda.SQSEvents.SQSEvent;

namespace FTS.Precatorio.Messaging
{
    public class Function
    {
        private IAWSService _awsService { get; }

        public Function() : this(new LambdaConfiguration().BuildServiceProvider()) { }

        public Function(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new ArgumentException(nameof(serviceProvider));

            _awsService = serviceProvider.GetRequiredService<IAWSService>();
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
    }
}
