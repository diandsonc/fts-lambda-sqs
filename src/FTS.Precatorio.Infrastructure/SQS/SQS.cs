using Amazon.SQS;
using Amazon.SQS.Model;
using FTS.Precatorio.Domain.Core.SQS;

namespace FTS.Precatorio.Infrastructure.SQS
{
    public class SQS : ISQS
    {
        private readonly IAmazonSQS _sqsClient;

        public SQS(IAmazonSQS sqsClient)
        {
            _sqsClient = sqsClient;
        }

        public void SendMessage(string queueName, string messageBody)
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
    }
}