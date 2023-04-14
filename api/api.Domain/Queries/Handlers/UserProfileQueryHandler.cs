using api.Data;
using api.Domain.Interfaces;
using api.Domain.Model.Dto;
using api.Infrastructure.Data.Repositories.Interfaces;
using MapsterMapper;
using MediatR;

namespace api.Domain.Queries.Handlers
{
    public class UserProfileQueryHandler :
        IRequestHandler<UserProfileQuery, UserProfileDto>
    {
        private readonly IApiUserManagerServices userManager;
        private readonly IMapper mapper;
        private readonly IUserProfileRepository userProfileRepository;

        public UserProfileQueryHandler(
            IApiUserManagerServices userManager,
            IMapper mapper,
            IUserProfileRepository userProfileRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.userProfileRepository = userProfileRepository;
        }

        public async Task<UserProfileDto> Handle(
            UserProfileQuery request, 
            CancellationToken cancellationToken)
        {
            var user = await this.userManager.GetCurrentUser();
            var userProfile = await this.userProfileRepository.GetUserProfileByUserId(user.Id);

            return this.mapper.Map<UserProfileDto>(userProfile == default 
                ? new UserProfileEntity() : userProfile);
        }
    }
}
