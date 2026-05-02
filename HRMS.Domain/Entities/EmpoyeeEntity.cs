using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{

    [Table("Employee")]
    public class EmployeeEntity : BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string EmployeeCode { get; set; }

        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20)]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public Guid DepartmentId { get; set; }

        [Required]
        [StringLength(200)]
        public string Designation { get; set; }

        public string? ProfileImagePath { get; set; }

        public DateTime? DateOfJoining { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
