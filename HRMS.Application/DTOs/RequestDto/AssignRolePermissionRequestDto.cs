namespace HRMS.Application.DTOs.RequestDto
{
    public class AssignRolePermissionRequestDto
    {
        public Guid RoleId { get; set; }

        public List<Guid> PermissionIds { get; set; }
    }
}
