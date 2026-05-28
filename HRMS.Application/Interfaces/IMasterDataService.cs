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
        Task<ApiResponse<List<CountryEntity>>> GetAllCountriesAsync();
        Task<ApiResponse<CountryEntity>> GeCountryByIdAsync(Guid CountryId);
        Task<ApiResponse<bool>> SaveAsync(CountryEntity entity);
        Task<ApiResponse<bool>> UpdateAsync(CountryEntity entity);
        Task<ApiResponse<bool>> DeactivateAsync(Guid CountryId);
        Task<ApiResponse<bool>> RepopenAsync(Guid CountryId);
        #endregion

        #region State
        Task<ApiResponse<bool>> IsStateExist(string StateName);
        Task<ApiResponse<List<StateEntity>>> GetAllStatesAsync();
        Task<ApiResponse<StateEntity>> GeStateByIdAsync(Guid StateId);
        Task<ApiResponse<bool>> IsStateExistById(Guid StateId);
        Task<ApiResponse<StateEntity>> GeStateByCountryIdAsync(Guid CountryId);
        Task<ApiResponse<bool>> SaveStateAsync(StateEntity entity);
        Task<ApiResponse<bool>> UpdateStateAsync(StateEntity entity);
        Task<ApiResponse<bool>> DeactivateStateAsync(Guid StateId);
        Task<ApiResponse<bool>> RepopenStateAsync(Guid StateId);
        #endregion

        #region City
        Task<ApiResponse<bool>> IsCityExist(string CityName);
        Task<ApiResponse<bool>> IsCityExistById(Guid CityId);
        Task<ApiResponse<List<CityEntity>>> GetAllCityAsync();
        Task<ApiResponse<CityEntity>> GeCityByIdAsync(Guid CityId);
        Task<ApiResponse<CityEntity>> GeCityByStateIdAsync(Guid StateId);
        Task<ApiResponse<bool>> SaveCityAsync(CityEntity entity);
        Task<ApiResponse<bool>> UpdateCityAsync(CityEntity entity);
        Task<ApiResponse<bool>> DeactivateCityAsync(Guid StateId);
        Task<ApiResponse<bool>> RepopenCityAsync(Guid CityId);
        #endregion



    }
}
