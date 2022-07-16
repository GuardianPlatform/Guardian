using System.Collections.Generic;
using System.Security.Claims;

namespace Guardian.Test.Integration.WebFactory.Authentication.Claims
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
