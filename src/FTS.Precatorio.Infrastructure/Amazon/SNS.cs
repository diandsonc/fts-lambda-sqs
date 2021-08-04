using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using FTS.Precatorio.Domain.SNS;

namespace FTS.Precatorio.Infrastructure.Amazon
{
    public class SNS : ISNS
    {
        private readonly IAmazonSimpleNotificationService _snsClient;

        public SNS(IAmazonSimpleNotificationService snsClient)
        {
            _snsClient = snsClient;
        }

        public void SendMessage(string topic, string messageBody)
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
    }
}