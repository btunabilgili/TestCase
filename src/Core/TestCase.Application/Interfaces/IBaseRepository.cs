using System.Linq.Expressions;
using TestCase.Domain.Common;

namespace TestCase.Application.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        void Attach(T entity);
        Task<T> GetByIdAsync(Guid id, Expression<Func<T, object>>? includeExpression = null);
        Task<List<T>> GetListAsync(Expression<Func<T, object>>? includeExpression = null, Expression<Func<T, bool>>? predicate = null);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? includeExpression = null);
    }
}
