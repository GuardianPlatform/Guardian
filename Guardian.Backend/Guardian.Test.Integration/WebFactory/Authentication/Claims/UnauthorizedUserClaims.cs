using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Guardian.Test.Integration.WebFactory.Authentication.Claims
{
    public class UnauthorizedUserClaims : IUserClaims
    {
        public IEnumerable<Claim> Claims()
        {
            return Array.Empty<Claim>();
        }
    }
}