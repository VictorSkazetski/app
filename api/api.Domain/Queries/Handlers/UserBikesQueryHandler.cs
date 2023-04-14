using api.Data;
using api.Domain.Interfaces;
using api.Domain.Model.Dto;
using api.Infrastructure.Data.Repositories.Interfaces;
using MapsterMapper;
using MediatR;

namespace api.Domain.Queries.Handlers
{
    public class UserBikesQueryHandler :
        IRequestHandler<UserBikesQuery, List<SellBikeDto>>
    {
        private readonly IApiUserManagerServices userManager;
        private readonly IMapper mapper;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly ICrudBaseRepository<BikeAdEntity, int> bikeAdRepository;

        public UserBikesQueryHandler(
            IApiUserManagerServices userManager,
            IMapper mapper,
            IUserProfileRepository userProfileRepository,
            ICrudBaseRepository<BikeAdEntity, int> bikeAdRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.userProfileRepository = userProfileRepository;
            this.bikeAdRepository = bikeAdRepository;
        }

        public async Task<List<SellBikeDto>> Handle(
            UserBikesQuery request, 
            CancellationToken cancellationToken)
        {
            var user = await this.userManager.GetCurrentUser();
            var userProfile = await this.userProfileRepository.GetUserProfileByUserId(user.Id);
            var bikes = await bikeAdRepository.GetAllAsync();
            var userBikes = bikes.Where(x => x.UserProfileId == user.UserProfile.Id)
                .Select(x => this.mapper.Map<SellBikeDto>(x))
                .ToList();
            
            return userBikes;
        }
    }
}
