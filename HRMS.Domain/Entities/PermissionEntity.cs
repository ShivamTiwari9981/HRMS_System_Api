using HRMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HRMS.Domain.Entities
{

    [Index(nameof(ClientId), nameof(MenuId), nameof(Action), IsUnique = true)]
    public class PermissionEntity:BaseEntity
    {
        [Key]
        public Guid PermissionId { get; set; } = Guid.NewGuid();

        public Guid ClientId { get; set; }
        [Required]
        public Guid MenuId { get; set; }

        [ForeignKey(nameof(MenuId))]
        public MenuEntity Menu { get; set; }

        [Required]
        [MaxLength(200)]
        public PermissionAction Action { get; set; }

        [Required]
        [MaxLength(200)]
        public string PermissionKey { get; set; }


        [MaxLength(200)]
        public string? Description { get; set; }

        public ICollection<RolePermissionEntity> RolePermissions { get; set; }
    = new List<RolePermissionEntity>();

    }

}
