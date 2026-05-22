using HRMS.Application.DTOs.ResponseDto;
using HRMS.Application.Interfaces;
namespace HRMS.Application.Services
{
    public class OTPService : IOTPService
    {
        private readonly IRedisCacheService _redis;

        public OTPService(
        IRedisCacheService redis
        )
        {
            _redis = redis;

        }

        public async Task SaveOTP(string userEmail, string otp)
        {
            string key = $"OTP:{userEmail}";

            await _redis.SetAsync(
                key,
                otp,
                TimeSpan.FromMinutes(5)
            );
        }

        

        public async Task<ApiResponse<bool>> VerifyOtp(string userEmail, string otp)
        {
            try
            {
                string key = $"OTP:{userEmail}";

                string savedOtp = await _redis.GetAsync(key);

                if (string.IsNullOrWhiteSpace(savedOtp))
                {
                    return ApiResponse<bool>.Fail(1, "OTP expired or not found");
                }

                if (savedOtp != otp)
                {
                    return ApiResponse<bool>.Fail(1, "Invalid OTP");
                }

                await _redis.RemoveAsync(key);

                return ApiResponse<bool>.Success(true, "OTP verified successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(1, ex.Message);
            }
        }
    }
}
