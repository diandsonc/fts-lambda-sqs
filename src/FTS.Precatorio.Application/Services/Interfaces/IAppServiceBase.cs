using System;

namespace FTS.Precatorio.Application.Services.Interfaces
{
    public interface IAppServiceBase : IDisposable
    {
        void SetGroupId(Guid controlGroupId);

        void IgnoreGroup(bool ignore);
    }
}