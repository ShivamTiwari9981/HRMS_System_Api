namespace HRMS.Application.DTOs.ResponseDto
{
    public class DesignationResponseDto
    {
        public Guid DesignationId { get; set; }
        public string DesignationCode { get; set; }
        public Guid DepartmentId { get; set; }
        public string DesignationName { get; set; }
        public string? Description { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
