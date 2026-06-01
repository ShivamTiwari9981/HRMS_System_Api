namespace HRMS.Application.DTOs.RequestDto
{
    public class EmployeeListRequestDto : PagedRequestDto
    {
        public Guid? DepartmentId { get; set; }

        public Guid? DesignationId { get; set; }

        public bool? IsActive { get; set; }
    }
}
