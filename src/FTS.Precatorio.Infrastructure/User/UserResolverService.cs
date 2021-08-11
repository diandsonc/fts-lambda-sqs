using System;
using FTS.Precatorio.Infrastructure.Countries;
using Microsoft.AspNetCore.Http;

namespace FTS.Precatorio.Infrastructure.User
{
    public class UserResolverService : IUserToken
    {
        private readonly IHttpContextAccessor _context;

        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public Guid GetTenantId()
        {
            return CountryIds.Brazil;
        }

        public string GetUserLogin()
        {
            if (_context?.HttpContext?.User == null || _context.HttpContext.User.Identity == null)
                return null;

            var username = _context.HttpContext.User.Identity.Name ?? "root";
            var pos = username.LastIndexOf("\\") + 1;

            return username.Substring(pos, username.Length - pos);
        }
    }
}