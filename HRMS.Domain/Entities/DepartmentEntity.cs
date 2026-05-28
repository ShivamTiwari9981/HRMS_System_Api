using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Domain.Entities
{
    [Index(nameof(ClientId), nameof(DepartmentName), IsUnique = true)]
    [Index(nameof(ClientId), nameof(DepartmentCode), IsUnique = true)]
    public class DepartmentEntity : BaseEntity
    {
        [Key]
        public Guid DepartmentId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public virtual ClientEntity Client { get; set; }

        [Required]
        [MaxLength(20)]
        public string DepartmentCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string DepartmentName { get; set; }

        public string? Description { get; set; }

        public int DisplayOrder { get; set; }

        public virtual ICollection<DesignationEntity> Designations { get; set; }
    = new List<DesignationEntity>();

        public virtual ICollection<EmployeeEntity> Employees { get; set; }
            = new List<EmployeeEntity>();
    }
}
