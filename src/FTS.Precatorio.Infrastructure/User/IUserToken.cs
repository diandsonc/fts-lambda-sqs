using System;

namespace FTS.Precatorio.Infrastructure.User
{
    public interface IUserToken
    {
        string GetUserName();
        Guid GetUserId();
        Guid GetTenantId();
    }
}