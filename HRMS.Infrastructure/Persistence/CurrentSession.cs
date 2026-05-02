using HRMS.Domain.Interfaces;
using HRMS.Shared.Constants;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HRMS.Infrastructure.Persistence
{
    public class CurrentSession : ICurrentSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? User =>
            _httpContextAccessor.HttpContext?.User;

        public string ClientId =>
            User?.FindFirst(Global.ClaimTypes.ClientId)?.Value ?? string.Empty;

        public string UserId =>
            User?.FindFirst(Global.ClaimTypes.UserId)?.Value ?? string.Empty;

        public string UserName =>
            User?.FindFirst(Global.ClaimTypes.UserName)?.Value ?? string.Empty;

        public string RoleName =>
            User?.FindFirst(Global.ClaimTypes.RoleName)?.Value ?? string.Empty;

        public bool IsAuthenticated =>
            User?.Identity?.IsAuthenticated ?? false;
    }
}
