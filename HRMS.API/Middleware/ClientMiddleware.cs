using HRMS.Application.Interfaces;
using HRMS.Domain.Interfaces;
namespace HRMS.API.Middleware
{
    public class ClientMiddleware
    {
        private readonly RequestDelegate _next;

        public ClientMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUnitOfWork uow, ICurrentUserService currentUser)
        {

            //if (currentUser.ClientId != null)
            //{
            //    var client = await uow.ClientRepository.GetByIdAsync(currentUser.ClientId.Value);

            //    if (client == null)
            //    {
            //        context.Response.StatusCode = 401;
            //        await context.Response.WriteAsync("Invalid tenant");
            //        return;
            //    }

            //    context.Items["Client"] = client; 
            //}

            await _next(context);
        }
    }
}
