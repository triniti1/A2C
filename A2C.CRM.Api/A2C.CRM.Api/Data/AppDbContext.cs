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

            // קבועים מראש כדי ש-EF יוכל לעקוב אחרי ה-Seed
            var adminRoleId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var userRoleId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var superUserRoleId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var adminUserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            // Seed UserRoles
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = adminRoleId, RoleName = RoleType.Admin },
                new UserRole { Id = userRoleId, RoleName = RoleType.User },
                new UserRole { Id = superUserRoleId, RoleName = RoleType.SuperUser }
            );

            // Seed Users (ללא Navigation Property!)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = adminUserId,
                    Name = "Admin",
                    Email = "admin@a2c.local",
                    PasswordHash = "admin", // תחליף ב-Hash אמיתי
                    CreatedAt = new DateTime(2025, 8, 15, 12, 0,0, DateTimeKind.Utc),
                    UserRoleId = adminRoleId
                });

            // הגדרת קשר 1:N בין User ל-UserRole
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserRole)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.UserRoleId)
                .OnDelete(DeleteBehavior.Restrict);

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
