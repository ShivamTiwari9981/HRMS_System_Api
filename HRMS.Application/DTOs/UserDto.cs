
namespace HRMS.Application.DTOs
{
    public class UserDto
    {
        public string UserCode { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid RoleId { get; set; }
        public string? ProfileImagePath { get; set; }
    }
}
