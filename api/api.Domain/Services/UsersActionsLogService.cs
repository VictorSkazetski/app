using api.Domain.Interfaces;
using api.Infrastructure.Data.Repositories;

namespace api.Domain.Services
{
    public class UsersActionsLogService : IUsersActionsLogService
    {
        private readonly UserActionsHistoryRepository userActionsHistoryRepository;

        public UsersActionsLogService(
            UserActionsHistoryRepository userActionsHistoryRepository)
        {
            this.userActionsHistoryRepository = userActionsHistoryRepository;
        }
        public void Log(string action, string userEmail)
        {
            userActionsHistoryRepository.CreateAsync(
                    new Data.UserActionsHistoryEntity()
                    {
                        Action = action,
                        UserEmail = userEmail,
                        DateTime = DateTime.UtcNow,
                    }
                );
        }
    }
}
