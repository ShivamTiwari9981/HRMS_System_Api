
namespace HRMS.Application.DTOs
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public Guid ClientId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string PasswordHash { get; set; }
        public string UserSalt { get; set; }
        public bool IsCompanyProfileCreated { get; set; }
    }
}
