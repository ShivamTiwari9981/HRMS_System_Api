namespace HRMS.Infrastructure.Persistence.Seeders
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(HRMSDbRepoContext context)
        {
            await MenuSeeder.SeedAsync(context);

            // Future
            // await RoleSeeder.SeedAsync(context);
            // await PermissionSeeder.SeedAsync(context);
        }
    }
}
