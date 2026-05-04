
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Domain.Entities
{

    [Index(nameof(ClientId), nameof(EmployeeCode), IsUnique = true)]
    [Index(nameof(ClientId), nameof(Email), IsUnique = true)]
    [Index(nameof(ClientId), nameof(Phone), IsUnique = true)]
    public class EmployeeEntity : BaseEntity
    {
        [Key]
        public Guid EmployeeId { get; set; } = Guid.NewGuid();
        [Required]
        public Guid ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public virtual ClientEntity Client { get; set; }

        [Required]
        [MaxLength(20)]
        public string EmployeeCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [Required]
        public Guid DepartmentId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Designation { get; set; }

        public string? ProfileImagePath { get; set; }

        public DateTime? DateOfJoining { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        public Guid? UserId { get; set; }

        public DateTime? DateOfBirth { get; set; }
        [MaxLength(10)]
        public string? Gender { get; set; }
        [MaxLength(200)]
        public string? Address { get; set; }
        [MaxLength(200)]
        public string? EmergencyContact { get; set; }
    }
}
