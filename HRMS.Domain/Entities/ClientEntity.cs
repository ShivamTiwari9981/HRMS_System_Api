using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Domain.Entities
{
    [Index(nameof(CompanyName), IsUnique = true)]
    [Index(nameof(CompanyEmail), IsUnique = true)]
    [Index(nameof(Phone), IsUnique = true)]
    public class ClientEntity : BaseEntity
    {
        [Key]
        public Guid ClientId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public string ClientName { get; set; }


        [Required]
        [MaxLength(200)]
        public string CompanyName { get; set; }

        [MaxLength(200)]
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

        [MaxLength(50)]
        public string? GSTNumber { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }
 
    }
}

