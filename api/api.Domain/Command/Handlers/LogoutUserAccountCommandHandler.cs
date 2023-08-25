using api.Domain.Interfaces;
using api.Infrastructure.Data.Repositories.Interfaces;
using MediatR;

namespace api.Domain.Command.Handlers
{
    public class LogoutUserAccountCommandHandler :
        IRequestHandler<LogoutUserAccountCommand>
    {
        private readonly IApiUserManagerServices userManager;
        private readonly IUserRefreshTokenRepository userRefreshTokenRepository;
        private readonly IUsersActionsLogService usersActionsLogService;

        public LogoutUserAccountCommandHandler(
            IApiUserManagerServices userManager,
            IUserRefreshTokenRepository userRefreshTokenRepository,
            IUsersActionsLogService usersActionsLogService)
        {
            this.userManager = userManager;
            this.userRefreshTokenRepository = userRefreshTokenRepository;
            this.usersActionsLogService = usersActionsLogService;
        }

        public async Task Handle(
            LogoutUserAccountCommand request, 
            CancellationToken cancellationToken)
        {
            var user = await this.userManager.GetCurrentUser();
            this.usersActionsLogService.Log("Разлогинился", user.Email);
            await this.userRefreshTokenRepository.DropUserRefreshToken(
                user.Id, request.userTokens.RefreshToken);
        }
    }
}
