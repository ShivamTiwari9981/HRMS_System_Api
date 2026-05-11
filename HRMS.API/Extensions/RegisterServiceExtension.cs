using FluentValidation;
using HRMS.Application.Interfaces;
using HRMS.Application.Mapper;
using HRMS.Application.Services;
using HRMS.Domain.Interfaces;
using HRMS.Infrastructure.Persistence;
using HRMS.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
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

            //#region RegisterAllValidator
            
            //services.AddValidatorsFromAssembly(typeof(AttendanceValidator).Assembly);
            //services.AddValidatorsFromAssembly(typeof(ClientDtoValidator).Assembly);
            //services.AddValidatorsFromAssembly(typeof(DepartmentDtoValidator).Assembly);
            //services.AddValidatorsFromAssembly(typeof(EmployeeDtoValidator).Assembly);
            //services.AddValidatorsFromAssembly(typeof(LeaveDtoValidator).Assembly);
            //services.AddValidatorsFromAssembly(typeof(MasterCodeGenerationDtoValidator).Assembly);
            //services.AddValidatorsFromAssembly(typeof(MenuDtoValidator).Assembly);
            //services.AddValidatorsFromAssembly(typeof(PayrollDtoValidator).Assembly);
            //services.AddValidatorsFromAssembly(typeof(PermissionDtoValidator).Assembly);
            //services.AddValidatorsFromAssembly(typeof(RoleDtoValidator).Assembly);
            //services.AddValidatorsFromAssembly(typeof(UserDtoValidator).Assembly);
            //services.AddValidatorsFromAssembly(typeof(SignupBusinessValidator).Assembly);
            //#endregion
        }
    }
}
