using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{
    [Index(nameof(ClientId), nameof(DepartmentId), nameof(DesignationName), IsUnique = true)]
    public class DesignationEntity : BaseEntity
    {
        [Key]
        public Guid DesignationId { get; set; } = Guid.NewGuid();
        [Required]
        public Guid ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public virtual ClientEntity Client { get; set; }
        [Required]
        public Guid DepartmentId { get; set; }
        [Required]
        [MaxLength(100)]

        public string DesignationName { get; set; }
        [Required]
        [MaxLength(10)]
        public string DesignationCode { get; set; }

        public string? Description { get; set; }

        public int? DisplayOrder { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual DepartmentEntity Department { get; set; }

        public virtual ICollection<EmployeeEntity> Employees { get; set; }
     = new List<EmployeeEntity>();
    }
}
