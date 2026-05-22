using HRMS.Shared.Helpers;
using System.ComponentModel.DataAnnotations;
namespace HRMS.Infrastructure.Persistence.Seeders.Constants
{
    public static class SubscriptionPlanConstants
    {
        public static readonly List<SubcriptionPlanModel> Subsctiption = new()
        {
            new SubcriptionPlanModel
            {
                PlanName = "Free",
                Price = 0,
                DurationInDays = 7,
                EmployeeLimit = 10,
            },
            new SubcriptionPlanModel
            {
                PlanName = "Starter",
                Price = 999,
                DurationInDays = 30,
                EmployeeLimit = 25,
            },
            new SubcriptionPlanModel
            {
                PlanName = "Basic",
                Price = 2499,
                DurationInDays = 30,
                EmployeeLimit = 100,
            },

             new SubcriptionPlanModel
            {
                PlanName = "Professional",
                Price = 4999,
                DurationInDays = 30,
                EmployeeLimit = 500,
            },
              new SubcriptionPlanModel
            {
                PlanName = "Enterprise",
                Price = 9999,
                DurationInDays = 365,
                EmployeeLimit = 999999,
            },


        };
    }
    public class SubcriptionPlanModel
    {
        public string PlanName { get; set; }

        public decimal Price { get; set; }

        [Required]
        public int EmployeeLimit { get; set; } = 0;

        [Required]
        public int DurationInDays { get; set; } = 0;

        public bool? IsActive { get; set; } = true;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid CreatedBy { get; set; } = SystemUser.DefaultSystemUser;

    }
}
