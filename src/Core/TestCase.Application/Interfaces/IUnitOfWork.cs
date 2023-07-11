using TestCase.Domain.Common;

namespace TestCase.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyRepository CompanyRepository { get; }
        IJobRepository JobRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
