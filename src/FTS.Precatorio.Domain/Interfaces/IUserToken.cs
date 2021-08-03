using System;

namespace FTS.Precatorio.Domain.Interfaces
{
    public interface IUserToken
    {
        string GetUser();
        Guid GetUserId();
        Guid GetControlId();
    }
}