using HRMS.Application.DTOs.ResponseDto;
using HRMS.Shared.Dto;

namespace HRMS.Application.Interfaces
{
    public interface IMasterDataService
    {
        Task<List<SubscriptionPlanResponseDto>> GetAllPlansAsync();
        List<EnumDto> GetAllCompanyType();
        Task<ApiResponse<CompanyDropdownListResponseDto>> GetPlanAndCompanyTypeAsync();
    }
}
