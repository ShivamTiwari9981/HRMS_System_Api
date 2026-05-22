using HRMS.Application.DTOs.ResponseDto;

namespace HRMS.Application.Interfaces
{
    public interface IEmailService
    {
        Task<ApiResponse<bool>> SendEmailOTP(string userEmail, string otp);
    }
}
