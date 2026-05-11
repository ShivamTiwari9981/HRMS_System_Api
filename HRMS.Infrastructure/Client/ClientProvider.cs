
using HRMS.Application.Interfaces;
using HRMS.Shared.Constants;
using Microsoft.AspNetCore.Http;

namespace HRMS.Infrastructure.Client
{
    public class ClientProvider 
    {
        private readonly IHttpContextAccessor _httpContext;
        public ClientProvider(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public Guid ClientId => throw new NotImplementedException();

        public Guid GetClientId()
        {
            return Guid.Parse(
                _httpContext.HttpContext.User.FindFirst(Global.ClientId).Value
            );
        }
    }
}
