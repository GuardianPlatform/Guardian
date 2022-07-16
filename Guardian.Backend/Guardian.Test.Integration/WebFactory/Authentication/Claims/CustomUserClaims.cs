using System.Collections.Generic;
using System.Security.Claims;

namespace Guardian.Test.Integration.WebFactory.Authentication.Claims
{
    public class CustomUserClaims : IUserClaims
    {
        private readonly IEnumerable<Claim> _claims;

        public CustomUserClaims(IEnumerable<Claim> claims)
        {
            _claims = claims;
        }

        public IEnumerable<Claim> Claims()
        {
            return _claims;
        }
    }
}