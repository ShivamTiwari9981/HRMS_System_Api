using System.Linq.Expressions;

namespace HRMS.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        // Read
        Task<T?> GetByIdAsync(object id);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        // Write

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        void Update(T entity);

        Task SoftDeleteAsync(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        // Query (optional advanced)
        IQueryable<T> Query(); // ⚠️ use carefully
    }
}
