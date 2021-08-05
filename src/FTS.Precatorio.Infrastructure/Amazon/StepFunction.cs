using Amazon.StepFunctions;
using Amazon.StepFunctions.Model;

namespace FTS.Precatorio.Infrastructure.Amazon
{
    public class StepFunction
    {
        private readonly IAmazonStepFunctions _sfClient;

        public StepFunction(IAmazonStepFunctions sfClient)
        {
            _sfClient = sfClient;
        }

        public void SendMessage(string topic, string messageBody)
        {
            var sendMessageRequest = new StartExecutionRequest
            {
                Input = messageBody,
                Name = "SchedulingEngine",
                StateMachineArn = "arn:aws:states:us-west-2:<SomeNumber>:stateMachine:SchedulingEngine"
            };

            _sfClient.StartExecutionAsync(sendMessageRequest).Wait();
        }
    }
}