using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace HRMS.Domain.Entities
{

    [Index(nameof(ClientId), nameof(Module), nameof(Action), IsUnique = true)]
    public class PermissionEntity:BaseEntity
    {
        [Key]
        public Guid PermissionId { get; set; } = Guid.NewGuid();

        public Guid ClientId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Module { get; set; }

        [Required]
        [MaxLength(50)]
        public string Action { get; set; }

        // computed key (not mapped in DB if using computed column)
        public string PermissionKey => $"{Module}_{Action}";
    }

}
