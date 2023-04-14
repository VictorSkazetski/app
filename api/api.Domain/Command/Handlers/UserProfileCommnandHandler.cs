using api.Data;
using api.Domain.Interfaces;
using api.Domain.Model.Dto;
using api.Domain.Services;
using api.Infrastructure.Data.Repositories.Interfaces;
using MapsterMapper;
using MediatR;

namespace api.Domain.Command.Handlers
{
    public class UserProfileCommnandHandler :
        IRequestHandler<UserProfileCommnand, UserProfileDto>
    {
        private readonly IApiUserManagerServices userManager;
        private readonly IMapper mapper;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly Func<Type, IUserUploadImg> userUloadImg;

        public UserProfileCommnandHandler(
            IApiUserManagerServices userManager,
            IMapper mapper,
            IUserProfileRepository userProfileRepository,
            Func<Type, IUserUploadImg> userUloadImg)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.userUloadImg = userUloadImg;
            this.userProfileRepository = userProfileRepository;
        }

        public async Task<UserProfileDto> Handle(
            UserProfileCommnand request,
            CancellationToken cancellationToken)
        {
            var user = await this.userManager.GetCurrentUser();
            var userProfile = await this.userProfileRepository.GetUserProfileByUserId(user.Id);
            UserProfileEntity userProfileCreated;
            string? userImgUploadPath = null;
            if (request.UserProfile.UploadImg != default 
                && request.UserProfile.IsUploadImg == true)
            {
                userImgUploadPath = 
                    await this.userUloadImg(typeof(UserUploadImgServices)).StoreUploadedImg(
                        request.UserProfile.UploadImg,
                        user.Id);
                if (userProfile != default)
                {
                    userProfile.UserImgPath = userImgUploadPath;
                }
            }

            if (userProfile != default &&
                request.UserProfile.IsUploadImg == null)
            {
                await this.userUloadImg(typeof(UserUploadImgServices)).DropImg(user.Id);
                userProfile.UserImgPath = null;
            }

            if (userProfile == default)
            {
                userProfileCreated = await this.userProfileRepository.CreateAsync(
                    new UserProfileEntity
                    {
                        UserId = user.Id,
                        UserImgPath = userImgUploadPath,
                        PickImgNum = request.UserProfile.PickImg,
                        BirthDay = request.UserProfile.BirthDay,
                        Phone = request.UserProfile.Phone,
                    });
            }
            else
            {
                userProfile.PickImgNum = request.UserProfile.PickImg;
                userProfile.BirthDay = request.UserProfile.BirthDay;
                userProfile.Phone = request.UserProfile.Phone;
                userProfileCreated = await this.userProfileRepository.UpdateAsync(
                    userProfile);
            }

            return this.mapper.Map<UserProfileDto>(userProfileCreated);
        }
    }
}
