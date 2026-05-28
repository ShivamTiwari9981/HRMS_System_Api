using HRMS.Application.Interfaces;
using HRMS.Application.Services;
using HRMS.Domain.Interfaces;
using HRMS.Infrastructure.Persistence;
using HRMS.Infrastructure.Repositories;
using HRMS.Shared.Configuration;
namespace HRMS.API.Extensions
{
    public static class RegisterServicesExtension
    {
        public static void RegisterService(IServiceCollection services, IConfiguration configuration)
        {
            #region RegisterAllService
            services.AddScoped<IUtilityService, UtilityService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUtilityService, UtilityService>();
            services.AddScoped<IRedisCacheService, RedisCacheService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IOTPService, OTPService>();
            services.AddScoped<IMasterDataService, MasterDataService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            


            #endregion

            #region RepoServiceRegister
            //services.AddScoped<ICurrentSession, CurrentSession>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion


            #region RegisterConfig
            services.Configure<LoggingSettings>(
            configuration.GetSection("LoggingSettings"));

            services.Configure<EmailSettings>(
            configuration.GetSection("EmailSettings"));

            services.Configure<RedisSettings>(
            configuration.GetSection("JWTConnectionStrings"));
            #endregion
        }
    }
}
