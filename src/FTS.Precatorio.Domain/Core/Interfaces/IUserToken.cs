using System;

namespace FTS.Precatorio.Domain.Core.Interfaces
{
    public interface IUserToken
    {
        string GetUser();
        Guid GetUserId();
        Guid GetControlId();
    }
}