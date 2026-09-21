using CondoHub.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CondoHub.DataBase.MySql.EntityFramework
{
    public class CondoHubContext : DbContext
    {
        #region Config

        public CondoHubContext(DbContextOptions<CondoHubContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("Configuration.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("CondoHubConnectionString");
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

        #endregion

        public DbSet<User> User { get; set; }
        public DbSet<UserData> UserData { get; set; }
        // Add DbSets here
    }
}