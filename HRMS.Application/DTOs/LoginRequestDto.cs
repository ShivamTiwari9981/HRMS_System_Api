using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.DTOs
{
    public class LoginRequestDto
    {
        [Required]
        public string ClientKey { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
