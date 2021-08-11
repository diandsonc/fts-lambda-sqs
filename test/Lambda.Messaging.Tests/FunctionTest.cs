using Xunit;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.SQSEvents;
using FTS.Precatorio.Messaging;
using FTS.Precatorio.Dto.Trades;
using System.Text.Json;
using System.Collections.Generic;

namespace Lambda.Messaging.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void SQSEventWithSucesses()
        {
            var sqsEvent = new SQSEvent
            {
                Records = new List<SQSEvent.SQSMessage> {
                    new SQSEvent.SQSMessage { Body = JsonSerializer.Serialize(new TradeViewModel{ Code = "92115"}) }
                }
            };

            var logger = new TestLambdaLogger();
            var context = new TestLambdaContext { Logger = logger };

            var function = new Function();
            function.FunctionHandler(sqsEvent, context);

            Assert.Contains("Message sent to step function", logger.Buffer.ToString());
        }
    }
}
