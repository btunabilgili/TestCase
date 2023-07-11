using Microsoft.EntityFrameworkCore;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;
using TestCase.Infrastructure.Contexts;

namespace TestCase.Infrastructure.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        private readonly TestCaseContext _context;
        private readonly DbSet<Company> _dbSet;
        public CompanyRepository(TestCaseContext dbContext) : base(dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = dbContext.Set<Company>();
        }

        public override async Task UpdateAsync(Company entity)
        {
            var dbEntity = await _dbSet.FindAsync(entity.Id);

            if (dbEntity is null)
                return;

            dbEntity.CompanyName = entity.CompanyName;
            dbEntity.Address = entity.Address;
            dbEntity.Phone = entity.Phone;
            dbEntity.Email = entity.Email;
        }
    }
}
