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
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // הגדרת קשר 1:N בין User ל-UserRole
            /*modelBuilder.Entity<User>()
                .HasOne(u => u.UserRole)
                .WithMany(r => r.User)
                .HasForeignKey(u => u.UserRoleId)
                .OnDelete(DeleteBehavior.Restrict);*/

            // המרת Enum RoleType לערך מספרי
            modelBuilder.Entity<UserRole>()
                .Property(r => r.RoleName)
                .HasConversion<int>();

            // הגדרת מפתחות ראשיים
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<UserRole>().HasKey(r => r.Id);
        }
    }
}
