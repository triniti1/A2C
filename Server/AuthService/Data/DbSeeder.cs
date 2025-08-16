using A2C.CRM.Api.Models;

namespace A2C.CRM.Api.Data
{
    public static class DbSeeder
    {
        public static void SeedRoles(AppDbContext context)
        {
            if (!context.UserRoles.Any())
            {
                var roles = new List<UserRole>
                {
                    new UserRole { RoleName = RoleType.User },
                    new UserRole { RoleName = RoleType.Admin },
                    new UserRole { RoleName = RoleType.SuperUser }
                };

                context.UserRoles.AddRange(roles);
                context.SaveChanges();
            }
        }
    }
}
