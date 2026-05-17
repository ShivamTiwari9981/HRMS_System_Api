namespace HRMS.Application.DTOs.ResponseDto
{
    public class ClientRolePermissionDto
    {
        public string Token { get; set; } = string.Empty;
        public ClientUserResponseDto clientUserResponse { get; set; } = new();
        public List<RoleResponseDto> RoleResponse { get; set; } = new();
        public List<MenuResponseDto> menuResponse { get; set; } = new();
        public List<RolePermissionResponseDto> rolePermissionResponse { get; set; } = new();
    }
}
