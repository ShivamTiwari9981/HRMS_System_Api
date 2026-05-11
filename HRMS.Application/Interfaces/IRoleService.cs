using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;

namespace HRMS.Application.Interfaces
{
    public interface IRoleService
    {
        Task<bool> IsRoleExist();
        Task<ApiResponse<List<RoleResponseDto>>> GetAllRole();
        Task<ApiResponse<string>> AddRole(RoleRequestDto dto);
        ApiResponse<string> AssignPermissions(AssignRolePermissionRequestDto dto);
    }
}
