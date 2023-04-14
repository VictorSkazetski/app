using api.Data;
using api.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Infrastructure.Data.Repositories
{
    public class UserProfileRepository :
        BaseRepository<UserProfileEntity, int>,
        IUserProfileRepository
    {
        public UserProfileRepository(
            ICrudBaseRepository<UserProfileEntity, int> crudBaseRepository)
            : base(crudBaseRepository)
        {
        }

        public async Task<UserProfileEntity> GetUserProfileByUserId(string userId)
        {
            var userProfile = await base.Query()
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            return userProfile;
        }
    }
}
