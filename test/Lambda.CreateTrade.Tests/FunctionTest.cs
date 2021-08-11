using Xunit;
using Amazon.Lambda.TestUtilities;
using FTS.Precatorio.StepFunction.Trade.Lambdas.Step1.CreateTrade;
using FTS.Precatorio.Dto.Trades;
using System.Text.Json;

namespace Lambda.CreateTrade.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestCreateTrade()
        {
            var function = new Function();
            var context = new TestLambdaContext();
            var message = JsonSerializer.Serialize(new TradeViewModel { Code = "92115" });
            var result = function.FunctionHandler(message, context) as TradeViewModel;

            Assert.Equal("92115", result.Code);
        }
    }
}
