using System.Security.Claims;
using api.Domain.Command;

namespace api.Domain.Interfaces
{
    public interface IJWTManager
    {
        Task<UserTokensData> GenerateToken(string userEmail);

        Task<UserTokensData> GenerateRefreshToken(string userEmail);

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
