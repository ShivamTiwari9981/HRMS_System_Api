using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{
    [Index(nameof(ClientId), nameof(TableName), IsUnique = true)]
    public class MasterCodeGenerationEntity : BaseEntity
    {
        [Key]
        public Guid MasterCodeGenerationId { get; set; } = Guid.NewGuid();

        public Guid ClientId { get; set; }

        [MaxLength(100)]
        public string TableName { get; set; }

        [MaxLength(3)]
        public string Prefix { get; set; }

        public int LastNumber { get; set; }
    }
    
}
