using System;
using System.Threading;

namespace FTS.Precatorio.Domain.Core.Interfaces
{
    public interface ICoreContext : ICoreContextBase
    {
        int SaveChanges();
    }

    public interface ICoreContextDynamo : ICoreContextBase
    {
        int SaveChanges<T>(T value, CancellationToken cancellationToken);
    }

    public interface ICoreContextBase : IDisposable
    {
        Guid GetGroupControl();
        void SetGroupControl(Guid controlId);
    }
}