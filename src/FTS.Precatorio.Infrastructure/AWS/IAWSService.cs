namespace FTS.Precatorio.Infrastructure.AWS
{
    public interface IAWSService
    {
        string GetKeyValue(string key);

        void SendMessageToSQS(string queueName, string messageBody);

        void SendMessageToSNS(string topic, string messageBody);

        void SendMessageToStepFunction(string topic, string messageBody);
    }
}