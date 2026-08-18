using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace HRMS.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<ClientEntity> ClientRepository { get; }
        IGenericRepository<UserEntity> UserRepository { get; }
        IGenericRepository<UserRoleEntity> UserRoleRepository { get; }
        IGenericRepository<MasterCodeGenerationEntity> MasterCodeGenerationRepository { get; }
        IGenericRepository<RoleEntity> RoleRepository { get; }
        IGenericRepository<PermissionEntity> PerimssionRepository { get; }
        IGenericRepository<HRMSAppSettingEntity> HRMSAppSettingRepository { get; }
        IGenericRepository<SubscriptionPlanEntity> SubscriptionPlanRepository { get; }
        IGenericRepository<DepartmentEntity> DepartmentRepository { get; }
        IGenericRepository<CountryEntity> CountryRepository { get; }
        IGenericRepository<StateEntity> StateRepository { get; }
        IGenericRepository<CityEntity> CityRepository { get; }
        IGenericRepository<EmployeeSalaryEntity> EmployeeSalaryRepository { get; }
        IGenericRepository<DesignationEntity> DesignationRepository { get; }
        IGenericRepository<LeaveTypeEntity> LeaveTypeRepository { get; }
        IGenericRepository<LeaveBalanceEntity> LeaveBalanceRepository { get; }
        IGenericRepository<EmployeeEntity> EmployeeRepository { get; }
        Task<bool> SaveChangesAsync();
        Task<IDbContextTransaction>BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

        //void Dispose();

        DbConnection GetConnection();

        DbTransaction GetTransaction();

        string GetConnectionString();
    }
}
