using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestProject.Domain.Repositories;

namespace TestProject.Database.Repositories
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, new()
    {
        private readonly DatabaseContext _dbContext;

        public RepositoryBase(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {
                throw new Exception($"{nameof(entity)} could not be saved");
            }
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception($"{nameof(DeleteAsync)} entity must not be null");
            }

            try
            {
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {
                throw new Exception($"{nameof(entity)} could not be deleted");
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Set<TEntity>().ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception($"{nameof(GetAllAsync)} couldn't retrieve entities");
            }
        }

        public async Task<TEntity> GetAsync(TKey key)
        {
            try
            {
                return await _dbContext.FindAsync<TEntity>(key);
            }
            catch (Exception)
            {
                throw new Exception($"{nameof(GetAsync)} couldn't retrieve entity");
            }
        }

        public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception($"{nameof(SearchAsync)} couldn't retrieve entity");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(UpdateAsync)} entity must not be null");
            }

            try
            {
                _dbContext.Update(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {
                throw new Exception($"{nameof(entity)} could not be updated");
            }
        }
    }
}
