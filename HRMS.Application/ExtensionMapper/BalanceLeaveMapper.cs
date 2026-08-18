using HRMS.Application.DTOs.ResponseDto;
using HRMS.Domain.Entities;

namespace HRMS.Application.ExtensionMapper
{
    public static class BalanceLeaveMapper
    {
        public static LeaveBalanceResponseDto GetDto(
            this LeaveBalanceEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new LeaveBalanceResponseDto
            {
                LeaveBalanceId = entity.LeaveBalanceId,
                LeaveTypeId = entity.LeaveTypeId,
                UsedLeave = entity.UsedLeave,
                RemainingLeave = entity.RemainingLeave,
                TotalLeave = entity.TotalLeave,
                EmployeeId = entity.EmployeeId
            };
        }
    }
}
