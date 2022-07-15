using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Guardian.Test.Integration.WebFactory.UserClaims
{
    public class UnauthorizedUserClaims : IUserClaims
    {
        public IEnumerable<Claim> Claims()
        {
            return Array.Empty<Claim>();
        }
    }
}