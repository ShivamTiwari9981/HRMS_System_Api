

using HRMS.Application.DTOs.ResponseDto;
using HRMS.Application.ExtensionMapper;
using HRMS.Application.Interfaces;
using HRMS.Domain.Interfaces;

namespace HRMS.Application.Services
{
    public class LeaveService : BaseService, ILeaveService
    {
        public LeaveService(IUnitOfWork unitOfWork,
            ICurrentUserService currentSession, IUtilityService utilityService) : base(unitOfWork, currentSession)
        {
        }

        #region GetLeaveBalance
        public async Task<ApiResponse<LeaveBalanceResponseDto>> GetAllLeaveBalanceAsync(Guid EmployeeId)
        {
            try
            {
                var leave = await _unitOfWork.LeaveBalanceRepository.FirstOrDefaultAsync(
                    x => x.ClientId == ClientId && 
                    x.EmployeeId == EmployeeId
                    );

                var dto = BalanceLeaveMapper.GetDto(leave);

                return ApiResponse<LeaveBalanceResponseDto>.Success(dto);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
