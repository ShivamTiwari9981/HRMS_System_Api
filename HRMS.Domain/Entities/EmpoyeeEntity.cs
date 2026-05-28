
using HRMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{

    [Index(nameof(ClientId), nameof(EmployeeCode), IsUnique = true)]
    [Index(nameof(EmployeeEmail), IsUnique = true)]
    [Index(nameof(Phone), IsUnique = true)]
    public class EmployeeEntity : BaseEntity
    {
        [Key]
        public Guid EmployeeId { get; set; } 
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
        public string EmployeeEmail { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [Required]
        public Guid DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual DepartmentEntity Department { get; set; }

        [Required]
        public Guid DesignationId { get; set; }

        [ForeignKey(nameof(DesignationId))]
        public virtual DesignationEntity Designation { get; set; }

        public DateTime? JoiningDate { get; set; }

        public DateTime? BirthDate { get; set; }
        public GenderType Gender { get; set; }
        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public Guid CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public CountryEntity Country { get; set; }

        public Guid StateId { get; set; }
        [ForeignKey(nameof(StateId))]
        public StateEntity State { get; set; }
        public Guid CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        public CityEntity City { get; set; }
        public string? PostalCode { get; set; }
        [MaxLength(200)]
        public string? EmergencyContact { get; set; }

        public Guid? ManagerId { get; set; }

        public virtual EmployeeEntity? Manager { get; set; }

        public ICollection<EmployeeSalaryEntity> Salaries { get; set; }
             = new List<EmployeeSalaryEntity>();

        public ICollection<EmployeeEntity> Subordinates { get; set; }
            = new List<EmployeeEntity>();
    }
}
