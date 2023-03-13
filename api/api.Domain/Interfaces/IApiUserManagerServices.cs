using api.Data;
using Microsoft.AspNetCore.Identity;

namespace api.Domain.Interfaces
{
    public interface IApiUserManagerServices
    {
        Task<UserEntity> FindByEmailAsync(string userEmail);

        Task<IdentityResult> ConfirmEmailAsync(UserEntity user, string token);

        Task<string> GenerateEmailConfirmationTokenAsync(UserEntity userEntity);
    }
}
