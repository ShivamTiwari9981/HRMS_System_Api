using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HRMS.Domain.Entities
{
    [Table("Permission")]
    public class PermissionEntity : BaseEntity
    {
        [Required]
        public string PermissionName { get; set; }
    }
}
