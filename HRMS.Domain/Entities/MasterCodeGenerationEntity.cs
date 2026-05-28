using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace HRMS.Domain.Entities
{
    [Index(nameof(MasterCodeGenerationId), nameof(TableName), nameof(Prefix), IsUnique = true)]
    public class MasterCodeGenerationEntity : BaseEntity
    {
        [Key]
        public Guid MasterCodeGenerationId { get; set; } = Guid.NewGuid();

        public Guid? ClientId { get; set; }
        [Required]

        [MaxLength(100)]
        public string TableName { get; set; }

        [Required]
        [MaxLength(3)]
        public string Prefix { get; set; }

        public int LastNumber { get; set; }
    }
    
}
