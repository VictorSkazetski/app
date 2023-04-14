using Microsoft.AspNetCore.Http;

namespace api.Domain.Interfaces
{
    public interface IUserUploadImg
    {
        Task<string> StoreUploadedImg(IFormFile img, string userId);

        Task DropImg(string userId, string imgPath = default);
    }
}
