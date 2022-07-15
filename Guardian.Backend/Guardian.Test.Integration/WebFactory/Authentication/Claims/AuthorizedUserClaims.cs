using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Guardian.Domain.Enum;

namespace Guardian.Test.Integration.WebFactory.UserClaims
{
    public class AuthorizedUserClaims : IUserClaims
    {
        public IEnumerable<Claim> Claims()
        {
            return new[]
            {
                new Claim(ClaimTypes.Role, "SuperAdmin"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Moderator"),
                new Claim(ClaimTypes.Role, "Basic"),
            };
        }
    }
}
