using api.Data;
using api.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace api.Infrastructure.Configuration
{
    public static class IdentityServicesConfiguration
    {
        public static IServiceCollection AddIdentityServices(
            this IServiceCollection services)
        {
            services.AddIdentityCore<UserEntity>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            })
            .AddEntityFrameworkStores<ApiContext>()
            .AddDefaultTokenProviders();
            services.AddDataProtection();

            return services;
        }
    }
}
