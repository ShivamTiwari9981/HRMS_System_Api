using HRMS.Application.DTOs;
using HRMS.Application.DTOs.ResponseDto;

namespace HRMS.Application.Interfaces
{
    public interface IAuthService
    {
        ApiResponse<string> UserSignUp(SignupRequestDto dto);
        ApiResponse<LoginResponseDto> Login(LoginRequestDto dto);
    }
}
