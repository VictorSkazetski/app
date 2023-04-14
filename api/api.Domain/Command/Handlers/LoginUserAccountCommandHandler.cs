using api.Data;
using api.Domain.Interfaces;
using api.Domain.Model.Dto;
using api.Error.Exceptions;
using api.Infrastructure.Data.Repositories.Interfaces;
using MapsterMapper;
using MediatR;

namespace api.Domain.Command.Handlers
{
    public class LoginUserAccountCommandHandler :
        IRequestHandler<LoginUserAccountCommand, UserTokensDto>
    {
        private readonly IApiUserManagerServices userManager;
        private readonly IMapper mapper;
        private readonly IJWTManager jWTManager;
        private readonly IUserRefreshTokenRepository userRefreshTokenRepository;

        public LoginUserAccountCommandHandler(
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
            LoginUserAccountCommand request, 
            CancellationToken cancellationToken)
        {
            var user = await this.userManager.FindByEmailAsync(request.UserEmail);
            if (user == null)
            {
                throw new UnauthorizedException("Не верный email или пароль");
            }
            
            var isUserEmailConfirmed = await this.userManager.IsUserEmailConfirmedAsync(user);
            if (!isUserEmailConfirmed)
            {
                throw new UnauthorizedException("Почта не подтверждена");
            }
            
            var isUserPasswordValid =
                await this.userManager.CheckUserPasswordAsync(user, request.UserPassword);
            if (isUserPasswordValid)
            {
                var tokens = this.jWTManager.GenerateToken(user.Email);
                await this.userRefreshTokenRepository.CreateAsync(
                    new UserRefreshTokensEntity
                    {
                        UserId = user.Id,
                        UserEmail = user.Email,
                        RefreshToken = tokens.RefreshToken,
                        ExpiryTime = DateTime.Now.AddMinutes(3),
                    });

                return this.mapper.Map<UserTokensDto>(tokens);
            }
            else
            {
                throw new UnauthorizedException("Не верный email или пароль");
            }
        }
    }
}

