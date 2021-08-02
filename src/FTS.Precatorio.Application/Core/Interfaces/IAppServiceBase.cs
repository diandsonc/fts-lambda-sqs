using System;

namespace FTS.Precatorio.Application.Core.Interfaces
{
    public interface IAppServiceBase : IDisposable
    {
        void SetGroupId(Guid controlGroupId);

        void IgnoreGroup(bool ignore);
    }
}