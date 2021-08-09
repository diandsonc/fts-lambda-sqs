using Xunit;
using Amazon.Lambda.TestUtilities;

namespace Lambda.CreateTrade.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestCreateTrade()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var result = function.FunctionHandler("{ \"Code\": \"123456\" }", context);

            Assert.Equal("{\"Id\":null,\"Code\":\"123456\"}", result);
        }
    }
}
