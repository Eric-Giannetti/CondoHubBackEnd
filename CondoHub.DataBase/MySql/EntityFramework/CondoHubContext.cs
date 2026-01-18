using Microsoft.EntityFrameworkCore;

namespace CondoHub.DataBase.MySql.EntityFramework
{
    public class CondoHubContext : DbContext
    {
        public CondoHubContext(DbContextOptions<CondoHubContext> options) : base(options)
        {
        }
        
        // Add DbSets here
    }
}
