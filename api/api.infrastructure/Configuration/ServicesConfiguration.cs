using api.Domain.Interfaces;
using api.Domain.Services;
using api.Error;
using api.Infrastructure.Data.Repositories;
using api.Infrastructure.Data.Repositories.Interfaces;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Hosting;
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
            services.AddScoped(typeof(ICrudBaseRepository<,>), typeof(CrudBaseRepository<,>));
            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<Func<Type, IUserUploadImg>>(sp =>
            {
                return (Type t) =>
                {
                    return t.Name switch
                    {
                        nameof(UserUploadBikeImgServices) => 
                            new UserUploadBikeImgServices(sp.GetService<IWebHostEnvironment>()),
                        nameof(UserUploadImgServices) => 
                            new UserUploadImgServices(sp.GetService<IWebHostEnvironment>()),
                        _ => throw new NotImplementedException()
                    };
                };
            });
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IApiUserManagerServices, ApiUserManagerServices>();
            services.AddScoped<IJWTManager, JWTManagerServices>();
            services.AddScoped<ExceptionMiddleware>();
            services.AddScoped<UserActionsHistoryRepository>();
            services.AddScoped<IUsersActionsLogService, UsersActionsLogService>();

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
