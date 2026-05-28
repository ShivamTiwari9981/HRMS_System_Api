
using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Infrastructure.Persistence
{
    public class HRMSDbRepoContext : DbContext
    {
        //private readonly Guid _clientId;
        public HRMSDbRepoContext(DbContextOptions<HRMSDbRepoContext> options)
       : base(options) {
            //_clientId = clientService.GetClientId();

        }
        public DbSet<EmployeeEntity> Employee { get; set; }
        public DbSet<ClientEntity> Client { get; set; }
        public DbSet<AttendanceEntity> Attendance { get; set; }
        public DbSet<ErrorLogEntity> ErrorLog { get; set; }
        public DbSet<MasterCodeGenerationEntity> MasterCodeGeneration { get; set; }
        public DbSet<DepartmentEntity> Department { get; set; }
        public DbSet<LeaveEntity> Leave { get; set; }
        public DbSet<MenuEntity> Menu { get; set; }
        public DbSet<PayrollEntity> Payroll { get; set; }
        public DbSet<PermissionEntity> Permission { get; set; }
        public DbSet<RoleEntity> Role { get; set; }
        public DbSet<UserEntity> User { get; set; }
        public DbSet<RolePermissionEntity> RolePermission { get; set; }
        public DbSet<UserRoleEntity> UserRole { get; set; }
        public DbSet<SubscriptionPlanEntity> SubscriptionPlan { get; set; }
        public DbSet<CountryEntity> Country { get; set; }
        public DbSet<StateEntity> State { get; set; }
        public DbSet<CityEntity> City { get; set; }
        public DbSet<EmployeeSalaryEntity> EmployeeSalary { get; set; }
        public DbSet<DesignationEntity> Designation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DesignationEntity>()
                .HasOne(x => x.Department)
                .WithMany(x => x.Designations)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeEntity>()
                .HasOne(x => x.Department)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeEntity>()
                .HasOne(x => x.Designation)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.DesignationId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMSDbRepoContext).Assembly);
        //    ApplyGlobalFilters(modelBuilder);
        //}

        //private void ApplyGlobalFilters(ModelBuilder modelBuilder)
        //{
        //    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //    {
        //        // Check if entity has ClientId
        //        if (typeof(IClientService).IsAssignableFrom(entityType.ClrType))
        //        {
        //            var method = typeof(HRMSDbRepoContext)
        //                .GetMethod(nameof(SetClientFilter), BindingFlags.NonPublic | BindingFlags.Instance)
        //                .MakeGenericMethod(entityType.ClrType);

        //            method.Invoke(this, new object[] { modelBuilder });
        //        }
        //    }
        //}

        //private void SetClientFilter<TEntity>(ModelBuilder modelBuilder)
        //    where TEntity : class, IClientService
        //{
        //    modelBuilder.Entity<TEntity>()
        //        .HasQueryFilter(e => e.ClientId == _clientId);
        //}
    }
}
