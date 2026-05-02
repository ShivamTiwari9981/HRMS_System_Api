using HRMS.Domain.Entities;
using System.Data.Common;

namespace HRMS.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

        void Dispose();

        DbConnection GetConnection();

        DbTransaction GetTransaction();

        string GetConnectionString();


        IGenericRepository<ClientEntity> ClientRepository { get; }
        IGenericRepository<AttendanceEntity> AttendanceRepository { get; }
        IGenericRepository<DepartmentEntity> DepartmentRepository { get; }
        IGenericRepository<EmployeeEntity> EmployeeRepository { get; }
        IGenericRepository<LeaveEntity> LeaveRepository { get; }
        IGenericRepository<MasterCodeGenerationEntity> MasterCodeGenerationRepository { get; }
        IGenericRepository<MenuEntity> MenuRepository { get; }
        IGenericRepository<PermissionEntity> PermissionRepository { get; }
        IGenericRepository<RoleEntity> RoleRepository { get; }
        IGenericRepository<UserEntity> UserRepository { get; }
    }
}
