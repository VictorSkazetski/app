using api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Infrastructure.Data.DatabaseContext.Configurations
{
    public class BikeAdEntityConfiguration
        : IEntityTypeConfiguration<BikeAdEntity>
    {
        public void Configure(EntityTypeBuilder<BikeAdEntity> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .HasOne(x => x.UserProfile)
                .WithMany(x => x.BikesAd)
                .HasForeignKey(x => x.UserProfileId);
        } 
    }
}
