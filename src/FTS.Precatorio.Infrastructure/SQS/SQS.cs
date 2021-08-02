using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<string> ReceiveMessage(string queueUrl, bool pPeek = true)
        {
            var sqsRequest = new ReceiveMessageRequest { QueueUrl = queueUrl, MessageAttributeNames = new List<string> { "All" } };
            var response = await _sqsClient.ReceiveMessageAsync(sqsRequest);

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                throw new ApplicationException("Problems in the endpoint communication!");

            if (response.Messages.Count == 0)
                return null;

            var message = response.Messages.FirstOrDefault();

            if (pPeek)
                await _sqsClient.DeleteMessageAsync(new DeleteMessageRequest { QueueUrl = queueUrl, ReceiptHandle = message.ReceiptHandle });

            return message.Body;
        }
    }
}