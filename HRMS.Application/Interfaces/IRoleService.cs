using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Domain.Entities;

namespace HRMS.Application.Interfaces
{
    public interface IRoleService
    {
        Task<ApiResponse<bool>> IsRoleExistAsync(Guid roleId);
        Task<ApiResponse<List<RoleResponseDto>>> GetAllRoleAsync();
        Task<ApiResponse<string>> AddRoleAsync(RoleRequestDto dto);
        Task<ApiResponse<bool>> UpdateRoleAsync(RoleRequestDto enity);
        Task<ApiResponse<bool>> DeactivateRoleAsync(Guid RoleId);
        Task<ApiResponse<bool>> ActivateRoleAsync(Guid RoleId);
        ApiResponse<string> AssignPermissions(AssignRolePermissionRequestDto dto);
    }
}
