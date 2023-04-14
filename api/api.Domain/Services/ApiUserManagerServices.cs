using api.Data;
using api.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace api.Domain.Services
{
    public class ApiUserManagerServices : IApiUserManagerServices
    {
        private readonly UserManager<UserEntity> userManager;
        private readonly IHttpContextAccessor http;

        public ApiUserManagerServices(
            UserManager<UserEntity> userManager, 
            IHttpContextAccessor http)
        {
            this.userManager = userManager;
            this.http = http;
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

        public Task<bool> CheckUserPasswordAsync(UserEntity user, string userPassword)
        {
            return this.userManager.CheckPasswordAsync(user, userPassword);
        }

        public Task<IdentityResult> CreateUserAsync(UserEntity user, string userPassword)
        {
            return this.userManager.CreateAsync(user, userPassword);
        }

        public Task<bool> IsUserEmailConfirmedAsync(UserEntity user)
        {
            return this.userManager.IsEmailConfirmedAsync(user);
        }

        public string GetUserAccessTokenFromHttpContext()
        {
            return this.http.HttpContext.Items["token"]
                .ToString();
        }

        private string GetUserEmailfromHttpContext()
        {
            return this.http.HttpContext.User.Identities.First()
                .Claims
                .First()
                .Value;
        }

        public async Task<UserEntity> GetCurrentUser()
        {
            var userEmail = GetUserEmailfromHttpContext();

            return await FindByEmailAsync(userEmail);
        }
    }
}
