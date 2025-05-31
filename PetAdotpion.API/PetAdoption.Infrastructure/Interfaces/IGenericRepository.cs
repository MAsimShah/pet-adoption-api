using System.Linq.Expressions;

namespace PetAdoption.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task SaveAsync();

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task AddRangeWithSaveAsync(IEnumerable<T> entities);

        Task AddWithSaveAsync(T entity);

        void Update(T entity);

        Task UpdateWithSaveAsync(T entity);

        void Delete(T entity);

        Task DeleteWithSaveAsync(T entity);

        void DeleteRange(IEnumerable<T> entities);

        Task DeleteRangeWithSaveAsync(IEnumerable<T> entities);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate, List<string> thenIncludes, params Expression<Func<T, object>>[] includes);

        Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        Task<int> Count(Expression<Func<T, bool>> predicate = null);
    }
}