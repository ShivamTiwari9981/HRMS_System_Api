using HRMS.Domain.Enums;
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
        public string CompanyName { get; set; }

        [Required]
        public CompanyType CompanyType { get; set; }

        [MaxLength(200)]
        [EmailAddress]
        public string CompanyEmail { get; set; }

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        public Guid SubscriptionPlanId { get; set; }
        
        public DateTime? SubscriptionStartDate { get; set; }
        [Required]
        public DateTime SubscriptionEndDate { get; set; }

        [MaxLength(50)]
        public string? GSTNumber { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }
 
    }
}

