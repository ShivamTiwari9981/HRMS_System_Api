using HRMS.Domain.Entities;
using HRMS.Infrastructure.Persistence.Seeders.Constants;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Infrastructure.Persistence.Seeders
{
    public static class MasterIdGenerationSeeder
    {
        public static async Task SeedAsync(HRMSDbRepoContext context)
        {
            foreach (var model in MasterIdGenerationConstaints.MasterIdGeneration)
            {
                await CreateMenuAsync(context, model);
            }

            await context.SaveChangesAsync();
        }

        private static async Task CreateMenuAsync(
            HRMSDbRepoContext context,
            MasterIdGenerationModel model)
        {


            var existingMasterCodeGeneration = await context.MasterCodeGeneration
                .FirstOrDefaultAsync(x =>
                    x.TableName == model.TableName);


            if (existingMasterCodeGeneration == null)
            {
                existingMasterCodeGeneration = new MasterCodeGenerationEntity
                {
                    MasterCodeGenerationId = Guid.NewGuid(),
                    TableName = model.TableName,
                    ClientId = model.ClientId,
                    Prefix = model.Prefix,
                    LastNumber = model.LastNumber,
                };

                await context.MasterCodeGeneration.AddAsync(existingMasterCodeGeneration);

                await context.SaveChangesAsync();
            }
        }
    }
}
