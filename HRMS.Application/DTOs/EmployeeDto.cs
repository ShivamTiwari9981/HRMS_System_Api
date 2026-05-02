
namespace HRMS.Application.DTOs
{
    public class EmployeeDto
    {
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid DepartmentId { get; set; }
        public string Designation { get; set; }
        public string? ProfileImagePath { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public decimal Salary { get; set; }
        public Guid UserId { get; set; }
    }
}
