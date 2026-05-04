using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Infrastructure.Persistence
{
    public class HRMSDbRepoContext : DbContext
    {
        public HRMSDbRepoContext(DbContextOptions<HRMSDbRepoContext> options)
       : base(options) { }


        public DbSet<EmployeeEntity> Employee { get; set; }
        public DbSet<ClientEntity> Client { get; set; }
        public DbSet<AttendanceEntity> Attendance { get; set; }
        public DbSet<MasterCodeGenerationEntity> MasterCodeGeneration { get; set; }
        public DbSet<DepartmentEntity> Department { get; set; }
        public DbSet<LeaveEntity> Leave { get; set; }
        public DbSet<MenuEntity> Menu { get; set; }
        public DbSet<PayrollEntity> Payroll { get; set; }
        public DbSet<PermissionEntity> Permission { get; set; }
        public DbSet<RoleEntity> Role { get; set; }
        public DbSet<UserEntity> User { get; set; }
        public DbSet<MenuPermissionMappingEntity> MenuPermissionMapping { get; set; }
        public DbSet<RolePermissionEntity> RolePermission { get; set; }
        public DbSet<UserRoleEntity> UserRole { get; set; }

    }
}
