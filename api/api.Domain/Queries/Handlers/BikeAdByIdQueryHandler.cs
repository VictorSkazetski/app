using api.Data;
using api.Domain.Model.Dto;
using api.Infrastructure.Data.Repositories.Interfaces;
using MapsterMapper;
using MediatR;

namespace api.Domain.Queries.Handlers
{
    public class BikeAdByIdQueryHandler :
        IRequestHandler<BikeAdByIdQuery, BikeAdDto>
    {
        private readonly ICrudBaseRepository<BikeAdEntity, int> bikeAdRepository;
        private readonly IMapper mapper;
        private readonly IUserProfileRepository userProfileRepository;

        public BikeAdByIdQueryHandler(
            ICrudBaseRepository<BikeAdEntity, int> bikeAdRepository,
            IMapper mapper,
            IUserProfileRepository userProfileRepository)
        {
            this.bikeAdRepository = bikeAdRepository;
            this.mapper = mapper;
            this.userProfileRepository = userProfileRepository;
        }

        public async Task<BikeAdDto> Handle(
            BikeAdByIdQuery request, 
            CancellationToken cancellationToken)
        {
            var bike = await this.bikeAdRepository.GetAsync(request.BikeAdId);
            var userProfile = await this.userProfileRepository.GetAsync(bike.UserProfileId);

            return this.mapper.Map<BikeAdDto>((bike, userProfile));
        }
    }
}
