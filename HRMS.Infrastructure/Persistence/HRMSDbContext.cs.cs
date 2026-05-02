using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace HRMS.Infrastructure.Persistence
{
    public class HRMSDbContext:DbContext
    {
        public HRMSDbContext(DbContextOptions<HRMSDbContext> options)
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example: Unique constraint
            modelBuilder.Entity<EmployeeEntity>()
                .HasIndex(x => x.EmployeeCode)
                .IsUnique();

            modelBuilder.Entity<EmployeeEntity>()
               .HasIndex(x => x.Email)
               .IsUnique();

            modelBuilder.Entity<EmployeeEntity>()
               .HasIndex(x => x.Phone)
               .IsUnique();


            modelBuilder.Entity<UserEntity>()
               .HasIndex(x => x.UserName)
               .IsUnique();

            modelBuilder.Entity<UserEntity>()
               .HasIndex(x => x.Email)
               .IsUnique();

            modelBuilder.Entity<UserEntity>()
              .HasIndex(x => x.UserCode)
              .IsUnique();

            modelBuilder.Entity<DepartmentEntity>()
               .HasIndex(x => x.DepartmentCode)
               .IsUnique();

            modelBuilder.Entity<DepartmentEntity>()
               .HasIndex(x => x.DepartmentName)
               .IsUnique();

            modelBuilder.Entity<ClientEntity>()
               .HasIndex(x => x.ClientCode)
               .IsUnique();

            modelBuilder.Entity<ClientEntity>()
               .HasIndex(x => x.ClientName)
               .IsUnique();

            modelBuilder.Entity<ClientEntity>()
               .HasIndex(x => x.CompanyName)
               .IsUnique();

            modelBuilder.Entity<ClientEntity>()
               .HasIndex(x => x.Email)
               .IsUnique();

            modelBuilder.Entity<ClientEntity>()
              .HasIndex(x => x.Phone)
              .IsUnique();

            modelBuilder.Entity<MenuEntity>()
              .HasIndex(x => x.MenuName)
              .IsUnique();
        }
    }
}
