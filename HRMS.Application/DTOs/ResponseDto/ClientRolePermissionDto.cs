namespace HRMS.Application.DTOs.ResponseDto
{
    public class ClientRolePermissionDto
    {
        public string Token { get; set; } = string.Empty;
        public UserResponseDto user { get; set; } = new();
        public ClientResponseDto client { get; set; } = new();
        public List<RoleResponseDto> role { get; set; } = new();
        public List<MenuResponseDto> menu { get; set; } = new();
        public List<RolePermissionResponseDto> rolepermission{ get; set; } = new();
    }
}
