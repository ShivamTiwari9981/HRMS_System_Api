using HRMS.API.Middleware;

namespace HRMS.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseApplicationMiddlewares(this IApplicationBuilder app)
        {
            //app.UseMiddleware<ExceptionMiddleware>();
            //app.UseMiddleware<RequestResponseLoggingMiddleware>();
            //app.UseAuthentication();
            //app.UseMiddleware<ClientMiddleware>();
            //app.UseMiddleware<SubscriptionMiddleware>();
            //app.UseMiddleware<ProfileCompletionMiddleware>();
            //app.UseMiddleware<PermissionMiddleware>();

            app.UseAuthorization();

            return app;
        }
    }
}
