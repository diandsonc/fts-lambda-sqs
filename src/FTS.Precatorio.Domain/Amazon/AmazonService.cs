using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Amazon.SQS;
using Amazon.SQS.Model;
using Amazon.StepFunctions;
using Amazon.StepFunctions.Model;
using Microsoft.Extensions.Configuration;

namespace FTS.Precatorio.Domain.Amazon
{
    public class AmazonService
    {
        private readonly IAmazonStepFunctions _sfClient;
        private readonly IAmazonSimpleNotificationService _snsClient;
        private readonly IAmazonSQS _sqsClient;
        private readonly IConfiguration _configuration;

        public AmazonService(IAmazonStepFunctions sfClient, IAmazonSimpleNotificationService snsClient,
            IAmazonSQS sqsClient, IConfiguration configuration)
        {
            _sfClient = sfClient;
            _snsClient = snsClient;
            _sqsClient = sqsClient;
            _configuration = configuration;
        }

        public string GetKeyValue(string key)
        {
            return _configuration.GetValue<string>(key);
        }

        public void SendMessageToSQS(string queueName, string messageBody)
        {
            var sqsRequest = new CreateQueueRequest { QueueName = queueName };
            var createQueueResponse = _sqsClient.CreateQueueAsync(sqsRequest);
            var myQueueUrl = createQueueResponse.Result.QueueUrl;

            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = myQueueUrl,
                MessageBody = messageBody
            };

            _sqsClient.SendMessageAsync(sendMessageRequest).Wait();
        }

        public void SendMessageToSNS(string topic, string messageBody)
        {
            var snsRequest = new CreateTopicRequest { Name = topic };
            var createQueueResponse = _snsClient.CreateTopicAsync(snsRequest);
            var topicArn = createQueueResponse.Result.TopicArn;

            var sendMessageRequest = new PublishRequest
            {
                TopicArn = topicArn,
                Message = messageBody
            };

            _snsClient.PublishAsync(sendMessageRequest).Wait();
        }

        public void SendMessageToStepFunction(string topic, string messageBody)
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