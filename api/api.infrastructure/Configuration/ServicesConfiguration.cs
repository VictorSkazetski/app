using api.Data;
using api.Domain.Interfaces;
using api.Domain.Services;
using api.Error;
using api.Infrastructure.Data.Repositories;
using api.Infrastructure.Data.Repositories.Interfaces;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace api.Infrastructure.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServices(
            this IServiceCollection services)
        {
            services.AddSingleton((IServiceProvider sp) => CreateMapperConfig());
            services.AddScoped((Func<IServiceProvider, IMapper>)((IServiceProvider sp) =>
                    new ServiceMapper(sp, sp.GetRequiredService<TypeAdapterConfig>())));
            services.AddScoped<ICrudBaseRepository<UserEntity>, CrudBaseRepository<UserEntity>>();
            services.AddScoped<IBaseRepository<UserEntity>>(sp =>
                new BaseRepository<UserEntity>(
                    sp.GetRequiredService<ICrudBaseRepository<UserEntity>>()));
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IApiUserManagerServices, ApiUserManagerServices>();
            services.AddScoped<ExceptionMiddleware>();

            return services;
        }

        static TypeAdapterConfig CreateMapperConfig()
        {
            TypeAdapterConfig typeAdapterConfig = new TypeAdapterConfig();
            typeAdapterConfig.ConfigureMapping();

            return typeAdapterConfig;
        }
    }
}
