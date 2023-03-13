using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace api.Infrastructure.Data
{
    public class DbContextOptionsFactory
    {
        public static DbContextOptions GetDbContextOptions()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();
            var builder = new DbContextOptionsBuilder<ApiContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return builder.Options;
        }
    }
}
