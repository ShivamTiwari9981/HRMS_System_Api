using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace HRMS.Domain.Interfaces
{
    public interface IUnitOfWork
    {
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
