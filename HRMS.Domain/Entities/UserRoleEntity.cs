using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{
    [Index(nameof(ClientId), nameof(UserId), nameof(RoleId), IsUnique = true)]
    public class UserRoleEntity:BaseEntity
    {
        [Key]
        public Guid UserRoleId { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }

        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual UserEntity User { get; set; } = null!;

        [ForeignKey(nameof(RoleId))]
        public virtual RoleEntity Role { get; set; } = null!;
    }
}
