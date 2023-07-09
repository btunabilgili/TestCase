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
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new JobConfiguration());
        }
    }
}
