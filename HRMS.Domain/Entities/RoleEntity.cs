
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{
    [Table("Role")]
    public class RoleEntity : BaseEntity
    {
        public string RoleName { get; set; }
    }
}
