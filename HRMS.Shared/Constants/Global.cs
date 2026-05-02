using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Shared.Constants
{
    public static class Global
    {
        public static class ClaimTypes
        {
            public static string ClientId = "ClientId";
            public static string UserId = "UserId";
            public static string UserName = "UserName";
            public static string RoleName = "RoleName";
            public static string IsAuthenticated = "IsAuthenticated";
        }
    }
}
