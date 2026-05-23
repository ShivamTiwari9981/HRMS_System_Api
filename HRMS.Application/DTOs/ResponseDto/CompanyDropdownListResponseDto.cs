using HRMS.Shared.Dto;


namespace HRMS.Application.DTOs.ResponseDto
{
    public class CompanyDropdownListResponseDto
    {
        public List<SubscriptionPlanResponseDto> SubscriptionPlans { get;set;}
        public List<EnumDto> CompanyTypes { get;set;}
    }
}
