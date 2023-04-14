using api.Data;
using api.Domain.Interfaces;
using api.Domain.Services;
using api.Infrastructure.Data.Repositories.Interfaces;
using MediatR;

namespace api.Domain.Command.Handlers
{
    public class DeleteBikeCommandHandler :
        IRequestHandler<DeleteBikeCommand, bool>
    {
        private readonly IApiUserManagerServices userManager;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly ICrudBaseRepository<BikeAdEntity, int> bikeAdRepository;
        private readonly Func<Type, IUserUploadImg> userUloadImg;

        public DeleteBikeCommandHandler(
            IApiUserManagerServices userManager,
            IUserProfileRepository userProfileRepository,
            ICrudBaseRepository<BikeAdEntity, int> bikeAdRepository,
            Func<Type, IUserUploadImg> userUloadImg)
        {
            this.userManager = userManager;
            this.userProfileRepository = userProfileRepository;
            this.bikeAdRepository = bikeAdRepository;
            this.userUloadImg = userUloadImg;
        }

        public async Task<bool> Handle(
            DeleteBikeCommand request,
            CancellationToken cancellationToken)
        {
            var user = await this.userManager.GetCurrentUser();
            var userProfile = await this.userProfileRepository.GetUserProfileByUserId(user.Id);
            var bikes = await bikeAdRepository.GetAllAsync();
            var userBike = bikes.Where(x => x.Id == request.BikeAdId)
                .FirstOrDefault();
            if (userBike != default)
            {
                await this.bikeAdRepository.DeleteAsync(userBike);
                await this.userUloadImg(typeof(UserUploadBikeImgServices)).DropImg(
                        "",
                        userBike.UploadImgPath);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
