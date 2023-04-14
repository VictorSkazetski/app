using MediatR;
using Microsoft.Extensions.DependencyInjection;
using api.Domain.Queries;
using api.Domain.Model.Dto;
using api.Domain.Queries.Handlers;
using api.Domain.Interfaces;
using MapsterMapper;
using api.Infrastructure.Data.Repositories.Interfaces;
using api.Data;

namespace api.Infrastructure.Configuration
{
    public static class MediatRQueriesConfiguration
    {
        public static IServiceCollection AddMediatRQueries(
            this IServiceCollection services)
        {
            services.AddScoped<
                IRequestHandler<UserProfileQuery, UserProfileDto>>(
                    sp => new UserProfileQueryHandler(
                        sp.GetRequiredService<IApiUserManagerServices>(),
                        sp.GetRequiredService<IMapper>(),
                        sp.GetRequiredService<IUserProfileRepository>()));
            services.AddScoped<
                IRequestHandler<UserBikesQuery, List<SellBikeDto>>>(
                    sp => new UserBikesQueryHandler(
                        sp.GetRequiredService<IApiUserManagerServices>(),
                        sp.GetRequiredService<IMapper>(),
                        sp.GetRequiredService<IUserProfileRepository>(),
                        sp.GetRequiredService<ICrudBaseRepository<BikeAdEntity, int>>()));
            services.AddScoped<
                IRequestHandler<AllBikeAdsQuery, List<BikeAdDto>>>(
                    sp => new AllBikeAdsQueryHandler(
                        sp.GetRequiredService<ICrudBaseRepository<BikeAdEntity, int>>(),
                        sp.GetRequiredService<IMapper>()));
            services.AddScoped<
                IRequestHandler<BikeAdByIdQuery, BikeAdDto>>(
                    sp => new BikeAdByIdQueryHandler(
                        sp.GetRequiredService<ICrudBaseRepository<BikeAdEntity, int>>(),
                        sp.GetRequiredService<IMapper>(),
                        sp.GetRequiredService<IUserProfileRepository>()));

            return services;
        }
    }
}
