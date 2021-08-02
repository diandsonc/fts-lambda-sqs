using System;
using System.Threading;

namespace FTS.Precatorio.Domain.Core.Interfaces
{
    public interface ICoreContext : IDisposable
    {
        int SaveChanges<T>(T value, CancellationToken cancellationToken);
        Guid GetGroupControl();
        void SetGroupControl(Guid controlId);
    }
}