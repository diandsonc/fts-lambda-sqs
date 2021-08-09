using Xunit;
using Amazon.Lambda.TestUtilities;

namespace Lambda.CreateAssets.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestCreateAssets()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var result = function.FunctionHandler("{ \"Code\": \"123456\" }", context);

            Assert.Equal("{ \"Code\": \"123456\" }", result);
        }
    }
}
