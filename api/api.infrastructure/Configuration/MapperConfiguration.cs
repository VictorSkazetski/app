using api.Data;
using api.Domain.Command;
using api.Domain.Model.Dto;
using Mapster;

namespace api.Infrastructure.Configuration
{
    public static class MapperConfiguration
    {
        public static void ConfigureMapping(this TypeAdapterConfig config)
        {
            config
                .NewConfig<UserEntity, AccountDto>()
                .Map(x => x.UserEmail, x => x.NormalizedEmail)
                .Map(x => x.EmailHost, x => "https://" +
                    x.NormalizedEmail.Split('@', StringSplitOptions.None)[1]);
            config
                .NewConfig<UserEntity, UserEmailVerifyDto>()
                .Map(x => x.UserEmail, x => x.NormalizedEmail);
            config
                .NewConfig<UserTokensData, UserTokensDto>()
                .Map(x => x.AccessToken, x => x.AccessToken)
                .Map(x => x.RefreshToken, x => x.RefreshToken);
            config
                .NewConfig<UserProfileEntity, UserProfileDto>()
                .Map(x => x.Img, x => x.UserImgPath)
                .Map(x => x.PickImg, x => x.PickImgNum)
                .Map(x => x.Birthday,
                    x => x.BirthDay != null
                        ? x.BirthDay.Value.ToString("MM/dd/yyyy")
                            .Replace('.', '/') : "")
                .Map(x => x.Phone, x => x.Phone);
            config
                .NewConfig<BikeAdEntity, SellBikeDto>()
                .Map(x => x.Id, x => x.Id)
                .Map(x => x.Brand, x => x.Brand)
                .Map(x => x.Type, x => x.Type)
                .Map(x => x.FrameSize, x => x.FrameSize)
                .Map(x => x.Gender, x => x.Gender)
                .Map(x => x.Description, x => x.Description)
                .Map(x => x.Price, x => x.Price)
                .Map(x => x.UploadImgPath, x => x.UploadImgPath);
            config
                .NewConfig<(BikeAdEntity, UserProfileEntity), BikeAdDto>()
                .Map(x => x.Id, x => x.Item1.Id)
                .Map(x => x.Brand, x => x.Item1.Brand)
                .Map(x => x.Type, x => x.Item1.Type)
                .Map(x => x.FrameSize, x => x.Item1.FrameSize)
                .Map(x => x.Gender, x => x.Item1.Gender)
                .Map(x => x.Description, x => x.Item1.Description)
                .Map(x => x.Price, x => x.Item1.Price)
                .Map(x => x.UploadImgPath, x => x.Item1.UploadImgPath)
                .Map(x => x.Phone, x => x.Item2.Phone)
                .Map(x => x.UserAvatarImg, x => x.Item2.UserImgPath)
                .Map(x => x.UserAvatarPickImg, x => x.Item2.PickImgNum);
        }
    }
}
