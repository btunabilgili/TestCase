using Microsoft.EntityFrameworkCore;
using TestCase.Domain.Entities;
using TestCase.Infrastructure.Contexts.Configurations;

namespace TestCase.Infrastructure.Contexts
{
    public class TestCaseContext : DbContext
    {
        public TestCaseContext(DbContextOptions<TestCaseContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Job> Jobs { get; set; }
    }
}
