using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{
    [Index(nameof(ClientId), nameof(RoleId), nameof(PermissionId), IsUnique = true)]
    public class RolePermissionEntity : BaseEntity
    {
        [Key]
        public Guid RolePermissionId { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }

        public Guid RoleId { get; set; }

        public Guid PermissionId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public RoleEntity Role { get; set; }

        [ForeignKey(nameof(PermissionId))]
        public PermissionEntity Permission { get; set; }
    }
}
