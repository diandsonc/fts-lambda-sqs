using System;
using System.Threading;
using System.Threading.Tasks;

namespace FTS.Precatorio.Infrastructure.Database.DynamoDB.Context
{
    public interface ICoreContext : IDisposable
    {
        Task SaveChangesAsync<T>(T value, CancellationToken cancellationToken);
    }
}