
using System.ComponentModel.DataAnnotations;

namespace HRMS.Application.DTOs.RequestDto
{
    public class ClientRequestDto
    {

        [Required]
        [MaxLength(200)]
        public string ClientName { get; set; }
        [Required]
        [MaxLength(200)]
        public string CompanyName { get; set; }

        public string? CompanyLogo { get; set; }
        [Required]
        [MaxLength(200)]
        public string Domain { get; set; }
        [Required]
        [MaxLength(200)]
        public string ContactPerson { get; set; }

        [MaxLength(200)]
        [EmailAddress]
        public string CompanyEmail { get; set; }
        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        public DateTime? ExpiryDate { get; set; }
        [Required]
        [MaxLength(50)]
        public string? GSTNumber { get; set; }
        [Required]
        [MaxLength(200)]
        public string? Address { get; set; }

        public bool? IsActive { get; set; }
        [Required]
        public bool? IsSynced { get; set; }
    }
}
