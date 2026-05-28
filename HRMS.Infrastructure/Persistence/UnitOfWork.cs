using HRMS.Application.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Domain.Interfaces;
using HRMS.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Data.Common;

namespace HRMS.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly HRMSDbRepoContext _context;
        private IDbContextTransaction _transaction;
        private ICurrentUserService _currentService;
        public IGenericRepository<ClientEntity> ClientRepository { get; }
        public IGenericRepository<UserEntity> UserRepository { get; }
        public IGenericRepository<UserRoleEntity> UserRoleRepository { get; }
        public IGenericRepository<RoleEntity> RoleRepository { get; }
        public IGenericRepository<PermissionEntity> PerimssionRepository { get; }
        public IGenericRepository<MasterCodeGenerationEntity> MasterCodeGenerationRepository { get; }
        public IGenericRepository<HRMSAppSettingEntity> HRMSAppSettingRepository { get; }
        public IGenericRepository<SubscriptionPlanEntity> SubscriptionPlanRepository { get; }
        public IGenericRepository<DepartmentEntity> DepartmentRepository { get; }
        public IGenericRepository<CountryEntity> CountryRepository { get; }
        public IGenericRepository<StateEntity> StateRepository { get; }
        public IGenericRepository<CityEntity> CityRepository { get; }
        public IGenericRepository<EmployeeSalaryEntity> EmployeeSalaryRepository { get; }
        public IGenericRepository<DesignationEntity> DesignationRepository { get; }
        public UnitOfWork(HRMSDbRepoContext context , ICurrentUserService currentUser)
        {
            _context = context;
            _currentService = currentUser;

            ClientRepository = new GenericRepository<ClientEntity>(_context,_currentService);
            UserRepository = new GenericRepository<UserEntity>(_context, _currentService);
            UserRoleRepository = new GenericRepository<UserRoleEntity>(_context, _currentService);
            MasterCodeGenerationRepository = new GenericRepository<MasterCodeGenerationEntity>(_context, _currentService);
            RoleRepository = new GenericRepository<RoleEntity>(_context, _currentService);
            PerimssionRepository = new GenericRepository<PermissionEntity>(_context, _currentService);
            HRMSAppSettingRepository = new GenericRepository<HRMSAppSettingEntity>(_context, _currentService);
            SubscriptionPlanRepository = new GenericRepository<SubscriptionPlanEntity>(_context, _currentService);
            DepartmentRepository = new GenericRepository<DepartmentEntity>(_context, _currentService);
            CountryRepository = new GenericRepository<CountryEntity>(_context, _currentService);
            StateRepository = new GenericRepository<StateEntity>(_context, _currentService);
            CityRepository = new GenericRepository<CityEntity>(_context, _currentService);
            EmployeeSalaryRepository = new GenericRepository<EmployeeSalaryEntity>(_context, _currentService);
            DesignationRepository = new GenericRepository<DesignationEntity>(_context, _currentService);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_transaction != null)
                return _transaction;

            _transaction = await _context.Database.BeginTransactionAsync();
            return _transaction;
        }
        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();

                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public DbConnection GetConnection()
        {
            return _context.Database.GetDbConnection();
        }

        // ✅ Get Current Transaction
        public DbTransaction GetTransaction()
        {
            return _transaction?.GetDbTransaction();
        }

        // ✅ Get Connection String
        public string GetConnectionString()
        {
            return _context.Database.GetConnectionString();
        }


        
    }
}



