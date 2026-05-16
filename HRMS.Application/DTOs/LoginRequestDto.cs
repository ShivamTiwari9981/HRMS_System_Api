using System.ComponentModel.DataAnnotations;

namespace HRMS.Application.DTOs
{
    public class LoginRequestDto
    {
        [Required]
        public string UserEmail { get; set; } = "shivamtiwari8756@gmail.com";
        [Required]
        public string Password { get; set; } = "12345";
    }
}
