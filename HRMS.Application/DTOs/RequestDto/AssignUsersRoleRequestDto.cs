namespace HRMS.Application.DTOs.RequestDto
{
    public class AssignUsersRoleRequestDto
    {
        public List<Guid> UserIds { get; set; }

        public List<Guid> RoleIds { get; set; }
    }
}
