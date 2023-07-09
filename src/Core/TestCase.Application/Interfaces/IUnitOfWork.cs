using Microsoft.EntityFrameworkCore;
using TestCase.Domain.Common;

namespace TestCase.Application.Interfaces
{
    public interface IUnitOfWork<TContext, T> where TContext : DbContext where T : BaseEntity
    {
        Task<bool> CommitAsync();
        IBaseRepository<T> Repository { get; }
    }
}
