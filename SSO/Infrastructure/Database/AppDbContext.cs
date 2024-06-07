using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SSO.Infrastructure.Database.Models;

namespace SSO.Infrastructure.Database
{
    public class AppDbContext  : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
            
        }
        DbSet<ApplicationUser> user => Set<ApplicationUser>();
    }
}
