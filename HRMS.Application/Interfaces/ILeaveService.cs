using HRMS.Application.DTOs.ResponseDto;

namespace HRMS.Application.Interfaces
{
    public interface ILeaveService
    {
        Task<ApiResponse<LeaveBalanceResponseDto>> GetAllLeaveBalanceAsync(Guid EmployeeId);
    }
}
