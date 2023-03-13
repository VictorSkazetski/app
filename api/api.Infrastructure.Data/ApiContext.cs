using api.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Infrastructure.Data
{
    public class ApiContext :
        IdentityDbContext<UserEntity>

    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserRefreshTokensEntity> UserRefreshToken { get; set; }
    }
}
