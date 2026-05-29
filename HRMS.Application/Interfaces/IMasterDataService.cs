using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Domain.Entities;
using HRMS.Shared.Dto;

namespace HRMS.Application.Interfaces
{
    public interface IMasterDataService
    {
        Task<List<SubscriptionPlanResponseDto>> GetAllPlansAsync();
        List<EnumDto> GetAllCompanyType();
        Task<ApiResponse<CompanyDropdownListResponseDto>> GetPlanAndCompanyTypeAsync();

        #region Country
        Task<ApiResponse<bool>> IsCountryExist(string CountryName);
        Task<ApiResponse<bool>> IsCountryExistById(Guid CountryId);
        Task<ApiResponse<List<CountryResponseDto>>> GetAllCountriesAsync();
        Task<ApiResponse<CountryResponseDto>> GeCountryByIdAsync(Guid CountryId);
        Task<ApiResponse<bool>> AddCountryAsync(CountryRequestDto entity);
        Task<ApiResponse<bool>> UpdateCountryAsync(CountryRequestDto entity);
        Task<ApiResponse<bool>> DeactivateCountryAsync(Guid CountryId);
        Task<ApiResponse<bool>> ActivateCountryAsync(Guid CountryId);
        #endregion

        #region State
        Task<ApiResponse<bool>> IsStateExist(string StateName);
        Task<ApiResponse<List<StateResponseDto>>> GetAllStatesAsync();
        Task<ApiResponse<StateResponseDto>> GeStateByIdAsync(Guid StateId);
        Task<ApiResponse<bool>> IsStateExistById(Guid StateId);
        Task<ApiResponse<StateResponseDto>> GeStateByCountryIdAsync(Guid CountryId);
        Task<ApiResponse<bool>> AddStateAsync(StateRequestDto entity);
        Task<ApiResponse<bool>> UpdateStateAsync(StateRequestDto entity);
        Task<ApiResponse<bool>> DeactivateStateAsync(Guid StateId);
        Task<ApiResponse<bool>> ActivateStateAsync(Guid StateId);
        #endregion

        #region City
        Task<ApiResponse<bool>> IsCityExist(string CityName);
        Task<ApiResponse<bool>> IsCityExistById(Guid CityId);
        Task<ApiResponse<List<CityResponseDto>>> GetAllCityAsync();
        Task<ApiResponse<CityResponseDto>> GeCityByIdAsync(Guid CityId);
        Task<ApiResponse<CityResponseDto>> GeCityByStateIdAsync(Guid StateId);
        Task<ApiResponse<bool>> AddCityAsync(CityRequestDto entity);
        Task<ApiResponse<bool>> UpdateCityAsync(CityRequestDto entity);
        Task<ApiResponse<bool>> DeactivateCityAsync(Guid StateId);
        Task<ApiResponse<bool>> ActivateCityAsync(Guid CityId);
        #endregion



    }
}
