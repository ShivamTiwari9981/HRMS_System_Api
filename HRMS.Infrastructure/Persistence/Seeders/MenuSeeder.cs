using HRMS.Domain.Entities;
using HRMS.Domain.Enums;
using HRMS.Infrastructure.Persistence.Seeders.Constants;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Infrastructure.Persistence.Seeders
{
    public static class MenuSeeder
    {
        public static async Task SeedAsync(HRMSDbRepoContext context)
        {
            foreach (var menu in MenuConstants.Menus)
            {
                await CreateMenuAsync(context, menu, null);
            }

            await context.SaveChangesAsync();
        }

        private static async Task CreateMenuAsync(
            HRMSDbRepoContext context,
            MenuSeedModel model,
            Guid? parentMenuId)
        {
            //-------------------------------------------------
            // CHECK MENU EXISTS
            //-------------------------------------------------

            var existingMenu = await context.Menu
                .FirstOrDefaultAsync(x =>
                    x.MenuName == model.MenuName &&
                    x.ParentMenuId == parentMenuId);

            //-------------------------------------------------
            // CREATE MENU
            //-------------------------------------------------

            if (existingMenu == null)
            {
                existingMenu = new MenuEntity
                {
                    MenuId = Guid.NewGuid(),
                    ParentMenuId = parentMenuId,
                    MenuName = model.MenuName,
                    MenuIcon = model.MenuIcon,
                    RouterLink = model.RouterLink,
                    DisplayOrder = model.DisplayOrder,
                    IsVisible = true,
                    MenuType = Enum.Parse<MenuType>(model.MenuType)
                };

                await context.Menu.AddAsync(existingMenu);

                await context.SaveChangesAsync();
            }

            //-------------------------------------------------
            // CREATE CHILD MENUS
            //-------------------------------------------------

            if (model.Children != null && model.Children.Any())
            {
                foreach (var child in model.Children)
                {
                    await CreateMenuAsync(
                        context,
                        child,
                        existingMenu.MenuId);
                }
            }
        }
    }
}
