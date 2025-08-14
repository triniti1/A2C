namespace A2C.CRM.Api.Models
{
    public enum RoleType
    {
        User,
        Admin,
        SuperUser
    }

    public class UserRole
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public RoleType RoleName { get; set; } = RoleType.User;

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
