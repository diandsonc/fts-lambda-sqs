using System;

namespace FTS.Precatorio.Domain.Trade.Services.Interfaces
{
    public interface IAppServiceBase : IDisposable
    {
        void SetGroupId(Guid controlGroupId);
        void IgnoreGroup(bool ignore);
    }
}