using Microsoft.AspNetCore.Http;

namespace api.Domain.Command
{
    public class SellBikeData
    {
        public string Brand { get; set; }

        public string Type { get; set; }

        public int FrameSize { get; set; }

        public string Gender { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public IFormFile UploadImg { get; set; }
    }
}
