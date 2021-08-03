namespace FTS.Precatorio.Domain.SQS
{
    public interface ISQS
    {
        void SendMessage(string queueName, string messageBody);
    }
}