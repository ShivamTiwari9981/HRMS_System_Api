using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Application.ExtensionMapper;
using HRMS.Application.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Domain.Enums;
using HRMS.Domain.Interfaces;
using HRMS.Shared.Dto;
using HRMS.Shared.Helpers;

namespace HRMS.Application.Services
{
    public class MasterDataService : BaseService, IMasterDataService
    {
        public MasterDataService(IUnitOfWork unitOfWork, ICurrentUserService currentSession) : base(unitOfWork, currentSession) { }

        public async Task<List<SubscriptionPlanResponseDto>> GetAllPlansAsync()
        {
            try
            {
                var dbResult = await _unitOfWork.SubscriptionPlanRepository.WhereAsync(x=>x.IsActive == true);

                return dbResult.Select(x => new SubscriptionPlanResponseDto
                {
                    SubscriptionPlanId = x.SubscriptionPlanId,
                    PlanName = x.PlanName,
                    EmployeeLimit = x.EmployeeLimit,
                    DurationInDays = x.DurationInDays,
                    Price = x.Price
                }).OrderBy(x=>x.EmployeeLimit).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<EnumDto> GetAllCompanyType()
        {
            var result = EnumHelper.GetEnumList<CompanyType>();
            return result;

        }

        public async Task<ApiResponse<CompanyDropdownListResponseDto>> GetPlanAndCompanyTypeAsync()
        {
            var response = new CompanyDropdownListResponseDto
            {
                SubscriptionPlans = await GetAllPlansAsync(), // DB call
                CompanyTypes = GetAllCompanyType()            // Enum (sync)
            };

            return ApiResponse<CompanyDropdownListResponseDto>.Success(response);
        }

        #region Country
        public async Task<ApiResponse<bool>> IsCountryExist(string CountryName)
        {
            bool IsCountryExist = await _unitOfWork.CountryRepository.
                AnyAsync(x =>  x.CountryName == CountryName
            );

            return ApiResponse<bool>.Success(IsCountryExist);
        }

        public async Task<ApiResponse<bool>> IsCountryExistById(Guid CountryId)
        {
            bool IsCountryExist = await _unitOfWork.CountryRepository.
                AnyAsync(x => x.CountryId == CountryId
            );

            return ApiResponse<bool>.Success(IsCountryExist);
        }

        public async Task<ApiResponse<List<CountryResponseDto>>> GetAllCountriesAsync()
        {
            try
            {
                var countryList = await _unitOfWork.CountryRepository.GetAllAsync();
                var dtoList=CountryMapper.GetDtoList(countryList);

                return ApiResponse<List<CountryResponseDto>>.Success(dtoList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<CountryResponseDto>> GeCountryByIdAsync(Guid CountryId)
        {
            try
            {
                var dbResult = await _unitOfWork.CountryRepository.FirstOrDefaultAsync(
                    x => x.CountryId == CountryId
                    );

                var dto = CountryMapper.GetDto(dbResult);

                return ApiResponse<CountryResponseDto>.Success(dto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> AddCountryAsync(CountryRequestDto dto)
        {
            try
            {
                var entity = CountryMapper.GetEntity(dto);

                await _unitOfWork.CountryRepository.AddAsync(entity);

                var result = await _unitOfWork.SaveChangesAsync();

                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> UpdateCountryAsync(CountryRequestDto dto)
        {
            try
            {
                var dbResult = await _unitOfWork.CountryRepository.FirstOrDefaultAsync(
                x => x.CountryId == dto.CountryId && x.IsActive == true);

                if(dbResult is null)
                    return ApiResponse<bool>.Fail(1,"Country is not found");

                _unitOfWork.CountryRepository.Update(dbResult);

                var result = await _unitOfWork.SaveChangesAsync();
                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> DeactivateCountryAsync(Guid CountryId)
        {
            try
            {
                var dbResult = await _unitOfWork.CountryRepository.FirstOrDefaultAsync(x => 
                x.CountryId == CountryId && x.IsActive == true);

                await _unitOfWork.CountryRepository.SoftDeleteAsync(dbResult);

                var result = await _unitOfWork.SaveChangesAsync();
                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> ActivateCountryAsync(Guid CountryId)
        {
            try
            {
                var dbResult = await _unitOfWork.CountryRepository.FirstOrDefaultAsync(x =>
                x.CountryId == CountryId && x.IsActive == false);

                await _unitOfWork.CountryRepository.ActivateAsync(dbResult);

                var result = await _unitOfWork.SaveChangesAsync();
                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion



        #region State
        public async Task<ApiResponse<bool>> IsStateExist(string StateName)
        {
            bool IsExist = await _unitOfWork.StateRepository.
                AnyAsync(x => x.StateName == StateName
            );

            return ApiResponse<bool>.Success(IsExist);
        }


        public async Task<ApiResponse<bool>> IsStateExistById(Guid StateId)
        {
            bool IsCountryExist = await _unitOfWork.StateRepository.
                AnyAsync(x => x.StateId == StateId
            );

            return ApiResponse<bool>.Success(IsCountryExist);
        }

        public async Task<ApiResponse<List<StateResponseDto>>> GetAllStatesAsync()
        {
            try
            {
                var StateList = await _unitOfWork.StateRepository.GetAllAsync();
                var dtoList = StateMapper.GetDtoList(StateList);

                return ApiResponse<List<StateResponseDto>>.Success(dtoList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<StateResponseDto>> GeStateByIdAsync(Guid StateId)
        {
            try
            {
                var dbResult = await _unitOfWork.StateRepository.FirstOrDefaultAsync(x => x.StateId == StateId);
               
                var dto = StateMapper.GetDto(dbResult);

                return ApiResponse<StateResponseDto>.Success(dto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<StateResponseDto>> GeStateByCountryIdAsync(Guid CountryId)
        {
            try
            {
                var dbResult = await _unitOfWork.StateRepository.FirstOrDefaultAsync(
                    x => x.CountryId == CountryId
                    );

                var dto = StateMapper.GetDto(dbResult);

                return ApiResponse<StateResponseDto>.Success(dto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> AddStateAsync(StateRequestDto dto)
        {
            try
            {
                var entity = StateMapper.GetEntity(dto);

                await _unitOfWork.StateRepository.AddAsync(entity);

                var result = await _unitOfWork.SaveChangesAsync();

                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> UpdateStateAsync(StateRequestDto dto)
        {
            try
            {
                var dbResult = await _unitOfWork.StateRepository.FirstOrDefaultAsync(
                x => x.CountryId == dto.CountryId && x.StateId == dto.StateId 
                && x.IsActive == true);

                if (dbResult is null)
                    return ApiResponse<bool>.Fail(1, "State is not found");

                _unitOfWork.StateRepository.Update(dbResult);

                var result = await _unitOfWork.SaveChangesAsync();
                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> DeactivateStateAsync(Guid StateId)
        {
            try
            {
                var dbResult = await _unitOfWork.StateRepository.FirstOrDefaultAsync(x =>
                x.StateId == StateId && x.IsActive == true);

                await _unitOfWork.StateRepository.SoftDeleteAsync(dbResult);

                var result = await _unitOfWork.SaveChangesAsync();
                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> ActivateStateAsync(Guid StateId)
        {
            try
            {
                var dbResult = await _unitOfWork.StateRepository.FirstOrDefaultAsync(x =>
                x.StateId == StateId && x.IsActive == false);

                await _unitOfWork.StateRepository.ActivateAsync(dbResult);

                var result = await _unitOfWork.SaveChangesAsync();
                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region City

        public async Task<ApiResponse<bool>> IsCityExist(string CityName)
        {
            bool IsExist = await _unitOfWork.CityRepository.
                AnyAsync(x => x.CityName == CityName
            );

            return ApiResponse<bool>.Success(IsExist);
        }

        public async Task<ApiResponse<bool>> IsCityExistById(Guid CityId)
        {
            bool IsCityExist = await _unitOfWork.CityRepository.
                AnyAsync(x => x.CityId == CityId
            );

            return ApiResponse<bool>.Success(IsCityExist);
        }

        public async Task<ApiResponse<List<CityResponseDto>>> GetAllCityAsync()
        {
            try
            {
                var cityList = await _unitOfWork.CityRepository.GetAllAsync();

                var dtoList = CityMapper.GetDtoList(cityList);

                return ApiResponse<List<CityResponseDto>>.Success(dtoList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<CityResponseDto>> GeCityByIdAsync(Guid CityId)
        {
            try
            {
                var dbResult = await _unitOfWork.CityRepository.FirstOrDefaultAsync(

                    x => x.CityId == CityId
                    );
                var dto = CityMapper.GetDto(dbResult);

                return ApiResponse<CityResponseDto>.Success(dto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<CityResponseDto>> GeCityByStateIdAsync(Guid StateId)
        {
            try
            {
                var dbResult = await _unitOfWork.CityRepository.FirstOrDefaultAsync(
                    x => x.StateId == StateId
                    );

                var dto = CityMapper.GetDto(dbResult);

                return ApiResponse<CityResponseDto>.Success(dto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> AddCityAsync(CityRequestDto dto)
        {
            try
            {
                var entity = CityMapper.GetEntity(dto);

                await _unitOfWork.CityRepository.AddAsync(entity);

                var result = await _unitOfWork.SaveChangesAsync();

                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> UpdateCityAsync(CityRequestDto dto)
        {
            try
            {
                var dbResult = await _unitOfWork.CityRepository.FirstOrDefaultAsync(
                x => x.CityId == dto.CityId && x.StateId == dto.StateId
                && x.IsActive == true);

                if (dbResult is null)
                    return ApiResponse<bool>.Fail(1, "City is not found");

                dbResult.CityName = dto.CityName;

                _unitOfWork.CityRepository.Update(dbResult);

                var result = await _unitOfWork.SaveChangesAsync();

                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> DeactivateCityAsync(Guid StateId)
        {
            try
            {
                var dbResult = await _unitOfWork.CityRepository.FirstOrDefaultAsync(x =>
                x.StateId == StateId && x.IsActive == true);

                await _unitOfWork.CityRepository.SoftDeleteAsync(dbResult);

                var result = await _unitOfWork.SaveChangesAsync();
                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> ActivateCityAsync(Guid CityId)
        {
            try
            {
                var dbResult = await _unitOfWork.CityRepository.FirstOrDefaultAsync(x =>
                x.CityId == CityId && x.IsActive == false);

                await _unitOfWork.CityRepository.ActivateAsync(dbResult);

                var result = await _unitOfWork.SaveChangesAsync();
                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
