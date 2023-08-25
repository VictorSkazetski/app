using api.Data;
using api.Infrastructure.Data.Repositories.Interfaces;

namespace api.Infrastructure.Data.Repositories;

public class UserActionsHistoryRepository : BaseRepository<UserActionsHistoryEntity, int>
{
    public UserActionsHistoryRepository(
            ICrudBaseRepository<UserActionsHistoryEntity, int> crudBaseRepository)
            : base(crudBaseRepository)
    {
    }
}
