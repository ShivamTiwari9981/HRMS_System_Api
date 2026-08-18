using HRMS.Shared.Dto;

namespace HRMS.Application.DTOs.ResponseDto
{
    public class EmployeeResponseDto
    {
        public Guid EmployeeId { get; set; }
        public Guid DesignationId { get; set; }
        public Guid DepartmentId { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public Guid CountryId { get; set; }
        public Guid StateId { get; set; }
        public Guid CityId { get; set; }
        public string PostalCode { get; set; }
        public string EmergencyContact { get; set; }
        public Guid? ManagerId { get; set; }
        public bool IsLoginUser { get; set; }
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

    public class LoadCreateEmployeeMasterDto
    {
        public List<EmployeeDesignationDto> Designation { get; set; }
        public List<EmployeeDepartmentDto> Departments { get; set; }
        public List<EmployeeManagerDto> Manager { get; set; }
        public List<EnumDto> Gender { get; set; }
    }

    public class EmployeeDesignationDto
    {
        public Guid ClientId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string DesignationCode { get; set; }
    }

    public class EmployeeDepartmentDto
    {
       public Guid ClientId { get; set; }
       public Guid DepartmentId { get; set; }
       public string DepartmentName { get; set; }
       public string DepartmentCode { get; set; }
    }

    public class EmployeeManagerDto
    {
        public Guid ClientId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid DepartmentId { get; set; }
        public string EmployeeEmail { get; set; }
        public string DepartmentCode { get; set; }
    }
}
