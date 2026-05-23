using System.ComponentModel.DataAnnotations;

namespace HRMS.Application.DTOs.ResponseDto
{
    public class SubscriptionPlanResponseDto
    {
        public Guid SubscriptionPlanId { get; set; }
        public string PlanName { get; set; }

        public decimal Price { get; set; }
        public int EmployeeLimit { get; set; } = 0;
        public int DurationInDays { get; set; } = 0;
    }
}
