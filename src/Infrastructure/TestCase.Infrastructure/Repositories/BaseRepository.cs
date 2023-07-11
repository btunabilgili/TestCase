using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestCase.Application.Interfaces;
using TestCase.Domain.Common;
using TestCase.Domain.Exceptions;
using TestCase.Infrastructure.Contexts;

namespace TestCase.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet;
        private readonly TestCaseContext _context;
        public BaseRepository(TestCaseContext dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = dbContext.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            var oldEntity = await _dbSet.FindAsync(entity.Id);

            if (oldEntity == null)
                return;

            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity is not null)
                _dbSet.Remove(entity);
        }

        public virtual async Task<T> GetByIdAsync(Guid id, Expression<Func<T, object>>? includeExpression = null)
        {
            var query = _dbSet.AsQueryable();

            if (includeExpression is not null)
                query = query.Include(includeExpression);

            query = query.Where(x => x.Id == id);

            return await query.AsNoTracking().FirstOrDefaultAsync() ?? throw new EntityNotFoundException("Entity not found");
        }

        public virtual async Task<List<T>> GetListAsync(Expression<Func<T, object>>? includeExpression = null, Expression<Func<T, bool>>? predicate = null)
        {
            var query = _dbSet.AsQueryable();

            if (includeExpression is not null)
                query = query.Include(includeExpression);

            if (predicate is not null)
                query = query.Where(predicate);

            return await query.AsNoTracking().ToListAsync();
        }

        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? includeExpression = null)
        {
            var query = _dbSet.AsQueryable();

            if (includeExpression is not null)
                query = query.Include(includeExpression);

            if (predicate is not null)
                query = query.Where(predicate);

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
