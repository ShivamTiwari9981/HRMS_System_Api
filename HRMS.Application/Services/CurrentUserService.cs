using HRMS.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _http;

        public CurrentUserService(IHttpContextAccessor http)
        {
            _http = http;
        }

        public Guid? UserId =>
            Guid.TryParse(_http.HttpContext?.User?.FindFirst("userId")?.Value, out var id) ? id : null;

        public Guid? ClientId =>
            Guid.TryParse(_http.HttpContext?.User?.FindFirst("clientId")?.Value, out var id) ? id : null;
    }
}
