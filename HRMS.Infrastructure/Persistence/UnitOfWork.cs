using HRMS.Domain.Entities;
using HRMS.Domain.Interfaces;
using HRMS.Infrastructure.Models;
using HRMS.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace HRMS.Infrastructure.Persistence
{
    public class UnitOfWork :IDisposable, IUnitOfWork
    {

        private IDbContextTransaction _transaction;
        private readonly HRMSDbContext _repoContext;
        private readonly ICurrentSession _currentSession;

        public UnitOfWork(HRMSDbContext context, ICurrentSession currentSession)
        {
            _repoContext = context;
            _currentSession = currentSession;
        }

        public async Task<bool> SaveChangesAsync()
        {
            var result = false;
            try
            {
                int saveResult = await _repoContext.SaveChangesAsync();
                result = (saveResult == 1) ? true : false;
            }
            catch (Exception e) // Changed from DbEntityValidationException to Exception
            {
                // Optionally log or handle validation errors here if needed
                throw;
            }
            return result;
        }



        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
                return;

            _transaction = await _repoContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _repoContext.SaveChangesAsync();
                await _transaction?.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            await _transaction?.RollbackAsync();
            await DisposeTransactionAsync();
        }

        private async Task DisposeTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _repoContext.Dispose();
        }



        public DbConnection GetConnection()
        {
            return _repoContext.Database.GetDbConnection();
        }

        public DbTransaction GetTransaction()
        {
            return _repoContext.Database.CurrentTransaction.GetDbTransaction();
        }

        public string GetConnectionString()
        {
            return _repoContext.Database.GetDbConnection().ConnectionString;
        }

        IGenericRepository<ClientEntity> _clientRepository;
        public IGenericRepository<ClientEntity> ClientRepository
        {
            get
            {
                if (_clientRepository == null)
                {
                    _clientRepository = new GenericRepository<ClientEntity>(_repoContext, _currentSession);
                }
                return _clientRepository;
            }
        }

        IGenericRepository<AttendanceEntity> _attendanceRepository;
        public IGenericRepository<AttendanceEntity> AttendanceRepository
        {
            get
            {
                if (_attendanceRepository == null)
                {
                    _attendanceRepository = new GenericRepository<AttendanceEntity>(_repoContext, _currentSession);
                }
                return _attendanceRepository;
            }
        }


        IGenericRepository<DepartmentEntity> _departmentRepository;
        public IGenericRepository<DepartmentEntity> DepartmentRepository
        {
            get
            {
                if (_departmentRepository == null)
                {
                    _departmentRepository = new GenericRepository<DepartmentEntity>(_repoContext, _currentSession);
                }
                return _departmentRepository;
            }
        }


        IGenericRepository<EmployeeEntity> _employeeRepository;
        public IGenericRepository<EmployeeEntity> EmployeeRepository
        {
            get
            {
                if (_employeeRepository == null)
                {
                    _employeeRepository = new GenericRepository<EmployeeEntity>(_repoContext, _currentSession);
                }
                return _employeeRepository;
            }
        }


        IGenericRepository<LeaveEntity> _leaveRepository;
        public IGenericRepository<LeaveEntity> LeaveRepository
        {
            get
            {
                if (_leaveRepository == null)
                {
                    _leaveRepository = new GenericRepository<LeaveEntity>(_repoContext, _currentSession);
                }
                return _leaveRepository;
            }
        }


        IGenericRepository<MasterCodeGenerationEntity> _masterCodeGeneration;
        public IGenericRepository<MasterCodeGenerationEntity> MasterCodeGenerationRepository
        {
            get
            {
                if (_masterCodeGeneration == null)
                {
                    _masterCodeGeneration = new GenericRepository<MasterCodeGenerationEntity>(_repoContext, _currentSession);
                }
                return _masterCodeGeneration;
            }
        }


        IGenericRepository<MenuEntity> _menuRepository;
        public IGenericRepository<MenuEntity> MenuRepository
        {
            get
            {
                if (_menuRepository == null)
                {
                    _menuRepository = new GenericRepository<MenuEntity>(_repoContext, _currentSession);
                }
                return _menuRepository;
            }
        }


        IGenericRepository<PayrollEntity> _payrollRepository;
        public IGenericRepository<PayrollEntity> PayrollRepository
        {
            get
            {
                if (_payrollRepository == null)
                {
                    _payrollRepository = new GenericRepository<PayrollEntity>(_repoContext, _currentSession);
                }
                return _payrollRepository;
            }
        }

        IGenericRepository<PermissionEntity> _permissionRepository;
        public IGenericRepository<PermissionEntity> PermissionRepository
        {
            get
            {
                if (_permissionRepository == null)
                {
                    _permissionRepository = new GenericRepository<PermissionEntity>(_repoContext, _currentSession);
                }
                return _permissionRepository;
            }
        }

        IGenericRepository<RoleEntity> _roleRepository;
        public IGenericRepository<RoleEntity> RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository = new GenericRepository<RoleEntity>(_repoContext, _currentSession);
                }
                return _roleRepository;
            }
        }

        IGenericRepository<UserEntity> _userRepository;
        public IGenericRepository<UserEntity> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new GenericRepository<UserEntity>(_repoContext, _currentSession);
                }
                return _userRepository;
            }
        }

    }
}
