using Microsoft.EntityFrameworkCore;
using PetAdoption.Application.DTO;
using PetAdoption.Infrastructure.DbContextApp;
using PetAdoption.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace PetAdoption.Infrastructure.Repositories
{
    public class GenericRepository<T>(AppDbContext dBContext) : IGenericRepository<T> where T : class
    {
        public async Task SaveAsync()
        {
            try
            {
                await dBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                await dBContext.Set<T>().AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                await dBContext.Set<T>().AddRangeAsync(entities);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddRangeWithSaveAsync(IEnumerable<T> entities)
        {
            try
            {
                await dBContext.Set<T>().AddRangeAsync(entities);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddWithSaveAsync(T entity)
        {
            try
            {
                await AddAsync(entity);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(T entity)
        {
            try
            {
                dBContext.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateWithSaveAsync(T entity)
        {
            try
            {
                Update(entity);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                dBContext.Set<T>().Remove(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteWithSaveAsync(T entity)
        {
            try
            {
                Delete(entity);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            try
            {
                dBContext.Set<T>().RemoveRange(entities);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteRangeWithSaveAsync(IEnumerable<T> entities)
        {
            try
            {
                DeleteRange(entities);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await dBContext.Set<T>().FirstOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => await dBContext.Set<T>().AnyAsync(predicate);

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {

            try
            {
                var query = dBContext.Set<T>().AsQueryable<T>();

                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                return await query.FirstOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, List<string> thenIncludes, params Expression<Func<T, object>>[] includes)
        {

            try
            {
                var query = dBContext.Set<T>().AsQueryable<T>();

                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                if (thenIncludes != null)
                {
                    foreach (var thenInclude in thenIncludes)
                    {
                        query = query.Include(thenInclude);
                    }
                }

                return await query.FirstOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                var query = dBContext.Set<T>().AsQueryable<T>();

                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                // search
                if (predicate != null)
                    query = query.Where(predicate);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Count(Expression<Func<T, bool>> predicate = null)
        {
            try
            {
                IQueryable<T> query = dBContext.Set<T>();

                // search
                if (predicate != null)
                    query = query.Where(predicate);

                return await query.CountAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
