using api.Domain.Model.Dto;
using MediatR;

namespace api.Domain.Command
{
    public class UserProfileCommnand : IRequest<UserProfileDto>
    {
        public UserProfileData UserProfile { get; set; }

        public UserProfileCommnand(UserProfileData userProfile)
        {
            this.UserProfile = userProfile;
        }
    }
}
