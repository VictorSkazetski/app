using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace api.Infrastructure.Data.DatabaseContext
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApiContext>
    {
        public ApiContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();
            var builder = new DbContextOptionsBuilder<ApiContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new ApiContext(builder.Options);
        }
    }
}
