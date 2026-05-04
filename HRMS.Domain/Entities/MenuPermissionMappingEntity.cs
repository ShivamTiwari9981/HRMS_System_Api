using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{
    [Index(nameof(ClientId), nameof(MenuId), nameof(PermissionId), IsUnique = true)]
    public class MenuPermissionMappingEntity : BaseEntity
    {
        [Key]
        public Guid MenuPermissionMappingId { get; set; } = new Guid();
        public Guid ClientId { get; set; }

        public int MenuId { get; set; }

        public Guid PermissionId { get; set; }

        [ForeignKey(nameof(MenuId))]
        public MenuEntity Menu { get; set; }

        [ForeignKey(nameof(PermissionId))]
        public PermissionEntity Permission { get; set; }
    }
}
