using Microsoft.AspNetCore.Http;

namespace api.Domain.Command
{
    public class UserProfileData
    {
        public int? PickImg { get; set; }

        public IFormFile? UploadImg { get; set; }

        public DateTime? BirthDay { get; set; }

        public string? Phone { get; set; }

        public bool? IsUploadImg { get; set; }
    }
}
