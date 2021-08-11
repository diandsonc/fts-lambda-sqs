using Xunit;
using Amazon.Lambda.TestUtilities;
using FTS.Precatorio.StepFunction.Trade.Lambdas.Step3.CreateAssets;
using FTS.Precatorio.Dto.Trades;
using System.Text.Json;

namespace Lambda.CreateAssets.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestCreateAssets()
        {
            var function = new Function();
            var context = new TestLambdaContext();
            var message = JsonSerializer.Serialize(new TradeViewModel { Code = "92115" });
            var result = function.FunctionHandler(message, context) as TradeViewModel;

            Assert.Equal("92115", result.Code);
        }
    }
}
