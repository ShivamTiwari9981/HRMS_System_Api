using HRMS.Application.DTOs;
using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;

namespace HRMS.Application.Interfaces
{
    public interface IClientService
    {
        Task<bool> IsClientExit();
        ApiResponse<LoginResponseDto> RegisterClient(ClientRequestDto dto);
    }
}
