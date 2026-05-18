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
        private readonly ICurrentUserService _currentUser;
        private IDbContextTransaction _transaction;
        public IGenericRepository<ClientEntity> ClientRepository { get; }
        public IGenericRepository<UserEntity> UserRepository { get; }
        public IGenericRepository<UserRoleEntity> UserRoleRepository { get; }
        public IGenericRepository<RoleEntity> RoleRepository { get; }
        public IGenericRepository<PermissionEntity> PerimssionRepository { get; }
        public IGenericRepository<MasterCodeGenerationEntity> MasterCodeGenerationRepository { get; }
        public IGenericRepository<HRMSAppSettingEntity> HRMSAppSettingRepository { get; }

        public UnitOfWork(HRMSDbRepoContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;

            ClientRepository = new GenericRepository<ClientEntity>(_context, _currentUser);
            UserRepository = new GenericRepository<UserEntity>(_context, _currentUser);
            UserRoleRepository = new GenericRepository<UserRoleEntity>(_context, _currentUser);
            MasterCodeGenerationRepository = new GenericRepository<MasterCodeGenerationEntity>(_context, _currentUser);
            RoleRepository = new GenericRepository<RoleEntity>(_context, _currentUser);
            PerimssionRepository = new GenericRepository<PermissionEntity>(_context, _currentUser);
            HRMSAppSettingRepository = new GenericRepository<HRMSAppSettingEntity>(_context, _currentUser);
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



