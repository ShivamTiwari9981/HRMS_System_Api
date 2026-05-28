using HRMS.Application.DTOs.ResponseDto;
using HRMS.Domain.Entities;

namespace HRMS.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<ApiResponse<bool>> IsDepartmentExist(Guid DepartmentId);
        Task<ApiResponse<List<DepartmentEntity>>> GetAllDepartmentsAsync();
        Task<ApiResponse<DepartmentEntity>> GetDepartmentByIdAsync(Guid DepartmentId);
        Task<ApiResponse<bool>> SaveAsync(DepartmentEntity department);
        Task<ApiResponse<bool>> UpdateAsync(DepartmentEntity department);
        Task<ApiResponse<bool>> DeactivateAsync(Guid departmentId);
        Task<ApiResponse<bool>> RepopenAsync(Guid departmentId);
    }
}
