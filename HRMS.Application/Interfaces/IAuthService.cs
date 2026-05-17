using HRMS.Application.DTOs;
using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;

namespace HRMS.Application.Interfaces
{
    public interface IAuthService
    {
        ApiResponse<string> UserSignUp(SignupRequestDto dto);
        ApiResponse<ClientRolePermissionDto> Login(LoginRequestDto dto);
        ClientRolePermissionDto GetUserRolePermissionsAsync(Guid ClientId, Guid UserId);
    }
}
