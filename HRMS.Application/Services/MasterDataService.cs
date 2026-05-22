using HRMS.Application.DTOs.ResponseDto;
using HRMS.Application.Interfaces;
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
                }).ToList();
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
    }
}
