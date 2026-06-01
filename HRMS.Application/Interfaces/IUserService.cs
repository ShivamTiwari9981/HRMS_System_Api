using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;

namespace HRMS.Application.Interfaces
{
    public interface IUserService
    {
        ApiResponse<bool> AddUser(UserRequestDto dto);
        ApiResponse<string> AssignRoles(AssignUsersRoleRequestDto dto);
    }
}
