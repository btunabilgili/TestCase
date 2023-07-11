using System.Linq.Expressions;
using TestCase.Application.Common;
using TestCase.Domain.Common;
using TestCase.Domain.Entities;

namespace TestCase.Application.Interfaces
{
    public interface IJobService
    {
        Task<Result<List<Job>>> GetJobsByCompanyId(Guid companyId);
        Task<Result<Job>> GetJobByIdAsync(Guid id);
        Task<Result<List<Job>>> GetJobsAsync(Expression<Func<Job, bool>> predicate);
        Task<Result<Job>> CreateJobAsync(Job job);
        Task<Result<bool>> UpdateJobAsync(Job job);
        Task<Result<bool>> DeleteJobAsync(Guid id);
        int CalculateJobQualityPoint(Job job);
    }
}
