namespace HRMS.Application.DTOs.ResponseDto
{
    public class RolePermissionResponseDto
    {
        public Guid PermissionId { get; set; }
        public Guid ClientId { get; set; }
        public Guid MenuId { get; set; }
        public int Action { get; set; }
        public string PermissionKey { get; set; }
        public bool IsActive { get; set; }
    }
}
