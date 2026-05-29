using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;

namespace HRMS.Application.Interfaces
{
    public interface IDesignationService
    {
        Task<ApiResponse<bool>> IsDesignationExist(Guid DesignationId);
        Task<ApiResponse<List<DesignationResponseDto>>> GetAllDesignationsAsync();
        Task<ApiResponse<DesignationResponseDto>> GetDesignationByIdAsync(Guid DesignationId);
        Task<ApiResponse<bool>> AddDesignationAsync(DesignationRequestDto entity);
        Task<ApiResponse<bool>> UpdateDesignationAsync(DesignationRequestDto Designation);
        Task<ApiResponse<bool>> DeactivateDesignationAsync(Guid DesignationId);
        Task<ApiResponse<bool>> ActivateDesignationAsync(Guid DesignationId);
    }
}
