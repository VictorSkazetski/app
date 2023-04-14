using api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Infrastructure.Data.DatabaseContext.Configurations
{
    public class UserProfileEntityConfiguration
        : IEntityTypeConfiguration<UserProfileEntity>
    {
        public void Configure(EntityTypeBuilder<UserProfileEntity> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.UserId)
                .IsRequired();
            builder
                .HasOne(x => x.User)
                .WithOne(u => u.UserProfile)
                .HasForeignKey<UserProfileEntity>(u => u.UserId);
        }
    }
}
