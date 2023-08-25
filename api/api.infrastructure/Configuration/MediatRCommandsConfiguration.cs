using api.Data;
using api.Domain.Command;
using api.Domain.Command.Handlers;
using api.Domain.Interfaces;
using api.Domain.Model.Dto;
using api.Domain.Services;
using api.Infrastructure.Data.Repositories.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace api.Infrastructure.Configuration
{
    public static class MediatRCommandsConfiguration
    {
        public static IServiceCollection AddMediatRCommands(
            this IServiceCollection services)
        {
            services.AddScoped<
                IRequestHandler<CreateUserAccountCommand, AccountDto>>(
                    sp => new CreateUserAccountCommandHandler(
                        sp.GetRequiredService<IEmailService>(),
                        sp.GetRequiredService<IApiUserManagerServices>(),
                        sp.GetRequiredService<IMapper>(),
                        sp.GetRequiredService<IUsersActionsLogService>()));
            services.AddScoped<
                IRequestHandler<VerifyUserAccountEmailCommand, UserEmailVerifyDto>>(
                    sp => new VerifyUserAccountEmailCommandHandler(
                        sp.GetRequiredService<IApiUserManagerServices>(),
                         sp.GetRequiredService<IMapper>()));
            services.AddScoped<
                IRequestHandler<LoginUserAccountCommand, UserTokensDto>>(
                    sp => new LoginUserAccountCommandHandler(
                        sp.GetRequiredService<IApiUserManagerServices>(),
                         sp.GetRequiredService<IMapper>(),
                         sp.GetRequiredService<IJWTManager>(),
                         sp.GetRequiredService<IUserRefreshTokenRepository>(),
                         sp.GetRequiredService<IUsersActionsLogService>()));
            services.AddScoped<
                IRequestHandler<RefreshUserTokensAccountCommand, UserTokensDto>>(
                    sp => new RefreshUserTokensAccountCommandHandler(
                        sp.GetRequiredService<IApiUserManagerServices>(),
                         sp.GetRequiredService<IMapper>(),
                         sp.GetRequiredService<IJWTManager>(),
                         sp.GetRequiredService<IUserRefreshTokenRepository>()));
            services.AddScoped<
                IRequestHandler<LogoutUserAccountCommand>>(
                    sp => new LogoutUserAccountCommandHandler(
                        sp.GetRequiredService<IApiUserManagerServices>(),
                        sp.GetRequiredService<IUserRefreshTokenRepository>(),
                        sp.GetRequiredService<IUsersActionsLogService>()));
            services.AddScoped<
                IRequestHandler<UserProfileCommnand, UserProfileDto>>(
                    sp => new UserProfileCommnandHandler(
                        sp.GetRequiredService<IApiUserManagerServices>(),
                        sp.GetRequiredService<IMapper>(),
                        sp.GetRequiredService<IUserProfileRepository>(),
                        sp.GetRequiredService<Func<Type, IUserUploadImg>>(),
                        sp.GetRequiredService<IUsersActionsLogService>()));
            services.AddScoped<
                IRequestHandler<SellBikeCommand, SellBikeDto>>(
                    sp => new SellBikeCommandHandler(
                        sp.GetRequiredService<IApiUserManagerServices>(),
                        sp.GetRequiredService<IMapper>(),
                        sp.GetRequiredService<ICrudBaseRepository<BikeAdEntity, int>>(),
                        sp.GetRequiredService<IUserProfileRepository>(),
                        sp.GetRequiredService<Func<Type, IUserUploadImg>>(),
                        sp.GetRequiredService<IUsersActionsLogService>()));
            services.AddScoped<
                IRequestHandler<DeleteBikeCommand, bool>>(
                    sp => new DeleteBikeCommandHandler(
                        sp.GetRequiredService<IApiUserManagerServices>(),
                        sp.GetRequiredService<IUserProfileRepository>(),
                        sp.GetRequiredService<ICrudBaseRepository<BikeAdEntity, int>>(),
                        sp.GetRequiredService<Func<Type, IUserUploadImg>>(),
                        sp.GetRequiredService<IUsersActionsLogService>()));

            return services;
        }
    }
}
