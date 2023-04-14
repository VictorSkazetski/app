using api.Data.Interfaces;

namespace api.Data
{
    public class BikeAdEntity : IEntityWithTypedId<int>
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Type { get; set; }

        public int FrameSize { get; set; }

        public string Gender { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public string UploadImgPath { get; set; }

        public int UserProfileId { get; set; }

        public UserProfileEntity UserProfile{ get; set; }
    }
}
