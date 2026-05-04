
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Domain.Entities
{
    [Index(nameof(ClientId), nameof(RoleName), IsUnique = true)]
    public class RoleEntity:BaseEntity
    {
        [Key]
        public Guid RoleId { get; set; } = Guid.NewGuid();

        public Guid ClientId { get; set; }

        [MaxLength(200)]
        public string RoleName { get; set; }
    }
   
}
