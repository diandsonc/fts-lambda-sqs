using System.Text.Json;
using Amazon.Lambda.Core;
using FTS.Precatorio.Domain.Notifications;
using FTS.Precatorio.Domain.Trades.Services;
using FTS.Precatorio.Dto.Trades;
using FTS.Precatorio.StepFunction.Trade.Shared.Configuration;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace FTS.Precatorio.StepFunction.Trade.Lambdas.Step1.CreateTrade
{
    public class Function
    {
        private TradeService _tradeService { get; }
        private IDomainNotification _notifications { get; }

        public Function()
        {
            var config = new LambdaConfiguration();

            _tradeService = config.GetService<TradeService>();
            _notifications = config.GetService<IDomainNotification>();
        }

        public object FunctionHandler(object input, ILambdaContext context)
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

            return trade;
        }
    }
}
