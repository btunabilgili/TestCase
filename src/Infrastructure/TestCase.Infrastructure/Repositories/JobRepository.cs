using Microsoft.EntityFrameworkCore;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;
using TestCase.Infrastructure.Contexts;

namespace TestCase.Infrastructure.Repositories
{
    public class JobRepository : BaseRepository<Job>, IJobRepository
    {
        private readonly TestCaseContext _context;
        private readonly DbSet<Job> _dbSet;
        public JobRepository(TestCaseContext dbContext) : base(dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = dbContext.Set<Job>();
        }

        public override async Task UpdateAsync(Job entity)
        {
            var dbEntity = await _dbSet.FindAsync(entity.Id);

            if (dbEntity is null)
                return;

            dbEntity.Position = entity.Position;
            dbEntity.JobDescription = entity.JobDescription;
            dbEntity.ListingDurationInDays = entity.ListingDurationInDays;
            dbEntity.SideRights = entity.SideRights;
            dbEntity.WorkType = entity.WorkType;
            dbEntity.SalaryInformation = entity.SalaryInformation;
        }
    }
}
