namespace FTS.Precatorio.Domain.SNS
{
    public interface ISNS
    {
        void SendMessage(string topic, string messageBody);
    }
}