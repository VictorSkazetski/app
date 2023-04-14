using System.Security.Claims;
using api.Domain.Command;

namespace api.Domain.Interfaces
{
    public interface IJWTManager
    {
        UserTokensData GenerateToken(string userEmail);

        UserTokensData GenerateRefreshToken(string userEmail);

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
