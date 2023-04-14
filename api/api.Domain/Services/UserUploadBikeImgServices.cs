using api.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace api.Domain.Services
{
    public class UserUploadBikeImgServices : IUserUploadImg
    {
        private readonly IWebHostEnvironment environment;

        private string UserUploadImgPath =>
            $"{Path.Combine(this.environment.WebRootPath, @"images\upload\bikes\")}";

        public UserUploadBikeImgServices(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<string> StoreUploadedImg(IFormFile img, string userId)
        {
            string uploadImgPath =
                $"{UserUploadImgPath}{Guid.NewGuid()}--{userId}" +
                $"{Path.GetExtension(img.FileName)}";
            using (FileStream stream = new FileStream(uploadImgPath, FileMode.Create))
                await img.CopyToAsync(stream);

            return Path.Combine(
                @"https://localhost:7154/", @$"images/upload/bikes/{Path.GetFileName(uploadImgPath)}");
        }

        public async Task DropImg(string userId, string imgPath)
        {
            var imgFileName = imgPath.Split('/')
                .Last();
            File.Delete($"{UserUploadImgPath}{imgFileName}");
        }
    }
}
