using api.Data;
using api.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Infrastructure.Data.Repositories
{
    public class UserRefreshTokenRepository :
        BaseRepository<UserRefreshTokensEntity, int>,
        IUserRefreshTokenRepository
    {
        public UserRefreshTokenRepository(
            ICrudBaseRepository<UserRefreshTokensEntity, int> crudBaseRepository)
            : base(crudBaseRepository)
        {
        }

        public async Task<UserRefreshTokensEntity> GetUserRefreshTokenByUserId(string userId)
        {
            var userRefreshToken = await base.Query()
                .Where(x => x.UserId == userId)
                .FirstAsync();

            return userRefreshToken;
        }

        public async Task DropUserRefreshToken(string userId, string refreshToken)
        {
            var userRefreshToken = await base.Query()
                .Where(x => x.UserId == userId && x.RefreshToken == refreshToken)
                .FirstAsync();
            await DeleteAsync(userRefreshToken);
        }
    }
}
