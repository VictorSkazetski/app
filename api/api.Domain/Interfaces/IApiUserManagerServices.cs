using api.Data;
using Microsoft.AspNetCore.Identity;

namespace api.Domain.Interfaces
{
    public interface IApiUserManagerServices
    {
        Task<UserEntity> FindByEmailAsync(string userEmail);

        Task<IdentityResult> ConfirmEmailAsync(UserEntity user, string token);

        Task<string> GenerateEmailConfirmationTokenAsync(UserEntity userEntity);

        Task<bool> CheckUserPasswordAsync(UserEntity user, string userPassword);

        Task<IdentityResult> CreateUserAsync(UserEntity user, string userPassword);

        Task<bool> IsUserEmailConfirmedAsync(UserEntity user);

        string GetUserAccessTokenFromHttpContext();

        Task<UserEntity> GetCurrentUser();

        Task<List<string>> GetUserRoles(string userEmail);
    }
}
