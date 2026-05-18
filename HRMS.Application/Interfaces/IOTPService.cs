using HRMS.Application.DTOs.ResponseDto;

namespace HRMS.Application.Interfaces
{
    public interface IOTPService
    {
        Task SaveOTP(string userEmail, string otp);
        Task<ApiResponse<bool>> VerifyOtp(string userEmail, string otp);
    }
}
