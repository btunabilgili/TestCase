using System.Linq.Expressions;
using System.Net;
using System.Text.RegularExpressions;
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

        public int CalculateJobQualityPoint(Job job)
        {
            int qualityPoint = 0;

            if (job.WorkType is not null)
                qualityPoint += 1;

            if (job.SalaryInformation is not null)
                qualityPoint += 1;

            if (job.SideRights?.Any() == true)
                qualityPoint += 1;

            //Yasaklı kelimeleri veritabanı ya da başka bir sağlayıcıdan çekmek daha doğru olacaktır ancak şimdilik projenin hızlı ilerlemesi açısından kod içerisinde bırakıldı.
            string badWordsPattern = @"\b(mobing|ırkçılık)\b";

            Regex regex = new(badWordsPattern, RegexOptions.IgnoreCase);

            if (!regex.IsMatch(job.JobDescription))
                qualityPoint += 2;
            else
                job.JobDescription = regex.Replace(job.JobDescription, " ");

            return qualityPoint;
        }

        public async Task<Result<List<Job>>> GetJobsAsync(Expression<Func<Job, bool>> predicate)
        {
            try
            {
                return (await _uow.Repository.GetListAsync(predicate: predicate)).ToResult();
            }
            catch (Exception ex)
            {
                return Result<List<Job>>.Failure(ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
