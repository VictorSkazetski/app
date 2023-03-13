using api.Data;
using api.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace api.Domain.Services
{
    public class ApiUserManagerServices : IApiUserManagerServices
    {
        private readonly UserManager<UserEntity> userManager;

        public ApiUserManagerServices(UserManager<UserEntity> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(UserEntity user, string token)
        {
            return await this.userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<UserEntity> FindByEmailAsync(string userEmail)
        {
            return await this.userManager.FindByEmailAsync(userEmail);
        }

        public Task<string> GenerateEmailConfirmationTokenAsync(UserEntity userEntity)
        {
            return this.userManager.GenerateEmailConfirmationTokenAsync(userEntity);
        }
    }
}
