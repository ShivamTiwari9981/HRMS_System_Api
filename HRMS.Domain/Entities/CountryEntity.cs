using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Domain.Entities
{
    [Index(nameof(CountryName), IsUnique = true)]
    public class CountryEntity : BaseEntity
    {
        [Key]
        public Guid CountryId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public string CountryName { get; set; }
    }
}
