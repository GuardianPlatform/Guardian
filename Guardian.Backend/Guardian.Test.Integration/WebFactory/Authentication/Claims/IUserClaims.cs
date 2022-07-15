using System.Collections.Generic;
using System.Security.Claims;

namespace Guardian.Test.Integration.WebFactory.UserClaims
{
    public interface IUserClaims
    {
        IEnumerable<Claim> Claims();
    }
}