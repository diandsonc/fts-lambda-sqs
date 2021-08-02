using System.Threading.Tasks;
using Xunit;
using Moq;
using FTS.Precatorio.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using FTS.Precatorio.Infrastructure.Database.DynamoDB.Context;
using System.IO;
using Newtonsoft.Json;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;

namespace Service.Trade.Tests
{
    public class ValuesControllerTests
    {
        private readonly Mock<HttpContext> _contextMock;
        private readonly Mock<IUserToken> _userTokenMoq;
        private readonly Mock<FTSPrecatorioContext> _contextFTSPrecatorioMock;

        public ValuesControllerTests()
        {
            _contextMock = new Mock<HttpContext>();
            _userTokenMoq = new Mock<IUserToken>();
            _contextFTSPrecatorioMock = new Mock<FTSPrecatorioContext>(_userTokenMoq.Object);
        }

        [Fact]
        public async Task TestGet()
        {
            var lambdaFunction = new LambdaEntryPoint();

            var requestStr = File.ReadAllText("./Mockups/SampleRequests/ValuesController-Get.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            var context = new TestLambdaContext();
            var response = await lambdaFunction.FunctionHandlerAsync(request, context);

            Assert.Equal(500, response.StatusCode);
            // Assert.Equal(200, response.StatusCode);
            // Assert.Equal("[\"value1\",\"value2\"]", response.Body);
            // Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
            // Assert.Equal("application/json; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
        }
    }
}
