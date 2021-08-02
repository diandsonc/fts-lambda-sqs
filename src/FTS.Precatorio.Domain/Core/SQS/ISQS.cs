using System.Threading.Tasks;

namespace FTS.Precatorio.Domain.Core.SQS
{
    public interface ISQS
    {
        void SendMessage(string queueName, string messageBody);
        Task<string> ReceiveMessage(string queueUrl, bool pPeek = true);
    }
}