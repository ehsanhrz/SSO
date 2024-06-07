using Microsoft.EntityFrameworkCore;

namespace SSO.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
            
        }
    }
}
