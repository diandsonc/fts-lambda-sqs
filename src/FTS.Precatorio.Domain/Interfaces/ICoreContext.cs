using System;
using System.Threading;
using System.Threading.Tasks;

namespace FTS.Precatorio.Domain.Interfaces
{
    public interface ICoreContext : IDisposable
    {
        Task SaveChangesAsync<T>(T value, CancellationToken cancellationToken);
        Guid GetGroupControl();
        void SetGroupControl(Guid controlId);
    }
}