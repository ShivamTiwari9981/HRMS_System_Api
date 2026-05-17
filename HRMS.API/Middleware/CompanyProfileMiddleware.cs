
namespace HRMS.API.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CompanyProfileMiddleware
    {
        private readonly RequestDelegate _next;

        public CompanyProfileMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.User.Identity?.IsAuthenticated == true)
            {
                var path = httpContext.Request.Path.Value?.ToLower();

                // Skip these routes
                var excludedPaths = new[]
                {
                "/registerclient",
                "/login",
                "/swagger"
            };

                bool isExcluded = excludedPaths.Any(x => path != null && path.StartsWith(x));

                if (!isExcluded)
                {
                    var claimValue = httpContext.User.FindFirst("IsCompanyProfileCreated")?.Value;

                    if (claimValue == "0")
                    {
                        httpContext.Response.Redirect("/RegisterClient");
                        //return;
                    }
                }
            }
                return _next(httpContext);
        }
    }
}
