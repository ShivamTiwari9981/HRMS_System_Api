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
        public IGenericRepository<ClientEntity> ClientRepository { get; }
        public IGenericRepository<UserEntity> UserRepository { get; }
        public IGenericRepository<UserRoleEntity> UserRoleRepository { get; }
        public IGenericRepository<RoleEntity> RoleRepository { get; }
        public IGenericRepository<PermissionEntity> PerimssionRepository { get; }
        public IGenericRepository<MasterCodeGenerationEntity> MasterCodeGenerationRepository { get; }
        public IGenericRepository<HRMSAppSettingEntity> HRMSAppSettingRepository { get; }
        public IGenericRepository<SubscriptionPlanEntity> SubscriptionPlanRepository { get; }

        public UnitOfWork(HRMSDbRepoContext context)
        {
            _context = context;

            ClientRepository = new GenericRepository<ClientEntity>(_context);
            UserRepository = new GenericRepository<UserEntity>(_context);
            UserRoleRepository = new GenericRepository<UserRoleEntity>(_context);
            MasterCodeGenerationRepository = new GenericRepository<MasterCodeGenerationEntity>(_context);
            RoleRepository = new GenericRepository<RoleEntity>(_context);
            PerimssionRepository = new GenericRepository<PermissionEntity>(_context);
            HRMSAppSettingRepository = new GenericRepository<HRMSAppSettingEntity>(_context);
            SubscriptionPlanRepository = new GenericRepository<SubscriptionPlanEntity>(_context);
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



