using api.Domain.Command;
using api.Domain.Command.Handlers;
using api.Domain.Interfaces;
using api.Domain.Model.Dto;
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
                        sp.GetRequiredService<IUsersRepository>(),
                        sp.GetRequiredService<IEmailService>(),
                        sp.GetRequiredService<IApiUserManagerServices>(),
                        sp.GetRequiredService<IMapper>()));
            services.AddScoped<
                IRequestHandler<VerifyUserAccountEmailCommand, UserEmailVerifyDto>>(
                    sp => new VerifyUserAccountEmailCommandHandler(
                        sp.GetRequiredService<IApiUserManagerServices>(),
                         sp.GetRequiredService<IMapper>()));

            return services;
        }
    }
}
