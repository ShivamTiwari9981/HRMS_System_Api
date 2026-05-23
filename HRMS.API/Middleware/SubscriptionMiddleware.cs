using HRMS.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HRMS.API.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SubscriptionMiddleware
    {
        private readonly RequestDelegate _next;

        public SubscriptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var client = context.Items["Client"] as ClientEntity;

            if (client != null && client.SubscriptionEndDate < DateTime.UtcNow)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Subscription expired");
                return;
            }

            await _next(context);
        }
    }
}
