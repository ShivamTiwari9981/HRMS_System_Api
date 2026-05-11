using System.ComponentModel.DataAnnotations;


namespace HRMS.Application.DTOs.RequestDto
{
    public class SignupRequestDto
    {
        [Required]
        public string ClientKey { get; set; } 
        [Required]
        public string UserName { get; set; } 
        [Required]
        public string UserEmail { get; set; } 
        [Required]
        public string Password { get; set; }
    }
}
