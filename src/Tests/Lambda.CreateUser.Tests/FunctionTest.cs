using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.SQSEvents;

namespace Lambda.CreateUser.Tests
{
    public class FunctionTest
    {
        [Fact]
        public async Task TestSQSEventLambdaFunction()
        {
            var sqsEvent = new SQSEvent
            {
                Records = new List<SQSEvent.SQSMessage>
                {
                    new SQSEvent.SQSMessage
                    {
                        Body = "foobar"
                    }
                }
            };

            var logger = new TestLambdaLogger();
            var context = new TestLambdaContext
            {
                Logger = logger
            };

            var function = new Function();
            await function.FunctionHandler(sqsEvent, context);

            Assert.Contains("Processed message foobar", logger.Buffer.ToString());
        }
        // [Fact]
        // public void TestToUpperFunction()
        // {
        //     // Invoke the lambda function and confirm the string was upper cased.
        //     var function = new Function();
        //     var context = new TestLambdaContext();
        //     // var upperCase = function.FunctionHandler("hello world", context);

        //     Assert.Equal("HELLO WORLD", upperCase);
        // }
    }
}
