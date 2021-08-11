using System;
using System.Text.Json;
using Amazon.Lambda.Core;
using FTS.Precatorio.Domain.Notifications;
using FTS.Precatorio.Domain.Trades.Services;
using FTS.Precatorio.Dto.Trades;
using FTS.Precatorio.StepFunction.Trade.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FTS.Precatorio.StepFunction.Trade.Lambdas.Step2.CreateTicket
{
    public class Function
    {
        private TradeService _tradeService { get; }
        private IDomainNotification _notifications { get; }

        public Function() : this(new LambdaConfiguration().BuildServiceProvider()) { }

        public Function(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new ArgumentException(nameof(serviceProvider));

            _tradeService = serviceProvider.GetRequiredService<TradeService>();
            _notifications = serviceProvider.GetRequiredService<IDomainNotification>();
        }

        public object FunctionHandler(object input, ILambdaContext context)
        {
            context.Logger.LogLine($"Processed message {input}");

            var trade = JsonSerializer.Deserialize<TradeViewModel>(input.ToString());

            context.Logger.LogLine($"Ticket created");

            return trade;
        }
    }
}
