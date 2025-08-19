namespace A2C.CRM.Api.Models
{
    public enum RoleType
    {
        User = 0,
        Admin = 1,
        SuperUser = 2
    }

    public class UserRole
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public RoleType RoleName { get; set; } = RoleType.User;
    }
}
