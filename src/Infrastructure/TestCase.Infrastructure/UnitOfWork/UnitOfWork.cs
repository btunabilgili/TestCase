using Microsoft.EntityFrameworkCore;
using TestCase.Application.Interfaces;
using TestCase.Domain.Common;

namespace TestCase.Infrastructure.UnitOfWork
{
    public class UnitOfWork<TContext, T> : IUnitOfWork<TContext, T> where TContext : DbContext where T : BaseEntity
    {
        public IBaseRepository<T> Repository { get; private set; }
        private readonly TContext _dbContext;

        public UnitOfWork(TContext dbContext, IBaseRepository<T> repository)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> CommitAsync()
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }
    }
}
