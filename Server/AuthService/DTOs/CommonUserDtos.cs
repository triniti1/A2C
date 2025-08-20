using A2C.CRM.Api.Models;

namespace A2C.CRM.Api.DTOs
{
    public class DeleteUserDtos
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class UserLoginRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class UserRegisterRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public RoleType? Role { get; set; } = RoleType.User;
    }

    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
