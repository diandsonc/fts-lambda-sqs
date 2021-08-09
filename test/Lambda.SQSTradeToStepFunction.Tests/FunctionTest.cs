using System.Collections.Generic;
using Xunit;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.SQSEvents;

namespace Lambda.SQSTradeToStepFunction.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void SQSEventWithSucesses()
        {
            var sqsEvent = new SQSEvent
            {
                Records = new List<SQSEvent.SQSMessage> {
                    new SQSEvent.SQSMessage { Body = "{ \"Code\": \"92115\"}" }
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
