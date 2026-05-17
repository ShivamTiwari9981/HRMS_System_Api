using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;

namespace HRMS.Application.Interfaces
{
    public interface IPermissionService
    {
        Task<ApiResponse<PermissionResponseDto>> GetPermissionById(Guid permissionId);
        Task<ApiResponse<List<PermissionResponseDto>>> GetAllPermission();
        Task<ApiResponse<string>> AddPermission(PermissionRequestDto dto);
    }
}
