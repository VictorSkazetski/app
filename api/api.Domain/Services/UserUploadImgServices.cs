using api.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace api.Domain.Services
{
    public class UserUploadImgServices : IUserUploadImg
    {
        private readonly IWebHostEnvironment environment;

        private string UserUploadImgPath =>
            $"{Path.Combine(this.environment.WebRootPath, @"images\upload\avatar\")}";

        public UserUploadImgServices(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<string> StoreUploadedImg(IFormFile img, string userId)
        {
            await DropImg(userId);
            string uploadImgPath =
                $"{UserUploadImgPath}{userId}" +
                $"{Path.GetExtension(img.FileName)}";
            using (FileStream stream = new FileStream(uploadImgPath, FileMode.Create))
            await img.CopyToAsync(stream);

            return Path.Combine(
                @"https://localhost:7154/", @$"images/upload/avatar/{Path.GetFileName(uploadImgPath)}");
        }

        public async Task DropImg(string userId, string imgPath = null)
        {
            if (Directory.GetFiles($@"{UserUploadImgPath}", $"{userId}.*").Length != 0)
            {
                if (File.Exists(Directory.GetFiles($@"{UserUploadImgPath}", $"{userId}.*")[0]))
                {
                    await Task.Run(() => File.Delete(
                        Directory.GetFiles($@"{UserUploadImgPath}", $"{userId}.*")[0]));
                }
            }
            
        }
    }
}
