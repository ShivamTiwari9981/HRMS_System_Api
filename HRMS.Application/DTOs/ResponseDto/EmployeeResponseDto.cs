namespace HRMS.Application.DTOs.ResponseDto
{
    public class EmployeeResponseDto
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string EmployeeEmail { get; set; }
        public string Phone { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string? ProfileImagePath { get; set; }
        public bool IsActive { get; set; }
        public DateTime? JoiningDate { get; set; }
        public int TotalRecords { get; set; }
    }
}
