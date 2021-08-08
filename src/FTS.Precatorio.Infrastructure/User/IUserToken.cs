using System;

namespace FTS.Precatorio.Infrastructure.User
{
    public interface IUserToken
    {
        Guid GetTenantId();
        string GetUserLogin();
    }
}