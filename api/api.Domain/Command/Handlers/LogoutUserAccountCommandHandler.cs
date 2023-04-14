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

        public LogoutUserAccountCommandHandler(
            IApiUserManagerServices userManager,
            IUserRefreshTokenRepository userRefreshTokenRepository)
        {
            this.userManager = userManager;
            this.userRefreshTokenRepository = userRefreshTokenRepository;
        }

        public async Task Handle(
            LogoutUserAccountCommand request, 
            CancellationToken cancellationToken)
        {
            var user = await this.userManager.GetCurrentUser();
            await this.userRefreshTokenRepository.DropUserRefreshToken(
                user.Id, request.userTokens.RefreshToken);
        }
    }
}
