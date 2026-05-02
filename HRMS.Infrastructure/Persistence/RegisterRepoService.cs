using HRMS.Application.Helper;
using HRMS.Application.Interfaces;
using HRMS.Application.Services;
using HRMS.Domain.Interfaces;
using HRMS.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Persistence
{
    public static class RegisterRepoService
    {
        public static void RegisterService(IServiceCollection services)
        {
            services.AddScoped<ICurrentSession, CurrentSession>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
