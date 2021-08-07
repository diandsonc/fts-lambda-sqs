using System;

namespace FTS.Precatorio.Infrastructure.User
{
    public interface IUserToken
    {
        string GetUser();
        Guid GetUserId();
        Guid GetTenantId();
    }
}