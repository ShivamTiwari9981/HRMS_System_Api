using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Domain.Entities
{
    [Index(nameof(CountryId), IsUnique = true)]
    [Index(nameof(StateName), IsUnique = true)]
    public class StateEntity : BaseEntity
    {
        [Key]
        public Guid StateId { get; set; } = Guid.NewGuid();

        public Guid CountryId { get; set; }
        [Required]
        [MaxLength(200)]
        public string StateName { get; set; }
    }
}
