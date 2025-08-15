namespace A2C.CRM.Api.Models
{
    public class User
    {
       
        // User entity with properties for user management
        public Guid Id { get; set; } = Guid.NewGuid(); // Primary key

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Role managed through UserRole entity (Foreign key to UserRole
        public Guid UserRoleId { get; set; } = Guid.NewGuid();
        //public UserRole UserRole { get; set; } = new UserRole { RoleName = RoleType.User };      
        public UserRole UserRole { get; set; }

    }
}
