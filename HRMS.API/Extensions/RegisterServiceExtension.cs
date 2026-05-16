using HRMS.Application.Interfaces;
using HRMS.Application.Services;
using HRMS.Domain.Interfaces;
using HRMS.Infrastructure.Persistence;
using HRMS.Infrastructure.Repositories;
namespace HRMS.API.Extensions
{
    public static class RegisterServicesExtension
    {
        public static void RegisterService(IServiceCollection services)
        {
            #region RegisterAllService
            services.AddScoped<IUtilityService, UtilityService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUtilityService, UtilityService>();


            #endregion

            #region RepoServiceRegister
            //services.AddScoped<ICurrentSession, CurrentSession>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion

            

        }
    }
}
