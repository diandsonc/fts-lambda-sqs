using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace FTS.Precatorio.Infrastructure.User
{
    public class UserResolverService : IUserToken
    {
        private readonly IHttpContextAccessor _context;
        public static string _ClainIdFormat = "{0}|{1}|{2}"; //ID|Name|TenantID

        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public Guid GetTenantId()
        {
            var token = GetClaim();
            if (token == null) return new Guid();

            var buff = token.Value.Split('|');
            return new Guid(buff[2]);
        }

        public string GetUser()
        {
            var token = GetClaim();
            if (token == null) return null;

            var buff = token.Value.Split('|');
            return buff[1];
        }

        public Guid GetUserId()
        {
            var token = GetClaim();
            if (token == null) return new Guid();

            var buff = token.Value.Split('|');
            return new Guid(buff[0]);
        }

        private Claim GetClaim()
        {
            if (_context?.HttpContext?.User == null || _context.HttpContext.User.Identity == null)
                return null;

            var token = _context.HttpContext.User.Claims.Where(x => x.Type == "role").FirstOrDefault();

            return token;
        }
    }
}