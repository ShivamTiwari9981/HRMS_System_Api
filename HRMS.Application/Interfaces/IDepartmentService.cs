using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Domain.Entities;

namespace HRMS.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<ApiResponse<bool>> IsDepartmentExist(Guid DepartmentId);
        Task<ApiResponse<bool>> IsDepartmentExistByName(string DepartmentName);
        Task<ApiResponse<List<DepartmentResponseDto>>> GetAllDepartmentsAsync();
        Task<ApiResponse<DepartmentResponseDto>> GetDepartmentByIdAsync(Guid DepartmentId);
        Task<ApiResponse<bool>> AddDepartmentAsync(DepartmentRequestDto dto);
        Task<ApiResponse<bool>> UpdateDepartmentAsync(DepartmentRequestDto dto);
        Task<ApiResponse<bool>> DeactivateDepartmentAsync(Guid departmentId);
        Task<ApiResponse<bool>> ActivateDepartmentAsync(Guid departmentId);
    }
}
