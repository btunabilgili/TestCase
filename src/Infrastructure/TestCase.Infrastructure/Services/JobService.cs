using System.Net;
using TestCase.Application.Common;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;
using TestCase.Infrastructure.Contexts;

namespace TestCase.Infrastructure.Services
{
    public class JobService : IJobService
    {
        private readonly IUnitOfWork<TestCaseContext, Job> _uow;
        public JobService(IUnitOfWork<TestCaseContext, Job> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<Result<Job>> CreateJobAsync(Job job)
        {
            try
            {
                await _uow.Repository.AddAsync(job);
                await _uow.CommitAsync();

                return job.ToResult();
            }
            catch (Exception ex)
            {
                return Result<Job>.Failure(ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result<bool>> DeleteJobAsync(Guid id)
        {
            try
            {
                await _uow.Repository.DeleteAsync(id);
                return true.ToResult();
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result<List<Job>>> GetJobsByCompanyId(Guid companyId)
        {
            try
            {
                return (await _uow.Repository.GetListAsync(predicate: x => x.CompanyId == companyId)).ToResult();
            }
            catch (Exception ex)
            {
                return Result<List<Job>>.Failure(ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result<Job>> GetJobByIdAsync(Guid id)
        {
            try
            {
                return (await _uow.Repository.GetByIdAsync(id)).ToResult();
            }
            catch (Exception ex)
            {
                return Result<Job>.Failure(ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result<bool>> UpdateJobAsync(Job job)
        {
            try
            {
                _uow.Repository.Update(job);
                await _uow.CommitAsync();
                return true.ToResult();
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
