using api.Data.Interfaces;

namespace api.Data
{
    public class UserRefreshTokensEntity :
        IEntityWithTypedId<int>
    {
		public int Id { get; set; }

        public string UserId { get; set; }

        public string UserEmail { get; set; }

		public string RefreshToken { get; set; }

		public bool IsActive { get; set; } = true;

        public DateTime ExpiryTime { get; set; }

        public UserEntity User { get; set; }
    }
}
