using Microsoft.EntityFrameworkCore;
using A2C.CRM.Api.Models;

namespace A2C.CRM.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
