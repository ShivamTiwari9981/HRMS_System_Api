namespace HRMS.Application.DTOs.ResponseDto
{
    public class LeaveBalanceResponseDto
    {
        public Guid LeaveBalanceId { get; set; }
        public Guid LeaveTypeId { get; set; }

        public Guid EmployeeId { get; set; }

        public decimal TotalLeave { get; set; }
        public decimal UsedLeave { get; set; }
        public decimal RemainingLeave { get; set; }
    }
}
