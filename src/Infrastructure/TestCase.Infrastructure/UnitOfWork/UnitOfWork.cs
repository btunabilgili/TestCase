using TestCase.Application.Interfaces;
using TestCase.Domain.Common;
using TestCase.Infrastructure.Contexts;
using TestCase.Infrastructure.Repositories;

namespace TestCase.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestCaseContext _context;

        public UnitOfWork(TestCaseContext context)
        {
            _context = context;
            CompanyRepository = new CompanyRepository(context);
            JobRepository = new JobRepository(context);
        }

        public ICompanyRepository CompanyRepository
        {
            get;
            private set;
        }
        public IJobRepository JobRepository
        {
            get;
            private set;
        }

        //public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        //{
        //    var entityType = typeof(TEntity);

        //    if (!_repositories.ContainsKey(entityType))
        //    {
        //        object[] args = new object[]
        //        {
        //            _context
        //        };

        //        var repositoryType = typeof(BaseRepository<>).MakeGenericType(entityType);
        //        _repositories[entityType] = Activator.CreateInstance(repositoryType, args)!;
        //    }

        //    return (IBaseRepository<TEntity>)_repositories[entityType];
        //}

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
