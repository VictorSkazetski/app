using api.Data;

namespace api.Infrastructure.Data.Repositories.Interfaces
{
    public interface IUserRefreshTokenRepository :
        IBaseRepository<UserRefreshTokensEntity, int>
    {
        Task DropUserRefreshToken(string userId, string refreshToken);

        Task<UserRefreshTokensEntity> GetUserRefreshTokenByUserId(string userId);
    }
}
