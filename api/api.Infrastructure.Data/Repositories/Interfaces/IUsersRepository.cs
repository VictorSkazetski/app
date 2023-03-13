using api.Data;

namespace api.Infrastructure.Data.Repositories.Interfaces
{
    public interface IUsersRepository : IBaseRepository<UserEntity>
    {
        Task<UserEntity> GetUserByEmail(string email);
    }
}
