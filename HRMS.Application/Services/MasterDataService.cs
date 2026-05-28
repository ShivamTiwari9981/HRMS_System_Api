using HRMS.Application.DTOs.ResponseDto;
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

        public async Task<ApiResponse<List<CountryEntity>>> GetAllCountriesAsync()
        {
            try
            {
                var countryList = await _unitOfWork.CountryRepository.GetAllAsync();
                return ApiResponse<List<CountryEntity>>.Success(countryList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<CountryEntity>> GeCountryByIdAsync(Guid CountryId)
        {
            try
            {
                var dbList = await _unitOfWork.CountryRepository.FirstOrDefaultAsync(
                    x => x.CountryId == CountryId
                    );
                return ApiResponse<CountryEntity>.Success(dbList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> SaveAsync(CountryEntity entity)
        {
            try
            {
                entity.CountryId = Guid.NewGuid();
                await _unitOfWork.CountryRepository.AddAsync(entity);
                var result = await _unitOfWork.SaveChangesAsync();

                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> UpdateAsync(CountryEntity entity)
        {
            try
            {
                var dbResult = await _unitOfWork.CountryRepository.FirstOrDefaultAsync(
                x => x.CountryId == entity.CountryId && x.IsActive == true);

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

        public async Task<ApiResponse<bool>> DeactivateAsync(Guid CountryId)
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

        public async Task<ApiResponse<bool>> RepopenAsync(Guid CountryId)
        {
            try
            {
                var dbResult = await _unitOfWork.CountryRepository.FirstOrDefaultAsync(x =>
                x.CountryId == CountryId && x.IsActive == false);

                await _unitOfWork.CountryRepository.ReopenAsync(dbResult);

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

        public async Task<ApiResponse<List<StateEntity>>> GetAllStatesAsync()
        {
            try
            {
                var StateList = await _unitOfWork.StateRepository.GetAllAsync();
                return ApiResponse<List<StateEntity>>.Success(StateList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<StateEntity>> GeStateByIdAsync(Guid StateId)
        {
            try
            {
                var dbList = await _unitOfWork.StateRepository.FirstOrDefaultAsync(

                    x => x.StateId == StateId
                    );
                return ApiResponse<StateEntity>.Success(dbList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<StateEntity>> GeStateByCountryIdAsync(Guid CountryId)
        {
            try
            {
                var dbList = await _unitOfWork.StateRepository.FirstOrDefaultAsync(
                    x => x.CountryId == CountryId
                    );
                return ApiResponse<StateEntity>.Success(dbList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> SaveStateAsync(StateEntity entity)
        {
            try
            {
                entity.StateId= Guid.NewGuid();
                await _unitOfWork.StateRepository.AddAsync(entity);
                var result = await _unitOfWork.SaveChangesAsync();

                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> UpdateStateAsync(StateEntity entity)
        {
            try
            {
                var dbResult = await _unitOfWork.StateRepository.FirstOrDefaultAsync(
                x => x.CountryId == entity.CountryId && x.StateId == entity.StateId 
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

        public async Task<ApiResponse<bool>> RepopenStateAsync(Guid StateId)
        {
            try
            {
                var dbResult = await _unitOfWork.StateRepository.FirstOrDefaultAsync(x =>
                x.StateId == StateId && x.IsActive == false);

                await _unitOfWork.StateRepository.ReopenAsync(dbResult);

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

        public async Task<ApiResponse<List<CityEntity>>> GetAllCityAsync()
        {
            try
            {
                var CityList = await _unitOfWork.CityRepository.GetAllAsync();
                return ApiResponse<List<CityEntity>>.Success(CityList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<CityEntity>> GeCityByIdAsync(Guid CityId)
        {
            try
            {
                var dbList = await _unitOfWork.CityRepository.FirstOrDefaultAsync(

                    x => x.CityId == CityId
                    );
                return ApiResponse<CityEntity>.Success(dbList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<CityEntity>> GeCityByStateIdAsync(Guid StateId)
        {
            try
            {
                var dbList = await _unitOfWork.CityRepository.FirstOrDefaultAsync(
                    x => x.StateId == StateId
                    );
                return ApiResponse<CityEntity>.Success(dbList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> SaveCityAsync(CityEntity entity)
        {
            try
            {
                entity.CityId = Guid.NewGuid();
                await _unitOfWork.CityRepository.AddAsync(entity);
                var result = await _unitOfWork.SaveChangesAsync();

                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> UpdateCityAsync(CityEntity entity)
        {
            try
            {
                var dbResult = await _unitOfWork.CityRepository.FirstOrDefaultAsync(
                x => x.CityId == entity.CityId && x.StateId == entity.StateId
                && x.IsActive == true);

                if (dbResult is null)
                    return ApiResponse<bool>.Fail(1, "City is not found");

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

        public async Task<ApiResponse<bool>> RepopenCityAsync(Guid CityId)
        {
            try
            {
                var dbResult = await _unitOfWork.CityRepository.FirstOrDefaultAsync(x =>
                x.CityId == CityId && x.IsActive == false);

                await _unitOfWork.CityRepository.ReopenAsync(dbResult);

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
