using System.ComponentModel.DataAnnotations;

namespace HRMS.Application.DTOs
{
    public class LoginRequestDto
    {
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string Password { get; set; } 
    }
}
