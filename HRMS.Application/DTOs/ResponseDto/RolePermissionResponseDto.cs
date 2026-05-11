namespace HRMS.Application.DTOs.ResponseDto
{
    public class RolePermissionResponseDto
    {
        public Guid PermissionId { get; set; }
        public string Module { get; set; }
        public string Action { get; set; }
        public string PermissionName { get; set; }
    }
}
