using api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Infrastructure.Data.DatabaseContext.Configurations
{
    public class UserRefreshTokensEntityConfiguration 
        : IEntityTypeConfiguration<UserRefreshTokensEntity>
    {
        public void Configure(EntityTypeBuilder<UserRefreshTokensEntity> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.UserEmail)
                .IsRequired();
            builder
                .Property(x => x.RefreshToken)
                .IsRequired();
            builder
                .Property(x => x.IsActive)
                .IsRequired();
            builder
                .HasOne(x => x.User)
                .WithOne(u => u.RefreshTokens)
                .HasForeignKey<UserRefreshTokensEntity>(u => u.UserId);
        }
    }
}
