using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace HRMS.Domain.Entities
{

    [Index(nameof(ClientId), nameof(PermissionName), nameof(Action), IsUnique = true)]
    public class PermissionEntity:BaseEntity
    {
        [Key]
        public Guid PermissionId { get; set; } = Guid.NewGuid();

        public Guid ClientId { get; set; }

        [Required]
        [MaxLength(100)]
        public string PermissionName { get; set; }

    }

}
