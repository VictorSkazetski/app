using api.Data.Interfaces;

namespace api.Data
{
    public class UserProfileEntity : IEntityWithTypedId<int>
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string? UserImgPath { get; set; }

        public int? PickImgNum { get; set; }

        public DateTime? BirthDay { get; set; }

        public string? Phone { get; set; }

        public UserEntity User { get; set; }

        public ICollection<BikeAdEntity> BikesAd { get; set; }
    }
}
