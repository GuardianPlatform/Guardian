using System.Collections.Generic;
using System.Security.Claims;

namespace Guardian.Test.Integration.WebFactory.Authentication.Claims
{
    public interface IUserClaims
    {
        IEnumerable<Claim> Claims();
    }
}