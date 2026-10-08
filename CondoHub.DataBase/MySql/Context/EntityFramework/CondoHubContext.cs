using CondoHub.Domain.Entity;
using CondoHub.Domain.Entity.Ticket;
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
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<TicketType> TicketType { get; set; }
        public DbSet<TicketCategory> TicketCategory { get; set; }
        public DbSet<TicketComment> TicketComment { get; set; }
        public DbSet<TicketAttachment> TicketAttachment { get; set; }
        // Add DbSets here
    }
}