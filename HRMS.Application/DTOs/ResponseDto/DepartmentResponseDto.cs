namespace HRMS.Application.DTOs.ResponseDto
{
    public class DepartmentResponseDto
    {
        public Guid DepartmentId { get; set; } 
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
    }
}
