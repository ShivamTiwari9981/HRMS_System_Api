using FluentValidation;
using HRMS.Application.Helper;
using HRMS.Application.Interfaces;
using HRMS.Application.Validators;
using Microsoft.Extensions.DependencyInjection;
namespace HRMS.Application.Services
{
    public static class RegisterServices
    {
        public static void RegisterService(IServiceCollection services)
        {
            #region RegisterAllService
            services.AddScoped<IUtilityService, UtilityService>();
            services.AddScoped<IHelperFactory, HelperFactory>();
            #endregion



            #region RegisterAllValidator
            services.AddValidatorsFromAssembly(typeof(AttendanceValidator).Assembly);
            services.AddValidatorsFromAssembly(typeof(ClientDtoValidator).Assembly);
            services.AddValidatorsFromAssembly(typeof(DepartmentDtoValidator).Assembly);
            services.AddValidatorsFromAssembly(typeof(EmployeeDtoValidator).Assembly);
            services.AddValidatorsFromAssembly(typeof(LeaveDtoValidator).Assembly);
            services.AddValidatorsFromAssembly(typeof(MasterCodeGenerationDtoValidator).Assembly);
            services.AddValidatorsFromAssembly(typeof(MenuDtoValidator).Assembly);
            services.AddValidatorsFromAssembly(typeof(PayrollDtoValidator).Assembly);
            services.AddValidatorsFromAssembly(typeof(PermissionDtoValidator).Assembly);
            services.AddValidatorsFromAssembly(typeof(RoleDtoValidator).Assembly);
            services.AddValidatorsFromAssembly(typeof(UserDtoValidator).Assembly);
            #endregion


        }
    }
}
