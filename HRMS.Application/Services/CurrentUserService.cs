using HRMS.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using static HRMS.Shared.Constants.Global;

namespace HRMS.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid ClientId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?
                    .User?
                    .FindFirst("ClientId")?.Value;

                return string.IsNullOrEmpty(value)
                    ? Guid.Empty
                    : Guid.Parse(value);
            }
        }

        public Guid UserId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?
                    .User?
                    .FindFirst(Claim_Types.UserId)?.Value;

                return string.IsNullOrEmpty(value)
                    ? Guid.Empty
                    : Guid.Parse(value);
            }
        }

        public Guid RoleId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?
                    .User?
                    .FindFirst(Claim_Types.RoleIdKey)?.Value;

                return string.IsNullOrEmpty(value)
                    ? Guid.Empty
                    : Guid.Parse(value);
            }
        }

        public string ClientKey
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?
                    .User?
                    .FindFirst(Claim_Types.ClientKey)?.Value;

                return string.IsNullOrEmpty(value)
                    ? string.Empty
                    : value;
            }
        }

    //public int RoleId
    //    {
    //        get
    //        {
    //            var value = _httpContextAccessor.HttpContext?
    //                .User?
    //                .FindFirst(Claim_Types.RoleIdKey)?.Value;

    //            return Convert.ToInt32(value)
    //                ? 0
    //                : value;
    //        }
    //    }
    //}
}
