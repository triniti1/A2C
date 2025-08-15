using A2C.CRM.Api.Models;

namespace A2C.CRM.Api.DTOs
{
    public class RegisterRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public RoleType? Role { get; set; } = RoleType.User;
    }
}
