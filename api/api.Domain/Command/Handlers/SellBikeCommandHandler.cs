using api.Data;
using api.Domain.Interfaces;
using api.Domain.Model.Dto;
using api.Domain.Services;
using api.Error.Exceptions;
using api.Infrastructure.Data.Repositories.Interfaces;
using MapsterMapper;
using MediatR;

namespace api.Domain.Command.Handlers
{
    public class SellBikeCommandHandler :
        IRequestHandler<SellBikeCommand, SellBikeDto>
    {
        private readonly IApiUserManagerServices userManager;
        private readonly IMapper mapper;
        private readonly ICrudBaseRepository<BikeAdEntity, int> bikeAdRepository;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly Func<Type, IUserUploadImg> uploadBikeImg;


        public SellBikeCommandHandler(
            IApiUserManagerServices userManager,
            IMapper mapper,
            ICrudBaseRepository<BikeAdEntity, int> bikeAdRepository,
            IUserProfileRepository userProfileRepository,
            Func<Type, IUserUploadImg> uploadBikeImg)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.bikeAdRepository = bikeAdRepository;
            this.userProfileRepository = userProfileRepository;
            this.uploadBikeImg = uploadBikeImg;
        }

        public async Task<SellBikeDto> Handle(
            SellBikeCommand request, 
            CancellationToken cancellationToken)
        {
            var user = await this.userManager.GetCurrentUser();
            var imgPath = await this.uploadBikeImg(typeof(UserUploadBikeImgServices)).StoreUploadedImg(
                request.BikeData.UploadImg, user.Id);
            var userProfile = await this.userProfileRepository.GetUserProfileByUserId(user.Id);
            if (userProfile == default)
            {
                throw new BadRequestException("Заполните информацию о себе в профиле");
            }

            var bikeAd = await this.bikeAdRepository.CreateAsync(new BikeAdEntity
            {
                Brand = request.BikeData.Brand,
                Type = request.BikeData.Type,
                FrameSize = request.BikeData.FrameSize,
                Gender = request.BikeData.Gender,
                Description = request.BikeData.Description,
                Price = request.BikeData.Price,
                UploadImgPath = imgPath,
                UserProfileId = userProfile.Id,
            });

            return this.mapper.Map<SellBikeDto>(bikeAd);
        }
    }
}
