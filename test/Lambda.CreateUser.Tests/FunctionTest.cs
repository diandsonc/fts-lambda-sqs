using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.SQSEvents;
using System;

namespace Lambda.CreateUser.Tests
{
    public class FunctionTest
    {
        [Fact]
        public async Task SQSEventWithSucesses()
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
            await function.FunctionHandler(sqsEvent, context);

            Assert.Contains("Trade created", logger.Buffer.ToString());
        }

        [Fact]
        public async Task SQSEventMultiMessageHasError()
        {
            var sqsEvent = new SQSEvent
            {
                Records = new List<SQSEvent.SQSMessage> {
                    new SQSEvent.SQSMessage { Body = "{ code: 'teste' }" },
                    new SQSEvent.SQSMessage { Body = "{ code: 'teste' }" }
                }
            };

            var logger = new TestLambdaLogger();
            var context = new TestLambdaContext { Logger = logger };

            var function = new Function();

            await Assert.ThrowsAsync<InvalidOperationException>(() => function.FunctionHandler(sqsEvent, context));
        }
    }
}
