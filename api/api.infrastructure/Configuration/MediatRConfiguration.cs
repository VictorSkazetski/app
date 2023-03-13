using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace api.Infrastructure.Configuration
{
    public static class MediatRConfiguration
    {
        public static IServiceCollection ConfigurationMediatR(
            this IServiceCollection services)
        {
            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddMediatRCommands();

            return services;
        }
    }
}
