using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestCase.Domain.Common;

namespace TestCase.Application.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetListAsync(Expression<Func<T, object>>? includeExpression = null, Expression<Func<T, bool>>? predicate = null);
        Task<List<T>> GetAllAsync(Expression<Func<T, object>>? includeExpression = null);
        Task<T> GetByIdAsync(Guid id, Expression<Func<T, object>>? includeExpression = null);
        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(Guid id);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    }
}
