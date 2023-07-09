using TestCase.Application.Common;
using TestCase.Domain.Common;
using TestCase.Domain.Entities;

namespace TestCase.Application.Interfaces
{
    public interface IJobService
    {
        Task<Result<List<Job>>> GetJobsByCompanyId(Guid companyId);
        Task<Result<Job>> GetJobByIdAsync(Guid id);
        Task<Result<Job>> CreateJobAsync(Job job);
        Task<Result<bool>> UpdateJobAsync(Job job);
        Task<Result<bool>> DeleteJobAsync(Guid id);
    }
}
