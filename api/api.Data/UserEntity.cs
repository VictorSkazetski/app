using Microsoft.AspNetCore.Identity;

namespace api.Data
{
    public class UserEntity : IdentityUser
    {
        public UserRefreshTokensEntity RefreshTokens { get; set; }

        public UserProfileEntity UserProfile { get; set; }
    }
}
