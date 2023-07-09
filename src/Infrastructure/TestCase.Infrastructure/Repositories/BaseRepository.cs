using Microsoft.EntityFrameworkCore;
using TestCase.Application.Interfaces;
using TestCase.Domain.Common;
using TestCase.Domain.Exceptions;
using TestCase.Infrastructure.Contexts;

namespace TestCase.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        private readonly DbSet<T> _dbSet;

        public BaseRepository(TestCaseContext dbContext)
        {
            _dbSet = dbContext?.Set<T>() ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity is not null)
                _dbSet.Remove(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id) ?? throw new EntityNotFoundException("Entity not found");
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
