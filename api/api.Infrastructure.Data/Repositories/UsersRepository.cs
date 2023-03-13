using api.Data;
using api.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Infrastructure.Data.Repositories
{
    public class UsersRepository :
        BaseRepository<UserEntity>,
        IUsersRepository
    {
        public UsersRepository(ICrudBaseRepository<UserEntity> crudBaseRepository)
            : base(crudBaseRepository)
        {
        }

        public async Task<UserEntity> GetUserByEmail(string email)
        {
            return await base.Query()
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync();
        }
    }
}
