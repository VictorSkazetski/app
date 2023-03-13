using api.Domain.Model.Poco;
using System.Security.Claims;

namespace api.Domain.Interfaces
{
    public interface IJWTManager
    {
        Tokens GenerateToken(string userEmail);

        Tokens GenerateRefreshToken(string userEmail);

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
