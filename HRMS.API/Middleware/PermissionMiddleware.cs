using HRMS.Application.Interfaces;
using HRMS.Domain.Interfaces;

namespace HRMS.API.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUnitOfWork uow, ICurrentUserService currentUser)
        {
            //var endpoint = context.GetEndpoint();
            //var permission = endpoint?.Metadata.GetMetadata<RequirePermissionAttribute>();

            //if (permission != null)
            //{
            //    var hasPermission = await uow.UserRoleRepository
            //        .FindAnyAsync(x => x.UserId == currentUser.UserId);

            //    if (!hasPermission)
            //    {
            //        context.Response.StatusCode = 403;
            //        await context.Response.WriteAsync("Permission denied");
            //        return;
            //    }
            //}

            await _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class PermissionMiddlewareExtensions
    {
        public static IApplicationBuilder UsePermissionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PermissionMiddleware>();
        }
    }
}
