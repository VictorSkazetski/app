using api.Data;

namespace api.Infrastructure.Data.Repositories.Interfaces
{
    public interface IUserProfileRepository :
        IBaseRepository<UserProfileEntity, int>
    {
        Task<UserProfileEntity> GetUserProfileByUserId(string userId);
    }
}
