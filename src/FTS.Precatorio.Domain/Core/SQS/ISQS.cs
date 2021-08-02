namespace FTS.Precatorio.Domain.Core.SQS
{
    public interface ISQS
    {
        void SendMessage(string queueName, string messageBody);
    }
}