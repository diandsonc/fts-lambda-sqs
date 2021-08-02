using System;

namespace FTS.Precatorio.Domain.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();

        void ConfigContext(ICoreContext context);
    }
}