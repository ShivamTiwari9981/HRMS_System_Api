using HRMS.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Application.DTOs.RequestDto
{
    public class EmployeeRequestDto
    {
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
        public string Phone { get; set; }

        [Required]
        public Guid DepartmentId { get; set; }

        [Required]
        public Guid DesignationId { get; set; }

        public DateTime? JoiningDate { get; set; }

        public DateTime? BirthDate { get; set; }
        public GenderType Gender { get; set; }
        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public Guid CountryId { get; set; }

        public Guid StateId { get; set; }
        public Guid CityId { get; set; }
        public string? PostalCode { get; set; }

        public string? EmergencyContact { get; set; }

        public Guid? ManagerId { get; set; }

        public bool IsLoginUser { get; set; }

        public EmployeeSalaryRequestDto Salary { get; set; }
        public UserRequestDto User { get; set; }
    }

}
