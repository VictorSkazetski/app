using api.Data;
using api.Domain.Interfaces;
using api.Domain.Model.Dto;
using api.Error.Exceptions;
using api.Infrastructure.Data.Repositories.Interfaces;
using MapsterMapper;
using MediatR;

namespace api.Domain.Command.Handlers
{
    public class RefreshUserTokensAccountCommandHandler :
        IRequestHandler<RefreshUserTokensAccountCommand, UserTokensDto>
    {
        private readonly IApiUserManagerServices userManager;
        private readonly IMapper mapper;
        private readonly IJWTManager jWTManager;
        private readonly IUserRefreshTokenRepository userRefreshTokenRepository;

        public RefreshUserTokensAccountCommandHandler(
            IApiUserManagerServices userManager,
            IMapper mapper,
            IJWTManager jWTManager,
            IUserRefreshTokenRepository userRefreshTokenRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.jWTManager = jWTManager;
            this.userRefreshTokenRepository = userRefreshTokenRepository;
        }

        public async Task<UserTokensDto> Handle(
            RefreshUserTokensAccountCommand request,
            CancellationToken cancellationToken)
        {
            var principal = this.jWTManager.GetPrincipalFromExpiredToken(
                    this.userManager.GetUserAccessTokenFromHttpContext());
            var userEmail = principal.Claims.First()
                    .Value;
            var user = await this.userManager.FindByEmailAsync(userEmail);
            var savedToken = await this.userRefreshTokenRepository.GetUserRefreshTokenByUserId(
                    user.Id);
            var newJwtToken = this.jWTManager.GenerateRefreshToken(userEmail);
            if (savedToken.RefreshToken != request.userTokens.RefreshToken ||
                savedToken.ExpiryTime <= DateTime.Now ||
                newJwtToken == null)
            {
                await this.userRefreshTokenRepository.DropUserRefreshToken(
                    user.Id, request.userTokens.RefreshToken);
                throw new UnauthorizedException("Неверная попытка");
            }

            await this.userRefreshTokenRepository.DropUserRefreshToken(
                user.Id, request.userTokens.RefreshToken);
            await this.userRefreshTokenRepository.CreateAsync(
                new UserRefreshTokensEntity
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    RefreshToken = newJwtToken.RefreshToken,
                    ExpiryTime = DateTime.Now.AddMinutes(3),
                });

            return this.mapper.Map<UserTokensDto>(newJwtToken);
        }
    }
}
