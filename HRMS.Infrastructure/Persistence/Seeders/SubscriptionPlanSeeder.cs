using HRMS.Domain.Entities;
using HRMS.Infrastructure.Persistence.Seeders.Constants;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Infrastructure.Persistence.Seeders
{
    public static class SubscriptionPlanSeeder
    {
        public static async Task SeedAsync(HRMSDbRepoContext context)
        {
            foreach (var menu in SubscriptionPlanConstants.Subsctiption)
            {
                await CreateMenuAsync(context, menu);
            }

            await context.SaveChangesAsync();
        }

        private static async Task CreateMenuAsync(
            HRMSDbRepoContext context,
            SubcriptionPlanModel model)
        {
            

            var existingSubscriptionPlan = await context.SubscriptionPlan
                .FirstOrDefaultAsync(x =>
                    x.PlanName == model.PlanName);


            if (existingSubscriptionPlan == null)
            {
                existingSubscriptionPlan = new SubscriptionPlanEntity
                {
                    SubscriptionPlanId = Guid.NewGuid(),
                    PlanName = model.PlanName,
                    EmployeeLimit = model.EmployeeLimit,
                    DurationInDays = model.DurationInDays,
                };

                await context.SubscriptionPlan.AddAsync(existingSubscriptionPlan);

                await context.SaveChangesAsync();
            }
        }
    }
}
