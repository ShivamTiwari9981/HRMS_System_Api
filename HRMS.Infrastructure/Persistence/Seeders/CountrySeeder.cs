using HRMS.Domain.Entities;
using HRMS.Infrastructure.Persistence.Seeders.Constants;
using Microsoft.EntityFrameworkCore;
namespace HRMS.Infrastructure.Persistence.Seeders
{
    public static class CountrySeeder
    {
        public static async Task SeedAsync(HRMSDbRepoContext context)
        {
            foreach (var country in CountryConstants.modelList)
            {
                await CreateCountryAsync(context, country);
            }

            await context.SaveChangesAsync();
        }

        private static async Task CreateCountryAsync(
            HRMSDbRepoContext context,
            CountryModel model)
        {


            var existingSubscriptionPlan = await context.Country
                .FirstOrDefaultAsync(x =>
                    x.CountryName == model.CountryName);


            if (existingSubscriptionPlan == null)
            {
                existingSubscriptionPlan = new CountryEntity
                {
                    CountryId = Guid.NewGuid(),
                    CountryName = model.CountryName,
                };

                await context.Country.AddAsync(existingSubscriptionPlan);

                await context.SaveChangesAsync();
            }
        }
    }
}
