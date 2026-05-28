using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Domain.Entities
{
    [Index(nameof(CityName), IsUnique = true)]
    [Index(nameof(StateId), IsUnique = true)]
    public class CityEntity : BaseEntity
    {
        [Key]
        public Guid CityId { get; set; } = Guid.NewGuid();
        [Required]
        public Guid StateId { get; set; }

        [Required]
        [MaxLength(200)]
        public string CityName { get; set; }
    }
}
